using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using le_mur.NetworkCalling;
using Xamarin.Forms;

namespace le_mur.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        public Command SendCodeCommand { get; }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        private AuthStatuses authStatus = AuthStatuses.NeedCode;
        private AuthStatuses AuthStatus
        {
            get { return authStatus; }
            set
            {
                if (authStatus == AuthStatuses.Ok)
                {
                    Visible = false;
                    //spisok
                }
                else
                {
                    Visible = true;
                }
            }
        }


        private ObservableCollection<string> chatsTitles;
        public ObservableCollection<string> ChatsTitles
        {
            get { return chatsTitles; }
            set
            {
                if (chatsTitles != value)
                {
                    chatsTitles = value;
                    OnPropertyChanged("ChatsTitles");
                }
            }
        }

        public AuthViewModel()
        {
            SendCodeCommand = new Command(onSendCodeCommand);
            ChatsTitles = new ObservableCollection<string>();

            Auth();
        }

        private async void Auth()
        {
            Visible = false;
            AuthStatus = await TelegramApi.CheckAuth();
        }

        private async void onSendCodeCommand()
        {
            await TelegramApi.Auth(Code);
            var chatsTitles = await TelegramApi.GetChatsTitles();
            foreach (var item in chatsTitles)
                ChatsTitles.Add(item);

        }
    }
}