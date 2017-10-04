using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using TddBuddy.DateTime.Extensions;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Create;
using Todo.Boundry.Todo.Fetch;
using Todo.Boundry.Todo.Update;
using Todo.Data.Context;
using Todo.Data.EfModels;

namespace Todo.Data.Repositories
{
    public class TodoItemRepository : ITodoRepository
    {
        private readonly TodoContext _dbContext;
        private readonly IMapper _mapper;

        public TodoItemRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = CreateAutoMapper();
        }

        public Guid Create(CreateTodoItemInput item)
        {
            var entity = _mapper.Map<TodoItemEfModel>(item);

            _dbContext.TodoItem.Add(entity);
            return entity.Id;
        }

        public List<TodoItemTo> FetchAll()
        {
            var result = new List<TodoItemTo>();
            _dbContext.TodoItem.Include(item=>item.Comments).ToList().ForEach(item =>
            {
                result.Add(_mapper.Map<TodoItemTo>(item));
            });
            return result;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(UpdateTodoItemInput item)
        {
            var entity = MapToEntity(item);
            _dbContext.TodoItem.AddOrUpdate(entity);
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

        public TodoItemTo FindById(Guid id)
        {
            var entity = LocateEntityById(id);
            return IfCouldNotFindTodoItem(entity) ? null : CreateOuput(entity);
        }

        private TodoItemTo CreateOuput(TodoItemEfModel entity)
        {
            return _mapper.Map<TodoItemTo>(entity);
        }

        private bool IfCouldNotFindTodoItem(TodoItemEfModel entity)
        {
            return entity == null;
        }

        private TodoItemEfModel MapToEntity(UpdateTodoItemInput item)
        {
            var entity = _mapper.Map<TodoItemEfModel>(item);
            entity.Id = item.Id;
            return entity;
        }

        private IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItemEfModel, TodoItemTo>()
                    .ForMember(m => m.DueDate,
                        opt => opt.ResolveUsing(src => src.DueDate.ConvertTo24HourFormatWithSeconds()))
                    .ForMember(m => m.Comments, opt => opt.MapFrom(src => src.Comments));
                cfg.CreateMap<CommentEfModel, TodoCommentTo>();
                cfg.CreateMap<CreateTodoItemInput, TodoItemEfModel>();
                cfg.CreateMap<UpdateTodoItemInput, TodoItemEfModel>().ForMember(m => m.Id, opt => opt.Ignore());
            });

            return new Mapper(configuration);
        }

        private bool EntityIsNotNull(TodoItemEfModel entity)
        {
            return entity != null;
        }

        private void MarkEntityAsDeleted(TodoItemEfModel entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        private TodoItemEfModel LocateEntityById(Guid id)
        {
            var entity = _dbContext.TodoItem.FirstOrDefault(x => x.Id == id);
            return entity;
        }
    }
}
