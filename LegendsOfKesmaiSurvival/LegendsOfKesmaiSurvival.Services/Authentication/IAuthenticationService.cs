using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Authentication
{
    [ServiceContract
    (Namespace = "LegendsOfKesmaiSurvival.Services.Authentication",
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(IAuthenticationServiceCallback))]
    public interface IAuthenticationService
    {
        [OperationContract(IsInitiating = true, IsOneWay = false)]
        bool Login(string userName, string password, out Guid key);

        [OperationContract(IsInitiating = false, IsOneWay = false)]
        List<Core.GameStateInformation.ServiceInformation> GetServiceInformation();

        [OperationContract(IsInitiating = true, IsOneWay = false)]
        bool CreateNewUser(string userName, string nickName, string password, out string exception);
    }
}
