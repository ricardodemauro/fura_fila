using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FuraFila.Payments.PagSeguro.Models
{
    [Serializable()]
    [DataContract]
    [XmlRoot("checkout")]
    public class CheckoutRequest
    {
        [DataMember]
        [XmlElement(ElementName = "sender")]
        public Sender Sender { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public Item[] Items { get; set; }

        [DataMember]
        public string RedirectURL { get; set; }

        [DataMember]
        public decimal ExtraAmount { get; set; }

        [DataMember]
        public string Reference { get; set; }

        [DataMember]
        public Shipping Shipping { get; set; }

        [DataMember]
        public int Timeout { get; set; }

        [DataMember]
        public int MaxAge { get; set; }

        [DataMember]
        public int MaxUses { get; set; }

        [DataMember]
        public Receiver Receiver { get; set; }

        [DataMember]
        public bool EnableRecover { get; set; }
    }
}
