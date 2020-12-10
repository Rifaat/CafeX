using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeX.Models
{
    public class CafeXContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CafeXContext() : base("name=CafeXContext")
        {
            // TODO: RAB
            // Create DB if not exist...
            // ref: https://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx
            // 
            //Database.SetInitializer<mvcWebAppContext>(new CreateDatabaseIfNotExists<mvcWebAppContext>());

        }

        public System.Data.Entity.DbSet<CafeX.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<CafeX.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<CafeX.Models.OrderTotal> OrderTotals { get; set; }
    }
}
