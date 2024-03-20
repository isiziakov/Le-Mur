using System;
using System.Collections.Generic;
using System.Text;

namespace le_mur.Model
{
    public class CustomWallInfo
    {
        public List<Tuple<ChatInfo, int>> ChatInfos { get; set; } = new List<Tuple<ChatInfo, int>>();
        public List<MessageInfo> Messages { get; set; } = new List<MessageInfo>();

        public CustomWallInfo(List<ChatInfo> chats)
        {
            foreach (var chat in chats)
            {
                ChatInfos.Add(new Tuple<ChatInfo, int>(chat, 0));
            }
        }

        public CustomWallInfo(List<ChatInfo> chats, List<int> offsets)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                ChatInfos.Add(new Tuple<ChatInfo, int>(chats[i], offsets[i]));
            }
        }
    }
}
