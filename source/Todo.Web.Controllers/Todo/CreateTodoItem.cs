using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundry.Todo.Create;

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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CreateTodoItemOuput))]
        public IHttpActionResult Execute([FromBody] CreateTodoItemInput input)
        {
            var presenter = CreatePresenter();
            
            _useCase.Execute(input, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<CreateTodoItemOuput, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<CreateTodoItemOuput, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}
