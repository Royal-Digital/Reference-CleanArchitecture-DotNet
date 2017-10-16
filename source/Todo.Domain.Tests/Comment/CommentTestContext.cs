namespace Todo.Domain.Tests.Comment
{
    public class CommentTestContext<TUseCase, TRepository>{

        public TUseCase UseCase { get; set; }
        public TRepository Repository { get; set; }
    }
}