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
using System.Collections.ObjectModel;

namespace le_mur.Model
{
    public class MessageInfo : INotifyPropertyChanged
    {
        int id;
        string text;
        List<Photo> imagesLinks;
        ObservableCollection<MediaInfo> media;
        long groupId;
        int height;
        DateTime date;
        string groupName;
        ImageSource groupImage;

        public InputPeer ChatId;

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

        public List<Photo> ImagesLinks
        {
            get { return imagesLinks; }
            set
            {
                imagesLinks = value;
                OnPropertyChanged("ImagesLinks");
            }
        }

        public ObservableCollection<MediaInfo> Media
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
        public string GroupName
        {
            get { return groupName; }
            set
            {
                groupName = value;
                OnPropertyChanged("GroupName");
            }
        }

        public ImageSource GroupImage
        {
            get { return groupImage; }
            set
            {
                groupImage = value;
                OnPropertyChanged("GroupImage");
            }
        }

        public MessageInfo(int id, string text, long groupId, DateTime date, InputPeer chatId)
        {
            Id = id;
            Text = text;
            GroupId = groupId;
            ImagesLinks = new List<Photo>();
            Media = new ObservableCollection<MediaInfo>();
            Height = 0;
            Date = date;
            ChatId = chatId;
        }

        public MessageInfo(int id, string text, long groupId, DateTime date, InputPeer chatId, string groupName, ImageSource groupImage) : this(id, text, groupId, date, chatId)
        {
            GroupImage = groupImage;
            GroupName = groupName;
        }

        public void AddImage(byte[] image)
        {
            Media.Add(new ImageInfo(ImageSource.FromStream(() => new MemoryStream(image))));
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
