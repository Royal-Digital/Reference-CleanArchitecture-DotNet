using System;
using NSubstitute;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Create;
using Todo.Domain.Todo.Create;

namespace Todo.Domain.Tests.Todo.Create
{

    public class CreateTodoUseCaseTestDataBuilder
    {
        private Guid _createdTodoItemId;

        public CreateTodoUseCaseTestDataBuilder WithTodoItemId(Guid id)
        {
            _createdTodoItemId = id;

            return this;
        }

        public TodoTestContext<ICreateTodoItemUseCase, ITodoRepository> Build()
        {
            var respository = CreateTodoRepository();
            var usecase = new CreateTodoItemUseCase(respository);

            return new TodoTestContext<ICreateTodoItemUseCase, ITodoRepository>{UseCase = usecase, Repository = respository};
        }

        private ITodoRepository CreateTodoRepository()
        {
            var respository = Substitute.For<ITodoRepository>();
            respository
                .Create(Arg.Any<CreateTodoInput>())
                .Returns(_createdTodoItemId);

            return respository;
        }
    }
}
