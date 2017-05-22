using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeavyMetal.BakeSale.Domain;
using HeavyMetal.BakeSale.Domain.TOs;
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
        public void Execute_WhenEmptyStringInput_ShouldNotReturnError()
        {
            //---------------Set up test pack-------------------
            var usecase = new PurchaseItemUseCase();
            var presenter = new PropertyPresenter<int,ErrorOutputTo>();
            var inputTo = new PurchaseItemInputTo();
            //---------------Execute Test ----------------------
            usecase.Execute(inputTo, presenter);
            //---------------Test Result -----------------------
            Assert.IsFalse(presenter.IsErrorResponse());
        }
    }
}
