using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;

namespace Todo.UseCase.Comment
{
    public class DeleteCommentUseCase : IDeleteCommentUseCase
    {
        private ICommentRepository _repository;

        public DeleteCommentUseCase(ICommentRepository repository)
        {
            _repository = repository;
        }

        public void Execute(DeleteCommentInput inputTo, IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            var mapper = CreateAutoMapper();
            var domainModel = mapper.Map<TodoComment>(inputTo);

            if (InvalidCommentId(domainModel))
            {
                RespondWithInvalidCommentIdError(presenter);
                return;
            }

            var isDeleted = _repository.Delete(domainModel);

            RespondWithSuccess(inputTo, presenter);
        }

        private bool InvalidCommentId(TodoComment domainModel)
        {
            return !domainModel.IsIdValid();
        }

        private void RespondWithSuccess(DeleteCommentInput inputTo, IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(new DeleteCommentOutput {Id = inputTo.Id, Message = "Commented deleted successfuly"});
        }

        private void RespondWithInvalidCommentIdError(IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError("Invalid comment Id");
            presenter.Respond(errorOutputMessage);
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DeleteCommentInput, TodoComment>();
                }))
                .Build();
        }
    }
}