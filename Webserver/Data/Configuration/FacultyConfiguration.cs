using Models;
using System.Data.Entity.ModelConfiguration;

namespace Webserver.Data.Configuration
{
    public class FacultyConfiguration : EntityTypeConfiguration<Faculty>
    {
        public FacultyConfiguration()
        {
            ToTable("Faculty");
            Property(faculty => faculty.Name).IsRequired().HasMaxLength(255);
            Property(faculty => faculty.Id).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}