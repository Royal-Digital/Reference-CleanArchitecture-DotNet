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
        List<FetchTodoItemOutput> FetchAll();
        bool Delete(Guid id);
        FetchTodoItemOutput FindById(Guid id);
    }
}