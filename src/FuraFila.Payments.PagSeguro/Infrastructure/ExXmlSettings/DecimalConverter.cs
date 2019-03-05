using ExtendedXmlSerializer.ContentModel.Conversion;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FuraFila.Payments.PagSeguro.Infrastructure.ExXmlSettings
{
    public class DecimalConverter : IConverter<decimal>
    {
        private readonly IFormatProvider _format = CultureInfo.InvariantCulture;

        public static DecimalConverter Default { get; } = new DecimalConverter();

        public DecimalConverter() { }

        public string Format(decimal instance)
        {
            string v = instance.ToString("N", _format);
            return v;
        }

        public bool IsSatisfiedBy(TypeInfo parameter)
        {
            return typeof(decimal).GetTypeInfo().IsAssignableFrom(parameter);
        }

        public decimal Parse(string data)
        {
            return decimal.Parse(data, _format);
        }
    }
}
