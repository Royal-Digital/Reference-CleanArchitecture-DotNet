using System;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundry.UseCase;
using Todo.Domain.UseCaseMessages;

namespace Todo.Api.Controllers.Todo
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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DeleteTodoItemOutput))]
        public IHttpActionResult Execute(Guid itemId)
        {
            var inputTo = CreateInput(itemId);
            var presenter = CreatePresenter();

            _useCase.Execute(inputTo, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<DeleteTodoItemOutput, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<DeleteTodoItemOutput, ErrorOutputMessage>(this);
            return presenter;
        }

        private DeleteTodoItemInput CreateInput(Guid itemId)
        {
            var inputTo = new DeleteTodoItemInput {Id = itemId};
            return inputTo;
        }
    }
}