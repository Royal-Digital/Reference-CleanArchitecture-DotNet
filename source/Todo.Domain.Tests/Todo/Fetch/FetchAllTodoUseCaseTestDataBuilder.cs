using System.Collections.Generic;
using NSubstitute;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;
using Todo.Domain.Todo.Fetch;

namespace Todo.Domain.Tests.Todo.Fetch
{
    public class FetchAllTodoUseCaseTestDataBuilder
    {
        private List<TodoTo> _items;

        public FetchAllTodoUseCaseTestDataBuilder WithItems(List<TodoTo> items)
        {
            _items = items;
            return this;
        }

        public TodoTestContext<IFetchAllTodoUseCase, ITodoRepository> Build()
        {
            var repository = CreateTodoRepository();

            return CreateTodoTestContext(repository);
        }

        private TodoTestContext<IFetchAllTodoUseCase, ITodoRepository> CreateTodoTestContext(ITodoRepository repository)
        {
            var usecase = new FetchAllTodoUseCase(repository);
            return new TodoTestContext<IFetchAllTodoUseCase, ITodoRepository>
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