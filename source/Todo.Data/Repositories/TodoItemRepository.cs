using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using Todo.AutoMapper;
using Todo.Data.Context;
using Todo.Data.EfModels;
using Todo.Domain.Repository;
using Todo.Entities;

namespace Todo.Data.Repositories
{
    public class TodoItemRepository : ITodoRepository
    {
        private readonly TodoContext _dbContext;
        private readonly IMapper _mapper;

        public TodoItemRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = new AutoMapperBuilder()
                    .WithConfiguration(new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<TodoItemEfModel, TodoItem>();
                        cfg.CreateMap<TodoItem, TodoItemEfModel>();
                    }))
                    .Build();
        }

        public TodoItem CreateItem(TodoItem input)
        {
            var entity = _mapper.Map<TodoItemEfModel>(input);

            _dbContext.TodoItem.Add(entity);
            var itemModel = _mapper.Map<TodoItem>(entity);
            return itemModel;
        }

        public List<TodoItem> FetchAll()
        {
            var result = new List<TodoItem>();
            _dbContext.TodoItem.ToList().ForEach(item =>
            {
                result.Add(_mapper.Map<TodoItem>(item));
            });
            return result;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(TodoItem model)
        {
            var entity = _mapper.Map<TodoItemEfModel>(model);
            _dbContext.TodoItem.AddOrUpdate(entity);
        }
    }
}
