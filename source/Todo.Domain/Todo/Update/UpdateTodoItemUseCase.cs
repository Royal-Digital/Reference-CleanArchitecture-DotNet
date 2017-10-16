using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
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

            UpdateTodoItem(inputTo);
            RespondWithSuccess(model.Id, presenter);
        }

        private void RespondWithSuccess(Guid id, IRespondWithSuccessOrError<UpdateTodoItemOutput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(new UpdateTodoItemOutput {Id = id, Message = "Item updated"});
        }

        private void UpdateTodoItem(UpdateTodoItemInput model)
        {
            _repository.Update(model);
            _repository.Save();
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
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateTodoItemInput, TodoItem>();
            });

            return new Mapper(configuration);
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