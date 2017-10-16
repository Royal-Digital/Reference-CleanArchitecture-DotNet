using System;
using NSubstitute;
using Todo.Boundry.Comment;
using Todo.Boundry.Comment.Create;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Fetch;
using Todo.Domain.Comment.Create;

namespace Todo.Domain.Tests.Comment.Create
{
    public class CreateCommentUseCaseTestDataBuilder
    {
        private Guid _commentId;
        private TodoItemTo _todoItemTo;

        public CreateCommentUseCaseTestDataBuilder()
        {
            _todoItemTo = new TodoItemTo();
        }

        public CreateCommentUseCaseTestDataBuilder WithCommentId(Guid id)
        {
            _commentId = id;
            return this;
        }

        public CreateCommentUseCaseTestDataBuilder WithTodoItemTo(TodoItemTo item)
        {
            _todoItemTo = item;
            return this;
        }

        public CommentTestContext<ICreateCommentUseCase, ICommentRepository> Build()
        {
            var commentRepository = CreateCommentRepository();
            var todoRepository = CreateTodoRepository();
            var usecase = new CreateCommentUseCase(commentRepository, todoRepository);

            return new CommentTestContext<ICreateCommentUseCase, ICommentRepository> { UseCase = usecase, Repository = commentRepository};
        }

        private ICommentRepository CreateCommentRepository()
        {
            var respository = Substitute.For<ICommentRepository>();
            respository.Create(Arg.Any<CreateCommentInput>()).Returns(_commentId);

            return respository;
        }

        private ITodoRepository CreateTodoRepository()
        {
            var respository = Substitute.For<ITodoRepository>();
            respository.FindById(Arg.Any<Guid>()).Returns(_todoItemTo);

            return respository;
        }

    }
}
