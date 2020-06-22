using MerchantFeeCalculator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantFeeCalculator
{
    class Program
    {
        const string TRANSACTION_FILE_PATH = "/AppData/transactions.txt";
        const decimal TRANSACTION_COMMISION = 0.01m;
        const decimal INVOICE_FEE = 29m;

        static void Main(string[] args)
        {
            var filePath = GetTransactionFilePath();
            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                var merchantFactory = new MerchantFactory();
                var merchants = new Dictionary<string, Merchant>();
                while ((line = file.ReadLine()) != null)
                {
                    var words = line.Split(' ');
                    var date = words[0];
                    var transactionDate = DateTime.Parse(words[0]);
                    var amount = decimal.Parse(words[2]);
                    var transaction = new Transaction(transactionDate, amount);

                    var merchantName = words[1];
                    if (merchants.ContainsKey(merchantName))
                        merchants[merchantName].AddTransaction(transaction);
                    else
                    {
                        var merchant = merchantFactory.CreateMerchant(merchantName, transaction, TRANSACTION_COMMISION,INVOICE_FEE);
                        merchants.Add(merchantName, merchant);
                    }
                    merchants[merchantName].CalculateMerchantFee();
                    Console.WriteLine(merchants[merchantName].GetMerchantFeeInformation());
                }
            };

        }
        private static string GetTransactionFilePath()
        {
            return Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + TRANSACTION_FILE_PATH;
        }     
    }
}
