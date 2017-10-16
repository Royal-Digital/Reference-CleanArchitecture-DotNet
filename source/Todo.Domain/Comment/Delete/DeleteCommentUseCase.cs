using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundary.Comment;
using Todo.Boundary.Comment.Delete;

namespace Todo.Domain.Comment.Delete
{
    public class DeleteCommentUseCase : IDeleteCommentUseCase
    {
        private readonly ICommentRepository _repository;

        public DeleteCommentUseCase(ICommentRepository repository)
        {
            _repository = repository;
        }

        public void Execute(DeleteCommentInput inputTo, IRespondWithResultFreeSuccessOrError<ErrorOutputMessage> presenter)
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

            presenter.Respond();
        }

        private bool CouldNotDelete(TodoComment domainModel)
        {
            var result=  !_repository.Delete(domainModel.Id);
            _repository.Save();
            return result;
        }

        private TodoComment ConvertToDomainModel(DeleteCommentInput inputTo)
        {
            var mapper = CreateAutoMapper();
            var domainModel = mapper.Map<TodoComment>(inputTo);
            return domainModel;
        }

        private void RespondWithError(string message, IRespondWithResultFreeSuccessOrError<ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError(message);
            presenter.Respond(errorOutputMessage);
        }

        private bool InvalidCommentId(TodoComment domainModel)
        {
            return !domainModel.IsIdValid();
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