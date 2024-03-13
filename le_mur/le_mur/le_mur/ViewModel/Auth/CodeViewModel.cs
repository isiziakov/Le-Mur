using le_mur.Consts;
using le_mur.Helpers;
using le_mur.NetworkCalling;
using le_mur.View;
using le_mur.View.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace le_mur.ViewModel.Auth
{
    public class CodeViewModel : BaseViewModel 
    {
        public INavigation Navigation { get; set; }
        public Command SendCodeCommand { get; }
        private string _number;
        public CodeViewModel(string number)
        {
            SendCodeCommand = new Command(onSendCodeCommand);
            _number = number;
        }

        private string code_0, code_1, code_2, code_3, code_4;
        public string Code0
        {
            get { return code_0; }
            set
            {
                if (value != code_0)
                {
                    code_0 = value;
                    OnPropertyChanged("Code0");
                }
            }
        }
        public string Code1
        {
            get { return code_1; }
            set
            {
                if (value != code_1)
                {
                    code_1 = value;
                    OnPropertyChanged("Code1");
                }
            }
        }
        public string Code2
        {
            get { return code_2; }
            set
            {
                if (value != code_2)
                {
                    code_2 = value;
                    OnPropertyChanged("Code2");
                }
            }
        }
        public string Code3
        {
            get { return code_3; }
            set
            {
                if (value != code_3)
                {
                    code_3 = value;
                    OnPropertyChanged("Code3");
                }
            }
        }
        public string Code4
        {
            get { return code_4; }
            set
            {
                if (value != code_4)
                {
                    code_4 = value;
                    OnPropertyChanged("Code4");
                }
            }
        }

        private async void onSendCodeCommand()
        {
            string code = $"{Code0}{Code1}{Code2}{Code3}{Code4}";
            AuthStatus status = await TelegramApi.Auth(code);
            switch (status)
            {
                case AuthStatus.Ok: PreferencesHelper.SetPhoneNumber(_number); await Navigation.PushAsync(new ChanelsPage()); break;
                case AuthStatus.NeedAuth: await Navigation.PushAsync(new NumberPage()); break;
                case AuthStatus.NeedCode: await Navigation.PushAsync(new CodePage(_number)); break;
            }
        }
    }
}
