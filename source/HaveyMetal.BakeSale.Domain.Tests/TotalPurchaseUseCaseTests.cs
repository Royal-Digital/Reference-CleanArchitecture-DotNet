using HeavyMetal.BakeSale.Domain.TOs;
using HeavyMetal.BakeSale.Domain.UseCase;
using HeavyMetal.BakeSale.Domain.UseCases;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace HaveyMetal.BakeSale.Domain.Tests
{
    [TestFixture]
    public class TotalPurchaseUseCaseTests
    {
        [Test]
        public void Ctor_ShouldNotThrowException()
        {
            //---------------Set up test pack-------------------
            
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.DoesNotThrow(()=>new TotalPurchaseUseCase());
        }

        [Test]
        public void Execute_WhenEmptyInput_ShouldNotReturnError()
        {
            //---------------Set up test pack-------------------
            var usecase = CreateTotalPurchaseUseCase();
            var presenter = CreatePropertyPresenter();
            var inputTo = new TotalPurchaseInputTo();
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.IsFalse(presenter.IsErrorResponse());
        }

        [TestCase("B", 0.65)]
        [TestCase("M", 1.00)]
        [TestCase("C", 1.35)]
        [TestCase("W", 1.50)]
        public void Execute_WhenSingleItem_ShouldItemPrice(string item, double price)
        {
            //---------------Set up test pack-------------------
            var usecase = CreateTotalPurchaseUseCase();
            var presenter = CreatePropertyPresenter();
            var inputTo = new TotalPurchaseInputTo {Purchases = item};
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(price,presenter.SuccessContent);
        }

        [TestCase("B,B",1.30)]
        [TestCase("B,W",2.15)]
        public void Execute_WhenTwoItems_ShouldReturnTotal(string items, double total)
        {
            //---------------Set up test pack-------------------
            var usecase = CreateTotalPurchaseUseCase();
            var presenter = CreatePropertyPresenter();
            var inputTo = new TotalPurchaseInputTo {Purchases = items};
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(total, presenter.SuccessContent);
        }

        private ITotalPurchaseUseCase CreateTotalPurchaseUseCase()
        {
            var usecase = new TotalPurchaseUseCase();
            return usecase;
        }

        private PropertyPresenter<double, ErrorOutputTo> CreatePropertyPresenter()
        {
            var presenter = new PropertyPresenter<double, ErrorOutputTo>();
            return presenter;
        }
    }
}
