using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindCustomerService.Models
{
    public class UpdatedTimes
    {
        private string _id;
        private DateTime _businessUpadteDateTime;
        private DateTime _businessGroupUpdatedTime;

        public DateTime BusinessUpadteDateTime
        {
            get { return _businessUpadteDateTime; }
            set { _businessUpadteDateTime = value; }
        }

        public DateTime BusinessGroupUpdatedTime
        {
            get { return _businessGroupUpdatedTime; }
            set { _businessGroupUpdatedTime = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
