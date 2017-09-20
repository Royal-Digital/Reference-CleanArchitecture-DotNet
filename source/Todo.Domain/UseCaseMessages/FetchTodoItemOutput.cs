using System;

namespace Todo.Domain.UseCaseMessages
{
    public class FetchTodoItemOutput
    {
        public Guid Id { get; set; }
        public string ItemDescription { get; set; }
        public string DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
