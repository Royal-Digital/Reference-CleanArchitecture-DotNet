using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;

namespace Todo.UseCase
{
    public class CreateCommentUseCase : ICreateCommentUseCase
    {
        public void Execute(CreateCommentInput input, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            var domainModel = CreateDomainModelFromInput(input);

            if (InvalidTodoItemId(domainModel))
            {
                RespondWithErrorMessage("Invalid item Id", presenter);
                return;
            }

            if (InvalidComment(domainModel)) 
            {
                RespondWithErrorMessage("Missing comment", presenter);
                return;
            }

            RespondWithSuccess(input, presenter);
        }

        private TodoComment CreateDomainModelFromInput(CreateCommentInput input)
        {
            var mapper = CreateAutoMapper();
            var domainModel = mapper.Map<TodoComment>(input);
            return domainModel;
        }

        private void RespondWithSuccess(CreateCommentInput input, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(
                new CreateCommentOuput {TodoItemId = input.TodoItemId, Message = "Successfully added the comment"});
        }

        public void RespondWithErrorMessage(string message, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError(message);
            presenter.Respond(errorOutputMessage);
        }

        private bool InvalidComment(TodoComment domainModel)
        {
            return !domainModel.IsCommentValid();
        }

        private bool InvalidTodoItemId(TodoComment domainModel)
        {
            return !domainModel.IsTodoItemIdValid();
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateCommentInput, TodoComment>();
                }))
                .Build();
        }
    }
}