

using System;

namespace MerchantFeeCalculator.Domain.Models
{
    public class StandartMerchant : Merchant
    {
        private decimal _transactionCommision;

        public StandartMerchant(string merchantName, Transaction transaction, decimal transactionCommision) : base(merchantName, transaction)
        {
            if (transactionCommision <= 0m)
                throw new ArgumentException();
            _transactionCommision = transactionCommision;
        }

        public override void CalculateMerchantFee()
        {
            MerchantFee = Transaction.CalculateTransactionFee(_transactionCommision);
        }
    }
}
