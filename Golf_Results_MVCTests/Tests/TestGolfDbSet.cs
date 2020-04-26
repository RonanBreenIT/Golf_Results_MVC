using Golf_Results_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf_Results_MVCTests.Tests
{
    class TestGolfDbSet : TestDbSet<Golfer>
    {
        public override Golfer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(p => p.ID == (int)keyValues.Single());
        }
    }

    class TestCompDbSet : TestDbSet<Competition>
    {
        public override Competition Find(params object[] keyValues)
        {
            return this.SingleOrDefault(p => p.ID == (int)keyValues.Single());
        }
    }

    class TestCompResultDbSet : TestDbSet<Comp_Result>
    {
        public override Comp_Result Find(params object[] keyValues)
        {
            return this.SingleOrDefault(p => p.CompResultID == (int)keyValues.Single());
        }
    }
}