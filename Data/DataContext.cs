using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        // Add here all the models that you want to create tables for
        public DbSet<ArtistDetails> ArtistDetails { get; set; }
        public DbSet<AdDistributionData> AdDistributionData { get; set; }
        public DbSet<SongRecommendationDetails> SongRecommendationDetails { get; set; }
        public DbSet<UserPlaybackBehaviour> UserPlaybackBehaviour { get; set; }
        public DbSet<SongDataBaseModel> SongDataBaseModel { get; set; }
        public DbSet<SongFeatures> SongFeatures { get; set; }
        public DbSet<Trends> Trends { get; set; }
        public DbSet<UserDemographicsDetails> UserDemographicsDetails { get; set; }
        public DbSet<MostPlayedArtistInformation> MostPlayedArtistInformation { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
