using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using TddBuddy.DateTime.Extensions;
using Todo.Boundary;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Create;
using Todo.Boundary.Todo.Fetch;
using Todo.Boundary.Todo.Fetch.Filtered;
using Todo.Boundary.Todo.Update;
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

        public Guid Create(CreateTodoInput item)
        {
            var entity = _mapper.Map<TodoItemEfModel>(item);

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

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(UpdateTodoInput item)
        {
            var entity = MapToEntity(item);
            _dbContext.TodoItem.AddOrUpdate(entity);
        }

        public bool Delete(Guid id)
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

        private TodoTo CreateOuput(TodoItemEfModel entity)
        {
            return _mapper.Map<TodoTo>(entity);
        }

        private bool IfCouldNotFindTodoItem(TodoItemEfModel entity)
        {
            return entity == null;
        }

        private TodoItemEfModel MapToEntity(UpdateTodoInput item)
        {
            var entity = _mapper.Map<TodoItemEfModel>(item);
            entity.Id = item.Id;
            return entity;
        }

        private IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItemEfModel, TodoTo>()
                    .ForMember(m => m.DueDate,
                        opt => opt.ResolveUsing(src => src.DueDate?.ConvertTo24HourFormatWithSeconds()))
                    .ForMember(m => m.Comments, opt => opt.MapFrom(src => src.Comments));
                cfg.CreateMap<CommentEfModel, TodoCommentTo>();
                cfg.CreateMap<CreateTodoInput, TodoItemEfModel>();
                cfg.CreateMap<UpdateTodoInput, TodoItemEfModel>().ForMember(m => m.Id, opt => opt.Ignore());
            });

            return new Mapper(configuration);
        }

        private bool EntityIsNull(TodoItemEfModel entity)
        {
            return entity == null;
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
