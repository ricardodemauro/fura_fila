using FuraFila.Payments.PagSeguro.Infrastructure;
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
            StringContent content = new StringContent(ToXml(data));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            string securePath = GetPathWithToken(path, email, accessToken);

            using (var request = new HttpRequestMessage(HttpMethod.Post, securePath))
            {
                request.Content = content;
                //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                using (var response = await _client.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    return FromXml<TResult>(await response.Content.ReadAsStringAsync());
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
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var xmlSerializer = new XmlSerializer(typeof(T));

            var sb = new StringBuilder();
            using (var stream = new StringWriter(sb, CultureInfo.InvariantCulture))
            using (var writer = new XmlFirstLowerWriter(stream))
            {
                xmlSerializer.Serialize(writer, data, emptyNamespaces);
            }
            return sb.ToString();

            //var serializer = new DataContractSerializer(typeof(T));
            //using (var output = new StringWriter())
            //using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented })
            //{
            //    serializer.WriteObject(writer, this);
            //    return output.GetStringBuilder().ToString();
            //}
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
