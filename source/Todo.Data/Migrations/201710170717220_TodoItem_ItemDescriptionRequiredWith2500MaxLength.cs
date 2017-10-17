namespace Todo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoItem_ItemDescriptionRequiredWith2500MaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TodoItem", "ItemDescription", c => c.String(nullable: false, maxLength: 2500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TodoItem", "ItemDescription", c => c.String(maxLength: 32));
        }
    }
}
