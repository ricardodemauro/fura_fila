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
    public class Error
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
