namespace Todo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment_AddedNotNullAndAdjustedMaxLengthForCommentField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comment", "Comment", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comment", "Comment", c => c.String(maxLength: 200));
        }
    }
}
