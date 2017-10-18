using System.Data.Entity.Migrations;

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
