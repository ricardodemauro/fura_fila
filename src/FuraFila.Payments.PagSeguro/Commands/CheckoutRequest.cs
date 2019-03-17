using FuraFila.Payments.PagSeguro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FuraFila.Payments.PagSeguro.Commands
{
    /// <summary>
    /// Checkout Request
    /// Reference: https://pagseguro.uol.com.br/v3/guia-de-integracao/api-de-pagamentos.html#v2-item-api-de-pagamentos-formato-xml
    /// </summary>
    [Serializable()]
    [DataContract]
    [XmlRoot("checkout")]
    public class CheckoutRequest
    {
        /// <summary>
        /// Dados do comprador
        /// </summary>
        [DataMember]
        public Sender Sender { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public Item[] Items { get; set; }

        /// <summary>
        /// URL de redirecionamento após o pagamento
        /// </summary>
        [DataMember]
        public string RedirectURL { get; set; }

        /// <summary>
        /// URL para envio de notificações sobre o pagamento
        /// </summary>
        [DataMember]
        public string NotificationURL { get; set; }

        /// <summary>
        /// Especifica um valor extra que deve ser adicionado ou subtraído ao valor total do pagamento
        /// </summary>
        [DataMember]
        public decimal ExtraAmount { get; set; }

        /// <summary>
        /// Define um código para fazer referência ao pagamento. Este código fica associado à transação criada 
        /// pelo pagamento e é útil para vincular as transações do PagSeguro às vendas registradas no seu sistema
        /// </summary>
        [DataMember]
        public string Reference { get; set; }

        [DataMember]
        public Shipping Shipping { get; set; }

        [DataMember]
        public int Timeout { get; set; }

        /// <summary>
        /// Prazo de validade do código de pagamento
        /// </summary>
        [DataMember]
        public int? MaxAge { get; set; }

        /// <summary>
        /// Número máximo de usos para o código de pagamento
        /// Determina o prazo (em segundos) durante o qual o código de pagamento criado pela chamada à API de Pagamentos poderá ser usado
        /// </summary>
        [DataMember]
        public int? MaxUses { get; set; }

        [DataMember]
        public Receiver Receiver { get; set; }

        [DataMember]
        public bool EnableRecover { get; set; }
    }
}
