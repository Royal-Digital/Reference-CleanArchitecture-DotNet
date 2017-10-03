using System;
using System.Collections.Generic;

namespace Todo.Boundry.Todo.Fetch
{
    public class TodoItemTo
    {
        public Guid Id { get; set; }
        public string ItemDescription { get; set; }
        public string DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public List<FetchTodoCommentOutput> Comments { get; set; }
    }
}
