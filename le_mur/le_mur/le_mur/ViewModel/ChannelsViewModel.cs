using le_mur.Model;
using le_mur.NetworkCalling;
using le_mur.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TL;
using Xamarin.Forms;

namespace le_mur.ViewModel
{
    public class ChannelsViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public Command ToolsCommand { get; }
        public Command TapCommand { get; }
        public Command FoldersCommand { get; }
        public Command ChatCommand { get; }

        private ObservableCollection<ChatInfo> channels;
        public ObservableCollection<ChatInfo> Channels
        {
            get { return channels; }
            set
            {
                if (channels != value)
                {
                    channels = value;
                    OnPropertyChanged("Channels");
                }
            }
        }

        private ObservableCollection<ChatInfo> allChannels;
        public ObservableCollection<ChatInfo> AllChannels
        {
            get { return allChannels; }
            set
            {
                if (allChannels != value)
                {
                    allChannels = value;
                    OnPropertyChanged("AllChannels");
                }
            }
        }

        private string searchRequest;
        public string SearchRequest
        {
            get { return searchRequest; }
            set
            {
                if (searchRequest != value)
                {
                    searchRequest = value;
                    Search();
                    OnPropertyChanged("SearchRequest");
                }
            }
        }

        #region даунлоад меню
        private bool isOpen = false;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen != value)
                {
                    isOpen = value;
                    OnPropertyChanged("IsOpen");
                }
            }
        }

        private string selected = "All Groups";
        public string Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        private string second = "Open";
        public string Second
        {
            get { return second; }
            set
            {
                if (second != value)
                {
                    second = value;
                    OnPropertyChanged("Second");
                }
            }
        }

        private string third = "Hidden";
        public string Third
        {
            get { return third; }
            set
            {
                if (third != value)
                {
                    third = value;
                    OnPropertyChanged("Third");
                }
            }
        }

        #endregion
        public ChannelsViewModel() 
        {
            ToolsCommand = new Command(OnToolsCommand);
            TapCommand = new Command(OnTapCommand);
            FoldersCommand = new Command(OnFoldersCommand);
            ChatCommand = new Command(OnChatCommand);

            AllChannels = new ObservableCollection<ChatInfo>();
            GetAllChanels();
        }

        private async void GetAllChanels()
        {
            var chatsTitles = await TelegramApi.GetChatsInfo();
            foreach (var item in chatsTitles)
                AllChannels.Add(item);
            Channels = AllChannels;
        }

        private void Search()
        {
            var list = AllChannels.Where(c => c.Title.ToLower().Contains(SearchRequest.ToLower()) == true).ToList();
            ObservableCollection<ChatInfo> collection = new ObservableCollection<ChatInfo>();
            foreach (var item in list)
                collection.Add(item);
            Channels = collection;
        }

        private void OnToolsCommand()
        {

        }

        private void OnFoldersCommand()
        {

        }

        private void OnTapCommand(object parameter)
        {
            int par = Convert.ToInt32(parameter);
            if (IsOpen)
            {
                var tmp = Selected;
                if (par == 2)
                {
                    Selected = Second;
                    Second = tmp;
                }
                if (par == 3)
                {
                    Selected = Third;
                    Third = tmp;
                }
            }
            IsOpen = !IsOpen;
        }

        private async void OnChatCommand(object parameter)
        {
            await Navigation.PushAsync(new ChannelPage((ChatInfo)parameter));
        }
    }
}
