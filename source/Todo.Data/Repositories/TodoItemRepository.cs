using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using Todo.AutoMapper;
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

        public List<FetchTodoItemOutput> FetchAll()
        {
            var result = new List<FetchTodoItemOutput>();
            _dbContext.TodoItem.ToList().ForEach(item =>
            {
                result.Add(_mapper.Map<FetchTodoItemOutput>(item));
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

        public FetchTodoItemOutput FindById(Guid id)
        {
            var entity = LocateEntityById(id);
            return IfCouldNotFindEfEntity(entity) ? null : CreateOuput(entity);
        }

        private FetchTodoItemOutput CreateOuput(TodoItemEfModel entity)
        {
            return _mapper.Map<FetchTodoItemOutput>(entity);
        }

        private bool IfCouldNotFindEfEntity(TodoItemEfModel entity)
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
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TodoItemEfModel, FetchTodoItemOutput>();
                    cfg.CreateMap<UpdateTodoItemInput, TodoItemEfModel>().ForMember(m=>m.Id, opt=>opt.Ignore());
                }))
                .Build();
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
