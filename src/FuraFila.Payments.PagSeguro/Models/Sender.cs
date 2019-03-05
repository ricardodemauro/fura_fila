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
    public class Sender
    {
        /// <summary>
        /// nome completo do comprador que está realizando o pagamento
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// e-mail do comprador que está realizando o pagamento
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Dados do telefone do comprador
        /// </summary>
        [DataMember]
        public Phone Phone { get; set; }

        /// <summary>
        /// Lista de documentos do comprador
        /// </summary>
        [DataMember]
        public Document[] Documents { get; set; }

        /// <summary>
        /// data de nascimento do comprador
        /// Formato:dd/MM/yyyy (dia/mês/ano).
        /// </summary>
        [DataMember]
        public DateTime? BornDate { get; set; }
    }
}
