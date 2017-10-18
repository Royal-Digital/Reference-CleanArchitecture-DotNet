using System.Collections.Generic;
using NSubstitute;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Fetch.Filtered;
using Todo.Domain.Todo.Fetch;

namespace Todo.Domain.Tests.Todo.Fetch
{
    public class FetchFilteredTodoUseCaseTestDataBuilder
    {
        private List<TodoTo> _items;

        public FetchFilteredTodoUseCaseTestDataBuilder WithItems(List<TodoTo> items)
        {
            _items = items;
            return this;
        }

        public TodoTestContext<IFetchFilteredTodoUseCase, ITodoRepository> Build()
        {
            var repository = CreateTodoRepository();

            return CreateTodoTestContext(repository);
        }

        private TodoTestContext<IFetchFilteredTodoUseCase, ITodoRepository> CreateTodoTestContext(ITodoRepository repository)
        {
            var usecase = new FetchFilteredTodoUseCase(repository);
            return new TodoTestContext<IFetchFilteredTodoUseCase, ITodoRepository>
            {
                UseCase = usecase,
                Repository = repository
            };
        }

        private ITodoRepository CreateTodoRepository()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchFiltered(Arg.Any<TodoFilterInput>()).Returns(_items);
            return repository;
        }
    }
}