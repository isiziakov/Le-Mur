using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using le_mur.Consts;
using le_mur.Helpers;
using le_mur.Model;
using TL;

namespace le_mur.NetworkCalling
{
    public static class TelegramApi
    {
        private static WTelegram.Client client;
        private static int apiId = GlobalConsts.apiId;
        private static string apiHash = GlobalConsts.apiHash;
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("files", "session.dat");
        static TelegramApi()
        {
            client = new WTelegram.Client(apiId, apiHash, path);
        }

        static async public Task<AuthStatus> CheckAuth()
        {
            PreferencesHelper.SetPhoneNumber(LocalConsts.phone);
            return await doLogin(PreferencesHelper.GetPhoneNumber());
        }

        static async public Task<AuthStatus> CheckAuth(string number)
        {
            return await doLogin(number);
        }

        static async public Task<AuthStatus> Auth(string code)
        {
            return await doLogin(code);
        }

        static async Task<AuthStatus> doLogin(string loginInfo)
        {
            switch (await client.Login(loginInfo))
            {
                case "verification_code": return AuthStatus.NeedCode;
                case null: return AuthStatus.Ok;
                default: return AuthStatus.NeedAuth;
            }
        }

        static async public Task<List<ChatInfo>> GetChatsInfo()
        {
            var chats = new List<ChatInfo>();
            var chatsPhoto = new List<IPeerInfo>();
            var res = await client.Messages_GetAllChats();
            foreach (var chat in res.chats)
            {
                chats.Add(new ChatInfo(chat.Value, chat.Value.Title));
                chatsPhoto.Add(chat.Value);
            }
            var images = await GetChatPhotos(chatsPhoto);
            for (int i = 0; i < images.Count(); i++)
            {
                chats[i].SetImage(images[i].Result);
            }
            return chats;
        }

        static async public Task<List<Task<byte[]>>> GetChatPhotos(List<IPeerInfo> peer)
        {
            var tasks = new List<Task<byte[]>>();
            foreach (var peerInfo in peer)
            {
                tasks.Add(GetChatPhoto(peerInfo));
            }
            foreach (var task in tasks)
            {
                await task;
            }
            return tasks;
        }

        static async public Task<byte[]> GetChatPhoto(IPeerInfo peer)
        {
            MemoryStream ms = new MemoryStream();
            await client.DownloadProfilePhotoAsync(peer, ms);
            return ms.ToArray();
        }

        static async public Task<List<MessageInfo>> GetMessages(InputPeer peer, int offsetId = 0)
        {
            var images = new List<Tuple<int, Photo>>();
            var res = new List<MessageInfo>();
            var msgs = await getMessages(peer, offsetId);
            foreach (var msg in msgs.Messages)
            {
                if (msg is Message)
                {
                    var message = (msg as Message);
                    if (message.grouped_id != 0 && res.Count > 0 && res.Last().GroupId == message.grouped_id)
                    {
                        if (message.message != "")
                        {
                            res.Last().Text = message.message;
                        }
                    }
                    else
                    {
                        res.Add(new MessageInfo(message.ID, message.message, message.grouped_id, message.date));
                    }
                    
                    if (message.media is MessageMediaPhoto)
                    {
                        var media = message.media as MessageMediaPhoto;
                        if (media.photo is Photo)
                        {
                            images.Add(new Tuple<int, Photo>(res.Count - 1, media.photo as Photo));
                        }
                    }
                }
            }
            if (res.Count > 0 && res.Last().GroupId != 0)
            {
                msgs = await getMessages(peer, res.Last().Id);
                foreach (var msg in msgs.Messages)
                {
                    if (msg is Message)
                    {
                        var message = (msg as Message);
                        if (message.grouped_id != res.Last().GroupId)
                        {
                            break;
                        }
                        else
                        {
                            if (message.message != "")
                            {
                                res.Last().Text = message.message;
                            }
                            if (message.media is MessageMediaPhoto)
                            {
                                var media = message.media as MessageMediaPhoto;
                                if (media.photo is Photo)
                                {
                                    images.Add(new Tuple<int, Photo>(res.Count - 1, media.photo as Photo));
                                }
                            }
                        }
                    }
                }
            }
            var imagesSource = await GetImages(images);
            foreach (var image in imagesSource)
            {
                res[image.Item1].AddImage(image.Item2.Result);
            }
            return res;
        }

        static async public Task<List<Tuple<int, Task<byte[]>>>> GetImages(List<Tuple<int, Photo>> photos)
        {
            var tasks = new List<Tuple<int, Task<byte[]>>>();
            foreach (var photo in photos)
            {
                tasks.Add(new Tuple<int, Task<byte[]>>(photo.Item1, GetImage(photo.Item2)));
            }
            foreach (var task in tasks)
            {
                await task.Item2;
            }
            return tasks;
        }

        static async public Task<byte[]> GetImage(Photo photo)
        {
            MemoryStream ms = new MemoryStream();
            await client.DownloadFileAsync(photo, ms);
            return ms.ToArray();
        }

        static async public Task<Messages_MessagesBase> getMessages(InputPeer peer, int offsetId)
        {
            return await client.Messages_GetHistory(peer, offsetId);
        }
    }
}