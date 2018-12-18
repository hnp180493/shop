namespace Shop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Pages", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.PostCategories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Posts", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.Products", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Products", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Products", "MetaKeyword", c => c.String(maxLength: 256));
            AlterColumn("dbo.Products", "MetaDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.ProductCategories", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.ProductCategories", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.ProductCategories", "MetaKeyword", c => c.String(maxLength: 256));
            AlterColumn("dbo.ProductCategories", "MetaDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.Pages", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Pages", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Pages", "MetaKeyword", c => c.String(maxLength: 256));
            AlterColumn("dbo.Pages", "MetaDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostCategories", "MetaKeyword", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostCategories", "MetaDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "MetaKeyword", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "MetaDescription", c => c.String(maxLength: 256));
        }

        public override void Down()
        {
            AlterColumn("dbo.Posts", "MetaDescription", c => c.String());
            AlterColumn("dbo.Posts", "MetaKeyword", c => c.String());
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String());
            AlterColumn("dbo.PostCategories", "MetaDescription", c => c.String());
            AlterColumn("dbo.PostCategories", "MetaKeyword", c => c.String());
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.String());
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.String());
            AlterColumn("dbo.Pages", "MetaDescription", c => c.String());
            AlterColumn("dbo.Pages", "MetaKeyword", c => c.String());
            AlterColumn("dbo.Pages", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Pages", "CreatedBy", c => c.String());
            AlterColumn("dbo.ProductCategories", "MetaDescription", c => c.String());
            AlterColumn("dbo.ProductCategories", "MetaKeyword", c => c.String());
            AlterColumn("dbo.ProductCategories", "UpdatedBy", c => c.String());
            AlterColumn("dbo.ProductCategories", "CreatedBy", c => c.String());
            AlterColumn("dbo.Products", "MetaDescription", c => c.String());
            AlterColumn("dbo.Products", "MetaKeyword", c => c.String());
            AlterColumn("dbo.Products", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Products", "CreatedBy", c => c.String());
            DropColumn("dbo.Posts", "UpdatedDate");
            DropColumn("dbo.PostCategories", "UpdatedDate");
            DropColumn("dbo.Pages", "UpdatedDate");
            DropColumn("dbo.ProductCategories", "UpdatedDate");
            DropColumn("dbo.Products", "UpdatedDate");
        }
    }
}