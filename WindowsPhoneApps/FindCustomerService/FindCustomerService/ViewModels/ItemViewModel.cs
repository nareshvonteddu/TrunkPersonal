using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel.Calls;
using Windows.System;
using FindCustomerService.AddOnClients;
using FindCustomerService.DataModel;
using FindCustomerService.Models;

namespace FindCustomerService.ViewModels
{
    public class ItemViewModel 
    {
        ObservableCollection<Business> _searchResults = new ObservableCollection<Business>();
        //private BingClient _bingClient;
        private string searchString = string.Empty;

        public ItemViewModel()
        {
            //_bingClient = new BingClient();
            //_bingClient.SearchWebCompleted += bingSearchCompleted;
        }

        private void bingSearchCompleted(object sender, List<string> result)
        {
            
        }

        public ObservableCollection<Business> SearchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; }
        }

        

        public void Search(string message)
        {
            searchString = message;
            _searchResults.Clear();

            var matches = BusinessData.BusinessList.Where(x => x.Name.ToLower().StartsWith(message.ToLower()));
            foreach (var item in matches)
                SearchResults.Add(item);
            
            //if (SearchResults.Count == 0)
            //{
            //   // _bingClient.SearcWeb(string.Format("Customer Service Number for {0}", message));
            //    string concatQuery = string.Format("Customer Service Number for {0}", message);
            //    Windows.System.Launcher.LaunchUriAsync(new Uri(string.Format("bing://search?q={0}", concatQuery)));
            //}

            if (SearchResults.Count == 0)
            {

            }
        }

        public void PhoneNumberClick(string phoneNumber)
        {
            PhoneCallManager.ShowPhoneCallUI(phoneNumber, "");
        }

        public void Bing()
        {
            if (!string.IsNullOrEmpty(searchString.Trim()))
            {
                string concatQuery = string.Format("Customer Service Number for {0}", searchString);
                Launcher.LaunchUriAsync(new Uri(string.Format("bing://search?q={0}", concatQuery)));
            }
        }
    }
}