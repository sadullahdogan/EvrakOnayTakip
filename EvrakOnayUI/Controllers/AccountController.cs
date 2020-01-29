using DatabaseAccsessLayer.DataContext;
using DatabaseAccsessLayer.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EvrakOnayUI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> CheckValid(string id)
        {


            bool isValid = false;
            int userId = Int32.Parse(Session["UserId"].ToString());
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44348/api/User/" + userId);
            var model = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
            model.RePassword = model.Password;
            isValid = id.Equals(model.Token);
            if (isValid)
            {
                model.IsEmailValidated = true;

                var data = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var returnResult = await client.PostAsync("https://localhost:44348/api/User?isUpdate=true", httpContent);

                return View();
            }
            else
            {
                return View();//error

            }


        }




        public async Task<ActionResult> ValidateEmail()
        {
            int userId = Int32.Parse(Session["UserId"].ToString());
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44348/api/User/" + userId);
            var model = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
            string siteUri = "https://localhost:44367/";
            string activateUrl = $"{siteUri}/Account/CheckValid/{model.Token}";
            string body = $"Merhaba {model.Name};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUrl}' target='_blank'>tıklayınız</a>.";
            Helper.Email.SendEmail("Email Validation", body, model.Email);

            return View();
        }
        public ActionResult Logout() {
            Session["UserId"] = null;
            Session["UserType"] = null;
            return RedirectToAction("Login");
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserTypeId = 2;
                user.Token = Helper.RandomString.GetRandomKey();
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(user);
                HttpContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var returnResult = await client.PostAsync("https://localhost:44348/api/User", httpContent);
            }
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Login(string email, string password)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44348/api/User");
            var model = JsonConvert.DeserializeObject<List<User>>(response.Content.ReadAsStringAsync().Result);
            var user = model.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["UserType"] = user.UserTypeId;
                if (user.UserTypeId == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.UserTypeId == 2)
                {
                    return RedirectToAction("Index", "User");
                }

            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}