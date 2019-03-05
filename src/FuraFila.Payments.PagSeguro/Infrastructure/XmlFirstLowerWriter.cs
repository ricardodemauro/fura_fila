using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FuraFila.Payments.PagSeguro.Infrastructure
{
    /// <summary>
    /// Implements an <see cref="XmlWriter"/> that turns the 
    /// first letter of outgoing elements and attributes into lowercase.
    /// </summary>
    /// <remarks>
    /// To be used in conjunction with <see cref="XmlFirstUpperReader"/>.
    /// <para>Author: Daniel Cazzulino, kzu@aspnet2.com</para>
    /// See http://weblogs.asp.net/cazzu/archive/2004/05/10/129106.aspx.
    /// </remarks>
    public class XmlFirstLowerWriter : XmlTextWriter
    {
        #region Fields & Ctor

        /// <summary>
        /// See <see cref="XmlTextWriter"/> ctors.
        /// </summary>
        public XmlFirstLowerWriter(TextWriter w, Encoding encoding) : base(w)
        {
            _settings.Encoding = encoding;
        }

        /// <summary>
        /// See <see cref="XmlTextWriter"/> ctors.
        /// </summary>
        public XmlFirstLowerWriter(Stream w, Encoding encoding) : base(w, encoding)
        {
            _settings.Encoding = encoding;
        }

        /// <summary>
        /// See <see cref="XmlTextWriter"/> ctors.
        /// </summary>
        public XmlFirstLowerWriter(string filename, Encoding encoding) : base(filename, encoding)
        {
            _settings.Encoding = encoding;
        }

        #endregion Fields & Ctor

        #region MakeFirstLower

        private readonly XmlWriterSettings _settings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true,
            Encoding = Encoding.UTF8,
        };

        public override XmlWriterSettings Settings => _settings;

        internal static string MakeFirstLower(string name)
        {
            // Don't process empty strings.
            if (name.Length == 0) return name;

            // If the first is already lower, don't process.
            if (char.IsLower(name[0])) return name;

            // If there's just one char, make it lower directly.
            if (name.Length == 1) return name.ToLower(System.Globalization.CultureInfo.CurrentCulture);

            // Finally, modify and create a string. 
            char[] letters = name.ToCharArray();
            letters[0] = char.ToLower(letters[0], System.Globalization.CultureInfo.CurrentCulture);
            return new string(letters);
        }

        #endregion MakeFirstUpper

        #region Methods

        /// <summary>
        /// See <see cref="XmlWriter.WriteQualifiedName"/>.
        /// </summary>
        public override void WriteQualifiedName(string localName, string ns)
        {
            base.WriteQualifiedName(MakeFirstLower(localName), ns);
        }

        /// <summary>
        /// See <see cref="XmlWriter.WriteStartAttribute"/>.
        /// </summary>
        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            base.WriteStartAttribute(prefix, MakeFirstLower(localName), ns);
        }


        /// <summary>
        /// See <see cref="XmlWriter.WriteStartElement"/>.
        /// </summary>
        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            base.WriteStartElement(prefix, MakeFirstLower(localName), ns);
        }

        public override void WriteStartDocument()
        {
            //base.WriteStartDocument();
        }

        public override void WriteStartDocument(bool standalone)
        {
            //base.WriteStartDocument(standalone);
        }

        public override Task WriteStartDocumentAsync(bool standalone)
        {
            return Task.CompletedTask;
            //return base.WriteStartDocumentAsync(standalone);
        }

        public override Task WriteStartDocumentAsync()
        {
            return Task.CompletedTask;
            //return base.WriteStartDocumentAsync();
        }

        public override void WriteString(string text)
        {
            base.WriteString(text);
        }

        public override void WriteValue(object value)
        {
            base.WriteValue(value);
        }

        public override void WriteValue(string value)
        {
            base.WriteValue(value);
        }

        public override void WriteValue(decimal value)
        {
            string decValue = value.ToString("N", CultureInfo.InvariantCulture);
            this.WriteString(decValue);
        }

        public override void WriteValue(float value)
        {
            base.WriteValue(value);
        }


        #endregion Methods
    }
}
