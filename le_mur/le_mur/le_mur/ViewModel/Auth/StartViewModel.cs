using le_mur.Consts;
using le_mur.NetworkCalling;
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
            }
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            if (elapsedTime < TimeSpan.FromSeconds(2))
                await Task.Delay(TimeSpan.FromSeconds(2) - elapsedTime);

            switch (status)
            {
                case AuthStatus.Ok: /*открыть окно каналов*/ break;
                case AuthStatus.NeedAuth: /*открыть окно телефона*/ break;
                case AuthStatus.NeedCode: /*открыть окно кода*/ break;
            }
        }
    }
}
