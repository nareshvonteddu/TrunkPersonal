using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace FarmersDiaryServiceWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FarmersDiaryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FarmersDiaryService.svc or FarmersDiaryService.svc.cs at the Solution Explorer and start debugging.

    [AspNetCompatibilityRequirements(
        RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FarmersDiaryService : IFarmersDiaryService
    {
        public string DoWork(int id)
        {
            return "you entered" + id;
        }

        public User GetUser(int Id)
        {
            return new User() { Id = Id, Name = "Naresh" };
        }
    }
}
