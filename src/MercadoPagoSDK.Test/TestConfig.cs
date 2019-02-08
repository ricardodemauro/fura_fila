using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPagoSDK.Test
{
    public static class TestConfig
    {
        static readonly Dictionary<string, string> d = new Dictionary<string, string>
        {
            { "CLIENT_SECRET", "8isTmaR10ZfTcaAMRE5LzWupEcQendqS" },
            { "CLIENT_ID", "6509685436439" },
            { "ACCESS_TOKEN", "TEST-6509685436439-101114-cfecc0a08312711aaf5d041d54af6982__LB_LC__-194008091" },
            { "APP_ID", "TEST-022b1376-c665-4906-9004-1e056f761084" }
        };

        public static string GetConfig(string key)
        {
            return d[key];
        }

        public static string GetEnvironmentVariable(string key)
        {
            return GetConfig(key);
        }
    }
}
