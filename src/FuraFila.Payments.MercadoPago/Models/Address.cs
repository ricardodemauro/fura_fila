using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.MercadoPago.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "street_name")]
        public string StreetName { get; set; }

        [JsonProperty(PropertyName = "street_number")]
        public object StreetNumber { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }
    }
}
