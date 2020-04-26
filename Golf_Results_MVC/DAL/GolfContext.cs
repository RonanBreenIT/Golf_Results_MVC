using Golf_Results_MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Golf_Results_MVC.DAL
{
    // This our Db Set up. Inheriting App user so we can use roles (admin, user etc..) in controllers, views etc. 
    public class GolfContext : IdentityDbContext<ApplicationUser>//, IGolfContext
    {
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Comp_Result> Comp_Results { get; set; }

        public GolfContext() : base("GolfContext", throwIfV1Schema: false) // This passes name of the connection string to the constructor
        {
            //Configuration.ProxyCreationEnabled = false; //* have in API controllers, may move to here
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder); // This line needed or nothing will work after adding auhtorisation for MVC see https://entityframework.net/knowledge-base/27660203/entity-framework-6-1-code-first-migration-error--entitytype--identityuserrole--has-no-key-defined        
        }

        public static GolfContext Create()
        {
            return new GolfContext();
        }

        ////This for IDeploymentsContext for Unit Test
        //public void MarkAsModified(Object item)
        //{
        //    Entry(item).State = EntityState.Modified;
        //}
    }
}