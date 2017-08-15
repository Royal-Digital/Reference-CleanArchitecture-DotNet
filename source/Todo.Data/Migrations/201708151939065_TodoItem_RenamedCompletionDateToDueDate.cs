namespace Todo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoItem_RenamedCompletionDateToDueDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoItems", "DueDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.TodoItems", "CompletionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TodoItems", "CompletionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.TodoItems", "DueDate");
        }
    }
}
