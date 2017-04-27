﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LegendsOfKesmaiSurvival.WinFormsClient.Zombies {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="LegendsOfKesmaiSurvival.Services.Zombies", ConfigurationName="Zombies.IGameplay", CallbackContract=typeof(LegendsOfKesmaiSurvival.WinFormsClient.Zombies.IGameplayCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IGameplay {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="LegendsOfKesmaiSurvival.Services.Zombies/IGameplay/Join")]
        void Join(System.Guid key);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="LegendsOfKesmaiSurvival.Services.Zombies/IGameplay/SendChat")]
        void SendChat(System.Guid key, string text);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="LegendsOfKesmaiSurvival.Services.Zombies/IGameplay/Move")]
        void Move(System.Guid key, LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions[] movements);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameplayCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="LegendsOfKesmaiSurvival.Services.Zombies/IGameplay/ChatReceivedCallback")]
        void ChatReceivedCallback(string text);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="LegendsOfKesmaiSurvival.Services.Zombies/IGameplay/GameStateUpdatedCallback")]
        void GameStateUpdatedCallback(LegendsOfKesmaiSurvival.Core.GameStateInformation.GameStateUpdate gameStateUpdate);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameplayChannel : LegendsOfKesmaiSurvival.WinFormsClient.Zombies.IGameplay, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameplayClient : System.ServiceModel.DuplexClientBase<LegendsOfKesmaiSurvival.WinFormsClient.Zombies.IGameplay>, LegendsOfKesmaiSurvival.WinFormsClient.Zombies.IGameplay {
        
        public GameplayClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GameplayClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GameplayClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameplayClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameplayClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Join(System.Guid key) {
            base.Channel.Join(key);
        }
        
        public void SendChat(System.Guid key, string text) {
            base.Channel.SendChat(key, text);
        }
        
        public void Move(System.Guid key, LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions[] movements) {
            base.Channel.Move(key, movements);
        }
    }
}
