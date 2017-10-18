using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundary.Todo.Create;

namespace Todo.Controllers.Web.Todo
{
    [RoutePrefix("todo")]
    public class CreateTodo : ApiController
    {
        private readonly ICreateTodoUseCase _useCase;

        public CreateTodo(ICreateTodoUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("create")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CreateTodoOutput))]
        public IHttpActionResult Execute([FromBody] CreateTodoInput input)
        {
            var presenter = CreatePresenter();
            
            _useCase.Execute(input, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<CreateTodoOutput, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<CreateTodoOutput, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}
