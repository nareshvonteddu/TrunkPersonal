using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindCustomerService.Models
{
    class SerializeBusinessGroup
    {
        private DateTime _businessGroupUpdatedDateTime;
        private DateTime _businessUpdatedDateTime;
        private List<BusinessGroupUI> _businessGroupUis = new List<BusinessGroupUI>();
        public  List<Business> BusinessList = new List<Business>();

        public DateTime BusinessGroupUpdatedDateTime
        {
            get { return _businessGroupUpdatedDateTime; }
            set { _businessGroupUpdatedDateTime = value; }
        }

        public DateTime BusinessUpdatedDateTime
        {
            get { return _businessUpdatedDateTime; }
            set { _businessUpdatedDateTime = value; }
        }

        public List<BusinessGroupUI> BusinessGroupUis
        {
            get { return _businessGroupUis; }
            set { _businessGroupUis = value; }
        }
    }
}
