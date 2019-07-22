using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardInterestApp01.Core.Enums;
using CreditCardInterestApp01.Core.Interfaces;

namespace CreditCardInterestApp01.Core.Implementations
{
    public class VisaCard : Card
    {
        public VisaCard(int id, decimal balance, IInterestCalculationLogic interestCalculationLogic) 
            : base(id, balance, 0.10, CardType.Visa, interestCalculationLogic)
        {
        }
    }
}
