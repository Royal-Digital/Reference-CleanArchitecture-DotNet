using System.Collections.Generic;
using System.Linq;
using HeavyMetal.BakeSale.Domain.TOs;
using HeavyMetal.BakeSale.Domain.UseCases;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace HeavyMetal.BakeSale.Domain.UseCase
{
    public class TotalPurchaseUseCase : ITotalPurchaseUseCase
    {
        private readonly IDictionary<string, double> _prices = new Dictionary<string, double>
        {
            {"B", 0.65},
            {"M", 1.00},
            {"C", 1.35},
            {"W", 1.50},
        };

        public void Execute(TotalPurchaseInputTo inputTo, IRespondWithSuccessOrError<double, ErrorOutputTo> presenter)
        {
            if (string.IsNullOrEmpty(inputTo.Purchases))
            {
                presenter.Respond(0.0);
                return;
            }
            
            var total = TotalPurchases(inputTo);

            presenter.Respond(total);
        }

        private double TotalPurchases(TotalPurchaseInputTo inputTo)
        {
            var tokens = inputTo.Purchases.Split(',');
            var total = tokens.Sum(token => _prices[token]);
            return total;
        }
    }
}
