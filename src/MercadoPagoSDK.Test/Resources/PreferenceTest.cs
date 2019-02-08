﻿using MercadoPago;
using MercadoPago.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MercadoPago.DataStructures.Preference;
using System.Net;
using Newtonsoft.Json.Linq;

namespace MercadoPagoSDK.Test.Resources
{
    [TestFixture()]
    public class PreferenceTest
    {
        Preference LastPreference;

        [SetUp]
        public void Init()
        {
            // Avoid SSL Cert error
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            SDK.CleanConfiguration();
            SDK.SetBaseUrl("https://api.mercadopago.com");
            SDK.ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            SDK.ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
        }

        [Test]
        public void Preference_CreateShouldBeOk() 
        {

            Shipment shipments = new Shipment()
            {
                ReceiverAddress = new ReceiverAddress()
                { ZipCode = "28834", StreetName = "Torrente Antonia", StreetNumber = int.Parse("1219"), Floor = "8", Apartment = "C" }
            };

            List<PaymentType> excludedPaymentTypes = new List<PaymentType>
            {
                new PaymentType()
                {
                    Id = "ticket"
                }
            };

            Preference preference = new Preference()
            {
                ExternalReference = "01-02-00000003",
                Expires = true,
                ExpirationDateFrom = DateTime.Now,
                ExpirationDateTo = DateTime.Now.AddDays(1), 
                PaymentMethods = new PaymentMethods()
                { 
                    ExcludedPaymentTypes = excludedPaymentTypes
                }
            };

            preference.Items.Add(
                new Item()
                {
                    Title = "Dummy Item",
                    Description = "Multicolor Item",
                    Quantity = 1,
                    UnitPrice = (Decimal)10.0
                }
            );

            preference.Shipments = shipments;

            //TODO - DOUBLE CHECK IF IS USED SOMEWHERE
            //preference.ProcessingModes.Add(MercadoPago.Common.ProcessingMode.aggregator);

            preference.Save();
            LastPreference = preference;

            Console.WriteLine("INIT POINT: " + preference.InitPoint);

            Assert.IsTrue(preference.Id.Length > 0 , "Failed: Payment could not be successfully created");
            Assert.IsTrue(preference.InitPoint.Length > 0, "Failed: Preference has not a valid init point");
        }

        [Test]
        public void Preference_FindByIDShouldbeOk()
        {
            Preference foundedPreference = Preference.FindById(LastPreference.Id); 
            Assert.AreEqual(foundedPreference.Id, LastPreference.Id); 
        }

        [Test]
        public void Preference_UpdateShouldBeOk()
        {
            LastPreference.ExternalReference = "DummyPreference for Integration Test";
            LastPreference.Update();
            Assert.AreEqual(LastPreference.ExternalReference, "DummyPreference for Integration Test"); 
        }  
    }
}
