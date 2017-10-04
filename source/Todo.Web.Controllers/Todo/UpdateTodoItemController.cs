using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundry.Todo.Update;

namespace Todo.Web.Controllers.Todo
{
    [RoutePrefix("todo")]
    public class UpdateTodoItemController : ApiController
    {
        private readonly IUpdateTodoItemUseCase _useCase;

        public UpdateTodoItemController(IUpdateTodoItemUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("update")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UpdateTodoItemOutput))]
        public IHttpActionResult Execute([FromBody] UpdateTodoItemInput inputTo)
        {
            var presenter = CreatePresenter();
            
            _useCase.Execute(inputTo, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<UpdateTodoItemOutput, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<UpdateTodoItemOutput, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}
