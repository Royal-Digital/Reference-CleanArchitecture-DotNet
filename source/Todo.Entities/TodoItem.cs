using System;

namespace Todo.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public bool IsOverdue()
        {
            if (IsCompleted)
            {
                return false;
            }

            return DateTime.Now.CompareTo(DueDate) >= 0;
        }
        
        public bool IsIdValid()
        {
            return Id != Guid.Empty;
        }

        public bool ItemDescriptionIsValid()
        {
            return !string.IsNullOrWhiteSpace(ItemDescription);
        }
    }
}
