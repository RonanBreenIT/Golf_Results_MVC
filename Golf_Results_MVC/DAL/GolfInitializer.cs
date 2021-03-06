﻿using Golf_Results_MVC.Models;
using System;
using System.Collections.Generic;

namespace Golf_Results_MVC.DAL
{
    /*When dont want to use initializer i.e. when going live follow here - https://docs.microsoft.com/en-us/ef/ef6/fundamentals/configuring/config-file Note

      *When you deploy an application to a production web server, you must remove or disable code that drops and re-creates the database. */
    
    // This seeds the data below automatically to our db. This is for testing purposes.
    public class GolfInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GolfContext> // This drops and recreates db each time using seed method. Need to use CF Migrations when going live as this is fine for testing but can't lose all data in live each time schema is changed.
    {
        protected override void Seed(GolfContext context)
        {
            var golfers = new List<Golfer>
            {
            new Golfer{Firstname="Rory", Surname="McIlroy"},
            new Golfer{Firstname="Tiger", Surname="Woods"},
            new Golfer{Firstname="Padraig", Surname="Harrington"},
            new Golfer{Firstname="Phil", Surname="Mickelson"},
            new Golfer{Firstname="Brooks", Surname="Koepka"}
            };
            golfers.ForEach(g => context.Golfers.Add(g));
            context.SaveChanges();

            var comps = new List<Competition>
            {
            new Competition{Name="Sentry Open"},
            new Competition{Name="Sony Open"},
            new Competition{Name="American Express"},
            new Competition{Name="Farmers Open"},
            new Competition{Name="Waste Management Open"},

            };
            comps.ForEach(c => context.Competitions.Add(c));
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
            results.ForEach(r => context.Comp_Results.Add(r));
            context.SaveChanges();
        }
    }
}