namespace PROG6_Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Afdelings",
                c => new
                    {
                        AfdelingId = c.Int(nullable: false, identity: true),
                        AfdelingNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AfdelingId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductNaam = c.String(nullable: false),
                        Afdeling_AfdelingId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Afdelings", t => t.Afdeling_AfdelingId)
                .Index(t => t.Afdeling_AfdelingId);
            
            CreateTable(
                "dbo.Merks",
                c => new
                    {
                        MerkId = c.Int(nullable: false, identity: true),
                        MerkNaam = c.String(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.MerkId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Merks", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "Afdeling_AfdelingId", "dbo.Afdelings");
            DropIndex("dbo.Merks", new[] { "Product_ProductId" });
            DropIndex("dbo.Products", new[] { "Afdeling_AfdelingId" });
            DropTable("dbo.Merks");
            DropTable("dbo.Products");
            DropTable("dbo.Afdelings");
        }
    }
}
