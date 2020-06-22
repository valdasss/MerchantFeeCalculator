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
            var standartMerchant = new StandartMerchant("TestMerchant", transaction, transactionCommsion);

            standartMerchant.CalculateMerchantFee();

            Assert.AreEqual(standartMerchant.MerchantFee, expectedRezult);
        }
        [TestCase("TELIA",200, 0.01)]
        public void Merchant_Factory_Create_Big_Merchant(string merchantName,decimal transactionAmount, decimal transactionCommsion)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);
            var merchantFactory = new MerchantFactory();

            var merchant = merchantFactory.CreateMerchant(merchantName, transaction, transactionCommsion);

            Assert.AreEqual(merchant.GetType(), typeof(BigMerchant));
        }
        [TestCase("7ELEVEN", 200, 0.01)]
        public void Merchant_Factory_Create_Standart_Merchant(string merchantName, decimal transactionAmount, decimal transactionCommsion)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);
            var merchantFactory = new MerchantFactory();

            var merchant = merchantFactory.CreateMerchant(merchantName, transaction, transactionCommsion);

            Assert.AreEqual(merchant.GetType(), typeof(StandartMerchant));
        }
        [TestCase(200, 0.01,0.1, 1.80)]
        [TestCase(110, 0.01,0.1, 0.99)]
        [TestCase(110, 0.99,0.2, 87.12)]
        public void Big_Merchant_Calculate_Fee(decimal transactionAmount, decimal transactionCommsion,decimal transactionCommisionDiscount, decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);
            var merchant = new BigMerchant("TestMerchant", transaction, transactionCommsion, transactionCommisionDiscount);

            merchant.CalculateMerchantFee();

            Assert.AreEqual(merchant.MerchantFee, expectedRezult);
        }

        [TestCase(200, 0.01, 0.0)]
        [TestCase(110, 0.01, -0.1)]
        public void Big_Merchant_Invalid_Commision_discount(decimal transactionAmount, decimal transactionCommsion, decimal transactionCommisionDiscount)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);

            Assert.Throws<ArgumentException>(() => new BigMerchant("TestMerchant", transaction, transactionCommsion, transactionCommisionDiscount));           
        }
        [TestCase(200, 0.0)]
        [TestCase(110, -0.1)]
        public void Standart_Merchant_Invalid_Commision_discount(decimal transactionAmount, decimal transactionCommsion)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);

            Assert.Throws<ArgumentException>(() => new StandartMerchant("TestMerchant", transaction, transactionCommsion));
        }
    }
}
