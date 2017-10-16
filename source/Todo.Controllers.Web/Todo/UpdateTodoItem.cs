using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundary.Todo.Update;

namespace Todo.Controllers.Web.Todo
{
    [RoutePrefix("todo")]
    public class UpdateTodoItem : ApiController
    {
        private readonly IUpdateTodoItemUseCase _useCase;

        public UpdateTodoItem(IUpdateTodoItemUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("update")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Execute([FromBody] UpdateTodoItemInput inputTo)
        {
            var presenter = CreatePresenter();
            
            _useCase.Execute(inputTo, presenter);

            return presenter.Render();
        }

        private ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage>(this);
            return presenter;
        }
    }
}
