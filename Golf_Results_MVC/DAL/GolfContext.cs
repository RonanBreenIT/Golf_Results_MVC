using Golf_Results_MVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Golf_Results_MVC.DAL
{
    public class GolfContext: DbContext
    {
        public GolfContext(): base("GolfContext") // This passes name of the connection string to the constructor
        {
        }

        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Comp_Result> Comp_Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}