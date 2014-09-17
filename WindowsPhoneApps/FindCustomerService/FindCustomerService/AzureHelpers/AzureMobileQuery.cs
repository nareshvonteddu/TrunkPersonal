using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindCustomerService.AzureHelpers
{
    public class AzureMobileQuery 
    {
        private string _uri;

        public string Uri
        {
            get{ return _uri;}  
        }
        
        public AzureMobileQuery(string uri)
        {
            _uri = uri;
        }
    }
}
