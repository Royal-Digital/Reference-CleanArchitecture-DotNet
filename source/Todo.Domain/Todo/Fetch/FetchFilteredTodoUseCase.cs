using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundary;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;

namespace Todo.Controllers.Web.Tests.Todo
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
            throw new System.NotImplementedException();
        }
    }
}