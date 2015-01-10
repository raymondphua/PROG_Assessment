namespace PROG6_Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pleaseUpdateDeFkingDatabase : DbMigration
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
                        Prijs = c.Int(nullable: false),
                        Afdeling_AfdelingId = c.Int(),
                        Merk_MerkId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Afdelings", t => t.Afdeling_AfdelingId)
                .ForeignKey("dbo.Merks", t => t.Merk_MerkId)
                .Index(t => t.Afdeling_AfdelingId)
                .Index(t => t.Merk_MerkId);
            
            CreateTable(
                "dbo.Merks",
                c => new
                    {
                        MerkId = c.Int(nullable: false, identity: true),
                        MerkNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MerkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Merk_MerkId", "dbo.Merks");
            DropForeignKey("dbo.Products", "Afdeling_AfdelingId", "dbo.Afdelings");
            DropIndex("dbo.Products", new[] { "Merk_MerkId" });
            DropIndex("dbo.Products", new[] { "Afdeling_AfdelingId" });
            DropTable("dbo.Merks");
            DropTable("dbo.Products");
            DropTable("dbo.Afdelings");
        }
    }
}
