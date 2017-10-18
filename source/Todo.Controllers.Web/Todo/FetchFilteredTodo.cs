using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Fetch.Filtered;

namespace Todo.Controllers.Web.Todo {

    [RoutePrefix("todo")]
    public class FetchFilteredTodo : ApiController
    {
        private readonly IFetchFilteredTodoUseCase _useCase;

        public FetchFilteredTodo(IFetchFilteredTodoUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("fetch")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<TodoTo>))]
        public IHttpActionResult Execute([FromBody] TodoFilterInput filter)
        {
            var presenter = CreatePresenter();

            _useCase.Execute(filter, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<List<TodoTo>, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<List<TodoTo>, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}