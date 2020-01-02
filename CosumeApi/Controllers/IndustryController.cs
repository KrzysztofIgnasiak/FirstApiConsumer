using CosumeApi.Models;
using CosumeApi.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CosumeApi.Controllers
{
    public class IndustryController : Controller
    {
        public ActionResult CreateIndustry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCompany(IndustryBindingModel Industry)
        {
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<IndustryBindingModel>("api/Industry", Industry);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Companies");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("NewIndustry");
            }
        }
    }
}