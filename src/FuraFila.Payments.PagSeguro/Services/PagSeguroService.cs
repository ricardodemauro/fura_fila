using System;
using System.Collections.Generic;
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

        internal async Task<TResult> SendRequest<TRequest, TResult>(TRequest data, string accessToken, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            StringContent content = new StringContent(ToXml(data));

            content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

            using (var request = new HttpRequestMessage(HttpMethod.Post, path))
            {
                request.Content = content;
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

                using (var response = await _client.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    return FromXml<TResult>(await response.Content.ReadAsStreamAsync());
                }
            }
        }

        /// <summary>
        /// Converts this instance to XML.
        /// </summary>
        /// <returns>XML representing this instance.</returns>
        public string ToXml<T>(T data)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var output = new StringWriter())
            using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented })
            {
                serializer.WriteObject(writer, this);
                return output.GetStringBuilder().ToString();
            }
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
    }
}
