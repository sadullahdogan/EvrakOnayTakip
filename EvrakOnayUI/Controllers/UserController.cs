using DatabaseAccsessLayer.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EvrakOnayUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public async Task<ActionResult> Index()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "2")
            {
                int userId = Int32.Parse(Session["UserId"].ToString());
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:44348/api/Request");
                var model = JsonConvert.DeserializeObject<List<Request>>(response.Content.ReadAsStringAsync().Result);

                model = model.Where(x => x.UserId == userId).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult CreateRequest()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "2")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public async Task<ActionResult> CreateRequest(Request r, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("Dosya Seçin", "Dosya Boş Olamaz");
                return View(r);
            }
            var yuklemeYeri = Path.Combine(Server.MapPath("~/RequestFiles"), file.FileName);
            file.SaveAs(yuklemeYeri);
            int userId = Int32.Parse(Session["UserId"].ToString());
            r.UserId = userId;
            r.FileName = file.FileName;
            r.RequestState = RequestState.Created;
            r.Time = DateTime.Now;
            var data = JsonConvert.SerializeObject(r);
            HttpClient client = new HttpClient();
            HttpContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var returnResult = await client.PostAsync("https://localhost:44348/api/Request?isUpdate=false", httpContent);
            return View();
        }
    }
}