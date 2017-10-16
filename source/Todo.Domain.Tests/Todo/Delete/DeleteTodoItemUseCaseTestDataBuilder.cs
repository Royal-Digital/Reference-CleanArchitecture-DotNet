using System;
using NSubstitute;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Delete;
using Todo.Domain.Todo.Delete;

namespace Todo.Domain.Tests.Todo.Delete
{
    public class DeleteTodoItemUseCaseTestDataBuilder
    {
        private bool _canDelete;

        public DeleteTodoItemUseCaseTestDataBuilder WithDeleteResult(bool canDelete)
        {
            _canDelete = canDelete;
            return this;
        }

        public TodoTestContext<IDeleteTodoItemUseCase, ITodoRepository> Build()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.Delete(Arg.Any<Guid>()).Returns(_canDelete);
            var usecase =  new DeleteTodoItemUseCase(repository);
            return new TodoTestContext<IDeleteTodoItemUseCase, ITodoRepository> { UseCase = usecase, Repository = repository };
        }
    }
}