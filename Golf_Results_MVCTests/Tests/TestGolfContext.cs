using Golf_Results_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf_Results_MVCTests.Tests
{
    class TestGolfContext: IGolfContext
    {
        public TestGolfContext()
        {
            this.Golfers = new TestGolfDbSet();
            this.Competitions = new TestCompDbSet();
            this.Comp_Results = new TestCompResultDbSet();
        }

        public DbSet<Golfer> Golfers { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Comp_Result> Comp_Results { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Object item) { }
        public void Dispose() { }
    }
}