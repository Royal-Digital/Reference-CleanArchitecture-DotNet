using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.Model;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;

namespace Todo.UseCase
{
    public class FetchTodoCollectionUseCase : IFetchTodoCollectionUseCase
    {
        private readonly ITodoRepository _repository;

        public FetchTodoCollectionUseCase(ITodoRepository repository)
        {
            _repository = repository;
        }

        public void Execute(IRespondWithSuccessOrError<List<TodoItemModel>, ErrorOutputMessage> presenter)
        {
            var collection = _repository.FetchAll();
            presenter.Respond(collection);
        }
    }
}