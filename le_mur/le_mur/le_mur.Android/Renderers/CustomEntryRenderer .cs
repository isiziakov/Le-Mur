using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using le_mur.Droid.Renderers;
using le_mur.View.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace le_mur.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement is CustomEntry customEntry)
            {
                Control.Background = new Android.Graphics.Drawables.GradientDrawable();

                var gd = new Android.Graphics.Drawables.GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                gd.SetStroke(customEntry.BorderWidth, customEntry.BorderColor.ToAndroid());

                Control.SetBackground(gd);
            }
        }
    }
}