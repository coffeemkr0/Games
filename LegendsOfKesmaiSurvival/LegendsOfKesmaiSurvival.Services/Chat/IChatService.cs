using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Chat
{
    [ServiceContract
    (Namespace = "LegendsOfKesmaiSurvival.Services.Chat",
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void Join(Guid key);

        [OperationContract(IsInitiating = false, IsOneWay = false)]
        void Leave();

        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void Say(Guid key, string text);

        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void Whisper(Guid key, string toNickName, string text);
    }
}
