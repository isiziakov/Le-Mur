using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using le_mur.Consts;
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
        private static string phone = LocalConsts.phone;
        static TelegramApi()
        {
            client = new WTelegram.Client(apiId, apiHash, path);
        }

        static async public Task<AuthStatus> CheckAuth()
        {
            return await doLogin(phone);
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

        static async public Task<List<string>> GetChatsTitles() // Depricated
        {
            var res = await client.Messages_GetAllChats();
            return res.chats.Select(i => i.Value.Title).ToList();
        }

        static async public Task<List<ChatInfo>> GetChatsInfo()
        {
            var chats = new List<ChatInfo>();
            var res = await client.Messages_GetAllChats();
            foreach (var chat in res.chats)
            {
                chats.Add(new ChatInfo(chat.Value, chat.Value.Title, await GetChatPhoto(chat.Value)));
            }
            return chats;
        }

        static async public Task<byte[]> GetChatPhoto(IPeerInfo peer)
        {
            MemoryStream ms = new MemoryStream();
            await client.DownloadProfilePhotoAsync(peer, ms);
            return ms.ToArray();
        }

        static async public Task<List<MessageInfo>> GetMessages(InputPeer peer, int offsetId = 0)
        {
            var res = new List<MessageInfo>();
            var msgs = await client.Messages_GetHistory(peer, offsetId);
            foreach (var msg in msgs.Messages)
            {
                if (msg is Message)
                {
                    var message = (msg as Message);
                    res.Add(new MessageInfo(message.ID, message.message));
                }
            }
            return res;
        }
    }
}