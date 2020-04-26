using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

// this is for Unit Tests
namespace Golf_Results_MVC.Models
{
    public interface IGolfContext : IDisposable
    {
        DbSet<Golfer> Golfers { get; }
        DbSet<Competition> Competitions { get; }

        DbSet<Comp_Result> Comp_Results { get; }
        int SaveChanges();
        void MarkAsModified(Object item);
    }
}