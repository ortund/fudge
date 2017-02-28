namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catimg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Image");
        }
    }
}
