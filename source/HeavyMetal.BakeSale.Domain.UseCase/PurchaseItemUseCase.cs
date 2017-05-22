using System.Collections;
using System.Collections.Generic;
using HeavyMetal.BakeSale.Domain.TOs;
using HeavyMetal.BakeSale.Domain.UseCases;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace HeavyMetal.BakeSale.Domain.UseCase
{
    public class PurchaseItemUseCase : IPurchaseUseCase
    {
        private IDictionary<string, double> _prices = new Dictionary<string, double>
        {
            {"B", 0.65},
            {"M", 1.00},
            {"C", 1.35},
            {"W", 1.50},
        };

        public void Execute(PurchaseItemInputTo inputTo, IRespondWithSuccessOrError<double, ErrorOutputTo> presenter)
        {
            if (string.IsNullOrEmpty(inputTo.Purchases))
            {
                presenter.Respond(0.0);
                return;
            }

            var price = _prices[inputTo.Purchases];
            presenter.Respond(price);
        }
    }
}
