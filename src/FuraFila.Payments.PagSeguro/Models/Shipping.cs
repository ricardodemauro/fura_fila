using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    [Serializable()]
    [DataContract]
    public class Shipping
    {
        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public ShippingType Type { get; set; }

        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        public bool AddressRequired { get; set; }
    }
}
