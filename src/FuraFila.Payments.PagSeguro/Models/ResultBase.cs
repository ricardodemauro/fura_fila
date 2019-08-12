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
    public abstract class ResultBase
    {
        [DataMember]
        public ErrorsCollection Errors { get; set; }
    }
}
