using System.Collections.Generic;
using NSubstitute;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Update;
using Todo.Domain.Todo.Update;

namespace Todo.Domain.Tests.Todo.Update
{
    public class UpdateTodoItemUseCaseTestDataBuilder
    {
        private List<TodoTo> _items;

        public UpdateTodoItemUseCaseTestDataBuilder WithItems(List<TodoTo> items)
        {
            _items = items;
            return this;
        }

        public TodoTestContext<IUpdateTodoUseCase, ITodoRepository> Build()
        {
            var repository = CreateTodoRepository();
            return CreateTodoTestContext(repository);
        }

        private TodoTestContext<IUpdateTodoUseCase, ITodoRepository> CreateTodoTestContext(ITodoRepository repository)
        {
            var usecase = new UpdateTodoUseCase(repository);

            return new TodoTestContext<IUpdateTodoUseCase, ITodoRepository>
            {
                UseCase = usecase,
                Repository = repository
            };
        }

        private ITodoRepository CreateTodoRepository()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(_items);
            return repository;
        }
    }
}