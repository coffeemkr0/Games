using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.Zombies
{
    public interface IAdministrationCallback
    {
        [OperationContract(IsOneWay = true)]
        void Callback();
    }
}
