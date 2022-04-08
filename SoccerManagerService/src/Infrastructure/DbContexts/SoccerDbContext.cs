namespace Soccer.Infrastructure.Repository.RDBRepository.DbContexts
{
    using Microsoft.EntityFrameworkCore;
    using Soccer.Domain.Entities;
    using Soccer.Models.Constants;

    public class SoccerDbContext : DbContext
    {
        public SoccerDbContext(DbContextOptions<SoccerDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Players>()
                .Property(e => e.Position)
                .HasConversion(
                    v => v.ToString(),
                    v => (PlayerPositionsEnum)Enum.Parse(typeof(PlayerPositionsEnum), v));
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Transfers> Transfers { get; set; }
    }
}