﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Models
{
    [Serializable()]
    [DataContract]
    public enum ShippingType
    {
        [DataMember]
        PAC = 1,
        [DataMember]
        SEDEX = 2,
        [DataMember]
        NaoEspecificado = 3
    }
}
