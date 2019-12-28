using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CosumeApi.Models;
using System.Threading.Tasks;
using System.Net;

namespace CosumeApi.Controllers
{
    public class AccountController : Controller
    {
        //GET : Account/Users
        public async Task<ActionResult> Users()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Account");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.IsSuccessStatusCode)
            {
                DisplayUserViewModel Model = new DisplayUserViewModel();
                Model.Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View(Model);
            }
            else
            {
                return View("SomethingWorng");
            } 
        }

        // GET: Account/User
        public async Task<ActionResult> User()
        {
            AccountDisplayBindingModel User = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Account/a4a20198-5cea-439a-99a4-8e4c01d6324a");
            if (response.IsSuccessStatusCode)
            {
                User = await response.Content.ReadAsAsync<AccountDisplayBindingModel>();
            }
            return View(User);
        }

        // GET :Account/Delete
        public async Task<ActionResult> Delete()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("Account/a4a20198-5cea-439a-99a4-8e4c01d6324a");
            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("Not Found");
            }
            else
            {
                return View("Users");
            }


        }
        // GET: Account/About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}