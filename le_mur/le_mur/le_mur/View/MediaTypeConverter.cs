using le_mur.NetworkCalling.MediaTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace le_mur.View
{
    public class MediaTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is VideoInfo)
                return "Video";
            else if (value is ImageInfo)
                return "Image";

            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
