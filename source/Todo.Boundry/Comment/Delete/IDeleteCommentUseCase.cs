using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Comment.Delete
{
    public interface IDeleteCommentUseCase : IResultFreeAction<DeleteCommentInput>
    {
    }
}