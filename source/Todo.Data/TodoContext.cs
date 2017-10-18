using System.Data.Common;
using System.Data.Entity;
using TddBuddy.EntityFramework.Utils;
using Todo.Data.Comment;
using Todo.Data.Todo;

namespace Todo.Data
{
    [DbConfigurationType(typeof(CommonDbConfiguration))]
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoContext")
        {
            Database.SetInitializer<TodoContext>(null);
        }

        public TodoContext(DbConnection connection) : base(connection, false){}

        public IDbSet<TodoItemEntityFrameworkModel> TodoItem { get; set; }
        public IDbSet<CommentEntityFrameworkModel> Comments { get; set; }
    }
}
