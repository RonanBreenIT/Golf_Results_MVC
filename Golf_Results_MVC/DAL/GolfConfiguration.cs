using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace Golf_Results_MVC.DAL
{
    // This do configuration tasks in code that you would otherwise do in the Web.config file.  For more information, see EntityFramework Code-Based Configuration.
    public class GolfConfiguration: DbConfiguration
    {
        public GolfConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}