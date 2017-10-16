using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundary.Todo.Fetch;

namespace Todo.Controllers.Web.Todo
{
    [RoutePrefix("todo")]
    public class FetchTodoItem : ApiController
    {
        private readonly IFetchAllTodoUseCase _useCase;

        public FetchTodoItem(IFetchAllTodoUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("fetch/all")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<TodoTo>))]
        public IHttpActionResult Execute()
        {
            var presenter = CreatePresenter();

            _useCase.Execute(presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<List<TodoTo>, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<List<TodoTo>, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}