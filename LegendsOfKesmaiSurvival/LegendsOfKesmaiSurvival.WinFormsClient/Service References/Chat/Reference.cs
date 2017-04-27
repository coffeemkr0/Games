﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LegendsOfKesmaiSurvival.WinFormsClient.Chat {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="LegendsOfKesmaiSurvival.Services.Chat", ConfigurationName="Chat.IChatService", CallbackContract=typeof(LegendsOfKesmaiSurvival.WinFormsClient.Chat.IChatServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IChatService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="LegendsOfKesmaiSurvival.Services.Chat/IChatService/Join")]
        void Join(System.Guid key);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="LegendsOfKesmaiSurvival.Services.Chat/IChatService/Leave", ReplyAction="LegendsOfKesmaiSurvival.Services.Chat/IChatService/LeaveResponse")]
        void Leave();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="LegendsOfKesmaiSurvival.Services.Chat/IChatService/Say")]
        void Say(System.Guid key, string text);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="LegendsOfKesmaiSurvival.Services.Chat/IChatService/Whisper")]
        void Whisper(System.Guid key, string toNickName, string text);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="LegendsOfKesmaiSurvival.Services.Chat/IChatService/SayCallback")]
        void SayCallback(string nickName, string text);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceChannel : LegendsOfKesmaiSurvival.WinFormsClient.Chat.IChatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatServiceClient : System.ServiceModel.DuplexClientBase<LegendsOfKesmaiSurvival.WinFormsClient.Chat.IChatService>, LegendsOfKesmaiSurvival.WinFormsClient.Chat.IChatService {
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Join(System.Guid key) {
            base.Channel.Join(key);
        }
        
        public void Leave() {
            base.Channel.Leave();
        }
        
        public void Say(System.Guid key, string text) {
            base.Channel.Say(key, text);
        }
        
        public void Whisper(System.Guid key, string toNickName, string text) {
            base.Channel.Whisper(key, toNickName, text);
        }
    }
}
