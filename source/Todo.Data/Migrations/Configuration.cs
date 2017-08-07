using System.Data.Entity.Migrations;
using Todo.Data.Context;

namespace Todo.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TodoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
