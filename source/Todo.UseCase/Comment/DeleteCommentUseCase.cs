using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.DomainEntities;

namespace Todo.UseCase.Comment
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
            var domainModel = ConvertInputToDomainModel(inputTo);

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

            RespondWithSuccess(inputTo, presenter);
        }

        private bool CouldNotDelete(TodoComment domainModel)
        {
            return !_repository.Delete(domainModel);
        }

        private TodoComment ConvertInputToDomainModel(DeleteCommentInput inputTo)
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

        private void RespondWithSuccess(DeleteCommentInput inputTo, IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(new DeleteCommentOutput {Id = inputTo.Id, Message = "Commented deleted successfuly"});
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