using MercadoPago;
using MercadoPago.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPagoSDK.Test
{
    [TestFixture]
    public class PaymentTest
    {
        string AccessToken;

        [Test]
        public void TestPayment()
        {
            // Avoid SSL Cert error
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            // HardCoding Credentials
            AccessToken = TestConfig.GetEnvironmentVariable("ACCESS_TOKEN");
            // Make a Clean Test
            SDK.CleanConfiguration();
            SDK.SetBaseUrl("https://api.mercadopago.com");

            SDK.SetAccessToken(AccessToken);

            List<PaymentMethod> paymentMethods = PaymentMethod.All();


            Payment payment = new Payment
            {
                TransactionAmount = float.Parse("158"),
                Token = "some-token",
                Description = "Fantastic Paper Clock",
                Installments = 1,
                PaymentMethodId = paymentMethods[0].Id,
                IssuerId = "1234",
                Payer = new MercadoPago.DataStructures.Payment.Payer
                {
                    Email = "richie_stamm@gmail.com"
                }
            };

            payment.Save();

            Assert.AreEqual(MercadoPago.Common.PaymentStatus.approved, payment.Status);
        }
    }
}
