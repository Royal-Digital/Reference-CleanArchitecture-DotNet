using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using TddBuddy.DateTime.Extensions;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Create;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Fetch.Filtered;
using Todo.Boundary.Todo.Update;
using Todo.Data.Comment;

namespace Todo.Data.Todo
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _dbContext;
        private readonly IMapper _mapper;

        public TodoRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = CreateAutoMapper();
        }

        public Guid Create(CreateTodoInput item)
        {
            var entity = _mapper.Map<TodoItemEntityFrameworkModel>(item);

            _dbContext.TodoItem.Add(entity);
            return entity.Id;
        }

        public List<TodoTo> FetchAll()
        {
            var result = new List<TodoTo>();
            MapEfEntityToTransferObject(result);
            return result;
        }

        private void MapEfEntityToTransferObject(List<TodoTo> result)
        {
            _dbContext.TodoItem.Include(item => item.Comments).ToList()
                .ForEach(item => { result.Add(_mapper.Map<TodoTo>(item)); });
        }

        public void Persist()
        {
            _dbContext.SaveChanges();
        }

        public void Update(UpdateTodoInput item)
        {
            var entity = MapToEntity(item);
            _dbContext.TodoItem.AddOrUpdate(entity);
        }

        public bool MarkForDelete(Guid id)
        {
            var entity = LocateEntityById(id);

            if (EntityIsNull(entity)) return false;

            MarkEntityAsDeleted(entity);
            return true;
        }

        public TodoTo FindById(Guid id)
        {
            var entity = LocateEntityById(id);
            return IfCouldNotFindTodoItem(entity) ? null : CreateOuput(entity);
        }

        public List<TodoTo> FetchFiltered(TodoFilterInput todoFilterInput)
        {
            if (todoFilterInput.IncludedCompleted)
            {
                return FetchAll();
            }
            
            // todo : extract a predicate builder to better implement the filter logic
            var result = new List<TodoTo>();
            FetchIncompleteItems(result);
            return result;
        }

        private void FetchIncompleteItems(List<TodoTo> result)
        {
            _dbContext.TodoItem
                .Where(x => x.IsCompleted == false)
                .Include(item => item.Comments).ToList()
                .ForEach(item => { result.Add(_mapper.Map<TodoTo>(item)); });
        }

        private TodoTo CreateOuput(TodoItemEntityFrameworkModel entity)
        {
            return _mapper.Map<TodoTo>(entity);
        }

        private bool IfCouldNotFindTodoItem(TodoItemEntityFrameworkModel entity)
        {
            return entity == null;
        }

        private TodoItemEntityFrameworkModel MapToEntity(UpdateTodoInput item)
        {
            var entity = _mapper.Map<TodoItemEntityFrameworkModel>(item);
            entity.Id = item.Id;
            return entity;
        }

        private IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItemEntityFrameworkModel, TodoTo>()
                    .ForMember(m => m.DueDate,
                        opt => opt.ResolveUsing(src => src.DueDate?.ConvertTo24HourFormatWithSeconds()))
                    .ForMember(m => m.Comments, opt => opt.MapFrom(src => src.Comments));
                cfg.CreateMap<CommentEntityFrameworkModel, TodoCommentTo>();
                cfg.CreateMap<CreateTodoInput, TodoItemEntityFrameworkModel>();
                cfg.CreateMap<UpdateTodoInput, TodoItemEntityFrameworkModel>().ForMember(m => m.Id, opt => opt.Ignore());
            });

            return new Mapper(configuration);
        }

        private bool EntityIsNull(TodoItemEntityFrameworkModel entity)
        {
            return entity == null;
        }

        private void MarkEntityAsDeleted(TodoItemEntityFrameworkModel entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        private TodoItemEntityFrameworkModel LocateEntityById(Guid id)
        {
            var entity = _dbContext.TodoItem.FirstOrDefault(x => x.Id == id);
            return entity;
        }
    }
}
