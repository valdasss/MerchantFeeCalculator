using System;
using MerchantFeeCalculator.Domain.Models;
using NUnit.Framework;

namespace MerchantFeeCalculator.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [TestCase(200,0.01,2.00)]
        [TestCase(110,0.01,1.10)]
        [TestCase(110, 0.99, 108.9)]
        public void Calculate_Transaction_Fee(decimal transactionAmount,decimal transactionCommision,decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);

            var transactionFee = transaction.CalculateTransactionFee(transactionCommision);

            Assert.AreEqual(transactionFee, expectedRezult);
        }
    }
}
