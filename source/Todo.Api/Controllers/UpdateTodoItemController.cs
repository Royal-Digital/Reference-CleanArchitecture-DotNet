using System.Web.Http;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Domain.Model;
using Todo.Domain.UseCase;

namespace Todo.Api.Controllers
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
        public IHttpActionResult Execute([FromBody] TodoItemModel itemModel)
        {
            var presenter = new SuccessOrErrorRestfulPresenter<string, ErrorOutputMessage>(this);
            
            _useCase.Execute(itemModel, presenter);

            return presenter.Render();
        }
    }
}
