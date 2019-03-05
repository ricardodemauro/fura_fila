using ExtendedXmlSerializer.ContentModel.Conversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Infrastructure.ExXmlSettings
{
    public class DateTimeConverter : IConverter<DateTime>
    {
        private readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        public static DateTimeConverter Default { get; } = new DateTimeConverter();

        private DateTimeConverter() { }

        public string Format(DateTime instance)
        {
            return instance.ToString("dd/MM/yyyy", formatProvider);
        }

        public bool IsSatisfiedBy(TypeInfo parameter)
        {
            return typeof(DateTime).GetTypeInfo().IsAssignableFrom(parameter);
        }

        public DateTime Parse(string data)
        {
            DateTime.TryParseExact(data, "dd/MM/yyyy", formatProvider, DateTimeStyles.AssumeUniversal, out DateTime dt);
            return dt;
        }
    }
}
