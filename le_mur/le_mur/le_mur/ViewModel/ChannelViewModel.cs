using System.Collections.ObjectModel;
using Xamarin.Forms;
using le_mur.Model;
using TL;
using le_mur.NetworkCalling;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Core;

namespace le_mur.ViewModel
{
    public class ChannelViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public Command FavouritesCommand { get; }
        public Command LoadVideoCommand { get; }

        private ChatInfo selectedChat;
        public ChatInfo SelectedChat
        {
            get { return selectedChat; }
            set
            {
                if (selectedChat != value)
                {
                    selectedChat = value;
                    OnPropertyChanged("SelectedChat");
                }
            }
        }

        private ObservableCollection<MessageInfo> messages;
        public ObservableCollection<MessageInfo> Messages
        {
            get { return messages; }
            set
            {
                if (messages != value)
                {
                    messages = value;
                    OnPropertyChanged("Messages");
                }
            }
        }

        public ChannelViewModel(ChatInfo chat)
        {
            SelectedChat = chat;
            Messages = new ObservableCollection<MessageInfo>();

            FavouritesCommand = new Command(OnFavouritesCommand);
            LoadVideoCommand = new Command(OnLoadVideoCommand);

            GetMessages(0);
        }

        public async void GetMessages(int id)
        {
            var newMessages = await TelegramApi.GetMessages(SelectedChat.Id, id);
            var allMessages = Messages.ToList();
            allMessages.AddRange(newMessages);
            Messages = new ObservableCollection<MessageInfo>(allMessages);
        }

        private void OnFavouritesCommand(object obj)
        {

        }

        public async void OnLoadVideoCommand(object obj)
        {
            foreach(var mess in Messages)
                foreach(var media in mess.Media)
                    if (media.Filename == obj.ToString())
                    {
                        await media.GetFile();
                        return;
                    }
        }
    }
}
