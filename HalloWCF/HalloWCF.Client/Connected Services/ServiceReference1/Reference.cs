﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HalloWCF.Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Fruits", Namespace="http://schemas.datacontract.org/2004/07/HalloWCF")]
    [System.SerializableAttribute()]
    public partial class Fruits : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AnzahlKerneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime VerfaultAmField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AnzahlKerne {
            get {
                return this.AnzahlKerneField;
            }
            set {
                if ((this.AnzahlKerneField.Equals(value) != true)) {
                    this.AnzahlKerneField = value;
                    this.RaisePropertyChanged("AnzahlKerne");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime VerfaultAm {
            get {
                return this.VerfaultAmField;
            }
            set {
                if ((this.VerfaultAmField.Equals(value) != true)) {
                    this.VerfaultAmField = value;
                    this.RaisePropertyChanged("VerfaultAm");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAllObst", ReplyAction="http://tempuri.org/IService1/GetAllObstResponse")]
        HalloWCF.Client.ServiceReference1.Fruits[] GetAllObst();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAllObst", ReplyAction="http://tempuri.org/IService1/GetAllObstResponse")]
        System.Threading.Tasks.Task<HalloWCF.Client.ServiceReference1.Fruits[]> GetAllObstAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddObst", ReplyAction="http://tempuri.org/IService1/AddObstResponse")]
        void AddObst(HalloWCF.Client.ServiceReference1.Fruits obst);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddObst", ReplyAction="http://tempuri.org/IService1/AddObstResponse")]
        System.Threading.Tasks.Task AddObstAsync(HalloWCF.Client.ServiceReference1.Fruits obst);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : HalloWCF.Client.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<HalloWCF.Client.ServiceReference1.IService1>, HalloWCF.Client.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public HalloWCF.Client.ServiceReference1.Fruits[] GetAllObst() {
            return base.Channel.GetAllObst();
        }
        
        public System.Threading.Tasks.Task<HalloWCF.Client.ServiceReference1.Fruits[]> GetAllObstAsync() {
            return base.Channel.GetAllObstAsync();
        }
        
        public void AddObst(HalloWCF.Client.ServiceReference1.Fruits obst) {
            base.Channel.AddObst(obst);
        }
        
        public System.Threading.Tasks.Task AddObstAsync(HalloWCF.Client.ServiceReference1.Fruits obst) {
            return base.Channel.AddObstAsync(obst);
        }
    }
}
