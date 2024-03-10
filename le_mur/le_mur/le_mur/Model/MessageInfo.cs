using System;
using System.Collections.Generic;
using System.Text;

namespace le_mur.Model
{
    public class MessageInfo
    {
        public int Id;
        public string Text;
        public List<byte[]> images = new List<byte[]>();
        public long GroupId;

        public MessageInfo(int id, string text, long groupId)
        {
            Id = id;
            Text = text;
            GroupId = groupId;
        }
    }
}
