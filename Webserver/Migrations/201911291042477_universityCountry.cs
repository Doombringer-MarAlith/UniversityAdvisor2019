namespace Webserver.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class universityCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.University", "City", c => c.String());
            AddColumn("dbo.University", "Country", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.University", "Country");
            DropColumn("dbo.University", "City");
        }
    }
}