using HeavyMetal.BakeSale.Domain.TOs;
using HeavyMetal.BakeSale.Domain.UseCase;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using TddBuddy.CleanArchitecture.Domain.TOs;

namespace HaveyMetal.BakeSale.Domain.Tests
{
    [TestFixture]
    public class CalculateChangeUseCaseTests
    {
        [Test]
        public void Execute_WhenPaymentMatchesTotal_ShouldReturnZero()
        {
            //---------------Set up test pack-------------------
            var expected = 0.0;
            var presenter = new PropertyPresenter<double, ErrorOutputTo>();
            var usecase = new CalculateChangeUseCase();
            var inputTo = new CalculateChangeInputTo {Total = 1.00, Payment = 1.00};
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, presenter.SuccessContent);
        }

        [Test]
        public void Execute_WhenPaymentMoreThenTotal_ShouldReturnChange()
        {
            //---------------Set up test pack-------------------
            var expected = 0.1;
            var presenter = new PropertyPresenter<double, ErrorOutputTo>();
            var usecase = new CalculateChangeUseCase();
            var inputTo = new CalculateChangeInputTo { Total = 0.90, Payment = 1.00 };
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, presenter.SuccessContent);
        }

        [Test]
        public void Execute_WhenPaymentLessThenTotal_ShouldReturnError()
        {
            //---------------Set up test pack-------------------
            var expected = "Error: Payment is less then Total";
            var presenter = new PropertyPresenter<double, ErrorOutputTo>();
            var usecase = new CalculateChangeUseCase();
            var inputTo = new CalculateChangeInputTo { Total = 1.00, Payment = 0.00 };
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, presenter.ErrorContent.Errors[0]);
        }
    }
}
