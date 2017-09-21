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
        private readonly IDeleteCommentUseCase _useCase;

        public DeleteCommentController(IDeleteCommentUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DeleteCommentOutput))]
        public IHttpActionResult Execute(Guid id)
        {
            //var inputTo = new DeleteCommentInput {Id = id};
            var presenter = new SuccessOrErrorRestfulPresenter<DeleteTodoItemOutput, ErrorOutputMessage>(this);

            if (id == Guid.Empty)
            {
                presenter.Respond(new ErrorOutputMessage());
                return presenter.Render();
            }

            presenter.Respond(new DeleteTodoItemOutput());
            //_usecase.Execute(input, presenter);

            return presenter.Render();
        }
    }
}