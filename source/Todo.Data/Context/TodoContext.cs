using System.Data.Common;
using System.Data.Entity;
using TddBuddy.EntityFramework.Utils;
using Todo.Data.EfModels;

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

        public IDbSet<TodoItemEfModel> TodoItem { get; set; }
        public IDbSet<CommentEfModel> Comments { get; set; }
    }
}
