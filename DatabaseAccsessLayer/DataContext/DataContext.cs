using DatabaseAccsessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccsessLayer.DataContext
{
   public class DataContext:DbContext
    {
        public DataContext()
        {
            Database.Connection.ConnectionString = "Server =.; Database = EvrakOnayDB; Integrated Security = True;";
            Database.SetInitializer(new DataInitilazier());
        }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }

    }
}
