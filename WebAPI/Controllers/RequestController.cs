using DatabaseAccsessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class RequestController : ApiController
    {
        // GET: api/Request
        public IEnumerable<Request> Get()
        {
            using (DatabaseAccsessLayer.DataContext.DataContext db = new DatabaseAccsessLayer.DataContext.DataContext()) {
                return db.Requests.ToList();
            }
        }

        // GET: api/Request/5
        public Request Get(int id)
        {
            using (DatabaseAccsessLayer.DataContext.DataContext db = new DatabaseAccsessLayer.DataContext.DataContext())
            {
                return db.Requests.FirstOrDefault(x=>x.Id==id);
            }
        }

        // POST: api/Request
        public void Post(Request r,bool isUpdate=false)
        {
            using (DatabaseAccsessLayer.DataContext.DataContext db = new DatabaseAccsessLayer.DataContext.DataContext())
            {
                if (isUpdate)
                {
                    db.Entry(r).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else {
                    db.Requests.Add(r);
                    db.SaveChanges();
                }
                
            }
        }

        // PUT: api/Request/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Request/5
        public void Delete(int id)
        {
        }
    }
}
