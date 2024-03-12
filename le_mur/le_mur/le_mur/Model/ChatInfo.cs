using System;
using System.Collections.Generic;
using System.Text;
using TL;

namespace le_mur.Model
{
    public class ChatInfo
    {
        public InputPeer Id;
        public string Title;
        public byte[] Image;

        public ChatInfo(InputPeer id, string title, byte[] image)
        {
            Id = id;
            Title = title;
            Image = image;
        }
    }
}
