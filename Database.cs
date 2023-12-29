using COMP2001.data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace COMP2001 {
    public class Database : DbContext {

        public static string ConnectionString = "Server=localhost;" +
            "Database=comp2001;" +
            "User Id=SA;" +
            "Password=DBPassword123;" +
            "TrustServerCertificate=True;";

        public Database()
            :base() {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.HasDefaultSchema("CW2");

            modelBuilder.Entity<User>().ToTable(table => table.HasTrigger("RecalcMeasurements"));

            modelBuilder.Entity<UserActivityJoin>().HasNoKey().ToView("Favourite Activities");
            modelBuilder.Entity<ActivityName>().HasNoKey().ToView("Activity Names");

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ActivityData> Activities { get; set; }
        public virtual DbSet<UserActivity> UserFavouriteActivity { get; set; }

        public virtual DbSet<UserActivityJoin> UserActivityJoinView { get; set; }
        public virtual DbSet<ActivityName> ActivityNamesView { get; set; }

    }
}
