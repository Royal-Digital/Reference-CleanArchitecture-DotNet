using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;

namespace Todo.Api.Controllers.Todo
{
    [RoutePrefix("todo")]
    public class FetchTodoItemController : ApiController
    {
        private readonly IFetchTodoCollectionUseCase _useCase;

        public FetchTodoItemController(IFetchTodoCollectionUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("fetch/all")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<FetchTodoItemOutput>))]
        public IHttpActionResult Execute()
        {
            var presenter = CreatePresenter();

            _useCase.Execute(presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<List<FetchTodoItemOutput>, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<List<FetchTodoItemOutput>, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}