﻿using System;
using NSubstitute;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Entities;
using Todo.UseCase;

namespace Todo.TestUtils
{
    public class CreateCommentUseCaseTestDataBuilder
    {
        private TodoComment _comment;
        private TodoItem _item;

        public CreateCommentUseCaseTestDataBuilder()
        {
            _comment = new TodoComment();
            _item = new TodoItem();
        }

        public CreateCommentUseCaseTestDataBuilder WithComment(TodoComment comment)
        {
            _comment = comment;

            return this;
        }

        public CreateCommentUseCaseTestDataBuilder WithTodoItem(TodoItem item)
        {
            _item = item;

            return this;
        }

        public ICreateCommentUseCase Build()
        {
            var commentRepository = CreateCommentRepository();
            var todoRepository = CreateTodoRepository();
            var usecase = new CreateCommentUseCase(commentRepository, todoRepository);

            return usecase;
        }

        private ICommentRepository CreateCommentRepository()
        {
            var respository = Substitute.For<ICommentRepository>();
            respository.Create(Arg.Any<TodoComment>()).Returns(_comment);

            return respository;
        }

        private ITodoRepository CreateTodoRepository()
        {
            var respository = Substitute.For<ITodoRepository>();
            respository.FindById(Arg.Any<Guid>()).Returns(_item);

            return respository;
        }

    }
}
