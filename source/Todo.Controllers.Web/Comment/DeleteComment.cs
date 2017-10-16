using System;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Boundary.Comment.Delete;

namespace Todo.Controllers.Web.Comment
{
    [RoutePrefix("comment")]
    public class DeleteComment : ApiController
    {
        private readonly IDeleteCommentUseCase _usecase;

        public DeleteComment(IDeleteCommentUseCase useCase)
        {
            _usecase = useCase;
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult Execute(Guid id)
        {
            var inputTo = CreateInput(id);
            var presenter = CreatePresenter();
            
            _usecase.Execute(inputTo, presenter);

            return presenter.Render();
        }

        private ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage> CreatePresenter()
        {
            var presenter = new ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage>(this);
            return presenter;
        }

        private DeleteCommentInput CreateInput(Guid id)
        {
            var inputTo = new DeleteCommentInput {Id = id};
            return inputTo;
        }
    }
}