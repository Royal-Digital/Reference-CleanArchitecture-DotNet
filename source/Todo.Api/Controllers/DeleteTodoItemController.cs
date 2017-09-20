using System;
using System.Web.Http;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Domain.Messages;
using Todo.Domain.UseCase;

namespace Todo.Api.Controllers
{
    [RoutePrefix("todo")]
    public class DeleteTodoItemController : ApiController
    {
        private readonly IDeleteTodoItemUseCase _useCase;

        public DeleteTodoItemController(IDeleteTodoItemUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("delete/{itemId}")]
        [HttpDelete]
        public IHttpActionResult Execute(Guid itemId)
        {
            var presenter = new SuccessOrErrorRestfulPresenter<DeleteTodoItemOutput, ErrorOutputMessage>(this);
            var inputTo = new DeleteTodoItemInput {Id = itemId};

            _useCase.Execute(inputTo, presenter);

            return presenter.Render();
        }
    }
}