using System;
using NSubstitute;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.UseCase.Todo;

namespace Todo.TestUtils
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