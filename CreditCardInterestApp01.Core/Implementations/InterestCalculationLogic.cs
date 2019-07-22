using CreditCardInterestApp01.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Implementations
{
    public class InterestCalculationLogic : IInterestCalculationLogic
    {
        public decimal GetInterest(decimal balance, double interestRate)
        {
            return Decimal.Multiply(balance, Convert.ToDecimal(interestRate));
        }
    }
}
