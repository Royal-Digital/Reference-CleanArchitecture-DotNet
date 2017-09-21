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