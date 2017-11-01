using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Fetch.Filtered;

namespace Todo.Domain.Todo.Fetch
{
    public class FetchFilteredTodoUseCase : IFetchFilteredTodoUseCase
    {
        private readonly ITodoRepository _repository;

        public FetchFilteredTodoUseCase(ITodoRepository repository)
        {
            _repository = repository;
        }

        public void Execute(TodoFilterInput inputTo, IRespondWithSuccessOrError<List<TodoTo>, ErrorOutputMessage> presenter)
        {
            if (inputTo == null)
            {
                RepsondWithNullFilter(presenter);
                return;
            }

            var filteredCollection = FetchFilteredCollection(inputTo);
            presenter.Respond(filteredCollection);
        }

        private List<TodoTo> FetchFilteredCollection(TodoFilterInput inputTo)
        {
            return _repository.FetchFiltered(inputTo);
        }

        private static void RepsondWithNullFilter(IRespondWithSuccessOrError<List<TodoTo>, ErrorOutputMessage> presenter)
        {
            var errors = new ErrorOutputMessage();
            errors.AddError("Null filter object");
            presenter.Respond(errors);
        }
    }
}