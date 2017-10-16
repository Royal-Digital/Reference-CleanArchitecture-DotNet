using System;
using System.Collections.Generic;

namespace Todo.Boundary.Todo.Fetch
{
    public class TodoTo
    {
        public Guid Id { get; set; }
        public string ItemDescription { get; set; }
        public string DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public List<TodoCommentTo> Comments { get; set; }

        public TodoTo()
        {
            Comments = new List<TodoCommentTo>();
        }
    }
}
