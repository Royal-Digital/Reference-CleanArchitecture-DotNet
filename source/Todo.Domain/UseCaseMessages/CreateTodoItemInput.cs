using System;

namespace Todo.Domain.UseCaseMessages
{
    public class CreateTodoItemInput
    {
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
    }
}