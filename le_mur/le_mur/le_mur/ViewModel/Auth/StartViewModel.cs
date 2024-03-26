using le_mur.Consts;
using le_mur.Helpers;
using le_mur.NetworkCalling;
using le_mur.View;
using le_mur.View.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace le_mur.ViewModel.Auth
{
    public class StartViewModel : BaseViewModel 
    {
        public INavigation Navigation { get; set; }

        public StartViewModel()
        {
            Auth();
        }

        private async void Auth()
        {
            AuthStatus status;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                status = await TelegramApi.CheckAuth();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                status = AuthStatus.NeedAuth;
                PreferencesHelper.SetPhoneNumber("");
            }
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            if (elapsedTime < TimeSpan.FromSeconds(3))
                await Task.Delay(TimeSpan.FromSeconds(3) - elapsedTime);
            switch (status)
            {
                case AuthStatus.Ok: await Navigation.PushAsync(new FeedPage()); break;
                case AuthStatus.NeedAuth: await Navigation.PushAsync(new NumberPage()); break;
                case AuthStatus.NeedCode: await Navigation.PushAsync(new CodePage(PreferencesHelper.GetPhoneNumber())); break;
            }
        }
    }
}
