namespace Todo.Domain.Tests.Todo
{
    public class TodoTestContext<TUseCase, TRepository>{

        public TUseCase UseCase { get; set; }
        public TRepository Repository { get; set; }
    }
}