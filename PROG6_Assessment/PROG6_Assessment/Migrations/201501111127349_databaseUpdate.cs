namespace PROG6_Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Merk_MerkId", "dbo.Merks");
            DropIndex("dbo.Products", new[] { "Merk_MerkId" });
            AddColumn("dbo.Merks", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.Merks", "Product_ProductId");
            AddForeignKey("dbo.Merks", "Product_ProductId", "dbo.Products", "ProductId");
            DropColumn("dbo.Products", "Merk_MerkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Merk_MerkId", c => c.Int());
            DropForeignKey("dbo.Merks", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Merks", new[] { "Product_ProductId" });
            DropColumn("dbo.Merks", "Product_ProductId");
            CreateIndex("dbo.Products", "Merk_MerkId");
            AddForeignKey("dbo.Products", "Merk_MerkId", "dbo.Merks", "MerkId");
        }
    }
}
