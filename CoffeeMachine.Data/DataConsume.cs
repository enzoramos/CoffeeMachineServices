using System.Runtime.Serialization;

namespace CoffeeMachine.Data
{
    [DataContract]
    public class DataConsume
    {
        [DataMember]
        public string Uid { get; set; }
        [DataMember]
        public DataDrink Consume { get; set; }
        [DataMember]
        public int SugarLevel { get; set; }
        [DataMember]
        public bool UsedMug { get; set; }
    }
}
