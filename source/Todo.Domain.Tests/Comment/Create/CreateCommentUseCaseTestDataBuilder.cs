using System;
using NSubstitute;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Create;
using Todo.Domain.Todo.Create;

namespace Todo.Domain.Tests.Comment.Create
{
    public class CreateTodoUseCaseTestDataBuilder
    {
        private Guid _createdTodoItemId;

        public CreateTodoUseCaseTestDataBuilder WithTodoItemId(Guid id)
        {
            _createdTodoItemId = id;

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
                .Returns(_createdTodoItemId);

            return respository;
        }
    }
}
