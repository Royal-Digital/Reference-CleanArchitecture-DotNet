using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;

namespace Todo.Domain.Todo.Fetch
{
    public class FetchAllTodoUseCase : IFetchAllTodoUseCase
    {
        private readonly ITodoRepository _todoRepository;

        public FetchAllTodoUseCase(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void Execute(IRespondWithSuccessOrError<List<TodoTo>, ErrorOutputMessage> presenter)
        {
            var collection = FetchTodoItems();
            RespondWithSuccess(presenter, collection);
        }

        private void RespondWithSuccess(IRespondWithSuccessOrError<List<TodoTo>, ErrorOutputMessage> presenter, List<TodoTo> result)
        {
            presenter.Respond(result);
        }

        private List<TodoTo> FetchTodoItems()
        {
            var collection = _todoRepository.FetchAll();
            return collection;
        }
    }
}