using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;

namespace Todo.Api.Controllers.Comment
{
    [RoutePrefix("comment")]
    public class CreateCommentController : ApiController
    {
        private readonly ICreateCommentUseCase _usecase;

        public CreateCommentController(ICreateCommentUseCase usecase)
        {
            _usecase = usecase;
        }

        [Route("create")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CreateCommentOuput))]
        public IHttpActionResult Execute([FromBody] CreateCommentInput input)
        {
            var presenter = CreatePresenter();

            _usecase.Execute(input, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<CreateCommentOuput, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<CreateCommentOuput, ErrorOutputMessage>(this);
            return presenter;
        }
    }
}