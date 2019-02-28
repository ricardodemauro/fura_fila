using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [DataContract]
    public partial class Checkout
    {
        [DataMember]
        /// <remarks/>
        public checkoutSender Sender { get; set; }

        [DataMember]
        /// <remarks/>
        public string Currency { get; set; }

        [DataMember]
        /// <remarks/>
        public CheckoutItem[] Items { get; set; }

        [DataMember]
        /// <remarks/>
        public string RedirectURL { get; set; }

        /// <remarks/>
        public decimal extraAmount { get; set; }

        /// <remarks/>
        public string reference { get; set; }

        /// <remarks/>
        public checkoutShipping shipping { get; set; }

        /// <remarks/>
        public byte timeout { get; set; }

        /// <remarks/>
        public uint maxAge { get; set; }

        /// <remarks/>
        public ushort maxUses { get; set; }

        /// <remarks/>
        public checkoutReceiver receiver { get; set; }

        /// <remarks/>
        public bool enableRecover { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class checkoutSender
    {
        /// <remarks/>
        public string name { get; set; }

        /// <remarks/>
        public string email { get; set; }

        /// <remarks/>
        public checkoutSenderPhone phone { get; set; }

        /// <remarks/>
        public checkoutSenderDocuments documents { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class checkoutSenderPhone
    {

        /// <remarks/>
        public byte areaCode { get; set; }

        /// <remarks/>
        public uint number { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class checkoutSenderDocuments
    {

        /// <remarks/>
        public checkoutSenderDocumentsDocument document { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class checkoutSenderDocumentsDocument
    {

        /// <remarks/>
        public string type { get; set; }

        /// <remarks/>
        public ulong value { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [DataContract]
    public partial class CheckoutItem
    {
        [DataMember]
        /// <remarks/>
        public int Id { get; set; }

        [DataMember]
        /// <remarks/>
        public string Description { get; set; }

        [DataMember]
        /// <remarks/>
        public decimal Amount { get; set; }

        [DataMember]
        /// <remarks/>
        public int Quantity { get; set; }

        [DataMember]
        /// <remarks/>
        public int Weight { get; set; }

        [DataMember]
        /// <remarks/>
        public decimal ShippingCost { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class checkoutShipping
    {

        private checkoutShippingAddress addressField;

        private byte typeField;

        private decimal costField;

        private bool addressRequiredField;

        /// <remarks/>
        public checkoutShippingAddress address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public byte type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public decimal cost
        {
            get
            {
                return this.costField;
            }
            set
            {
                this.costField = value;
            }
        }

        /// <remarks/>
        public bool addressRequired
        {
            get
            {
                return this.addressRequiredField;
            }
            set
            {
                this.addressRequiredField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class checkoutShippingAddress
    {

        private string streetField;

        private ushort numberField;

        private string complementField;

        private string districtField;

        private string cityField;

        private string stateField;

        private string countryField;

        private uint postalCodeField;

        /// <remarks/>
        public string street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        public ushort number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        public string complement
        {
            get
            {
                return this.complementField;
            }
            set
            {
                this.complementField = value;
            }
        }

        /// <remarks/>
        public string district
        {
            get
            {
                return this.districtField;
            }
            set
            {
                this.districtField = value;
            }
        }

        /// <remarks/>
        public string city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public uint postalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class checkoutReceiver
    {

        private string emailField;

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }


}
