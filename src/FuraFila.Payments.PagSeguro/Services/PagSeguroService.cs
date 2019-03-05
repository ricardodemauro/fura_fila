using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using FuraFila.Payments.PagSeguro.Infrastructure;
using FuraFila.Payments.PagSeguro.Infrastructure.ExXmlSettings;
using FuraFila.Payments.PagSeguro.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FuraFila.Payments.PagSeguro.Services
{
    public class PagSeguroService
    {
        private readonly HttpClient _client;

        public PagSeguroService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        private string GetPathWithToken(string path, string email, string token)
        {
            return $"{path}?email={email}&token={token}";
        }

        internal async Task<TResult> SendRequest<TRequest, TResult>(TRequest data, string path, string accessToken, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            string xmlContent = ToXml(data);
            StringContent content = new StringContent(xmlContent);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            string securePath = GetPathWithToken(path, email, accessToken);

            using (var request = new HttpRequestMessage(HttpMethod.Post, securePath))
            {
                request.Content = content;
                //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                using (var response = await _client.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    string xmlResult = await response.Content.ReadAsStringAsync();
                    return FromXml<TResult>(xmlResult);
                }
            }
        }

        internal Task<CheckoutResult> Checkout(CheckoutRequest request, string token, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendRequest<CheckoutRequest, CheckoutResult>(request, "/v2/checkout", token, email, cancellationToken);
        }

        /// <summary>
        /// Converts this instance to XML.
        /// </summary>
        /// <returns>XML representing this instance.</returns>
        public string ToXml<T>(T data)
        {
            var xmlSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true, Encoding = Encoding.UTF8 };
            string output = ExtendedXmlSerializers.Checkout.Serialize(xmlSettings, data);
            return output;
        }

        /// <summary>
        /// Converts this instance to XML.
        /// </summary>
        /// <returns>XML representing this instance.</returns>
        public T FromXml<T>(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
            {
                return (T)serializer.ReadObject(reader);

            }
        }

        /// <summary>
        /// Converts this instance to XML.
        /// </summary>
        /// <returns>XML representing this instance.</returns>
        public T FromXml<T>(string content)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(content))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
