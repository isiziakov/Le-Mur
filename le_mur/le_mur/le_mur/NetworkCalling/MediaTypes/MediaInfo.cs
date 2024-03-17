using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using TL;

namespace le_mur.NetworkCalling.MediaTypes
{
    public class MediaInfo : INotifyPropertyChanged
    {
        protected string filename;
        protected Stream data;
        protected Document document;

        public MediaInfo(string filename, Document document)
        {
            this.filename = filename;
            this.document = document;
        }
        public string Filename
        {
            get { return filename; }
            set
            {
                filename = value;
                OnPropertyChanged("Id");
            }
        }

        public Stream Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged("Id");
            }
        }

        public Document Document
        {
            get { return document; }
            set
            {
                document = value;
                OnPropertyChanged("Title");
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
