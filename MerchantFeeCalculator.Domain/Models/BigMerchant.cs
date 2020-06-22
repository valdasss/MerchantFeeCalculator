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

        public BigMerchant(string merchantName, Transaction transaction, decimal transactionCommision, decimal tranasactionCommisionDiscount,
            decimal invoiceFee) : base(merchantName, transaction, transactionCommision, invoiceFee)
        {
            if (tranasactionCommisionDiscount <= 0)
                throw new ArgumentException();
            _transactionCommisionDiscount = tranasactionCommisionDiscount;
        }

        public override void AddInvoinceFee()
        {
            MerchantFee = MerchantFee * (1 - _transactionCommisionDiscount);
            if (MerchantFee > 0M)
                base.AddInvoinceFee();
        }
        public override void CalculateMerchantFee()
        {
            base.CalculateMerchantFee();

        }
    }
}
