using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CosumeApi.Models;
using System.Threading.Tasks;

namespace CosumeApi.Controllers
{
    public class AccountController : Controller
    {
        //GET : Account/Users
        public async Task<ActionResult> Users()
        {
            DisplayUserViewModel Model = new DisplayUserViewModel();
           List <AccountDisplayBindingModel> Users = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Account");
            if (response.IsSuccessStatusCode)
            {
                Model.Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
            }
            return View(Model);
           
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

        // GET: Account/About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}