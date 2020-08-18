using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace HalloWCF
{

    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        IEnumerable<Obst> GetAllObst();


        [OperationContract]
        void AddObst(Obst obst);
    }

    [DataContract(Name = "Fruits")]
    public class Obst
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public int AnzahlKerne { get; set; }
        
        [DataMember]
        public DateTime VerfaultAm { get; set; }
    }
}
