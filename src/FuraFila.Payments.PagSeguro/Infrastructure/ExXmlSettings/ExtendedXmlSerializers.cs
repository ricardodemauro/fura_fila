using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Content;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using FuraFila.Payments.PagSeguro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Infrastructure.ExXmlSettings
{
    public static class ExtendedXmlSerializers
    {
        internal static readonly IExtendedXmlSerializer Checkout = new ConfigurationContainer()
            .Register(DecimalConverter.Default)
            .EnableImplicitTyping(typeof(CheckoutRequest))
            .Type<CheckoutRequest>()
                .Member(p => p.Currency)
                    .Name("currency")
                .Member(p => p.EnableRecover)
                    .Name("enableRecover")
                .Member(m => m.ExtraAmount)
                    .Name("extraAmount")
                .Member(m => m.Items)
                    .Name("items")
                .Member(m => m.MaxAge)
                    .Name("maxAge")
                .Member(m => m.MaxUses)
                    .Name("maxUses")
                .Member(m => m.NotificationURL)
                    .Name("notificationURL")
                .Member(m => m.Receiver)
                    .Name("receiver")
                .Member(m => m.RedirectURL)
                    .Name("redirectURL")
                .Member(m => m.Reference)
                    .Name("reference")
                .Member(m => m.Sender)
                    .Name("sender")
                .Member(m => m.Shipping)
                    .Name("shipping")
                .Member(m => m.Timeout)
                    .Name("timeout")
            .ConfigureType<CheckoutRequest>()
            .Name("checkout")

            .Create();
    }
}
