using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    /// <summary>
    /// Dados do vendedor
    /// </summary>
    [Serializable()]
    [DataContract]
    public class Receiver
    {
        /// <summary>
        /// Especifica o e-mail que deve aparecer na tela de pagamento
        /// </summary>
        [DataMember]
        public string Email { get; set; }
    }
}
