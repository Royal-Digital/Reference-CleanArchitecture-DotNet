using System.Collections.Generic;
using NSubstitute;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;
using Todo.Domain.Todo.Fetch;

namespace Todo.Domain.Tests.Todo.Fetch
{
    public class FetchTodoCollectionUseCaseTestDataBuilder
    {
        private List<TodoItemTo> _items;

        public FetchTodoCollectionUseCaseTestDataBuilder WithItems(List<TodoItemTo> items)
        {
            _items = items;
            return this;
        }

        public TodoTestContext<IFetchTodoCollectionUseCase, ITodoRepository> Build()
        {
            var repository = CreateTodoRepository();

            return CreateTodoTestContext(repository);
        }

        private TodoTestContext<IFetchTodoCollectionUseCase, ITodoRepository> CreateTodoTestContext(ITodoRepository repository)
        {
            var usecase = new FetchTodoCollectionUseCase(repository);
            return new TodoTestContext<IFetchTodoCollectionUseCase, ITodoRepository>
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