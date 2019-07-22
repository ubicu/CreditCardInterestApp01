using CreditCardInterestApp01.Core.Enums;
using CreditCardInterestApp01.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Implementations
{
    public abstract class Card : ICard, ICalculateInterest
    {
        public int ID { get; set; }
        public decimal Balance { get; set; }
        public double InterestRate { get; set; }
        public CardType TypeCard { get; set; }

        private IInterestCalculationLogic interestCalculationLogic;

        public Card(int id, decimal balance, double interestRate, CardType cardType, IInterestCalculationLogic interestCalculationLogic)
        {
            this.ID = id;
            this.Balance = balance;
            this.InterestRate = interestRate;
            this.TypeCard = cardType;
            this.interestCalculationLogic = interestCalculationLogic;
        }

        public virtual decimal CalculateInterest()
        {
            return this.interestCalculationLogic.GetInterest(this.Balance, this.InterestRate);
        }
    }
}
