using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;

namespace Todo.UseCase.Tests
{
    [TestFixture]
    public class CreateCommentUseCaseTests
    {
        [Test]
        public void Execute_WhenValidTodoItemId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var persitedEntity = new TodoComment {Id = id, TodoItemId = itemId, Comment = "a comment"};

            var usecase = CreateCreateCommentUseCaseWithPersistedComment(persitedEntity);
            var input = new CreateCommentInput {TodoItemId = itemId, Comment = "a comment"};
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.AreEqual(id, presenter.SuccessContent.Id);
        }

        [Test]
        public void Execute_WhenInvalidTodoItemId_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var usecase = CreateCreateCommentUseCase();
            var input = new CreateCommentInput { TodoItemId = Guid.Empty, Comment = "a comment" };
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Invalid item Id", presenter.ErrorContent.Errors[0]);
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public void Execute_WhenInvalidComment_ShouldReturnError(string comment)
        {
            //---------------Arrange-------------------
            var usecase = CreateCreateCommentUseCase();
            var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = comment };
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Missing comment", presenter.ErrorContent.Errors[0]);
        }

        [Test]
        public void Execute_WhenTodoItemIdNotFound_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var usecase = CreateCreateCommentUseCaseWithMissingTodoItemCase();
            var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = "a comment" };
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Invalid item Id", presenter.ErrorContent.Errors[0]);
        }

        private CreateCommentUseCase CreateCreateCommentUseCaseWithPersistedComment(TodoComment comment)
        {
            var repository = Substitute.For<ICommentRepository>();
            repository.Create(Arg.Any<TodoComment>()).Returns(comment);
            var todoItemRepository = CreateTodoItemRepository();
            return CreateCommentUseCaseWithRepository(repository, todoItemRepository);
        }

        private CreateCommentUseCase CreateCreateCommentUseCaseWithMissingTodoItemCase()
        {
            var repository = Substitute.For<ICommentRepository>();
            var todoItemRepository = Substitute.For<ITodoRepository>();
            todoItemRepository.FindById(Arg.Any<Guid>()).Returns((TodoItem)null);
            return CreateCommentUseCaseWithRepository(repository, todoItemRepository);
        }

        private CreateCommentUseCase CreateCreateCommentUseCase()
        {
            var repository = Substitute.For<ICommentRepository>();
            var todoItemRepository = CreateTodoItemRepository();
            return CreateCommentUseCaseWithRepository(repository, todoItemRepository);
        }

        private CreateCommentUseCase CreateCommentUseCaseWithRepository(ICommentRepository repository, ITodoRepository todoItemRepository)
        {
            var usecase = new CreateCommentUseCase(repository, todoItemRepository);
            return usecase;
        }

        private ITodoRepository CreateTodoItemRepository()
        {
            var todoItemRepository = Substitute.For<ITodoRepository>();
            todoItemRepository.FindById(Arg.Any<Guid>()).Returns(new TodoItem());
            return todoItemRepository;
        }
    }
}
