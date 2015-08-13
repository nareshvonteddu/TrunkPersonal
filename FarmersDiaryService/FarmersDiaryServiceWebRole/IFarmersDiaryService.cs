using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace FarmersDiaryServiceWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFarmersDiaryService" in both code and config file together.
    [ServiceContract]
    public interface IFarmersDiaryService
    {
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DoWork/?key={id}", ResponseFormat = WebMessageFormat.Json)]
        string DoWork(int id);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "GetUser/?key={id}", ResponseFormat = WebMessageFormat.Json)]
        User GetUser(int Id);
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
