using System.Collections.ObjectModel;
using Xamarin.Forms;
using le_mur.Model;
using TL;
using le_mur.NetworkCalling;

namespace le_mur.ViewModel
{
    public class ChannelViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public Command FavouritesCommand { get; }

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

            GetMessages();
        }

        private async void GetMessages()
        {
            var list = await TelegramApi.GetMessages(SelectedChat.Id);
            Messages = new ObservableCollection<MessageInfo>(list);
        }

        private void OnFavouritesCommand(object obj)
        {

        }
    }
}
