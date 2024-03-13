using System.Collections.ObjectModel;
using le_mur.NetworkCalling;
using le_mur.Consts;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using le_mur.Helpers;
using le_mur.View.Auth;

namespace le_mur.ViewModel.Auth
{
    public class NumberViewModel : BaseViewModel
    {
        public Command SendNumberCommand { get; }
        public INavigation Navigation { get; set; }

        public NumberViewModel()
        {
            SendNumberCommand = new Command(onSendNumberCommand);
        }

        private string number;
        public string Number
        {
            get { return number; }
            set
            {
                string phoneNumber = Regex.Replace(value, "[^0-9]", "");
                string pattern = "", replacement = "";
                switch (phoneNumber.Length)
                {
                    case 0: case 1: case 2: case 3:
                        pattern = @"(\d{1,3})?";
                        replacement = @"$1";
                        break;
                    case 4: case 5: case 6:
                        pattern = @"(\d{3})(\d{1,3})";
                        replacement = @"$1-$2";
                        break;
                    case 7: case 8:
                        pattern = @"(\d{3})(\d{3})(\d{1,2})";
                        replacement = @"$1-$2-$3";
                        break;
                    case 9: case 10:
                        pattern = @"(\d{3})(\d{3})(\d{2})(\d{1,2})";
                        replacement = @"$1-$2-$3-$4";
                        break;
                    default:
                        pattern = @"(\d{3})(\d{3})(\d{2})(\d{2})(\d{1,}?)";
                        replacement = @"$1-$2-$3-$4";
                        break;
                }
                Regex regex = new Regex(pattern);
                string result = regex.Replace(phoneNumber, replacement);
                if (number != result)
                {
                    number = result;
                    OnPropertyChanged("Number");
                }
            }
        }

        private async void onSendNumberCommand()
        {
            string phoneNumber = Regex.Replace(Number, "[^0-9]", "");
            if (phoneNumber.Length == 10)
            {
                phoneNumber = $"+7{phoneNumber}";
                AuthStatus status = await TelegramApi.CheckAuth(phoneNumber);
                switch (status)
                {
                    case AuthStatus.Ok: /*открыть каналы*/ break;
                    case AuthStatus.NeedAuth: await Navigation.PushAsync(new NumberPage()); break;
                    case AuthStatus.NeedCode: await Navigation.PushAsync(new CodePage(phoneNumber)); break;
                }
            }
        }
    }
}