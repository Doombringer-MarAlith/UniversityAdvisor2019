namespace Webserver.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EntityCleanup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Review", "LecturerId");
            DropColumn("dbo.University", "Location");
            DropColumn("dbo.University", "FoundingDate");
        }

        public override void Down()
        {
            AddColumn("dbo.University", "FoundingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.University", "Location", c => c.String());
            AddColumn("dbo.Review", "LecturerId", c => c.Int(nullable: false));
        }
    }
}