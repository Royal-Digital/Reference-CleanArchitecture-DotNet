using System;
using NSubstitute;
using Todo.Domain.Messages;
using Todo.Domain.Model;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.UseCase;

namespace Todo.TestUtils
{
    public class CreateTodoUseCaseTestDataBuilder
    {
        private readonly TodoItemModel _todoItemModel;

        public CreateTodoUseCaseTestDataBuilder()
        {
            _todoItemModel = new TodoItemModel();
        }

        public CreateTodoUseCaseTestDataBuilder WithCreateId(Guid id)
        {
            _todoItemModel.Id = id;
            return this;
        }

        public ICreateTodoItemUseCase Build()
        {
            var respository = Substitute.For<ITodoRepository>();
            respository
                .CreateItem(Arg.Any<CreateTodoItemInputMessage>())
                .Returns(_todoItemModel);

            var usecase = new CreateTodoItemUseCase(respository);
            return usecase;
        }
    }
}
