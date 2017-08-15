using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Todo.Data.Context;
using Todo.Data.Entities;
using Todo.Domain.Messages;
using Todo.Domain.Model;
using Todo.Domain.Repository;

namespace Todo.Data.Repositories
{
    public class TodoItemRepository : ITodoRepository
    {
        private readonly TodoContext _dbContext;

        public TodoItemRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TodoItemModel CreateItem(CreateTodoItemInputMessage inputMessage)
        {
            var entity = new TodoItem
            {
                ItemDescription = inputMessage.ItemDescription,
                DueDate = inputMessage.DueDate,
                IsCompleted = false
            };

            _dbContext.TodoItem.Add(entity);
            var itemModel = CreateTodoItemModel(entity);
            return itemModel;
        }

        public List<TodoItemModel> FetchAll()
        {
            var result = new List<TodoItemModel>();
            _dbContext.TodoItem.ToList().ForEach(item =>
            {
                ConvertEntityToModel(item, result);
            });
            return result;
        }

        //public void UpdateAudit(TodoItemModel todoItemModel)
        //{
        //    //_dbContext.Entry(todoItemModel).State = EntityState.Modified;
        //}

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(TodoItemModel model)
        {
            var entity = ConvertToEntity(model);
            _dbContext.TodoItem.AddOrUpdate(entity);
        }

        private void ConvertEntityToModel(TodoItem item, List<TodoItemModel> result)
        {
            result.Add(new TodoItemModel
            {
                Id = item.Id,
                ItemDescription = item.ItemDescription,
                DueDate = item.DueDate,
                IsCompleted = item.IsCompleted
            });
        }

        private TodoItemModel CreateTodoItemModel(TodoItem entity)
        {
            var itemModel = new TodoItemModel
            {
                Id = entity.Id
            };
            return itemModel;
        }

        private TodoItem ConvertToEntity(TodoItemModel model)
        {
            return new TodoItem
            {
                Id = model.Id,
                ItemDescription = model.ItemDescription,
                IsCompleted = model.IsCompleted,
                DueDate = model.DueDate
            };
        }
    }
}
