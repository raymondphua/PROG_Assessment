namespace PROG6_Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDeFuckingDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Merks", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Merks", new[] { "Product_ProductId" });
            AddColumn("dbo.Products", "Merk_MerkId", c => c.Int());
            CreateIndex("dbo.Products", "Merk_MerkId");
            AddForeignKey("dbo.Products", "Merk_MerkId", "dbo.Merks", "MerkId");
            DropColumn("dbo.Merks", "Product_ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Merks", "Product_ProductId", c => c.Int());
            DropForeignKey("dbo.Products", "Merk_MerkId", "dbo.Merks");
            DropIndex("dbo.Products", new[] { "Merk_MerkId" });
            DropColumn("dbo.Products", "Merk_MerkId");
            CreateIndex("dbo.Merks", "Product_ProductId");
            AddForeignKey("dbo.Merks", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
