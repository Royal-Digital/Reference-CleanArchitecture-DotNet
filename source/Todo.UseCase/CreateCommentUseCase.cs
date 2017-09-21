using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;

namespace Todo.UseCase
{
    public class CreateCommentUseCase : ICreateCommentUseCase
    {
        private readonly ICommentRepository _repository;

        public CreateCommentUseCase(ICommentRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CreateCommentInput input, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            var domainEntity = CreateDomainModelFromInput(input);

            if (InvalidTodoItemId(domainEntity))
            {
                RespondWithErrorMessage("Invalid item Id", presenter);
                return;
            }

            if (InvalidComment(domainEntity)) 
            {
                RespondWithErrorMessage("Missing comment", presenter);
                return;
            }

            var updateEntity = PersistDomainEntity(domainEntity);

            RespondWithSuccess(updateEntity.Id, presenter);
        }

        private TodoComment PersistDomainEntity(TodoComment domainModel)
        {
            var updatedModel = _repository.Create(domainModel);
            _repository.Save();
            return updatedModel;
        }

        private TodoComment CreateDomainModelFromInput(CreateCommentInput input)
        {
            var mapper = CreateAutoMapper();
            var domainModel = mapper.Map<TodoComment>(input);
            return domainModel;
        }

        private void RespondWithSuccess(Guid commentId, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(
                new CreateCommentOuput {Id = commentId});
        }

        public void RespondWithErrorMessage(string message, IRespondWithSuccessOrError<CreateCommentOuput, ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError(message);
            presenter.Respond(errorOutputMessage);
        }

        private bool InvalidComment(TodoComment domainModel)
        {
            return !domainModel.IsCommentValid();
        }

        private bool InvalidTodoItemId(TodoComment domainModel)
        {
            return !domainModel.IsTodoItemIdValid();
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateCommentInput, TodoComment>();
                }))
                .Build();
        }
    }
}