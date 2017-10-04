using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Fetch;

namespace Todo.Domain.Todo.Fetch
{
    public class FetchTodoCollectionUseCase : IFetchTodoCollectionUseCase
    {
        private readonly ITodoRepository _todoRepository;

        public FetchTodoCollectionUseCase(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void Execute(IRespondWithSuccessOrError<List<TodoItemTo>, ErrorOutputMessage> presenter)
        {
            var collection = FetchTodoItems();
            RespondWithSuccess(presenter, collection);
        }

        private void RespondWithSuccess(IRespondWithSuccessOrError<List<TodoItemTo>, ErrorOutputMessage> presenter, List<TodoItemTo> result)
        {
            presenter.Respond(result);
        }

        private List<TodoItemTo> FetchTodoItems()
        {
            var collection = _todoRepository.FetchAll();
            return collection;
        }
    }
}