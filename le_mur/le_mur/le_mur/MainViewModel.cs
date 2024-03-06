using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using le_mur.Models;

namespace le_mur
{
    public class MainViewModel : BaseViewModel
    {
        public Command LikeCommand { get; }
        public Command CommentCommand { get; }
        public Command FavouritesCommand { get; }

        public Command DownloadCommand { get; }

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

        private ObservableCollection<Post> posts;
        public ObservableCollection<Post> Posts
        {
            get { return posts; }
            set
            {
                if (posts != value)
                {
                    posts = value;
                    OnPropertyChanged("Posts");
                }
            }
        }
        public MainViewModel()
        {
            DownloadCommand = new Command(onDownloadCommand);

            Groups = new ObservableCollection<string>();

            LikeCommand = new Command(OnLikeCommand);
            CommentCommand = new Command(OnCommentCommand);
            FavouritesCommand = new Command(OnFavouritesCommand);

            Posts = new ObservableCollection<Post>();
            Posts.Add(new Post { Id = 1, Image = "post1.png", ChanelImage = "chanel.gif", ChanelName = "ИГЭУ имени В.И. Ленина", ChanelTheme = "Университет" });
            Posts.Add(new Post { Id = 2, Image = "post2.png", ChanelImage = "chanel.gif", ChanelName = "ИГЭУ имени В.И. Ленина", ChanelTheme = "Университет" });

        }

        private async void onDownloadCommand()
        {
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
        }
        public void OnLikeCommand(object obj)
        {

        }

        public void OnCommentCommand(object obj)
        {

        }

        public void OnFavouritesCommand(object obj)
        {

        }
    }
}
