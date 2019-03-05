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
    public class Item
    {
        /// <summary>
        /// Identificam os itens sendo pagos. Você pode escolher códigos que tenham significado para seu sistema 
        /// e informá-los nestes parâmetros. 
        /// O PagSeguro não realiza qualquer validação sobre esses identificadores, 
        /// mas eles não podem se repetir em um mesmo pagamento
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Descrevem os itens sendo pagos. A descrição é o texto que o PagSeguro mostra associado a cada item quando 
        /// o comprador está finalizando o pagamento, portanto é importante que ela seja clara e explicativa
        /// </summary>
        [DataMember]
        /// <remarks/>
        public string Description { get; set; }

        /// <summary>
        /// Representam os preços unitários de cada item sendo pago
        /// </summary>
        [DataMember]
        /// <remarks/>
        public decimal Amount { get; set; }

        /// <summary>
        /// quantidades de cada item sendo pago
        /// </summary>
        [DataMember]
        /// <remarks/>
        public int Quantity { get; set; }

        /// <summary>
        /// peso (em gramas) de cada item sendo pago
        /// </summary>
        [DataMember]
        /// <remarks/>
        public int Weight { get; set; }

        /// <summary>
        /// custos de frete de cada item sendo pago
        /// </summary>
        [DataMember]
        /// <remarks/>
        public decimal? ShippingCost { get; set; }
    }
}
