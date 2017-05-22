using HeavyMetal.BakeSale.Domain.TOs;
using TddBuddy.CleanArchitecture.Domain;

namespace HeavyMetal.BakeSale.Domain.UseCases
{
    public interface IPurchaseUseCase : IUseCase<PurchaseItemInputTo,double>
    {
    }
}
