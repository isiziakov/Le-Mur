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
    public partial class ChannelPage : ContentPage
    {
        public ChannelPage(ChatInfo chat)
        {
            InitializeComponent();
            this.Appearing += ChannelPage_Appearing;
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new ChannelViewModel(chat) { Navigation = this.Navigation };
        }

        private void ChannelPage_Appearing(object sender, EventArgs e)
        {
            this.messagesList.ItemAppearing += OnItemAppearing;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.Item == ((ChannelViewModel)BindingContext).Messages.Last())
            {
                ((ChannelViewModel)BindingContext).GetMessages(((ChannelViewModel)BindingContext).Messages.Last().Id);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ((ChannelViewModel)BindingContext).OnLoadVideoCommand(((TappedEventArgs)e).Parameter);
            ((Image)sender).IsVisible = false;
        }
    }
}
