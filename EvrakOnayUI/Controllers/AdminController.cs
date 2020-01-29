using DatabaseAccsessLayer.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EvrakOnayUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public async Task<ActionResult> Index()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:44348/api/Request");
                var model = JsonConvert.DeserializeObject<List<Request>>(response.Content.ReadAsStringAsync().Result);
                //model = model.Where(x => x.RequestState == RequestState.Created).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public async Task<ActionResult> Respond(string submitButton,int id,string result)
        {
            switch (submitButton)
            {
                case "Confirm":
                    // delegate sending to another controller action
                    return ( await Confirm(id,result));
                case "Deny":
                    // call another action to perform the cancellation
                    return (await Deny(id,result));
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Index");
            }
        }
        public async Task<ActionResult> Details(int id,string result) {

            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:44348/api/Request/"+id);
                var model = JsonConvert.DeserializeObject<Request>(response.Content.ReadAsStringAsync().Result);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<ActionResult> Confirm(int id,string result) {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")
            {
                
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:44348/api/Request/" + id);
                var model = JsonConvert.DeserializeObject<Request>(response.Content.ReadAsStringAsync().Result);
                model.RequestState = RequestState.Accepted;
                model.Result = result;
                var data = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var returnResult = await client.PostAsync("https://localhost:44348/api/Request?isUpdate=true", httpContent);
                
                response = await client.GetAsync("https://localhost:44348/api/User/" + model.UserId);
                var user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                string body = $"Merhaba {user.Name};<br><br>{model.Header}"+
                    $" isimli Talebiniz onaylanmıştır.<br><br> Yönetici not olarak <br>{model.Result}"+" notunu bırakmıştır" ;
                Helper.Email.SendEmail("Talep Sonucu", body, user.Email);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<ActionResult> Deny(int id,string result)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")
            {

                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:44348/api/Request/" + id);
                var model = JsonConvert.DeserializeObject<Request>(response.Content.ReadAsStringAsync().Result);
                model.RequestState = RequestState.Declined;
                model.Result = result;
                var data = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var returnResult = await client.PostAsync("https://localhost:44348/api/Request?isUpdate=true", httpContent);
                response = await client.GetAsync("https://localhost:44348/api/User/" + model.UserId);
                var user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                string body = $"Merhaba {user.Name};<br><br>{model.Header}" +
                    $" isimli Talebiniz reddedilmiştir.<br><br> Yönetici not olarak <br>{model.Result}" + " notunu bırakmıştır";
                Helper.Email.SendEmail("Talep Sonucu", body, user.Email);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }



    }
}