using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using le_mur.ViewModel;
using le_mur.Model;

namespace le_mur.View
{
    public partial class FeedPage : ContentPage
    {
        public FeedPage()
        {
            InitializeComponent();
            this.Appearing += ChannelPage_Appearing;
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new FeedViewModel() { Navigation = this.Navigation };
        }

        private void ChannelPage_Appearing(object sender, EventArgs e)
        {
            this.messagesList.ItemAppearing += OnItemAppearing;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.Item == ((FeedViewModel)BindingContext).Messages.Last())
            {
                ((FeedViewModel)BindingContext).GetMessages(((FeedViewModel)BindingContext).Messages.Last().Id);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ((FeedViewModel)BindingContext).OnLoadVideoCommand(((TappedEventArgs)e).Parameter);
            ((Image)sender).IsVisible = false;
        }
    }
}
