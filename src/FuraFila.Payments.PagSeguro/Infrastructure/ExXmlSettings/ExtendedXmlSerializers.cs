﻿using ExtendedXmlSerializer.Configuration;
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
        internal static readonly IExtendedXmlSerializer CheckoutRQ = new ConfigurationContainer()
            .Register(DecimalConverter.Default)
            .Register(DateTimeConverter.Default)
            .EnableImplicitTyping(new Type[] { typeof(CheckoutRequest), typeof(Item), typeof(Document), typeof(Address) })
            .Type<Error>()
                .Member(m => m.Code).Name("code")
                .Member(m => m.Message).Name("message")
            .Type<CheckoutRequest>()
                .Member(p => p.Currency).Name("currency")
                .Member(p => p.EnableRecover).Name("enableRecover")
                .Member(m => m.ExtraAmount).Name("extraAmount")
                .Member(m => m.Items).Name("items")
                .Member(m => m.MaxAge).Name("maxAge")
                .Member(m => m.MaxUses).Name("maxUses")
                .Member(m => m.NotificationURL).Name("notificationURL")
                .Member(m => m.Receiver).Name("receiver")
                .Member(m => m.RedirectURL).Name("redirectURL")
                .Member(m => m.Reference).Name("reference")
                .Member(m => m.Sender).Name("sender")
                .Member(m => m.Shipping).Name("shipping")
                .Member(m => m.Timeout).Name("timeout")
            .Type<Document>()
                .Member(m => m.Type).Name("type")
                .Member(m => m.Value).Name("value")
            .Type<Address>()
                .Member(m => m.City).Name("city")
                .Member(m => m.Complement).Name("complement")
                .Member(m => m.Country).Name("country")
                .Member(m => m.District).Name("district")
                .Member(m => m.Number).Name("number")
                .Member(m => m.PostalCode).Name("postalCode")
                .Member(m => m.State).Name("state")
                .Member(m => m.Street).Name("street")
            .ConfigureType<Item>()
                .Name("item")
                .Member(m => m.Amount).Name("amount")
                .Member(m => m.Description).Name("description")
                .Member(m => m.Id).Name("id")
                .Member(m => m.Quantity).Name("quantity")
                .Member(m => m.ShippingCost).Name("shippingCost")
                .Member(m => m.Weight).Name("weight")
            .Type<Phone>()
                .Member(m => m.AreaCode).Name("areaCode")
                .Member(m => m.Number).Name("number")
            .Type<Receiver>()
                .Member(m => m.Email).Name("email")
            .Type<Sender>()
                .Member(m => m.BornDate).Name("bornDate")
                .Member(m => m.Documents).Name("documents")
                .Member(m => m.Email).Name("email")
                .Member(m => m.Name).Name("name")
                .Member(m => m.Phone).Name("phone")
            .Type<Shipping>()
                .Member(m => m.Address).Name("address")
                .Member(m => m.AddressRequired).Name("addressRequired")
                .Member(m => m.Cost).Name("cost")
                .Member(m => m.Type).Name("type")
            .ConfigureType<CheckoutRequest>()
                .Name("checkout")
            .ConfigureType<CheckoutResult>()
                .Name("checkout")
                .Member(m => m.Errors).Name("errors")
                .Member(m => m.Date).Name("date")
                .Member(m => m.Code).Name("code")

            .Create();

        internal static readonly IExtendedXmlSerializer CheckoutRS = new ConfigurationContainer()
            .Register(DecimalConverter.Default)
            .Register(DateTimeConverter.Default)
            .EnableImplicitTyping(new Type[] { typeof(CheckoutResult), typeof(Error) })
            .Type<Error>()
                .Member(m => m.Code).Name("code")
                .Member(m => m.Message).Name("message")
            .ConfigureType<CheckoutResult>()
                .Name("checkout")
                .Member(m => m.Errors).Name("errors")
                .Member(m => m.Date).Name("date")
                .Member(m => m.Code).Name("code")

            .Create();
    }
}