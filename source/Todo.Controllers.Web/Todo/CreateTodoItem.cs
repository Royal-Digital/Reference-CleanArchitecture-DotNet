using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundary.Todo.Create;

namespace Todo.Controllers.Web.Todo
{
    [RoutePrefix("todo")]
    public class CreateTodoItem : ApiController
    {
        private readonly ICreateTodoItemUseCase _useCase;

        public CreateTodoItem(ICreateTodoItemUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("create")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CreateTodoOuput))]
        public IHttpActionResult Execute([FromBody] CreateTodoInput input)
        {
            var presenter = CreatePresenter();
            
            _useCase.Execute(input, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<CreateTodoOuput, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<CreateTodoOuput, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}
