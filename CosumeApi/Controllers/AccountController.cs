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
        // GET: Account/Users
        public async Task<ActionResult> Users()
        {
            AccountDisplayBindingModel User = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Account/48097758-a085-4f3f-966b-dfa4e27ae803");
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