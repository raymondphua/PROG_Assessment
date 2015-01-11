namespace PROG6_Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KortingToevoegen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kortings",
                c => new
                    {
                        KortingId = c.Int(nullable: false, identity: true),
                        Coupon = c.String(),
                        StartDatum = c.DateTime(nullable: false),
                        EindDatum = c.DateTime(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.KortingId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kortings", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Kortings", new[] { "Product_ProductId" });
            DropTable("dbo.Kortings");
        }
    }
}
