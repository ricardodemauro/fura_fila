using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    [DataContract]
    public class CheckoutResult
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}
