using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using le_mur.TelegramApi;
using le_mur.Models;

namespace le_mur
{
    public class MainViewModel : BaseViewModel
    {
        public Command LikeCommand { get; }
        public Command CommentCommand { get; }
        public Command FavouritesCommand { get; }

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
            LikeCommand = new Command(OnLikeCommand);
            CommentCommand = new Command(OnCommentCommand);
            FavouritesCommand = new Command(OnFavouritesCommand);

            Posts = new ObservableCollection<Post>();
            Posts.Add(new Post { Id = 1, Image = "post1.png", ChanelImage = "chanel.gif", ChanelName = "ИГЭУ имени В.И. Ленина", ChanelTheme = "Университет" });
            Posts.Add(new Post { Id = 2, Image = "post2.png", ChanelImage = "chanel.gif", ChanelName = "ИГЭУ имени В.И. Ленина", ChanelTheme = "Университет" });
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
