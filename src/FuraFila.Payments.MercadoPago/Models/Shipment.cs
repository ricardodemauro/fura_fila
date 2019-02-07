using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.MercadoPago.Models
{
    public class Shipment
    {
        [JsonProperty(PropertyName = "receiver_address")]
        public Address ReceiverAddress { get; set; }
    }
}
