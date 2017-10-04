using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundry.Todo.Fetch;

namespace Todo.Web.Controllers.Todo
{
    [RoutePrefix("todo")]
    public class FetchTodoItem : ApiController
    {
        private readonly IFetchTodoCollectionUseCase _useCase;

        public FetchTodoItem(IFetchTodoCollectionUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("fetch/all")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<TodoItemTo>))]
        public IHttpActionResult Execute()
        {
            var presenter = CreatePresenter();

            _useCase.Execute(presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<List<TodoItemTo>, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<List<TodoItemTo>, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}