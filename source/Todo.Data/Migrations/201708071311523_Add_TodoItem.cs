namespace Todo.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Add_TodoItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemDescription = c.String(maxLength: 32),
                        CompletionDate = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TodoItems");
        }
    }
}
