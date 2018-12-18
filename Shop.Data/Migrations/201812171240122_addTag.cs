namespace Shop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Tags", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Products", "Tags");
        }
    }
}