using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    [DataContract]
    public abstract class ResultBase
    {
        [DataMember]
        public List<Error> Errors { get; set; }
    }
}
