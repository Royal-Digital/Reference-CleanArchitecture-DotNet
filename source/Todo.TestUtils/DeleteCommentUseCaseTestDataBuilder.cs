using NSubstitute;
using Todo.Boundry.Repository;
using Todo.Boundry.UseCase;
using Todo.Domain.Entities;
using Todo.UseCase.Comment;

namespace Todo.TestUtils
{
    public class DeleteCommentUseCaseTestDataBuilder
    {
        private bool _canDelete;

        public DeleteCommentUseCaseTestDataBuilder WithDeleteResult(bool canDelete)
        {
            _canDelete = canDelete;
            return this;
        }


        public IDeleteCommentUseCase Build()
        {
            var repository = Substitute.For<ICommentRepository>();
            repository.Delete(Arg.Any<TodoComment>()).Returns(_canDelete);
            var useCase = new DeleteCommentUseCase(repository);
            return useCase;
        }
    }
}