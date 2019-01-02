namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeedback1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
