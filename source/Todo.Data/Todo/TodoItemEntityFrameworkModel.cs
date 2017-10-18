using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo.Data.Comment;

namespace Todo.Data.Todo
{
    [Table("TodoItem")]
    public class TodoItemEntityFrameworkModel
    {
        public TodoItemEntityFrameworkModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(2500)]
        [Required]
        public string ItemDescription { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public virtual ICollection<CommentEntityFrameworkModel> Comments { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
