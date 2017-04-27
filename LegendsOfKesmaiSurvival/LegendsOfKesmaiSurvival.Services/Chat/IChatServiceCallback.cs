using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.Chat
{
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void SayCallback(string nickName, string text);
    }
}
