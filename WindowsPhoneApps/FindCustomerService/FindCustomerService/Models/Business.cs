using System;
using FindCustomerService.Helpers;

namespace FindCustomerService.Models
{
    public class Business:BindableBase
    {
        private string _id;
        private string _name;
        private string _groupName;
        private string _phoneNumber;
        private DateTime _updateDateTime;

        public string Id 
        { 
            get { return _id; }
            set { SetProperty(ref _id, value); } 
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string PhoneNumber 
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; SetProperty(ref _groupName, value);}
        }

        public DateTime UpdateDateTime
        {
            get { return _updateDateTime; }
            set { _updateDateTime = value; }
        }
    }
}
