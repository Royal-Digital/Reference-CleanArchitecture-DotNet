using System;

namespace Todo.DomainEntities
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

            return HasDueDateHappened();
        }

        private bool HasDueDateHappened()
        {
            return DateTime.Now.Date.CompareTo(DueDate.Date) > 0;
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
