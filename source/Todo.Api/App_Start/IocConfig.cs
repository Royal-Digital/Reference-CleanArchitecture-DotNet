using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Todo.Data.Context;
using Todo.Data.Repositories;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.UseCase;
using Todo.UseCase.Comment;
using Todo.UseCase.Todo;

namespace Todo.Api
{
    public static class IocConfig
    {
        public static void Configure(HttpConfiguration configuration)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.RegisterWebApiControllers(configuration);

            RegisterContext(container);
            RegisterRepositories(container);
            RegisterUseCases(container);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterRepositories(Container container)
        {
            container.Register<ITodoRepository, TodoItemRepository>();
            container.Register<ICommentRepository, CommentRepository>();
        }

        private static void RegisterUseCases(Container container)
        {
            container.Register<ICreateTodoItemUseCase, CreateTodoItemUseCase>();
            container.Register<IFetchTodoCollectionUseCase, FetchTodoCollectionUseCase>();
            container.Register<IDeleteTodoItemUseCase, DeleteTodoItemUseCase>();
            container.Register<IUpdateTodoItemUseCase, UpdateTodoItemUseCase>();

            container.Register<ICreateCommentUseCase, CreateCommentUseCase>();
            container.Register<IDeleteCommentUseCase, DeleteCommentUseCase>();
        }

        private static void RegisterContext(Container container)
        {
            container.Register<TodoContext>(() => new TodoContext(), Lifestyle.Scoped);
        }
    }
}