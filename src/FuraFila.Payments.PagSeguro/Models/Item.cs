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
    public class Item
    {
        [DataMember]
        /// <remarks/>
        public int Id { get; set; }

        [DataMember]
        /// <remarks/>
        public string Description { get; set; }

        [DataMember]
        /// <remarks/>
        public decimal Amount { get; set; }

        [DataMember]
        /// <remarks/>
        public int Quantity { get; set; }

        [DataMember]
        /// <remarks/>
        public int Weight { get; set; }

        [DataMember]
        /// <remarks/>
        public decimal ShippingCost { get; set; }
    }
}
