using HeavyMetal.BakeSale.Domain.TOs;
using TddBuddy.CleanArchitecture.Domain;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace HeavyMetal.BakeSale.Domain
{
    public class PurchaseItemUseCase : IUseCase<PurchaseItemInputTo, int>
    {
        public void Execute(PurchaseItemInputTo inputTo, IRespondWithSuccessOrError<int, ErrorOutputTo> presenter)
        {
            presenter.Respond(0);
        }
    }
}
