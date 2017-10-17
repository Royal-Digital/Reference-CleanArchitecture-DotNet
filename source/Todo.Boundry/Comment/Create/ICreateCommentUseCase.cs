using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Comment.Create
{
    public interface ICreateCommentUseCase : IUseCase<CreateCommentInput, CreateCommentOutput>
    {
    }
}