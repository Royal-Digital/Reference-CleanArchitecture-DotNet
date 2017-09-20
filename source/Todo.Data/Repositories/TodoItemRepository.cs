using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using Todo.Data.Context;
using Todo.Data.Entities;
using Todo.Domain.Model;
using Todo.Domain.Repository;

namespace Todo.Data.Repositories
{
    public class TodoItemRepository : ITodoRepository
    {
        private readonly TodoContext _dbContext;
        private readonly IMapper _mapper;

        public TodoItemRepository(TodoContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public TodoItemModel CreateItem(TodoItemModel input)
        {
            var entity = _mapper.Map<TodoItem>(input);

            _dbContext.TodoItem.Add(entity);
            var itemModel = _mapper.Map<TodoItemModel>(entity);
            return itemModel;
        }

        public List<TodoItemModel> FetchAll()
        {
            var result = new List<TodoItemModel>();
            _dbContext.TodoItem.ToList().ForEach(item =>
            {
                result.Add(_mapper.Map<TodoItemModel>(item));
            });
            return result;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(TodoItemModel model)
        {
            var entity = _mapper.Map<TodoItem>(model);
            _dbContext.TodoItem.AddOrUpdate(entity);
        }
    }
}
