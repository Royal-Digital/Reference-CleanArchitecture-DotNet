namespace Todo.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class CommentTable_AddedFkToTodoItem : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Comment", "TodoItemId");
            AddForeignKey("dbo.Comment", "TodoItemId", "dbo.TodoItem", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "TodoItemId", "dbo.TodoItem");
            DropIndex("dbo.Comment", new[] { "TodoItemId" });
        }
    }
}
