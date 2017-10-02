using System.Collections.Generic;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Boundry.Comment;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Fetch;
using Todo.Domain.Comment;
using Todo.Extensions;

namespace Todo.Domain.Todo.Fetch
{
    public class FetchTodoCollectionUseCase : IFetchTodoCollectionUseCase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ICommentRepository _commentRepository;

        public FetchTodoCollectionUseCase(ITodoRepository todoRepository, ICommentRepository commentRepository)
        {
            _todoRepository = todoRepository;
            _commentRepository = commentRepository;
        }

        public void Execute(IRespondWithSuccessOrError<List<FetchTodoItemOutput>, ErrorOutputMessage> presenter)
        {
            var collection = FetchPersistedTodoItems();
            RespondWithSuccess(presenter, collection);
        }

        private void RespondWithSuccess(IRespondWithSuccessOrError<List<FetchTodoItemOutput>, ErrorOutputMessage> presenter, List<FetchTodoItemOutput> result)
        {
            presenter.Respond(result);
        }

        private List<FetchTodoItemOutput> FetchPersistedTodoItems()
        {
            var collection = _todoRepository.FetchAll();
            return collection;
        }
    }
}