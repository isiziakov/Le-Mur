using System.Collections.ObjectModel;
using Xamarin.Forms;
using le_mur.Model;
using TL;
using le_mur.NetworkCalling;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Core;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using le_mur.Consts;
using le_mur.View;

namespace le_mur.ViewModel
{
    public class FeedViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public Command FavouritesCommand { get; }
        public Command LoadVideoCommand { get; }
        public Command ToChannelsCommand { get; }


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

        public FeedViewModel()
        {
            Messages = new ObservableCollection<MessageInfo>();

            FavouritesCommand = new Command(OnFavouritesCommand);
            LoadVideoCommand = new Command(OnLoadVideoCommand);
            ToChannelsCommand = new Command(onChannelsCommand);

            GetMessages(0);
        }

        public async void GetMessages(int id)
        {
            // todo видимые каналы  
            var chats = await TelegramApi.GetChatsInfo();
            var tmp = new List<ChatInfo> { chats[0], chats[1], chats[2], chats[3], chats[4], chats[5], chats[6], chats[7], chats[8] };
            var wallInfo = await TelegramApi.GetCustomWall(new CustomWallInfo(tmp));

            List<MessageInfo> messages = wallInfo.Messages.ToList();
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].Media.Count > 0 && messages[i].Height == 0)
                    messages[i].Height = 300;
                var channel = wallInfo.ChatInfos.Find(x => x.Item1.Id.ID == messages[i].ChatId.ID);
                messages[i].GroupName = channel.Item1.Title;
                messages[i].GroupImage = channel.Item1.Image;
                var allMessages = Messages.ToList();
                allMessages.AddRange(messages);
                Messages = new ObservableCollection<MessageInfo>(allMessages);
            }
        }

        private void OnFavouritesCommand(object obj)
        {

        }

        public async void OnLoadVideoCommand(object obj)
        {
            foreach (var mess in Messages)
                foreach (var media in mess.Media)
                    if (media.Filename == obj.ToString())
                    {
                        await media.GetFile();
                        return;
                    }
        }

        private async void onChannelsCommand()
        {
            await Navigation.PushAsync(new ChannelsPage());
        }
    }
}
