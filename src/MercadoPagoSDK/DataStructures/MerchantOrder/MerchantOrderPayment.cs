﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MercadoPago.Common;

namespace MercadoPago.DataStructures.MerchantOrder
{
    public struct MerchantOrderPayment
    {
        #region Properties

        private string id;
        private float transactionAmount;
        private float totalPaidAmount;
        private float shippingCost;
       
        [StringLength(3)]
        private CurrencyId currencyId;
        private string status;
        private string statusDetail;
        public enum OperationType
        {
            RegularPayment,
            PaymentAddition
        }
        private OperationType operationType;
        private DateTime dateApproved;
        private DateTime dateCreated;
        private DateTime lastModified;
        private float amountRefunded;
        
        #endregion

        #region Accessors

        public string ID
        {
            get { return id; }            
        }
       
        public float TransactionAmount
        {
            get { return transactionAmount; }            
        }
       
        public float TotalPaidAmount
        {
            get { return totalPaidAmount; }            
        }
       
        public float ShippingCost
        {
            get { return shippingCost; }            
        }
             
        public CurrencyId PaymentCurrencyId
        {
            get { return currencyId; }            
        }
       
        public string Status
        {
            get { return status; }            
        }
       
        public string StatusDetail
        {
            get { return statusDetail; }            
        }

        public OperationType PaymentOperationType
        {
            get { return operationType; }            
        }
        
        public DateTime DateApproved
        {
            get { return dateApproved; }            
        }
       
        public DateTime DateCreated
        {
            get { return dateCreated; }            
        }        

        public DateTime LastModified
        {
            get { return lastModified; }            
        }
       
        public float AmountRefunded
        {
            get { return amountRefunded; }            
        }

        #endregion
    }
}
