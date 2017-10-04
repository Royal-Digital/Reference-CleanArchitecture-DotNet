using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundry.Comment;
using Todo.Boundry.Comment.Delete;

namespace Todo.Domain.Comment.Delete
{
    public class DeleteCommentUseCase : IDeleteCommentUseCase
    {
        private readonly ICommentRepository _repository;

        public DeleteCommentUseCase(ICommentRepository repository)
        {
            _repository = repository;
        }

        public void Execute(DeleteCommentInput inputTo, IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            var domainModel = ConvertToDomainModel(inputTo);

            if (InvalidCommentId(domainModel))
            {
                RespondWithError("Invalid comment Id", presenter);
                return;
            }

            if (CouldNotDelete(domainModel))
            {
                RespondWithError("Could not locate item Id", presenter);
                return;
            }

            RespondWithSuccess(inputTo.Id, presenter);
        }

        private bool CouldNotDelete(TodoComment domainModel)
        {
            return !_repository.Delete(domainModel.Id);
        }

        private TodoComment ConvertToDomainModel(DeleteCommentInput inputTo)
        {
            var mapper = CreateAutoMapper();
            var domainModel = mapper.Map<TodoComment>(inputTo);
            return domainModel;
        }

        private void RespondWithError(string message, IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError(message);
            presenter.Respond(errorOutputMessage);
        }

        private bool InvalidCommentId(TodoComment domainModel)
        {
            return !domainModel.IsIdValid();
        }

        private void RespondWithSuccess(Guid id, IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(new DeleteCommentOutput {Id = id, Message = "Commented deleted successfuly"});
        }

        private IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeleteCommentInput, TodoComment>();
            });

            return new Mapper(configuration);
        }
    }
}