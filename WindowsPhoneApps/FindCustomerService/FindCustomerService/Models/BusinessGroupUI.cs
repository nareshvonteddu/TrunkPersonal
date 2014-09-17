using System.Collections.ObjectModel;

namespace FindCustomerService.Models
{
    public class BusinessGroupUI:BusinessGroup
    {
        private ObservableCollection<Business> _businesses = new ObservableCollection<Business>();

        public ObservableCollection<Business> Businesses
        {
            get { return _businesses; }
            set { _businesses = value; }
        }
    }
}
