using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundry.Comment.Create;

namespace Todo.Controllers.Web.Comment
{
    [RoutePrefix("comment")]
    public class CreateComment : ApiController
    {
        private readonly ICreateCommentUseCase _usecase;

        public CreateComment(ICreateCommentUseCase usecase)
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