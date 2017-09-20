using System;
using NSubstitute;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Entities;
using Todo.UseCase;

namespace Todo.TestUtils
{
    public class CreateTodoUseCaseTestDataBuilder
    {
        private readonly TodoItem _todoItemModel;

        public CreateTodoUseCaseTestDataBuilder()
        {
            _todoItemModel = new TodoItem();
        }

        public CreateTodoUseCaseTestDataBuilder WithModelId(Guid id)
        {
            _todoItemModel.Id = id;

            return this;
        }

        public ICreateTodoItemUseCase Build()
        {
            var respository = CreateTodoRepository();
            var usecase = new CreateTodoItemUseCase(respository);

            return usecase;
        }

        private ITodoRepository CreateTodoRepository()
        {
            var respository = Substitute.For<ITodoRepository>();
            respository
                .CreateItem(Arg.Any<TodoItem>())
                .Returns(_todoItemModel);

            return respository;
        }
    }
}
