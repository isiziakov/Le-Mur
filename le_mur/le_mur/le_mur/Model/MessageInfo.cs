using le_mur.Helpers;
using le_mur.NetworkCalling.MediaTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TL;
using Xamarin.Forms;

namespace le_mur.Model
{
    public class MessageInfo : INotifyPropertyChanged
    {
        int id;
        string text;
        List<ImageSource> images;
        List<MediaInfo> media;
        long groupId;
        int height;
        DateTime date;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public List<ImageSource> Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChanged("Images");
            }
        }

        public List<MediaInfo> Media
        {
            get { return media; }
            set
            {
                media = value;
                OnPropertyChanged("Media");
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    OnPropertyChanged("Height");
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public long GroupId
        {
            get { return groupId; }
            set
            {
                groupId = value;
                OnPropertyChanged("GroupId");
            }
        }

        public MessageInfo(int id, string text, long groupId, DateTime date)
        {
            Id = id;
            Text = text;
            GroupId = groupId;
            Images = new List<ImageSource>();
            Height = 0;
            Date = date;
        }

        public void AddImage(byte[] image)
        {
            Images.Add(ImageSource.FromStream(() => new MemoryStream(image)));
            if (ImageHelper.GetHeightOfImage(image) > Height)
                Height = ImageHelper.GetHeightOfImage(image);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
