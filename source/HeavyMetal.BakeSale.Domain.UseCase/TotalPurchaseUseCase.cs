using System.Collections.Generic;
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

            // note: I refactored this out too early
            var tokens = inputTo.Purchases.Split(',');
            var total = 0.0;
            foreach (var token in tokens)
            {
                double price;
                if(CannotFindItem(token, out price))
                {
                    var error = CreateInvalidInputError();
                    presenter.Respond(error);
                    return;
                }
               
                total += price;
            }

            presenter.Respond(total);
        }

        private ErrorOutputTo CreateInvalidInputError()
        {
            var error = new ErrorOutputTo();
            error.AddError("Error: Invalid input detected");
            return error;
        }

        private bool CannotFindItem(string token, out double price)
        {
            return !_prices.TryGetValue(token, out price);
        }
    }
}
