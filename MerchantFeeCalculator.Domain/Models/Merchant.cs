using System;

namespace MerchantFeeCalculator.Domain.Models
{
    public abstract class Merchant
    {
        private decimal merchantFee;
        public decimal MerchantFee { get { return Math.Round(this.merchantFee, 2); } protected set { this.merchantFee = value; } }
        public string MerchantName { get; }
        public Transaction Transaction { get; private set; }

       
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
            return $"{Transaction.Date:yyyy-MM-dd} {MerchantName} {string.Format("{0:0.00}", MerchantFee)}";
        }

    }
}
