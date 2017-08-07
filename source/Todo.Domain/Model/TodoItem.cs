using System;

namespace Todo.Domain.Model
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string ItemDescription { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool IsCompleted { get; set; }

        public bool IsOverdue()
        {
            if (IsCompleted)
            {
                return false;
            }

            return DateTime.Now.CompareTo(CompletionDate) > 0;
        }
    }
}
