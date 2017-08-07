using NSubstitute;
using Todo.Domain.Messages;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.UseCase;

namespace Todo.TestUtils
{
    public class CreateTodoUseCaseTestDataBuilder
    {
        private string _id;

        public CreateTodoUseCaseTestDataBuilder()
        {
            _id = string.Empty;
        }

        public CreateTodoUseCaseTestDataBuilder WithCreateId(string id)
        {
            _id = id;
            return this;
        }

        public ICreateTodoItemUseCase Build()
        {
            var respository = Substitute.For<ITodoRepository>();
            respository
                .CreateTodoItem(Arg.Any<CreateTodoItemInputMessage>())
                .Returns(_id);

            var usecase = new CreateTodoItemUseCase(respository);
            return usecase;
        }
    }
}
