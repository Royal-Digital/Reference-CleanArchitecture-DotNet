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

            var output = Persist(inputTo);

            RespondWithSuccess(output, presenter);
        }

        private CreateCommentOuput Persist(CreateCommentInput domainModel)
        {
            var id = _repository.Create(domainModel);
            _repository.Save();
            return new CreateCommentOuput {Id = id};
        }

        private TodoComment CreateDomainModelFromInput(CreateCommentInput input)
        {
            var mapper = CreateAutoMapper();
            var domainModel = mapper.Map<TodoComment>(input);
            return domainModel;
        }

        private void RespondWithSuccess(CreateCommentOuput output, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(output);
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