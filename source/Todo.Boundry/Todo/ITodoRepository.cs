using System;
using System.Collections.Generic;
using Todo.Boundry.Todo.Create;
using Todo.Boundry.Todo.Fetch;
using Todo.Boundry.Todo.Update;

namespace Todo.Boundry.Todo
{
    public interface ITodoRepository
    {
        Guid Create(CreateTodoItemInput item);
        void Update(UpdateTodoItemInput item);
        void Save();
        List<TodoItemTo> FetchAll();
        bool Delete(Guid id);
        TodoItemTo FindById(Guid id);
    }
}