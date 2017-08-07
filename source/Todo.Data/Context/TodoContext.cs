using System.Data.Common;
using System.Data.Entity;
using Todo.Data.Entities;

namespace Todo.Data.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoConnectionString")
        {
            Database.SetInitializer<TodoContext>(null);
        }

        public TodoContext(DbConnection connection) : base(connection, false){}

        public IDbSet<TodoItem> TodoItem { get; set; }
    }
}
