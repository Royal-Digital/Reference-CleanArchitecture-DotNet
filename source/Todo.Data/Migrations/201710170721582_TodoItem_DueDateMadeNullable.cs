namespace Todo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoItem_DueDateMadeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TodoItem", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TodoItem", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
