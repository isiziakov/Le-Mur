using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace le_mur.Models
{
    public class Post : INotifyPropertyChanged
    {
        int id;
        string image;
        string chanelImage;
        string chanelName;
        string chanelTheme;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public string ChanelImage
        {
            get { return chanelImage; }
            set
            {
                chanelImage = value;
                OnPropertyChanged("ChanelImage");
            }
        }

        public string ChanelName
        {
            get { return chanelName; }
            set
            {
                chanelName = value;
                OnPropertyChanged("ChanelName");
            }
        }

        public string ChanelTheme
        {
            get { return chanelTheme; }
            set
            {
                chanelTheme = value;
                OnPropertyChanged("ChanelTheme");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
