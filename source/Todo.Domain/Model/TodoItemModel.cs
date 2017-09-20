using System;

namespace Todo.Domain.Model
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class TodoItemModel
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
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

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            return IsIdValid() && Id == (obj as TodoItemModel).Id;
        }
    }
}
