

using EFCodeFirst.Entity;
namespace EFCodeFirst.Mapping
{
    public partial class NewsCommentMap : NopEntityTypeConfiguration<NewsComment>
    {
        public NewsCommentMap()
        {
            this.ToTable("NewsComment");
            this.HasKey(pr => pr.Id);

            this.HasRequired(nc => nc.NewsItem)
                .WithMany(n => n.NewsComments)
                .HasForeignKey(nc => nc.NewsItemId);
        }
    }
}