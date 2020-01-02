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
        public async Task<ActionResult> User(string Id)
        {
            AccountDisplayBindingModel User = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Account/" +Id);
            if (response.IsSuccessStatusCode)
            {
                User = await response.Content.ReadAsAsync<AccountDisplayBindingModel>();
                return View(User);
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            else
            {
                return View("SomethingWrong");
            }
        }
        // PUT : Account/Update
        public ActionResult UpdateUser(string Id)
        {
            AccountSetBindingModel User = new AccountSetBindingModel();
            var responseTask = ApiHelper.ApiClient.GetAsync("Account/" + Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<AccountSetBindingModel>();
                readTask.Wait();

                User = readTask.Result;
            }
            return View(User);
        }

        [HttpPost]
        public ActionResult UpdateUser(AccountSetBindingModel User)
        {

            var putTask = ApiHelper.ApiClient.PutAsJsonAsync<AccountSetBindingModel>("Account/" + User.Id, User);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Users");
            }
            else
            {
                return View("Error");
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
                return View("NotFound");
            }
            else
            {
                return RedirectToAction("Users");
                //return View("Users");
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