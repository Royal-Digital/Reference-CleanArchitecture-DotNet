using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Boundry.Comment;
using Todo.Boundry.Comment.Create;
using Todo.Boundry.Todo;

namespace Todo.Domain.Comment.Create
{
    public class CreateCommentUseCase : ICreateCommentUseCase
    {
        private readonly ICommentRepository _repository;
        private readonly ITodoRepository _todoItemRepository;

        public CreateCommentUseCase(ICommentRepository repository, ITodoRepository todoItemRepository)
        {
            _repository = repository;
            _todoItemRepository = todoItemRepository;
        }

        public void Execute(CreateCommentInput inputTo, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            var domainEntity = CreateDomainModelFromInput(inputTo);

            if (InvalidTodoItemId(domainEntity))
            {
                RespondWithErrorMessage("Invalid item Id", presenter);
                return;
            }

            if (InvalidComment(domainEntity)) 
            {
                RespondWithErrorMessage("Missing comment", presenter);
                return;
            }

            var updatedEntity = PersistDomainEntity(domainEntity);

            RespondWithSuccess(updatedEntity.Id, presenter);
        }

        private TodoComment PersistDomainEntity(TodoComment domainModel)
        {
            var updatedModel = _repository.Create(domainModel);
            _repository.Save();
            return updatedModel;
        }

        private TodoComment CreateDomainModelFromInput(CreateCommentInput input)
        {
            var mapper = CreateAutoMapper();
            var domainModel = mapper.Map<TodoComment>(input);
            return domainModel;
        }

        private void RespondWithSuccess(Guid commentId, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(
                new CreateCommentOuput {Id = commentId});
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
            return !domainModel.IsTodoItemIdValid() || CannotLocateTodoItem(domainModel);
        }

        private bool CannotLocateTodoItem(TodoComment domainModel)
        {
            var item = _todoItemRepository.FindById(domainModel.TodoItemId);
            return item == null;
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