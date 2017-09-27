using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Update;

namespace Todo.Domain.Todo.Update
{
    public class UpdateTodoItemUseCase : IUpdateTodoItemUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;

        public UpdateTodoItemUseCase(ITodoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = CreateAutoMapper();
        }

        public void Execute(UpdateTodoItemInput inputTo, IRespondWithSuccessOrError<UpdateTodoItemOutput, ErrorOutputMessage> presenter)
        {
            var model = MapInputToModel(inputTo);

            if (InvalidId(model))
            {
                RespondWithError("Id cannot be empty", presenter);
                return;
            }

            if (InvalidItemDescription(model))
            {
                RespondWithError("ItemDescription cannot be null or empty", presenter);
                return;
            }

            UpdateTodoItem(model);
            RespondWithSuccess(presenter, model);
        }

        private static void RespondWithSuccess(IRespondWithSuccessOrError<UpdateTodoItemOutput, ErrorOutputMessage> presenter, TodoItem model)
        {
            presenter.Respond(new UpdateTodoItemOutput {Id = model.Id, Message = "Item updated"});
        }

        private void UpdateTodoItem(TodoItem model)
        {
            _repository.Update(model);
        }

        private TodoItem MapInputToModel(UpdateTodoItemInput input)
        {
            var model = _mapper.Map<TodoItem>(input);
            return model;
        }

        private bool InvalidItemDescription(TodoItem model)
        {
            return !model.ItemDescriptionIsValid();
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UpdateTodoItemInput, TodoItem>();
                }))
                .Build();
        }

        private bool InvalidId(TodoItem inputTo)
        {
            return !inputTo.IsIdValid();
        }

        private void RespondWithError(string message, IRespondWithSuccessOrError<UpdateTodoItemOutput, ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError(message);
            presenter.Respond(errorOutputMessage);
        }
    }
}