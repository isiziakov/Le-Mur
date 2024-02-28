using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace le_mur.TelegramApi
{
    public static class ApiOperations
    {
        static int apiId = 15526450;
        static string apiHash = "4370eb53775b7b474321d4691ca5dacf";
        static string phone = "+79632153559";
        static string currentHash = "";
        static TelegramClient client;
        static TeleSharp.TL.TLUser user;

        public static async Task MakeClient()
        {
            TelegramClient client = new TelegramClient(15526450, "4370eb53775b7b474321d4691ca5dacf");
            await client.ConnectAsync();
        }

        public static async Task<string> Auth()
        {
            if (!checkAuth())
            {
                return await client.SendCodeRequestAsync(phone);
            }
            else
            {
                return "";
            }
        }

        public static async Task<bool> MakeAuth(string hash, string code)
        {
            user = await client.MakeAuthAsync("+79632153559", hash, code);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<string[]> GetChannels()
        {
            var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
            return dialogs.Chats.Where(s => s.GetType().FullName.ToString() == "TeleSharp.TL.TLChannel")
                .Select(s => (s as TeleSharp.TL.TLChannel).Title).ToArray()
        }

        static bool checkAuth()
        {
            return client.IsUserAuthorized();
        }
    }
}
