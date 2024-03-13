using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace le_mur.View.Auth
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty = 
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), Color.Default);
        public static readonly BindableProperty BorderWidthProperty = 
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(CustomEntry), 1);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }
        public Entry NextView { get; set; }

        public Entry PreviousView { get; set; }
    }
}
