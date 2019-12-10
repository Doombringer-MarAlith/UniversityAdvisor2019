namespace Webserver.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faculty",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    UniversityId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Programme",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    FacultyId = c.Int(nullable: false),
                    UniversityId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Review",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UniversityId = c.Int(nullable: false),
                    FacultyId = c.Int(nullable: false),
                    LecturerId = c.Int(nullable: false),
                    ProgrammeId = c.Int(nullable: false),
                    UserId = c.String(nullable: false),
                    Text = c.String(nullable: false, maxLength: 4000),
                    Value = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.University",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Description = c.String(),
                    Location = c.String(),
                    FoundingDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.University");
            DropTable("dbo.Review");
            DropTable("dbo.Programme");
            DropTable("dbo.Faculty");
        }
    }
}