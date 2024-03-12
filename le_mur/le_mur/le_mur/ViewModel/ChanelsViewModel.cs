using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace le_mur.ViewModel
{
    public class ChanelsViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public Command ToolsCommand { get; }

        public ChanelsViewModel() 
        {
            ToolsCommand = new Command(OnToolsCommand);
        }

        private void OnToolsCommand()
        {

        }
    }
}
