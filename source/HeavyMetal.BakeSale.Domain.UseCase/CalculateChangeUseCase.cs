using System;
using HeavyMetal.BakeSale.Domain.TOs;
using HeavyMetal.BakeSale.Domain.UseCases;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace HeavyMetal.BakeSale.Domain.UseCase
{
    public class CalculateChangeUseCase : ICalculateChangeUseCase
    {
        public void Execute(CalculateChangeInputTo inputTo, IRespondWithSuccessOrError<double, ErrorOutputTo> presenter)
        {
            if (inputTo.Payment < inputTo.Total)
            {
                var error = new ErrorOutputTo();
                error.AddError("Error: Payment is less then Total");
                presenter.Respond(error);
                return;
            }

            var change = Math.Round(inputTo.Payment - inputTo.Total,2);

            presenter.Respond(change);
        }
    }
}
