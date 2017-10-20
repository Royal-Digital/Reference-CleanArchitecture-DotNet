using System;
using NSubstitute;
using Todo.Boundary.Comment;
using Todo.Boundary.Comment.Delete;
using Todo.Domain.Comment.Delete;

namespace Todo.Domain.Tests.Comment.Delete
{
    public class DeleteCommentUseCaseTestDataBuilder
    {
        private bool _canDelete;

        public DeleteCommentUseCaseTestDataBuilder WithDeleteResult(bool canDelete)
        {
            _canDelete = canDelete;
            return this;
        }
        
        public CommentTestContext<IDeleteCommentUseCase, ICommentRepository> Build()
        {
            var repository = Substitute.For<ICommentRepository>();
            repository.MarkForDelete(Arg.Any<Guid>()).Returns(_canDelete);
            var useCase = new DeleteCommentUseCase(repository);
            return new CommentTestContext<IDeleteCommentUseCase, ICommentRepository>  { UseCase = useCase, Repository = repository};
        }
    }
}