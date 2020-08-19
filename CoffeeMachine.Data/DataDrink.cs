using System.Runtime.Serialization;

namespace CoffeeMachine.Data
{
    [DataContract]
    public class DataDrink
    {
        [DataMember]
        public string Name { get; set; }
    }
}
