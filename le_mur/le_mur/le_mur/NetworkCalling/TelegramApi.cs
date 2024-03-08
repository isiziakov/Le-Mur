﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using le_mur.Consts;

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

        static async public Task<List<string>> GetChatsTitles()
        {
            var res = await client.Messages_GetAllChats();
            return res.chats.Select(i => i.Value.Title).ToList();
        }
    }
}