using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    /// <summary>
    /// For more info checkout 
    /// https://dev.pagseguro.uol.com.br/docs/checkout-web-notificacoes#section--a-name-status-da-transacao-a-status-da-transa-o
    /// </summary>
    public enum TransactionStatus
    {
        AguardandoPagamento = 1,
        EmAnalise = 2,
        Paga = 3,
        Disponivel = 4,
        EmDisputa = 5,
        Devolvida = 6,
        Cancelada = 7,
        Debitado = 8,
        RetencaoTemporaria = 9
    }
}
