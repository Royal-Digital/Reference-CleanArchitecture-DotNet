﻿using System;

namespace Todo.Domain.Comment
{
    public class TodoComment
    {
        public Guid Id { get; set; }
        public Guid TodoItemId { get; set; }
        public string Comment { get; set; }
        public string Created { get; set; }

        public bool IsCommentValid()
        {
            return !string.IsNullOrWhiteSpace(Comment);
        }

        public bool IsTodoItemIdValid()
        {
            return TodoItemId != Guid.Empty;
        }

        public bool IsIdValid()
        {
            return Id != Guid.Empty;
        }
    }
}