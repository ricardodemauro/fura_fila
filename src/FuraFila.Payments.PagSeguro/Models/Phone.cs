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
    public class Phone
    {
        /// <summary>
        /// código de área (DDD)
        /// </summary>
        [DataMember]
        public string AreaCode { get; set; }

        /// <summary>
        /// número do telefone 
        /// </summary>
        [DataMember]
        public string Number { get; set; }
    }
}
