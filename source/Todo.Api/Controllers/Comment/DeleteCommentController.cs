using System;
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
    public class DeleteCommentController : ApiController
    {
        private readonly IDeleteCommentUseCase _usecase;

        public DeleteCommentController(IDeleteCommentUseCase useCase)
        {
            _usecase = useCase;
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DeleteCommentOutput))]
        public IHttpActionResult Execute(Guid id)
        {
            var inputTo = CreateInput(id);
            var presenter = CreatePresenter();
            
            _usecase.Execute(inputTo, presenter);

            return presenter.Render();
        }

        private SuccessOrErrorRestfulPresenter<DeleteCommentOutput, ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<DeleteCommentOutput, ErrorOutputMessage>(this);
            return presenter;
        }

        private static DeleteCommentInput CreateInput(Guid id)
        {
            var inputTo = new DeleteCommentInput {Id = id};
            return inputTo;
        }
    }
}