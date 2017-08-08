namespace Todo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoItem_AddedCreatedModifiedFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoItems", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.TodoItems", "Modified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoItems", "Modified");
            DropColumn("dbo.TodoItems", "Created");
        }
    }
}
