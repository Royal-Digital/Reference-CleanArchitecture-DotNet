using System.Collections.Generic;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;

namespace Todo.UseCase
{
    public class FetchTodoCollectionUseCase : IFetchTodoCollectionUseCase
    {
        private readonly ITodoRepository _repository;

        public FetchTodoCollectionUseCase(ITodoRepository repository)
        {
            _repository = repository;
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
            var collection = _repository.FetchAll();
            return collection;
        }

        private List<FetchTodoItemOutput> ConvertToFetchTodoItemOutputs(List<TodoItem> collection)
        {
            var mapper = CreateAutoMapper();
            var result = new List<FetchTodoItemOutput>();
            collection.ForEach(item => { result.Add(mapper.Map<FetchTodoItemOutput>(item)); });
            return result;
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TodoItem, FetchTodoItemOutput>().ForMember(m=>m.DueDate, opt => opt.ResolveUsing(src => src.DueDate.ToString("yyyy-MM-dd")));
                }))
                .Build();
        }
    }
}