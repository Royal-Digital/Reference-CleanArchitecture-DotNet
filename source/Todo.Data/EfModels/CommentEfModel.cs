using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Data.EfModels
{
    [Table("Comment")]
    public class CommentEfModel
    {
        public CommentEfModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public Guid TodoItemId { get; set; }

        [MaxLength(200)]
        public string Comment { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
