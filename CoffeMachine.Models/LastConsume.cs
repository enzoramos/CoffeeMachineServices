using System.Runtime.Serialization;

namespace CoffeMachine.Models
{


    [DataContract]
    public partial class LastConsume
    {
        [DataMember]
        public int Id { get; set; }
        public int Drink { get; set; }
        [DataMember]
        public string Uid { get; set; }
        [DataMember]
        public int SugarLevel { get; set; }
        [DataMember]
        public bool UseMug { get; set; }
        [DataMember]
        public virtual Drink Drink1 { get; set; }
    }
}
