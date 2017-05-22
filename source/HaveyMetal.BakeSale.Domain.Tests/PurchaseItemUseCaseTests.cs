using HeavyMetal.BakeSale.Domain.TOs;
using HeavyMetal.BakeSale.Domain.UseCase;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace HaveyMetal.BakeSale.Domain.Tests
{
    [TestFixture]
    public class PurchaseItemUseCaseTests
    {
        [Test]
        public void Ctor_ShouldNotThrowException()
        {
            //---------------Set up test pack-------------------
            
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.DoesNotThrow(()=>new PurchaseItemUseCase());
        }

        [Test]
        public void Execute_WhenEmptyInput_ShouldNotReturnError()
        {
            //---------------Set up test pack-------------------
            var usecase = new PurchaseItemUseCase();
            var presenter = new PropertyPresenter<double,ErrorOutputTo>();
            var inputTo = new PurchaseItemInputTo();
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
            var usecase = new PurchaseItemUseCase();
            var presenter = new PropertyPresenter<double, ErrorOutputTo>();
            var inputTo = new PurchaseItemInputTo {Purchases = item};
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(price,presenter.SuccessContent);
        }
    }
}
