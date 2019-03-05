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
    public class Document
    {
        /// <summary>
        /// tipo de documento do comprador que está realizando o pagamento
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// número do documento
        /// </summary>
        [DataMember]
        public string Value { get; set; }
    }
}
