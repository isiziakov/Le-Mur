using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using le_mur.Consts;
using le_mur.Helpers;
using le_mur.Model;
using le_mur.NetworkCalling.MediaTypes;
using TL;

namespace le_mur.NetworkCalling
{
    public static class TelegramApi
    {
        private static WTelegram.Client client;
        private static int apiId = GlobalConsts.apiId;
        private static string apiHash = GlobalConsts.apiHash;
        public static string filesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("files", "session.dat");
        static TelegramApi()
        {
            client = new WTelegram.Client(apiId, apiHash, path);
        }

        #region Authorization
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
        #endregion

        #region GetChatsList
        static async public Task<List<ChatInfo>> GetChatsInfo()
        {
            var chats = new List<ChatInfo>();
            var chatsPhoto = new List<IPeerInfo>();
            var res = await client.Messages_GetAllChats();
            foreach (var chat in res.chats.Where(i => i.Value.IsChannel && i.Value.IsActive 
            && ((i.Value as Channel).username != null || (i.Value as Channel).usernames != null)
            && i.Value.Title != "Unsupported Chat").ToList())
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
        #endregion

        #region GetChatPhotos
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
        #endregion

        #region GetMessagesForChat
        static async public Task<List<MessageInfo>> GetMessages(InputPeer peer, int offsetId = 0, bool loadImages = true)
        {
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
                        res.Add(new MessageInfo(message.ID, message.message, message.grouped_id, message.date, peer));
                    }
                    
                    if (message.media is MessageMediaPhoto)
                    {
                        var media = message.media as MessageMediaPhoto;
                        if (media.photo is Photo)
                        {
                            res.Last().ImagesLinks.Add(media.photo as Photo);
                        }
                    }
                    if (message.media is MessageMediaDocument)
                    {
                        var media = message.media as MessageMediaDocument;
                        if (media.document is Document)
                        {
                            var doc = media.document as Document;
                            if (doc.mime_type.IndexOf("video") > -1)
                            {
                                var fileName = doc.Filename;
                                if (fileName == null)
                                {
                                    fileName = doc.ID.ToString() + doc.mime_type.Replace("video/", ".");
                                }
                                res.Last().Media.Add(new VideoInfo(fileName, doc));
                            }
                            else
                            {
                                res.Last().Media.Add(new MediaInfo(doc.Filename, doc));
                            }
                        }
                    }
                    res.Last().Id = message.id;
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
                                    res.Last().ImagesLinks.Add(media.photo as Photo);
                                }
                            }
                        }
                        res.Last().Id = message.id;
                    }
                }
            }

            if (loadImages)
                await GetImages(res);
            
            return res;
        }

        static async public Task<Messages_MessagesBase> getMessages(InputPeer peer, int offsetId)
        {
            return await client.Messages_GetHistory(peer, offsetId);
        }
        #endregion

        #region GetCustomWall
        static public async Task<CustomWallInfo> GetCustomWall(CustomWallInfo wallInfo)
        {
            wallInfo.Messages.Clear();
            var bufferMessages = new List<MessageInfo>();
            var messagesTasks = new List<Task<List<MessageInfo>>>();
            foreach (var chat in wallInfo.ChatInfos)
            {
                messagesTasks.Add(GetMessages(chat.Item1.Id, chat.Item2, false));
                bufferMessages.AddRange(await GetMessages(chat.Item1.Id, chat.Item2, false));
            }
            for (var i = 0; i < messagesTasks.Count; i++)
            {
                await messagesTasks[i];
                bufferMessages.AddRange(messagesTasks[i].Result);
            }
            wallInfo.Messages.AddRange(bufferMessages.OrderByDescending(i => i.Date).ToList().GetRange(0, 100));
            await GetImages(wallInfo.Messages);
            for (int i = 0; i < wallInfo.ChatInfos.Count; i++)
            {
                var offset = 0;
                var msgs = wallInfo.Messages.Where(m => m.ChatId == wallInfo.ChatInfos[i].Item1.Id).ToList();
                if (msgs.Count > 0)
                {   
                    offset = msgs.OrderBy(m => m.Id).Last().Id;
                }
                wallInfo.ChatInfos[i] = new Tuple<ChatInfo, int>(wallInfo.ChatInfos[i].Item1, offset);
            }
            return wallInfo;
        }
        #endregion

        #region GetImagesForMessagesList
        static async public Task GetImages(List<MessageInfo> messages)
        {
            var tasks = new List<Tuple<int, Task<byte[]>>>();
            for (int i = 0; i < messages.Count; i++)
            {
                foreach (var image in messages[i].ImagesLinks)
                {
                    tasks.Add(new Tuple<int, Task<byte[]>>(i, GetImage(image)));
                }
            }
            foreach (var task in tasks)
            {
                await task.Item2;
            }
            foreach (var task in tasks)
            {
                messages[task.Item1].AddImage(task.Item2.Result);
            }
            return;
        }

        static async public Task<byte[]> GetImage(Photo photo)
        {
            MemoryStream ms = new MemoryStream();
            await client.DownloadFileAsync(photo, ms);
            return ms.ToArray();
        }
        #endregion

        #region GetDocument
        static async public Task<byte[]> GetDocument(Document document)
        {
            MemoryStream ms = new MemoryStream();
            await client.DownloadFileAsync(document, ms);
            return ms.ToArray();
        }
        #endregion
    }
}