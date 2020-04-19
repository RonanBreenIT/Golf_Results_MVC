namespace Golf_Results_MVC.Migrations
{
    using Golf_Results_MVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Golf_Results_MVC.DAL.GolfContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Golf_Results_MVC.DAL.GolfContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var golfers = new List<Golfer>
            {
            new Golfer{Firstname="Rory", Surname="McIlroy"},
            new Golfer{Firstname="Tiger", Surname="Woods"},
            new Golfer{Firstname="Padraig", Surname="Harrington"},
            new Golfer{Firstname="Phil", Surname="Mickelson"},
            new Golfer{Firstname="Brooks", Surname="Koepka"}
            };
            golfers.ForEach(g => context.Golfers.AddOrUpdate(b => b.Surname , g));
            context.SaveChanges();

            var comps = new List<Competition>
            {
            new Competition{Name="Sentry Open"},
            new Competition{Name="Sony Open"},
            new Competition{Name="American Express"},
            new Competition{Name="Farmers Open"},
            new Competition{Name="Waste Management Open"},

            };
            comps.ForEach(c => context.Competitions.AddOrUpdate(a => a.Name, c));
            context.SaveChanges();

            var results = new List<Comp_Result>
            {
                new Comp_Result{CompetitionID=1, GolferID=1, Position="1", GolferScore="-15", Season= 2020, StartDate= DateTime.Parse("2020-01-02"), EndDate=DateTime.Parse("2020-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=2, Position="2", GolferScore="-14", Season= 2020, StartDate= DateTime.Parse("2020-01-02"), EndDate=DateTime.Parse("2020-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=3, Position="5", GolferScore="+5", Season= 2020, StartDate= DateTime.Parse("2020-01-02"), EndDate=DateTime.Parse("2020-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=4, Position="3", GolferScore="-10", Season= 2020, StartDate= DateTime.Parse("2020-01-02"), EndDate=DateTime.Parse("2020-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=5, Position="4", GolferScore="-8", Season= 2020, StartDate= DateTime.Parse("2020-01-02"), EndDate=DateTime.Parse("2020-01-05")},
                new Comp_Result{CompetitionID=2, GolferID=1, Position="5", GolferScore="+6", Season= 2020, StartDate= DateTime.Parse("2020-01-09"), EndDate=DateTime.Parse("2020-01-12")},
                new Comp_Result{CompetitionID=2, GolferID=2, Position="2", GolferScore="-9", Season= 2020, StartDate= DateTime.Parse("2020-01-09"), EndDate=DateTime.Parse("2020-01-12")},
                new Comp_Result{CompetitionID=2, GolferID=3, Position="5", GolferScore="+2", Season= 2020, StartDate= DateTime.Parse("2020-01-09"), EndDate=DateTime.Parse("2020-01-12")},
                new Comp_Result{CompetitionID=2, GolferID=4, Position="3", GolferScore="-4", Season= 2020, StartDate= DateTime.Parse("2020-01-09"), EndDate=DateTime.Parse("2020-01-12")},
                new Comp_Result{CompetitionID=2, GolferID=5, Position="1", GolferScore="-11", Season= 2020, StartDate= DateTime.Parse("2020-01-09"), EndDate=DateTime.Parse("2020-01-12")},
                new Comp_Result{CompetitionID=3, GolferID=1, Position="5", GolferScore="+6", Season= 2020, StartDate= DateTime.Parse("2020-01-16"), EndDate=DateTime.Parse("2020-01-19")},
                new Comp_Result{CompetitionID=3, GolferID=2, Position="2", GolferScore="-4", Season= 2020, StartDate= DateTime.Parse("2020-01-16"), EndDate=DateTime.Parse("2020-01-19")},
                new Comp_Result{CompetitionID=3, GolferID=3, Position=null, GolferScore=null, Season= 2020, StartDate= DateTime.Parse("2020-01-16"), EndDate=DateTime.Parse("2020-01-19")},
                new Comp_Result{CompetitionID=3, GolferID=4, Position=null, GolferScore=null, Season= 2020, StartDate= DateTime.Parse("2020-01-16"), EndDate=DateTime.Parse("2020-01-19")},
                new Comp_Result{CompetitionID=3, GolferID=5, Position="1", GolferScore="-6", Season= 2020, StartDate= DateTime.Parse("2020-01-16"), EndDate=DateTime.Parse("2020-01-19")},
                new Comp_Result{CompetitionID=1, GolferID=1, Position="1", GolferScore="-7", Season= 2019, StartDate= DateTime.Parse("2019-01-02"), EndDate=DateTime.Parse("2019-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=2, Position="2", GolferScore="-6", Season= 2019, StartDate= DateTime.Parse("2019-01-02"), EndDate=DateTime.Parse("2019-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=3, Position=null, GolferScore=null, Season= 2019, StartDate= DateTime.Parse("2019-01-02"), EndDate=DateTime.Parse("2019-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=4, Position="3", GolferScore="-4", Season= 2019, StartDate= DateTime.Parse("2019-01-02"), EndDate=DateTime.Parse("2019-01-05")},
                new Comp_Result{CompetitionID=1, GolferID=5, Position="4", GolferScore="-3", Season= 2019, StartDate= DateTime.Parse("2019-01-02"), EndDate=DateTime.Parse("2019-01-05")},
            };
            foreach (Comp_Result res in results)
            {
                var foundcompres = context.Comp_Results.Where(x => x.CompetitionID == res.CompetitionID && x.Season == res.Season && x.GolferID == res.GolferID).FirstOrDefault();
                if (foundcompres == null)
                {
                    context.Comp_Results.Add(res);
                }
            }
            context.SaveChanges();
        }
    }
}
   