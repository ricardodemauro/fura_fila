using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    public class CreditorFee
    {
        public decimal intermediationRateAmount { get; set; }

        public decimal intermediationFeeAmount { get; set; }
    }
}
