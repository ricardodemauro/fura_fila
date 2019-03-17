using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    public enum PaymentMethodCode
    {
        CCVisa = 101,
        CCMasterCard = 102,
        CCAmericanExpress = 103,
        CCDiners = 104,
        CCHipercard = 105,
        CCAura = 106,
        CCElo = 107,
        CCPLENOCard = 108,
        CCPersonalCard = 109,
        CCJCB = 110,
        CCDiscover = 111,
        CCBrasilCard = 112,
        CCFORTBRASIL = 113,
        CCCARDBAN = 114,
        CCVALECARD = 115,
        CCCabal = 116,
        CCMais = 117,
        CCAvista = 118,
        CCGRANDCARD = 119,
        CCSorocred = 120,
        CCUpPolicard = 122,
        CCBaneseCard = 123,
        Boleto_Bradesco = 201,
        Boleto_Santander = 202,
        DebitoBradesco = 301,
        DebitoItaú = 302,
        DebitoUnibanco = 303,
        DebitoBancoDoBrasil = 304,
        DebitoBancoReal = 305,
        DebitoBanrisul = 306,
        DebitoHSBC = 307,
        SaldoPagSeguro = 401,
        OiPaggo = 501,
        DepositoEmConta = 701,
    }
}
