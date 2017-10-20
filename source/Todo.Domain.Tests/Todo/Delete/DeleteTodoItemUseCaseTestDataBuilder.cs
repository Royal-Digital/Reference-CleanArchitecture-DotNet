using System;
using NSubstitute;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Delete;
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

        public TodoTestContext<IDeleteTodoUseCase, ITodoRepository> Build()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.MarkForDelete(Arg.Any<Guid>()).Returns(_canDelete);
            var usecase =  new DeleteTodoUseCase(repository);
            return new TodoTestContext<IDeleteTodoUseCase, ITodoRepository> { UseCase = usecase, Repository = repository };
        }
    }
}