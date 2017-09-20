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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItem, TodoItemModel>();
                cfg.CreateMap<TodoItemModel, TodoItem>();
                cfg.CreateMap<CreateTodoItemInput, TodoItem>();

            });

            return new Mapper(config);
        }
    }
}
