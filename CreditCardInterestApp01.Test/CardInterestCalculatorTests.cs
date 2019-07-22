using System;
using System.Collections.Generic;
using System.Linq;
using CreditCardInterestApp01.Core.Implementations;
using CreditCardInterestApp01.Core.Interfaces;
using CreditCardInterestApp01.Core.Interfaces.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreditCardInterestApp01.Test
{
    [TestClass]
    public class CardInterestCalculatorTests
    {
        private IInterestCalculationLogic calculationLogic;

        [TestInitialize]
        public void TestPreprocessing()
        {
            this.calculationLogic = new StubIInterestCalculationLogic()
            {
                GetInterestDecimalDouble = (balance, interestRate) => Decimal.Multiply(balance, Convert.ToDecimal(interestRate))
            };
        }


        [TestMethod]
        public void CalculateInterest_1Person_1Wallet_3Cards_100DollarBalanceEachCard_InterestIs16Dollars()
        {
            var person = new Person
            {
                ID = 1,
                Wallets = new List<IWallet> {
                            new Wallet { ID = 1, Cards = new List<Card> {
                                new VisaCard(1, 100.00m, calculationLogic),
                                new MasterCard(2, 100.00m, calculationLogic),
                                new DiscoverCard(3, 100.00m, calculationLogic)
                                        }
                            }
                }
            };

            var cardInterestCalculator = new CardInterestCalculator {
                                            PersonSet = new List<IPerson> { person }
                                         };

            var personInterestTotal = cardInterestCalculator.GetPersonInterestTotal();
            var personWalletCardInterestTotal = cardInterestCalculator.GetPersonPerWalletPerCardInterestTotal();

            decimal expectedPersonInterestTotal = 16.00m;
            Assert.AreEqual(expectedPersonInterestTotal, personInterestTotal[1], $"For Person (ID = {person.ID}), " +
                $"Expected person total: {expectedPersonInterestTotal}, " +
                $"Actual person total: {personInterestTotal[1]}");

            var expectedPersonPerWalletPerCardInterestTotal =
                new Dictionary<Tuple<int, int, int>, decimal>
                {
                    { new Tuple<int,int,int>(1,1,1), 10.00m },
                    { new Tuple<int,int,int>(1,1,2),  5.00m },
                    { new Tuple<int,int,int>(1,1,3),  1.00m }
                };
            foreach (var total in expectedPersonPerWalletPerCardInterestTotal)
            {
                Assert.AreEqual(total.Value, personWalletCardInterestTotal[total.Key], $"For (PersonID, WalletID, CardID) = ({total.Key}), " +
                $"Expected card total: {total.Value}, " +
                $"Actual card total: {personWalletCardInterestTotal[total.Key]}");
            }

        }

        [TestMethod]
        public void CalculateInterest_1Person_2Wallet_Wallet1HasVisaAndDiscover_Wallet2HasMasterCard_100DollarBalanceEachCard()
        {
            var person = new Person
            {
                ID = 1,
                Wallets = new List<IWallet> {
                            new Wallet { ID = 1, Cards = new List<Card> {
                                new VisaCard(1, 100.00m, calculationLogic),
                                new DiscoverCard(2, 100.00m, calculationLogic)
                                        }
                            },
                            new Wallet { ID = 2, Cards = new List<Card> {
                                new MasterCard(1, 100.00m, calculationLogic)
                                        }
                            }
                }
            };

            var cardInterestCalculator = new CardInterestCalculator
            {
                PersonSet = new List<IPerson> { person }
            };

            var personInterestTotal = cardInterestCalculator.GetPersonInterestTotal();
            var personWalletInterestTotal = cardInterestCalculator.GetPersonPerWalletInterestTotal();

            decimal expectedPersonInterestTotal = 16.00m;
            Assert.AreEqual(expectedPersonInterestTotal, personInterestTotal[1], $"For Person (ID = {person.ID}), " +
                $"Expected person total: {expectedPersonInterestTotal}, " +
                $"Actual person total: {personInterestTotal[1]}");

            var expectedPersonPerWalletInterestTotal =
                new Dictionary<Tuple<int, int>, decimal>
                {
                    { new Tuple<int,int>(1,1), 11.00m },
                    { new Tuple<int,int>(1,2),  5.00m }
                };
            foreach (var total in expectedPersonPerWalletInterestTotal)
            {
                Assert.AreEqual(total.Value, personWalletInterestTotal[total.Key], $"For (PersonID, WalletID) = ({total.Key}), " +
                $"Expected card total: {total.Value}, " +
                $"Actual card total: {personWalletInterestTotal[total.Key]}");
            }
        }

        [TestMethod]
        public void CalculateInterest_2Person_1WalletEach_Wallet1Has2VisaAnd1MC_Wallet2Has1Visa1MC_100DollarBalanceEachCard()
        {
            var personSet = new List<IPerson>
            {
                new Person
                {
                ID = 1,
                Wallets = new List<IWallet> {
                            new Wallet { ID = 1, Cards = new List<Card> {
                                new MasterCard(1, 100.00m, calculationLogic),
                                new MasterCard(2, 100.00m, calculationLogic),
                                new VisaCard(3, 100.00m, calculationLogic)
                                        }
                            }
                }
                },
                new Person
                {
                ID = 2,
                Wallets = new List<IWallet> {
                            new Wallet { ID = 1, Cards = new List<Card> {
                                new VisaCard(1, 100.00m, calculationLogic),
                                new MasterCard(2, 100.00m, calculationLogic)
                                        }
                            }
                }
                }
            };

            var cardInterestCalculator = new CardInterestCalculator
            {
                PersonSet = personSet
            };

            var personInterestTotal = cardInterestCalculator.GetPersonInterestTotal();
            var personWalletInterestTotal = cardInterestCalculator.GetPersonPerWalletInterestTotal();

            var expectedPersonInterestTotal =
                new Dictionary<int, decimal>
                {
                    { 1, 20.00m },
                    { 2, 15.00m },
                };
            foreach (var total in expectedPersonInterestTotal)
            {
                Assert.AreEqual(total.Value, expectedPersonInterestTotal[total.Key], $"For (PersonID) = ({total.Key}), " +
                $"Expected card total: {total.Value}, " +
                $"Actual card total: {expectedPersonInterestTotal[total.Key]}");
            }

            var expectedPersonPerWalletInterestTotal =
                new Dictionary<Tuple<int, int>, decimal>
                {
                    { new Tuple<int,int>(1,1), 20.00m },
                    { new Tuple<int,int>(2,1), 15.00m },

                };
            foreach (var total in expectedPersonPerWalletInterestTotal)
            {
                Assert.AreEqual(total.Value, personWalletInterestTotal[total.Key], $"For (PersonID, WalletID) = ({total.Key}), " +
                $"Expected card total: {total.Value}, " +
                $"Actual card total: {personWalletInterestTotal[total.Key]}");
            }
        }
    }
}
