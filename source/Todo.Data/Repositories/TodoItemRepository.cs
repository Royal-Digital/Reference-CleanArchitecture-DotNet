using System;
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

        public void CreateItem(CreateTodoItemInputMessage inputMessage)
        {
            var entity = new TodoItem
            {
                ItemDescription = inputMessage.ItemDescription,
                CompletionDate = inputMessage.CompletionDate,
                IsCompleted = false
            };

            _dbContext.TodoItem.Add(entity);
        }

        public void UpdateAudit(TodoItemModel todoItemModel)
        {
            //_dbContext.Entry(todoItemModel).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
