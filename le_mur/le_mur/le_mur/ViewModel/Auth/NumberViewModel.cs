using System.Collections.ObjectModel;
using le_mur.NetworkCalling;
using le_mur.Consts;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using le_mur.Helpers;

namespace le_mur.ViewModel.Auth
{
    public class NumberViewModel : BaseViewModel
    {
        public Command SendNumberCommand { get; }

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
                string formattedPhoneNumber = "";
                for (int i = 0; i < phoneNumber.Length && i <= 10; i++)
                {
                    if (i == 3 || i == 6 || i == 8)
                        formattedPhoneNumber += "-";
                    formattedPhoneNumber += phoneNumber[i];
                }
                if (number != formattedPhoneNumber)
                {
                    number = formattedPhoneNumber;
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
                    case AuthStatus.Ok: /*открыть окно каналов*/ break;
                    case AuthStatus.NeedAuth: /*открыть окно телефона*/ break;
                    case AuthStatus.NeedCode: /*открыть окно кода*/ break;
                }
            }
        }
    }
}