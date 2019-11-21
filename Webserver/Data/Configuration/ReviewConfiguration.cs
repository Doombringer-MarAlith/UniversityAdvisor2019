using Models;
using System.Data.Entity.ModelConfiguration;

namespace Webserver.Data.Configuration
{
    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            ToTable("Review");
            Property(review => review.Text).IsRequired().HasMaxLength(4000);
            Property(review => review.UserId).IsRequired();
            Property(review => review.Value).IsRequired();
            Property(review => review.Id).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}