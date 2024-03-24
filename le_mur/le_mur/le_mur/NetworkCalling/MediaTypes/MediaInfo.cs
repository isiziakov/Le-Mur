using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TL;
using Xamarin.CommunityToolkit.Core;

namespace le_mur.NetworkCalling.MediaTypes
{
    public class MediaInfo : INotifyPropertyChanged
    {
        protected string filename;
        protected byte[] data;
        protected Document document;
        protected MediaSource source;
        protected bool isLoad;

        public MediaInfo(string filename, Document document)
        {
            Filename = filename;
            Document = document;
        }

        public string Filename
        {
            get { return filename; }
            set
            {
                filename = value;
                OnPropertyChanged("Filename");
            }
        }

        public byte[] Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged("Data");
            }
        }

        public Document Document
        {
            get { return document; }
            set
            {
                document = value;
                OnPropertyChanged("Document");
            }
        }

        public MediaSource Source
        {
            get { return source; }
            set
            {
                if (source != value)
                {
                    source = value;
                    OnPropertyChanged("Source");
                }
            }
        }

        public bool IsLoad
        {
            get { return isLoad; }
            set
            {
                isLoad = value;
                OnPropertyChanged("IsLoad");
            }
        }

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public async Task<byte[]> GetFile()
        {
            //if (data != null)
            //    return data;

            if (File.Exists(TelegramApi.filesPath + "/" + filename))
            {
                data = File.ReadAllBytes(TelegramApi.filesPath + "/" + filename);
                Source = new FileMediaSource
                {
                    File = TelegramApi.filesPath + "/" + filename
                };
                //this.source = TelegramApi.filesPath + "/" + filename;
                isLoad = true;
                return data;
            }

            data = await TelegramApi.GetDocument(document);
            File.WriteAllBytes(TelegramApi.filesPath + "/" + filename, data);
            Source = new FileMediaSource
            {
                File = TelegramApi.filesPath + "/" + filename
            };
            //this.source = TelegramApi.filesPath + "/" + filename;
            isLoad = true;
            return data;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
