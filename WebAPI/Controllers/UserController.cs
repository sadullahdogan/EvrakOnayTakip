using DatabaseAccsessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<User> Get()
        {
            using (DatabaseAccsessLayer.DataContext.DataContext db = new DatabaseAccsessLayer.DataContext.DataContext())
            {
                return db.Users.ToList();
            }
        }

        // GET: api/User/5
        public User Get(int id)
        {
            using (DatabaseAccsessLayer.DataContext.DataContext db = new DatabaseAccsessLayer.DataContext.DataContext())
            {
                return db.Users.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        // POST: api/User
        public void Post(User user, bool isUpdate = false)
        {
            
            try {
                using (DatabaseAccsessLayer.DataContext.DataContext db = new DatabaseAccsessLayer.DataContext.DataContext())
                {
                    if (isUpdate)
                    {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        user.RePassword = user.Password;
                        var u = db.Users.Find(user.Id);
                        u.RePassword = u.RePassword;
                        
                        db.Entry(u).CurrentValues.SetValues(user);
                        ModelState["EntityValidationErrors"].Errors.Clear();
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException e) {
            
            }

        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
