namespace PROG6_Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPDATEDEZEFKINGBITCH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recepts",
                c => new
                    {
                        ReceptId = c.Int(nullable: false, identity: true),
                        ReceptNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReceptId);
            
            AddColumn("dbo.Products", "Recept_ReceptId", c => c.Int());
            CreateIndex("dbo.Products", "Recept_ReceptId");
            AddForeignKey("dbo.Products", "Recept_ReceptId", "dbo.Recepts", "ReceptId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Recept_ReceptId", "dbo.Recepts");
            DropIndex("dbo.Products", new[] { "Recept_ReceptId" });
            DropColumn("dbo.Products", "Recept_ReceptId");
            DropTable("dbo.Recepts");
        }
    }
}
