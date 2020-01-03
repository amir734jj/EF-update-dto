using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public sealed class EntityDbContext : DbContext
    {
        public DbSet<DummyModel> DummyModels { get; set; }
        
        // ReSharper disable once SuggestBaseTypeForParameter
        public EntityDbContext(DbContextOptions<EntityDbContext> optionsBuilderOptions) : base(optionsBuilderOptions)
        {
            Database.EnsureCreated();
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DummyModel>()
                .HasMany(x => x.Level1s)
                .WithOne(x => x.DummyModelRef)
                .HasForeignKey(x => x.DummyModelRefId);

            modelBuilder.Entity<Level1>()
                .HasMany(x => x.L1P3)
                .WithOne(x => x.Level1Ref)
                .HasForeignKey(x => x.Level1RefId);

            modelBuilder.Entity<Level2>()
                .HasMany(x => x.L2P3)
                .WithOne(x => x.Level2Ref)
                .HasForeignKey(x => x.Level2RefId);
        }*/
    }
}