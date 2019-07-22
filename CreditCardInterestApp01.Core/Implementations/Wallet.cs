using CreditCardInterestApp01.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Implementations
{
    public class Wallet : IWallet
    {
        public int ID { get; set; }
        public List<Card> Cards { get; set; }

        public decimal CalculateInterest()
        {
            decimal total = 0.0m;
            foreach (var card in Cards)
            {
                total += card.CalculateInterest();
            }
            return total;
        }
    }
}
