using CreditCardInterestApp01.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Interfaces
{
    public interface ICard
    {
        int ID { get; set; }
        decimal Balance { get; set; }
        double InterestRate { get; set; }
        CardType TypeCard { get; set; }
    }
}
