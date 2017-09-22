using System;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Todo.AutoMapper;
using Todo.Data.Context;
using Todo.Data.EfModels;
using Todo.Domain.Repository;
using Todo.Entities;

namespace Todo.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TodoContext _dbContext;
        private readonly IMapper _mapper;

        public CommentRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = CreateAutoMapper();
        }

        public TodoComment Create(TodoComment domainModel)
        {
            var entity = _mapper.Map<CommentEfModel>(domainModel);
            _dbContext.Comments.Add(entity);

            var domainEntity = _mapper.Map<TodoComment>(entity);
            return domainEntity;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public bool Delete(TodoComment domainModel)
        {
            var entity = LocateEntityById(domainModel.Id);

            if (EntityIsNotNull(entity))
            {
                MarkEntityAsDeleted(entity);
                return true;
            }

            return false;
        }

        private CommentEfModel LocateEntityById(Guid id)
        {
            var entity = _dbContext.Comments.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        private bool EntityIsNotNull(CommentEfModel entity)
        {
            return entity != null;
        }

        private void MarkEntityAsDeleted(CommentEfModel entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TodoComment, CommentEfModel>().ForMember(m => m.Id, opt => opt.Ignore());
                    cfg.CreateMap<CommentEfModel, TodoComment>();
                }))
                .Build();
        }
    }
}