using System;
using NSubstitute;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Create;
using Todo.Domain.Todo;
using Todo.Domain.Todo.Create;

namespace Todo.Domain.Tests.Comment.Create
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
                .Create(Arg.Any<CreateTodoItemInput>())
                .Returns(Guid.NewGuid());

            return respository;
        }
    }
}
