using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using TddBuddy.DateTime.Extensions;
using Todo.Boundary.Comment;
using Todo.Boundary.Comment.Create;
using Todo.Boundary.Todo.Fetch;

namespace Todo.Data.Comment
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
            var entity = _mapper.Map<CommentEntityFrameworkModel>(message);
            _dbContext.Comments.Add(entity);
            return entity.Id;
        }

        public void Persist()
        {
            _dbContext.SaveChanges();
        }

        public bool MarkForDelete(Guid id)
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

        private IOrderedEnumerable<CommentEntityFrameworkModel> GetCommentEfEntities(Guid itemId)
        {
            var efEntities = _dbContext.Comments.Where(c => c.TodoItemId == itemId)
                .ToList()
                .OrderBy(x => x.Created.TimeOfDay);
            return efEntities;
        }

        private List<TodoCommentTo> ConvertToTransferObject(IEnumerable<CommentEntityFrameworkModel> efEntities)
        {
            return efEntities.Select(efEntity => _mapper.Map<TodoCommentTo>(efEntity)).ToList();
        }

        private CommentEntityFrameworkModel LocateEntityById(Guid id)
        {
            var entity = _dbContext.Comments.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        private bool EntityIsNotNull(CommentEntityFrameworkModel entity)
        {
            return entity != null;
        }

        private void MarkEntityAsDeleted(CommentEntityFrameworkModel entity) 
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        private IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCommentInput, CommentEntityFrameworkModel>()
                    .ForMember(m => m.Id, opt => opt.Ignore());
                cfg.CreateMap<CommentEntityFrameworkModel, TodoCommentTo>()
                    .ForMember(x => x.Created,
                        opt => opt.ResolveUsing(src => src.Created.ConvertTo24HourFormatWithSeconds()));
            });
            return new Mapper(configuration);
        }
    }
}