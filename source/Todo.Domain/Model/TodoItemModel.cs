using System;

namespace Todo.Domain.Model
{
    public class TodoItemModel
    {
        public Guid Id { get; set; }
        public string ItemDescription { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool IsCompleted { get; set; }

        public bool IsOverdue()
        {
            if (IsCompleted)
            {
                return false;
            }

            return DateTime.Now.CompareTo(CompletionDate) >= 0;
        }

        public override bool Equals(object obj)
        {
            return Id != Guid.Empty && Id == (obj as TodoItemModel).Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
