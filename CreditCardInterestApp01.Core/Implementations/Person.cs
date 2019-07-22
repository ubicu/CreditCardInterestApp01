using CreditCardInterestApp01.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Implementations
{
    public class Person : IPerson
    {
        public int ID { get; set; }
        public IEnumerable<IWallet> Wallets { get; set; }
        public decimal CalculateInterest()
        {
            decimal total = 0.0m;
            foreach (var wallet in Wallets)
            {
                total += wallet.CalculateInterest();
            }
            return total;
        }
    }
}
