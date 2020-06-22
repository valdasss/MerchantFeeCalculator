using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantFeeCalculator.Domain.Models
{
    public class MerchantFactory
    {
        private static IDictionary<string, decimal> _bigMerchantDiscount = new Dictionary<string, decimal>() { { "TELIA", 0.1M }, { "CIRCLE_K", 0.2M } };

        public Merchant CreateMerchant(string merchantName, Transaction transaction, decimal transactionCommision, decimal invoiceFee)
        {
            if (IsMerchantStandart(merchantName))
                return new StandartMerchant(merchantName, transaction, transactionCommision, invoiceFee);
            else
                return new BigMerchant(merchantName, transaction, transactionCommision, _bigMerchantDiscount[merchantName], invoiceFee);
        }

        private bool IsMerchantStandart(string merchantName)
        {
            return !_bigMerchantDiscount.ContainsKey(merchantName);
        }
    }
}
