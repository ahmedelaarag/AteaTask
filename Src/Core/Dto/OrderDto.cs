using System.Runtime.Serialization;
using Core.Enums;

namespace Core.Dto
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public string OrderNumber { get; set; }
        
        [DataMember]
        public int UserId { get; set; }
        
        [DataMember]
        public decimal Amount { get; set; }
        
        [DataMember]
        public Gateway PaymentGateway { get; set; }
        
        [DataMember]
        public string Description { get; set; }
    }
}