namespace Forum.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedOpinions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Opinions", "Action", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Opinions", "Action");
        }
    }
}
