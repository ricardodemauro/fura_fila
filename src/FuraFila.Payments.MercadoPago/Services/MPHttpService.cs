using FuraFila.Payments.MercadoPago.Preferences;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FuraFila.Payments.MercadoPago.Services
{
    public class MPHttpService
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public MPHttpService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<PreferenceResponse> SendRequest(PreferenceRequest rq, string accessToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            string jContent = JsonConvert.SerializeObject(rq, _settings);
            StringContent strContent = new StringContent(jContent);
            strContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var request = new HttpRequestMessage(HttpMethod.Post, $"/checkout/preferences?access_token={accessToken}"))
            {
                request.Content = strContent;
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await _client.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    var jResult = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<PreferenceResponse>(jResult);

                    return result;
                }
            }
        }
    }
}
