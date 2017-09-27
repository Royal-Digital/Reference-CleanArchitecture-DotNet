using System;
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

        public IDeleteTodoItemUseCase Build()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.Delete(Arg.Any<Guid>()).Returns(_canDelete);
            return new DeleteTodoItemUseCase(repository);
        }
    }
}