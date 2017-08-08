using System.Data.Common;
using System.Data.Entity;
using TddBuddy.EntityFramework.Utils;
using Todo.Data.Entities;

namespace Todo.Data.Context
{
    [DbConfigurationType(typeof(CommonDbConfiguration))]
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoContext")
        {
            Database.SetInitializer<TodoContext>(null);
        }

        public TodoContext(DbConnection connection) : base(connection, false){}

        public IDbSet<TodoItem> TodoItem { get; set; }
    }
}
