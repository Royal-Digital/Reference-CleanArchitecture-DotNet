﻿using System.Collections.Generic;
using System.Web.Http;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Presenters;
using Todo.Domain.Model;
using Todo.Domain.UseCase;

namespace Todo.Api.Controllers
{
    [RoutePrefix("todo")]
    public class FetchTodoItemController : ApiController
    {
        private readonly IFetchTodoCollectionUseCase _useCase;

        public FetchTodoItemController(IFetchTodoCollectionUseCase useCase)
        {
            _useCase = useCase;
        }

        [Route("fetch/all")]
        [HttpGet]
        public IHttpActionResult Execute()
        {
            var presenter = new SuccessOrErrorRestfulPresenter<List<TodoItemModel>, ErrorOutputMessage>(this);

            _useCase.Execute(presenter);

            return presenter.Render();
        }
    }
}