using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Zombies
{
    [ServiceContract
    (Namespace = "LegendsOfKesmaiSurvival.Services.Zombies",
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(IAdministrationCallback))]
    public interface IAdministration
    {
        //TODO:This needs to go away and not be a WCF contract.  The only reason for this interface now would be to administer games
        //remotely (not on the server).
        [OperationContract(IsInitiating=true,IsOneWay=false)]
        void StartGame(string mapFile);
    }
}
