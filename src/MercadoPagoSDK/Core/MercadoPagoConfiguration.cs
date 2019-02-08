using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPagoSDK.Core
{
    public class MercadoPagoConfiguration
    {
        public Dictionary<string, string> Settings { get; set; }

        public string ClientSecret { get { return Get(); } set { Set(value); } }

        public string ClientId { get { return Get(); } set { Set(value); } }

        public string AccessToken { get { return Get(); } set { Set(value); } }

        public string AppId { get { return Get(); } set { Set(value); } }

        private string Get([CallerMemberName] string keyName = null)
        {
            return Settings[keyName];
        }

        private void Set(string value, [CallerMemberName] string keyName = null)
        {
            Settings.Add(keyName, value);
        }
    }
}
