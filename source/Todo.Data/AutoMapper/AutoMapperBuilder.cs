using AutoMapper;
using Todo.Data.Entities;
using Todo.Domain.Messages;
using Todo.Domain.Model;

namespace Todo.Data.AutoMapper
{
    public class AutoMapperBuilder
    {
        public Mapper Build()
        {
            var config = CreateMapperConfiguration();

            return new Mapper(config);
        }

        private MapperConfiguration CreateMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItem, TodoItemModel>();
                cfg.CreateMap<TodoItemModel, TodoItem>();
                cfg.CreateMap<CreateTodoItemInput, TodoItemModel>();
                cfg.CreateMap<UpdateTodoItemInput, TodoItemModel>();
            });

            return config;
        }
    }
}
