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
        private List<TodoItemTo> _items;

        public UpdateTodoItemUseCaseTestDataBuilder WithItems(List<TodoItemTo> items)
        {
            _items = items;
            return this;
        }

        public TodoTestContext<IUpdateTodoItemUseCase, ITodoRepository> Build()
        {
            var repository = CreateTodoRepository();
            return CreateTodoTestContext(repository);
        }

        private TodoTestContext<IUpdateTodoItemUseCase, ITodoRepository> CreateTodoTestContext(ITodoRepository repository)
        {
            var usecase = new UpdateTodoItemUseCase(repository);

            return new TodoTestContext<IUpdateTodoItemUseCase, ITodoRepository>
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