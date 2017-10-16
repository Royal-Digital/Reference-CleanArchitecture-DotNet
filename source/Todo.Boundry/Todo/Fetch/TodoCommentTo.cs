using System;

namespace Todo.Boundary.Todo.Fetch
{
    public class TodoCommentTo
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public string Created { get; set; }
    }
}