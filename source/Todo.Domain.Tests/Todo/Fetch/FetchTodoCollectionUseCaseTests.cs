using System;
using System.Collections.Generic;
using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.AutoMapper;
using Todo.Boundry.Comment;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Fetch;
using Todo.Domain.Comment;
using Todo.Domain.Todo;
using Todo.Domain.Todo.Fetch;
using Todo.Extensions;

namespace Todo.Domain.Tests.Todo.Fetch
{
    [TestFixture]
    public class FetchTodoCollectionUseCaseTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldReturnCollectionOfAllItems()
        {
            //---------------Arrange-------------------
            var itemModels = CreateTodoItems();
            var expected = CreateTodoOutputItems(itemModels[0].Id, itemModels[1].Id);
            var usecase = CreateFetchTodoCollectionUseCase(itemModels, expected);
            var presenter = new PropertyPresenter<List<TodoItemTo>, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(presenter);
            //---------------Assert-------------------
            AssertTodoItemsMatchExpected(expected, presenter);
        }

        private void AssertTodoItemsMatchExpected(IReadOnlyList<TodoItemTo> expected, PropertyPresenter<List<TodoItemTo>, ErrorOutputMessage> presenter)
        {
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, presenter.SuccessContent[i].Id);
                var expectedComments = expected[i].Comments;
                for (var z = 0; z < expectedComments.Count; z++)
                {
                    Assert.AreEqual(expected[i].Comments[z].Id, presenter.SuccessContent[i].Comments[z].Id);
                    Assert.AreEqual(expected[i].Comments[z].Comment, presenter.SuccessContent[i].Comments[z].Comment);
                }
            }
        }

        private List<TodoItemTo> CreateTodoOutputItems(Guid id1, Guid id2)
        {
            var itemModels = new List<TodoItemTo>
            {
                new TodoItemTo
                {
                    Id = id1,
                    ItemDescription = "task 1",
                    DueDate = DateTime.Today.ConvertTo24HourFormatWithSeconds(),
                    Comments = new List<TodoCommentTo>
                    {
                        new TodoCommentTo
                        {
                            Id = Guid.NewGuid(),
                            Comment = "a comment"
                        },
                        new TodoCommentTo
                        {
                            Id = Guid.NewGuid(),
                            Comment = "another comment"
                        }
                    }
                        
                },
                new TodoItemTo
                {
                    Id = id2,
                    ItemDescription = "task 2",
                    DueDate = DateTime.Today.ConvertTo24HourFormatWithSeconds(),
                    Comments = new List<TodoCommentTo>()
                }
            };

            return itemModels;
        }

        private List<TodoItemTo> CreateTodoItems()
        {
            var itemModels = new List<TodoItemTo>
            {
                new TodoItemTo {Id = Guid.NewGuid(), ItemDescription = "task 1", DueDate = DateTime.Today.ConvertTo24HourFormatWithSeconds()},
                new TodoItemTo {Id = Guid.NewGuid(), ItemDescription = "task 2", DueDate = DateTime.Today.ConvertTo24HourFormatWithSeconds()}
            };

            return itemModels;
        }

        private FetchTodoCollectionUseCase CreateFetchTodoCollectionUseCase(List<TodoItemTo> itemModels, List<TodoItemTo> expected)
        {
            var todoRepository = CreateTodoRepository(itemModels);
            var commentsRepository = CreateCommentsRepository(expected);

            return CreateUseCase(todoRepository, commentsRepository);
        }

        private FetchTodoCollectionUseCase CreateUseCase(ITodoRepository todoRepository, ICommentRepository commentsRepository)
        {
            var usecase = new FetchTodoCollectionUseCase(todoRepository, commentsRepository);
            return usecase;
        }

        private ICommentRepository CreateCommentsRepository(List<TodoItemTo> expected)
        {
            var commentsRepository = Substitute.For<ICommentRepository>();
            
            foreach (var item in expected)
            {
                SetupCommentsForEachTodoItem(item, commentsRepository);
            }
            return commentsRepository;
        }

        private void SetupCommentsForEachTodoItem(TodoItemTo item, ICommentRepository commentsRepository)
        {
            var mapper = CreateAutoMapper();
            var id = item.Id;
            var comments = item.Comments;

            var commentsToFind = ConvertCommentsToDomainEntities(comments, mapper);
            commentsRepository.FindForItem(id).Returns(new List<TodoCommentTo>());
        }

        private List<TodoComment> ConvertCommentsToDomainEntities(List<TodoCommentTo> comments, IMapper mapper)
        {
            var commentsToFind = new List<TodoComment>();
            comments.ForEach(comment =>
            {
                var convertedComment = mapper.Map<TodoComment>(comment);
                commentsToFind.Add(convertedComment);
            });
            return commentsToFind;
        }

        private ITodoRepository CreateTodoRepository(List<TodoItemTo> itemModels)
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(itemModels);

            return repository;
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TodoItem, TodoItemTo>().ForMember(m => m.DueDate, opt => opt.ResolveUsing(src => src.DueDate.ConvertTo24HourFormatWithSeconds()));
                    cfg.CreateMap<TodoCommentTo, TodoComment>();
                    cfg.CreateMap<TodoComment, TodoCommentTo>();
                }))
                .Build();
        }
    }
}
