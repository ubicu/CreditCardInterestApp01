using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Interfaces
{
    public interface IInterestCalculationLogic
    {
        decimal GetInterest(decimal balance, double interestRate);
    }
}
