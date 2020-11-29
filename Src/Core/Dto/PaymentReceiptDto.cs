using System;
using System.Runtime.Serialization;

namespace Core.Dto
{
    [DataContract]
    public class PaymentReceiptDto
    {
        [DataMember]
        public string OrderNumber { get; set; }
        
        [DataMember]
        public decimal Amount { get; set; }
        
        [DataMember]
        //Could be extended to F/L name by user object in a real system.
        public int UserId { get; set; }
        
        [DataMember]
        public DateTime PaymentDate { get; set; }
        
        [DataMember]
        public string Description { get; set; }
    }
}