using System;

namespace MerchantFeeCalculator.Domain.Models
{
    public class Transaction 
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public Transaction(DateTime transactionDate, decimal transactionAmount)
        {
            Date = transactionDate;
            Amount = transactionAmount;
        }

        public decimal CalculateTransactionFee(decimal commision)
        {
            return Amount * commision;
        }
    }
}
