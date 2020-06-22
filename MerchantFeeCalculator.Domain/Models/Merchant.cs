﻿using System;

namespace MerchantFeeCalculator.Domain.Models
{
    public abstract class Merchant
    {

        public string MerchantName { get; }
        public Transaction Transaction { get; private set; }
        public decimal MerchantFee { get; protected set; }
        protected Merchant(string merchantName, Transaction transaction)
        {
            MerchantName = merchantName;
            Transaction = transaction;
        }
        public virtual void AddTransaction(Transaction transaction)
        {
            Transaction = transaction;
        }
        public abstract void CalculateMerchantFee();
        public virtual string GetMerchantFeeInformation()
        {
            return $"{Transaction.Date:yyyy-MM-dd} {MerchantName} {string.Format("{0:0.00}", Math.Round(MerchantFee, 2))}";
        }

    }
}
