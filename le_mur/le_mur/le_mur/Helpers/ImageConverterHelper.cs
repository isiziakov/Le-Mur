using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace le_mur.Helpers
{
    public class ImageConverterHelper
    {
        public static ImageSource ConvertByteArrayToImageSource(byte[] bytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }
    }
}
