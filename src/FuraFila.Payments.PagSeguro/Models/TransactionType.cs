﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    public enum TransactionType
    {
        CartaoCredito = 1,
        Boleto = 2,
        DebitoOnline = 3,
        SaldoPagSeguro = 4,
        OiPaggo = 5,
        DepositoEmConta = 6
    }
}
