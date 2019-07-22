using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardInterestApp01.Core.Interfaces
{
    public interface IPerson: ICalculateInterest
    {
        int ID { get; set; }
        List<IWallet> Wallets { get; set; }
    }
}
