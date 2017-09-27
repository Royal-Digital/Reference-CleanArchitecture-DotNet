using System.Collections.Generic;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Boundry.Comment;
using Todo.Boundry.Repository;
using Todo.Boundry.Todo.Fetch;
using Todo.Domain.Comment;
using Todo.Utils;

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
            var result = ConvertToFetchTodoItemOutputs(collection);
            RespondWithSuccess(presenter, result);
        }

        private void RespondWithSuccess(IRespondWithSuccessOrError<List<FetchTodoItemOutput>, ErrorOutputMessage> presenter, List<FetchTodoItemOutput> result)
        {
            presenter.Respond(result);
        }

        private List<TodoItem> FetchPersistedTodoItems()
        {
            var collection = _todoRepository.FetchAll();
            return collection;
        }

        private List<FetchTodoItemOutput> ConvertToFetchTodoItemOutputs(List<TodoItem> todoItems)
        {
            var mapper = CreateAutoMapper();
            var result = new List<FetchTodoItemOutput>();
            todoItems.ForEach(item =>
            {
                var emitEntity = ConvertTodoItemToEmitEntity(mapper, item);
                AttachCommentToItem(item, mapper, emitEntity);
                result.Add(emitEntity);
            });
            return result;
        }

        private static FetchTodoItemOutput ConvertTodoItemToEmitEntity(IMapper mapper, TodoItem item)
        {
            var emitEntity = mapper.Map<FetchTodoItemOutput>(item);
            return emitEntity;
        }

        private void AttachCommentToItem(TodoItem item, IMapper mapper, FetchTodoItemOutput domainEntity)
        {
            var comments = _commentRepository.FindForItem(item.Id);
            var emitComments = new List<FetchTodoCommentOutput>();
            comments.ForEach(comment =>
            {
                var emitComment = mapper.Map<FetchTodoCommentOutput>(comment);
                emitComments.Add(emitComment);
            });
            domainEntity.Comments = emitComments;
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TodoItem, FetchTodoItemOutput>().ForMember(m=>m.DueDate, opt => opt.ResolveUsing(src => src.DueDate.ConvertTo24HourFormatWithSeconds()));
                    cfg.CreateMap<FetchTodoCommentOutput, TodoComment>();
                    cfg.CreateMap<TodoComment, FetchTodoCommentOutput>();
                }))
                .Build();
        }
    }
}