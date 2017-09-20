using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Domain.Messages;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Entities;

namespace Todo.UseCase
{
    public class UpdateTodoItemUseCase : IUpdateTodoItemUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoItemUseCase(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            _mapper = _mapper = new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UpdateTodoItemInput, TodoItem>();
                }))
                .Build();
        }

        public void Execute(UpdateTodoItemInput input, IRespondWithSuccessOrError<UpdateTodoItemOutput, ErrorOutputMessage> presenter)
        {
            var model = _mapper.Map<TodoItem>(input);

            if (InvalidId(model))
            {
                RespondWithError("Id cannot be empty", presenter);
                return;
            }

            if (!model.ItemDescriptionIsValid())
            {
                RespondWithError("ItemDescription cannot be null or empty", presenter);
            }

            _todoRepository.Update(model);

            presenter.Respond(new UpdateTodoItemOutput{Id = model.Id, Message = "item updated"});
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