namespace Todo.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Comment_CreateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TodoItemId = c.Guid(nullable: false),
                        Comment = c.String(maxLength: 200),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comment");
        }
    }
}
