using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Data.Entities
{
    public class TodoItem
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(32)]
        public string ItemDescription { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
