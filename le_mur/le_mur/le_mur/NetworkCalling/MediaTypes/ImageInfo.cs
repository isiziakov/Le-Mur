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
        public ImageSource ImageSource;

        public ImageInfo(ImageSource imageSource)
        {
            ImageSource = imageSource;
        }
    }
}
