using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantFeeCalculator.Domain.Models
{
    public class BigMerchant : StandartMerchant
    {
        private readonly decimal _transactionCommisionDiscount;

        public BigMerchant(string merchantName, Transaction transaction, decimal transactionCommision, decimal tranasactionCommisionDiscount) : base(merchantName, transaction, transactionCommision)
        {
            if (tranasactionCommisionDiscount <= 0)
                throw new ArgumentException();
            _transactionCommisionDiscount = tranasactionCommisionDiscount;
        }

        public override void CalculateMerchantFee()
        {
            base.CalculateMerchantFee();
            MerchantFee = MerchantFee * (1 - _transactionCommisionDiscount);
        }
    }
}
