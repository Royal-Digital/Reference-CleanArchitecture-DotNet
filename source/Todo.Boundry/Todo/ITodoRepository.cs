using System;
using System.Collections.Generic;
using Todo.Boundary.Todo.Create;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Update;

namespace Todo.Boundary.Todo
{
    public interface ITodoRepository
    {
        Guid Create(CreateTodoInput item);
        void Update(UpdateTodoItemInput item);
        void Save();
        List<TodoItemTo> FetchAll();
        bool Delete(Guid id);
        TodoItemTo FindById(Guid id);
    }
}