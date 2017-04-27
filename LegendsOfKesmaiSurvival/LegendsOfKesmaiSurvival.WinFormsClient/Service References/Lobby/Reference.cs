﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LegendsOfKesmaiSurvival.WinFormsClient.Lobby {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="LegendsOfKesmaiSurvival.Services.Lobby", ConfigurationName="Lobby.ILobbyService", CallbackContract=typeof(LegendsOfKesmaiSurvival.WinFormsClient.Lobby.ILobbyServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface ILobbyService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="LegendsOfKesmaiSurvival.Services.Lobby/ILobbyService/Join")]
        void Join(System.Guid key);
        
        [System.ServiceModel.OperationContractAttribute(Action="LegendsOfKesmaiSurvival.Services.Lobby/ILobbyService/Leave", ReplyAction="LegendsOfKesmaiSurvival.Services.Lobby/ILobbyService/LeaveResponse")]
        void Leave();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="LegendsOfKesmaiSurvival.Services.Lobby/ILobbyService/PlayerJoinedCallback")]
        void PlayerJoinedCallback(string nickName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyServiceChannel : LegendsOfKesmaiSurvival.WinFormsClient.Lobby.ILobbyService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyServiceClient : System.ServiceModel.DuplexClientBase<LegendsOfKesmaiSurvival.WinFormsClient.Lobby.ILobbyService>, LegendsOfKesmaiSurvival.WinFormsClient.Lobby.ILobbyService {
        
        public LobbyServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LobbyServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LobbyServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Join(System.Guid key) {
            base.Channel.Join(key);
        }
        
        public void Leave() {
            base.Channel.Leave();
        }
    }
}
