using Models;
using System.Data.Entity.ModelConfiguration;

namespace Webserver.Data.Configuration
{
    public class ProgrammeConfiguration : EntityTypeConfiguration<Programme>
    {
        public ProgrammeConfiguration()
        {
            ToTable("Programme");
            Property(programme => programme.Name).IsRequired().HasMaxLength(255);
            Property(programme => programme.FacultyId).IsRequired();
            Property(programme => programme.UniversityId).IsRequired();
            Property(programme => programme.Id).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);        }
    }
}