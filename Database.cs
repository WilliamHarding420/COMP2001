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
    }
}
