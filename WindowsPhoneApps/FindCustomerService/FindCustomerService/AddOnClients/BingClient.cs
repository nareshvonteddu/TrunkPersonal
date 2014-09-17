using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Net;
using Bing;

namespace FindCustomerService.AddOnClients
{
    public class BingClient
    {
        private readonly BingSearchContainer _client;

        public BingClient()
        {
            _client = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Data.ashx/Bing/Search/v1/"));
            _client.Credentials = new NetworkCredential("accountKey", "QUJ3QqeLysD7LkdAKG09cWdsu+y4Y0au5rg0T5XC9B8");
        }

        public void SearcWeb(string message)
        {
            var query = _client.Web(message, null,null,null , null, null, null, null);
            query.BeginExecute(onSearchCompleted, query);
        }

        private void onSearchCompleted(IAsyncResult queryResult)
        {
            DataServiceQuery<Bing.WebResult> query = queryResult.AsyncState as DataServiceQuery<Bing.WebResult>;
            var resultList = new List<string>();

            foreach (var result in query.EndExecute(queryResult))
                resultList.Add(result.ToString());

            SearchWebCompleted(this, resultList);
        }

        public event SearchWebCompletedEventHandler SearchWebCompleted;
        public delegate void SearchWebCompletedEventHandler(object sender, List<string> result);

    }


}
