using System;
using NUnit.Framework;
using MerchantFeeCalculator.Domain.Models;

namespace MerchantFeeCalculator.Tests
{
    /// <summary>
    /// Summary description for MerchantTests
    /// </summary>
    [TestFixture]
    public class MerchantTests
    {
        [TestCase(200, 0.01, 2.00)]
        [TestCase(110, 0.01, 1.10)]
        [TestCase(110, 0.99, 108.9)]
        public void Standart_Merchant_Calculate_Fee(decimal transactionAmount, decimal transactionCommsion, decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);
            var merchant = new StandartMerchant("TestMerchant", transaction, transactionCommsion);

            merchant.CalculateMerchantFee();

            Assert.AreEqual(merchant.MerchantFee, expectedRezult);
        }
    }
}
