﻿using System;
using System.Collections.Generic;
using Todo.Boundary.Todo.Create;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Fetch.Filtered;
using Todo.Boundary.Todo.Update;

namespace Todo.Boundary.Todo
{
    public interface ITodoRepository
    {
        Guid Create(CreateTodoInput item);
        void Update(UpdateTodoInput item);
        void Persist();
        List<TodoTo> FetchAll();
        bool MarkForDelete(Guid id);
        TodoTo FindById(Guid id);
        List<TodoTo> FetchFiltered(TodoFilterInput todoFilterInput);
    }
}