using CreditCardInterestApp01.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Interfaces
{
    public interface IWallet: ICalculateInterest
    {
        int ID { get; set; }
        List<Card> Cards { get; set; }
    }
}
