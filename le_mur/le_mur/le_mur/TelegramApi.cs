using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace le_mur
{
    public static class TelegramApi
    {
        private static WTelegram.Client client;
        private static int apiId = 15526450;
        private static string apiHash = "4370eb53775b7b474321d4691ca5dacf";
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("files", "session.dat");
        private static string phone = "+79106983594";
        static TelegramApi()
        {
            client = new WTelegram.Client(apiId, apiHash, path);
        }

        static async public Task<AuthStatuses> CheckAuth()
        {
            return await doLogin(phone);
        }

        static async public Task<AuthStatuses> Auth(string code)
        {
            return await doLogin(code);
        }

        static async Task<AuthStatuses> doLogin(string loginInfo)
        {
            switch (await client.Login(loginInfo))
            {
                case "verification_code": return AuthStatuses.NeedCode;
                case null: return AuthStatuses.Ok;
                default: return AuthStatuses.NeedAuth;
            }
        }

        static async public Task<List<string>> GetChatsTitles()
        {
            var res = await client.Messages_GetAllChats();
            return res.chats.Select(i => i.Value.Title).ToList();
        }
    }

    public enum AuthStatuses
    {
        Ok,
        NeedAuth,
        NeedCode
    }
}