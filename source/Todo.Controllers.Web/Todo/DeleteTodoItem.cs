using System;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundary.Todo.Delete;

namespace Todo.Controllers.Web.Todo
{
    [RoutePrefix("todo")]
    public class DeleteTodoItem : ApiController
    {
        private readonly IDeleteTodoItemUseCase _useCase;

        public DeleteTodoItem(IDeleteTodoItemUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("delete/{itemId}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Execute(Guid itemId)
        {
            var inputTo = CreateInput(itemId);
            var presenter = CreatePresenter();

            _useCase.Execute(inputTo, presenter);

            return presenter.Render();
        }

        private ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage>(this);
            return presenter;
        }

        private DeleteTodoInput CreateInput(Guid itemId)
        {
            var inputTo = new DeleteTodoInput {Id = itemId};
            return inputTo;
        }
    }
}