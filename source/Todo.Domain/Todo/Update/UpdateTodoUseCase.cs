using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Update;

namespace Todo.Domain.Todo.Update
{
    public class UpdateTodoUseCase : IUpdateTodoUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;

        public UpdateTodoUseCase(ITodoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = CreateAutoMapper();
        }

        public void Execute(UpdateTodoInput inputTo, IRespondWithResultFreeSuccessOrError<ErrorOutputMessage> presenter)
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
            presenter.Respond();
        }

        private void UpdateTodoItem(UpdateTodoInput model)
        {
            _repository.Update(model);
            _repository.Persist();
        }

        private TodoItem MapInputToModel(UpdateTodoInput input)
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
                cfg.CreateMap<UpdateTodoInput, TodoItem>();
            });

            return new Mapper(configuration);
        }

        private bool InvalidId(TodoItem inputTo)
        {
            return !inputTo.IsIdValid();
        }

        private void RespondWithError(string message, IRespondWithResultFreeSuccessOrError<ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError(message);
            presenter.Respond(errorOutputMessage);
        }
    }
}