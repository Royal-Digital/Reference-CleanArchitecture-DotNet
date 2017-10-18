using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo.Data.Todo;

namespace Todo.Data.Comment
{
    [Table("Comment")]
    public class CommentEntityFrameworkModel
    {
        public CommentEntityFrameworkModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid TodoItemId { get; set; }

        [ForeignKey("TodoItemId")]
        public TodoItemEntityFrameworkModel TodoItemEntityFrameworkModel { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Comment { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
