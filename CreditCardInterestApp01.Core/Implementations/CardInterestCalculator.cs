using CreditCardInterestApp01.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Implementations
{
    public class CardInterestCalculator
    {
        public List<IPerson> PersonSet { get; set; }

        public Dictionary<int, decimal> GetPersonInterestTotal()
        {
            return this.PersonSet.ToDictionary(p => p.ID, p => p.CalculateInterest());
        }

        public Dictionary<Tuple<int, int>, decimal> GetPersonPerWalletInterestTotal()
        {
            Dictionary<Tuple<int, int>, decimal> result = new Dictionary<Tuple<int, int>, decimal>();
            if (this.PersonSet == null || this.PersonSet.Count == 0)
                return result;

            foreach (var person in PersonSet)
            {
                foreach(var wallet in person.Wallets)
                {
                    result.Add(new Tuple<int, int>(person.ID, wallet.ID), wallet.CalculateInterest());
                }
            }
            return result;
        }

        public Dictionary<Tuple<int, int, int>, decimal> GetPersonPerWalletPerCardInterestTotal()
        {
            Dictionary<Tuple<int, int, int>, decimal> result = new Dictionary<Tuple<int, int, int>, decimal>();
            if (this.PersonSet == null || this.PersonSet.Count == 0)
                return result;

            foreach (var person in PersonSet)
            {
                foreach (var wallet in person.Wallets)
                {
                    foreach (var card in wallet.Cards)
                    {
                        result.Add(new Tuple<int, int, int>(person.ID, wallet.ID, card.ID), card.CalculateInterest());
                    }
                }
            }
            return result;
        }




    }
}
