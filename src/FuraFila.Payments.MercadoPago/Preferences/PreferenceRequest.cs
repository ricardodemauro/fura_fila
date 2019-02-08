using FuraFila.Payments.MercadoPago.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.MercadoPago.Preferences
{
    public class PreferenceRequest
    {
        public List<Item> Items { get; set; }

        public Payer Payer { get; set; }

        [JsonProperty(PropertyName = "payment_methods")]
        public PaymentMethods PaymentMethods { get; set; }

        [JsonProperty(PropertyName = "back_urls")]
        public BackUrls BackUrls { get; set; }

        [JsonProperty(PropertyName = "notification_url")]
        public string NotificationUrl { get; set; }

        [JsonProperty(PropertyName = "auto_return")]
        public string AutoReturn { get; set; }
    }
}
