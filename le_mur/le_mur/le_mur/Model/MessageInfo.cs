using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace le_mur.Model
{
    public class MessageInfo
    {
        public int Id;
        public string Text;
        public List<ImageSource> Images = new List<ImageSource>();
        public long GroupId;

        public MessageInfo(int id, string text, long groupId)
        {
            Id = id;
            Text = text;
            GroupId = groupId;
        }

        public void AddImage(byte[] image)
        {
            Images.Add(ImageSource.FromStream(() => new MemoryStream(image)));
        }
    }
}
