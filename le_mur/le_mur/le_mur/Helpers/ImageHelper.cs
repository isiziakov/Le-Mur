using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Extended;

namespace le_mur.Helpers
{
    public class ImageHelper
    {
        public static ImageSource ConvertByteArrayToImageSource(byte[] bytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public static int GetHeightOfImage(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            using (var skStream = new SKManagedStream(stream))
            using (var bitmap = SKBitmap.Decode(skStream))
            {
                return (int)(bitmap.Height / 2.45);
            }
        }
    }
}
