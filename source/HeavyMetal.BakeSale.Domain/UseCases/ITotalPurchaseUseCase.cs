using TddBuddy.CleanArchitecture.Domain;

namespace HeavyMetal.BakeSale.Domain.UseCases
{
    // note: it was a mistake to use a TO when a primative does just fine
    public interface ITotalPurchaseUseCase : IUseCase<string,double>
    {
    }
}
