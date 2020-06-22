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
        [TestCase(200, 0.01, 10, 2.00)]
        [TestCase(110, 0.01, 10, 1.10)]
        [TestCase(110, 0.99, 10, 108.9)]
        public void Standart_Merchant_Calculate_Fee_Not_First_Monthly_Operation(decimal transactionAmount, decimal transactionCommsion, decimal invoiceFee, decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Parse("2020-06-01"), transactionAmount);
            var nextTransaction = new Transaction(DateTime.Parse("2020-06-02"), transactionAmount);
            var standartMerchant = new StandartMerchant("TestMerchant", transaction, transactionCommsion, invoiceFee);
            standartMerchant.CalculateMerchantFee();
            standartMerchant.AddTransaction(nextTransaction);

            standartMerchant.CalculateMerchantFee();

            Assert.AreEqual(expectedRezult, standartMerchant.MerchantFee);
        }
        [TestCase("TELIA", 200, 0.01, 10)]
        public void Merchant_Factory_Create_Big_Merchant(string merchantName, decimal transactionAmount, decimal transactionCommsion, decimal invoiceFee)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);
            var merchantFactory = new MerchantFactory();

            var merchant = merchantFactory.CreateMerchant(merchantName, transaction, transactionCommsion, invoiceFee);

            Assert.AreEqual(typeof(BigMerchant), merchant.GetType());
        }
        [TestCase("7ELEVEN", 200, 0.01, 10)]
        public void Merchant_Factory_Create_Standart_Merchant(string merchantName, decimal transactionAmount, decimal transactionCommsion, decimal invoiceFee)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);
            var merchantFactory = new MerchantFactory();

            var merchant = merchantFactory.CreateMerchant(merchantName, transaction, transactionCommsion, invoiceFee);

            Assert.AreEqual(typeof(StandartMerchant),merchant.GetType());
        }
        [TestCase(200, 0.01, 0.1, 10, 1.80)]
        [TestCase(110, 0.01, 0.1, 10, 0.99)]
        [TestCase(110, 0.99, 0.2, 10, 87.12)]
        public void Big_Merchant_Calculate_Fee_Not_First_Monthly_Operation(decimal transactionAmount, decimal transactionCommsion, decimal transactionCommisionDiscount, decimal invoiceFee, decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Parse("2020-06-01"), transactionAmount);
            var nextTransaction = new Transaction(DateTime.Parse("2020-06-02"), transactionAmount);
            var merchant = new BigMerchant("TestMerchant", transaction, transactionCommsion, transactionCommisionDiscount, invoiceFee);
            merchant.CalculateMerchantFee();
            merchant.AddTransaction(nextTransaction);

            merchant.CalculateMerchantFee();

            Assert.AreEqual(expectedRezult,merchant.MerchantFee);
        }

        [TestCase(200, 0.01, 0.0, 10)]
        [TestCase(110, 0.01, -0.1, 10)]
        public void Big_Merchant_Invalid_Commision_discount(decimal transactionAmount, decimal transactionCommsion, decimal transactionCommisionDiscount, decimal invoiceFee)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);

            Assert.Throws<ArgumentException>(() => new BigMerchant("TestMerchant", transaction, transactionCommsion, transactionCommisionDiscount, invoiceFee));
        }
        [TestCase(200, 0.0, 10)]
        [TestCase(110, -0.1, 10)]
        public void Standart_Merchant_Invalid_Commision_discount(decimal transactionAmount, decimal transactionCommsion, decimal invoiceFee)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);

            Assert.Throws<ArgumentException>(() => new StandartMerchant("TestMerchant", transaction, transactionCommsion, invoiceFee));
        }
        [TestCase(200, 0.1, 0)]
        [TestCase(110, 0.1, -0.01)]
        public void Standart_Merchant_Invalid_Incoive_fee(decimal transactionAmount, decimal transactionCommsion, decimal invoiceFee)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);

            Assert.Throws<ArgumentException>(() => new StandartMerchant("TestMerchant", transaction, transactionCommsion, invoiceFee));
        }
        [TestCase(200, 0.1, 0)]
        [TestCase(110, 0.1, -0.01)]
        public void Big_Merchant_Invalid_Incoive_fee(decimal transactionAmount, decimal transactionCommsion, decimal invoiceFee)
        {
            var transaction = new Transaction(DateTime.Now, transactionAmount);

            Assert.Throws<ArgumentException>(() => new StandartMerchant("TestMerchant", transaction, transactionCommsion, invoiceFee));
        }
        [TestCase(200, 0.01, 10, 12.00)]
        [TestCase(110, 0.01, 10, 11.10)]
        [TestCase(110, 0.99, 10, 118.9)]
        public void Standart_Merchant_Calculate_Fee_First_Monthly_Operation(decimal transactionAmount, decimal transactionCommsion, decimal invoiceFee, decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Parse("2020-06-01"), transactionAmount);          
            var standartMerchant = new StandartMerchant("TestMerchant", transaction, transactionCommsion, invoiceFee);
            standartMerchant.AddTransaction(transaction);

            standartMerchant.CalculateMerchantFee();

            Assert.AreEqual(expectedRezult, standartMerchant.MerchantFee);
        }
        [TestCase(200, 0.01, 0.1, 10, 11.80)]
        [TestCase(110, 0.01, 0.1, 10, 10.99)]
        [TestCase(110, 0.99, 0.2, 10, 97.12)]
        public void Big_Merchant_Calculate_Fee_First_Monthly_Operation(decimal transactionAmount, decimal transactionCommsion, decimal transactionCommisionDiscount, decimal invoiceFee, decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Parse("2020-06-01"), transactionAmount);         
            var merchant = new BigMerchant("TestMerchant", transaction, transactionCommsion, transactionCommisionDiscount, invoiceFee);
            merchant.AddTransaction(transaction);

            merchant.CalculateMerchantFee();

            Assert.AreEqual(expectedRezult, merchant.MerchantFee);
        }
        [TestCase(0.2, 0.01, 0.1, 10, 0)]
        [TestCase(0.1, 0.01, 0.1, 10, 0d)]
        public void Big_Merchant_Calculate_Fee_First_Monthly_Operation_Transaction_fee_too_low(decimal transactionAmount, decimal transactionCommsion, decimal transactionCommisionDiscount, decimal invoiceFee, decimal expectedRezult)
        {
            var transaction = new Transaction(DateTime.Parse("2020-06-01"), transactionAmount);
            var merchant = new BigMerchant("TestMerchant", transaction, transactionCommsion, transactionCommisionDiscount, invoiceFee);
            merchant.AddTransaction(transaction);

            merchant.CalculateMerchantFee();

            Assert.AreEqual(expectedRezult, merchant.MerchantFee);
        }
    }
}
