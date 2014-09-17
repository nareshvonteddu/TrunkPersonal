using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using FindCustomerService.Common;

namespace FindCustomerService
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int textCount = 0;
        //private TextBox _textBox;
        
        private readonly NavigationHelper navigationHelper;
        ViewModels.MainViewModel ViewModel { get { return this.DataContext as ViewModels.MainViewModel; } }

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            //_textBox = sender as TextBox;
            //_textBox.IsEnabled = false;
            //if (((TextBox)sender).Text == string.Empty || textCount != 0) return;
            //string textString = _textBox.Text;
            //Frame.Navigate(typeof(ItemPage), textString);
            //if (_textBox != null) _textBox.Text = string.Empty;
            //_textBox.IsEnabled = true;
            //textCount++;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            object content = ((ContentControl)(sender)).Content;
            if (content != null)
            {
                string phoneNbr = content.ToString();
                this.ViewModel.PhoneNumberClick(phoneNbr);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ItemPage));
        }
    }
}
