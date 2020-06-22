

namespace MerchantFeeCalculator.Domain.Models
{
    public class StandartMerchant : Merchant
    {

        public StandartMerchant(string merchantName, Transaction transaction,decimal transactionCommision) : base(merchantName, transaction, transactionCommision)
        { }

        public override void CalculateMerchantFee()
        {
            MerchantFee = Transaction.CalculateTransactionFee(TransactionCommision);
        }
    }
}
