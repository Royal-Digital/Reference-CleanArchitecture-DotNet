using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Data.Entities
{
    public class TodoItem
    {
        public TodoItem()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [MaxLength(32)]
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
