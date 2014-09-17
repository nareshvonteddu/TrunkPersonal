using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FindCustomerService.AzureHelpers
{
    public class AzureMobileHelper
    {
        string _baseurl;
        string _appKey;
        string _installationId;

        public AzureMobileHelper(string url, string appKey, string installationId = null)
        {
            _baseurl = url; _appKey = appKey; _installationId = installationId;
        }

        public AzureMobileTable getTable(string tableName)
        {
            return new AzureMobileTable(tableName);
        }

        public string Execute(AzureMobileQuery query)
        {
            string fullUri = _baseurl + query.Uri; 
            HttpWebRequest _azureRequest = WebRequest.Create(fullUri) as HttpWebRequest; 
            //_azureRequest.Headers.Add("X-ZUMO-APPLICATION", _appKey); 
            //_azureRequest.Headers.Add("X-ZUMO-INSTALLATION-ID", _installationId);
            using (HttpWebResponse restResponse = _azureRequest.GetResponseAsync().Result as HttpWebResponse)
            {
                Encoding enc = System.Text.Encoding.GetEncoding("UTF8Encoding");
                StreamReader _jsonStream = new StreamReader(restResponse.GetResponseStream(),enc); 
                return _jsonStream.ReadToEnd();  
            }  
        } 
        
    }
}
