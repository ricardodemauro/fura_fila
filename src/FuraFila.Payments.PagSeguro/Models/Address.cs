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
    public class Address
    {
        /// <summary>
        /// Informa o nome da rua do endereço de envio do produto
        /// </summary>
        [DataMember]
        public string Street { get; set; }

        /// <summary>
        /// Informa o número do endereço de envio do produto
        /// </summary>
        [DataMember]
        public string Number { get; set; }

        /// <summary>
        /// Informa o complemento (bloco, apartamento, etc.) do endereço de envio do produto
        /// </summary>
        [DataMember]
        public string Complement { get; set; }

        /// <summary>
        /// Informa o bairro do endereço de envio do produto
        /// </summary>
        [DataMember]
        public string District { get; set; }

        /// <summary>
        /// Informa a cidade do endereço de envio do produto
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Informa o estado do endereço de envio do produto
        /// </summary>
        [DataMember]
        public string State { get; set; }

        /// <summary>
        /// país do endereço de envio do produto
        /// somente BRA é permitido
        /// </summary>
        [DataMember]
        public string Country { get; set; }

        /// <summary>
        /// Informa o CEP do endereço de envio do produto
        /// </summary>
        [DataMember]
        public string PostalCode { get; set; }
    }
}
