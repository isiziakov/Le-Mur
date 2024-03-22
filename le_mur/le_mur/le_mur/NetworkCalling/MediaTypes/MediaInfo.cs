using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TL;

namespace le_mur.NetworkCalling.MediaTypes
{
    public class MediaInfo : INotifyPropertyChanged
    {
        protected string filename;
        protected byte[] data;
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

        public async Task<byte[]> GetFile()
        {
            if (data != null)
                return data;

            if (File.Exists(TelegramApi.filesPath + "/" + filename))
            {
                data = File.ReadAllBytes(TelegramApi.filesPath + "/" + filename);
                return data;
            }

            data = await TelegramApi.GetDocument(document);
            File.WriteAllBytes(TelegramApi.filesPath + "/" + filename, data);
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
