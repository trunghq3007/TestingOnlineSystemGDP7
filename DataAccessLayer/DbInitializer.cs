using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DBEntityContext>
    {
        protected override void Seed(DBEntityContext context)
        {
            base.Seed(context);
        }
    }
}
