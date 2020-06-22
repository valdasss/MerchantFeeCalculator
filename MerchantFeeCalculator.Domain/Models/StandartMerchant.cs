

using MerchantFeeCalculator.Domain.Contracts;
using System;

namespace MerchantFeeCalculator.Domain.Models
{
    public class StandartMerchant : Merchant, IInvoiceFee
    {
        private decimal _transactionCommision;
        private decimal _InvoiceFee;
        private DateTime _lastInvoiceFeeDate;

        public StandartMerchant(string merchantName, Transaction transaction, decimal transactionCommision, decimal invoiceFee) : base(merchantName, transaction)
        {
            if (transactionCommision <= 0m)
                throw new ArgumentException();
            if (invoiceFee <= 0m)
                throw new ArgumentException();
            _transactionCommision = transactionCommision;
            _InvoiceFee = invoiceFee;
        }

        public virtual void AddInvoinceFee()
        {
            if (CanAddInvoiceFee())
            {
                MerchantFee = MerchantFee + _InvoiceFee;
                _lastInvoiceFeeDate = Transaction.Date;
            }
        }

        public override void CalculateMerchantFee()
        {
            MerchantFee = Transaction.CalculateTransactionFee(_transactionCommision);
            AddInvoinceFee();
        }

        private bool CanAddInvoiceFee()
        {
            if (_lastInvoiceFeeDate == null)
                return true;
            else if ((_lastInvoiceFeeDate.Year == Transaction.Date.Year && _lastInvoiceFeeDate.Month < Transaction.Date.Month) ||
                _lastInvoiceFeeDate.Year < Transaction.Date.Year)
                return true;
            return false;
        }
    }
}
