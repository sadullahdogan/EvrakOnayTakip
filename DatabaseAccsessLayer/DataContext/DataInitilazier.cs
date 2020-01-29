using DatabaseAccsessLayer.Entities;
using System.Data.Entity;

namespace DatabaseAccsessLayer.DataContext
{
    public class DataInitilazier: DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            UserType Admin = new UserType() { Name = "Admin" };
            UserType User = new Entities.UserType() { Name = "User" };
            context.UserTypes.Add(Admin);
            context.SaveChanges();
            context.UserTypes.Add(User);
            context.SaveChanges();
            context.Users.Add(new Entities.User()
            {
                Name = "Admin",
                Lastname = "Admin",
                Email = "exampleadmin@example.com",
                Password = "admin",
                RePassword = "admin",
                UserType = Admin
            });
            context.SaveChanges();
            context.Users.Add(new Entities.User()
            {
                Name = "User",
                Lastname = "User",
                Email = "exampleuser@example.com",
                Password = "user",
                RePassword = "user",
                UserType = User
            });
            context.Users.Add(new Entities.User()
            {
                Name = "Sadullah",
                Lastname = "DOĞAN",
                Email = "sado.doan@gmail.com",
                Password = "Sadollah1",
                RePassword = "Sadollah1",
                UserType = User
            });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}