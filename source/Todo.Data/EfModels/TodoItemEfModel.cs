using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Data.EfModels
{
    [Table("TodoItem")]
    public class TodoItemEfModel
    {
        public TodoItemEfModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [MaxLength(32)]
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public virtual ICollection<CommentEfModel> Comments { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
