using Models;
using System.Data.Entity.ModelConfiguration;

namespace Webserver.Data.Configuration
{
    public class UniversityConfiguration : EntityTypeConfiguration<University>
    {
        public UniversityConfiguration()
        {
            ToTable("University");
            Property(university => university.Name).IsRequired().HasMaxLength(255);
            Property(university => university.Id).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}