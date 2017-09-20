namespace Todo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoItem_CreateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemDescription = c.String(maxLength: 32),
                        DueDate = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {   
            DropTable("dbo.TodoItem");
        }
    }
}
