using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace NRIUturn.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            //DataLayerService.DataLayerServiceClient sc = new DataLayerService.DataLayerServiceClient();
            //sc.GetPostsCompleted += new EventHandler<DataLayerService.GetPostsCompletedEventArgs>(sc_GetPostsCompleted);
            //sc.GetPostsAsync("GetPostsFor", "");

            //sc.GetDataCompleted += new EventHandler<DataLayerService.GetDataCompletedEventArgs>(sc_GetDataCompleted);
            //sc.GetDataAsync(1);
        }

        //void sc_GetDataCompleted(object sender, DataLayerService.GetDataCompletedEventArgs e)
        //{
        //    string res = e.Result;
        //}

        //void sc_GetPostsCompleted(object sender, DataLayerService.GetPostsCompletedEventArgs e)
        //{
        //    ObservableCollection<Dictionary<string,string>> res = e.Result;
        //}

        // Executes when the user navigates to this page.
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}
    }
}