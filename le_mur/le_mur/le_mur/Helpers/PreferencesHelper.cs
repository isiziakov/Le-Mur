using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace le_mur.Helpers
{
    static public class PreferencesHelper
    {
        static public string GetPhoneNumber()
        {
            return Preferences.Get("phone", "");
        }

        static public void SetPhoneNumber(string phone)
        {
            Preferences.Set("phone", phone);
        }
    }
}
