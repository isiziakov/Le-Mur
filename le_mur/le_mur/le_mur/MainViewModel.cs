using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace le_mur
{
    public class MainViewModel : BaseViewModel
    {
        public Command DownloadCommand { get; }

        private WTelegram.Client client = null;
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

        private ObservableCollection<string> groups;
        public ObservableCollection<string> Groups
        {
            get { return groups; }
            set
            {
                if (groups != value)
                {
                    groups = value;
                    OnPropertyChanged("Groups");
                }
            }
        }

        public MainViewModel()
        {
            DownloadCommand = new Command(onDownloadCommand);

            Groups = new ObservableCollection<string>();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("files", "session.dat");
            client = new WTelegram.Client(15526450, "4370eb53775b7b474321d4691ca5dacf", path);
            ChangeAuth();
        }

        public async Task ChangeAuth()
        {
            await DoLogin("+79632153559");
        }

        private async void onDownloadCommand()
        {
            await DoLogin(code);
            /*await TelegramApi.ApiOperations.MakeClient();
            var res = await TelegramApi.ApiOperations.Auth();
            if (res == "")
            {
                foreach (string d in await ApiOperations.GetChannels())
                    Groups.Add(d);
            }
            else
            {
                if (await ApiOperations.MakeAuth(res, Code))
                {
                    foreach (string d in await ApiOperations.GetChannels())
                        Groups.Add(d);
                }
            }*/
            var data = await client.Messages_GetAllChats();
            var a = 1;
        }
    }
}
