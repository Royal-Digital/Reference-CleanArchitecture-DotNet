using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using TddBuddy.DateTime.Extensions;
using Todo.AutoMapper;
using Todo.Boundry.Comment;
using Todo.Boundry.Comment.Create;
using Todo.Boundry.Todo.Fetch;
using Todo.Data.Context;
using Todo.Data.EfModels;

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

        public Guid Create(CreateCommentInput message)
        {
            var entity = _mapper.Map<CommentEfModel>(message);
            _dbContext.Comments.Add(entity);
            return entity.Id;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var entity = LocateEntityById(id);

            if (EntityIsNotNull(entity))
            {
                MarkEntityAsDeleted(entity);
                return true;
            }

            return false;
        }

        public List<TodoCommentTo> FindForItem(Guid itemId)
        {
            var efEntities = GetCommentEfEntities(itemId);
            var result = ConvertToTransferObject(efEntities);
            return result;
        }

        private IOrderedEnumerable<CommentEfModel> GetCommentEfEntities(Guid itemId)
        {
            var efEntities = _dbContext.Comments.Where(c => c.TodoItemId == itemId)
                .ToList()
                .OrderBy(x => x.Created.TimeOfDay);
            return efEntities;
        }

        private List<TodoCommentTo> ConvertToTransferObject(IEnumerable<CommentEfModel> efEntities)
        {
            return efEntities.Select(efEntity => _mapper.Map<TodoCommentTo>(efEntity)).ToList();
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
                    cfg.CreateMap<CreateCommentInput, CommentEfModel>()
                        .ForMember(m => m.Id, opt => opt.Ignore());
                    cfg.CreateMap<CommentEfModel, TodoCommentTo>()
                        .ForMember(x => x.Created, opt => opt.ResolveUsing(src => src.Created.ConvertTo24HourFormatWithSeconds()));
                }))
                .Build();
        }
    }
}