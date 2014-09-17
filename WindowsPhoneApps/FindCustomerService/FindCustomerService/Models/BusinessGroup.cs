using System;
using FindCustomerService.Helpers;

namespace FindCustomerService.Models
{
    public class BusinessGroup:BindableBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        
        private DateTime _updateDateTime;
        public DateTime UpdateDateTime
        {
            get { return _updateDateTime; }
            set { _updateDateTime = value; }
        }
    }
}
