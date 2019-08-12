using FuraFila.Payments.PagSeguro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Queries
{
    public class TransactionResult : ResultBase
    {
        public DateTime Date { get; set; }

        public string Code { get; set; }

        public string Reference { get; set; }

        public TransactionType Type { get; set; }

        public TransactionStatus Status { get; set; }

        public PaymentMethod paymentMethod { get; set; }

        public decimal GrossAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public CreditorFee CreditorFees { get; set; }

        /// <summary>
        /// Valor líquido da transação
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// Valor extra
        /// </summary>
        public decimal ExtraAmount { get; set; }

        /// <summary>
        /// Número de parcelas
        /// </summary>
        public int InstallmentCount { get; set; }

        /// <summary>
        /// Número de itens da transação
        /// </summary>
        public int ItemCount { get; set; }

        public List<Item> Items { get; set; }

        public Sender Sender { get; set; }

        public Shipping Shipping { get; set; }
    }
}
