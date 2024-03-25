using le_mur.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TL;
using Xamarin.Forms;

namespace le_mur.NetworkCalling.MediaTypes
{
    public class ImageInfo : MediaInfo
    {
        protected ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public ImageInfo(ImageSource imageSource)
        {
            ImageSource = imageSource;
        }
    }
}
