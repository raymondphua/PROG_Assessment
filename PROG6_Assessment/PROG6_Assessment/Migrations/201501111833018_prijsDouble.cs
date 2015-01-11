namespace PROG6_Assessment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prijsDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Prijs", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Prijs", c => c.Int(nullable: false));
        }
    }
}
