using CosumeApi.Models;
using CosumeApi.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CosumeApi.Controllers
{
    public class RoleController : Controller
    {
        public async Task<ActionResult> Roles()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Role/GetAll");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<RoleViewBindingModel> Roles = null;
             
                Roles = await response.Content.ReadAsAsync<List<RoleViewBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View(Roles);
            }
            else
            {
                return View("SomethingWorng");
            }
        }
      public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(RoleAddBindingModel NewRole)
        {
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<RoleAddBindingModel>("api/Role/Create", NewRole);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("NewRole");
            }
        }

        public ActionResult AddUserToRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserToRole(UserRoleBindingModel Model)
        {
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<UserRoleBindingModel>("api/Role/AddUserToRole", Model);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View();
            }
        }

        public ActionResult RemoveUserFromRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RemoveUserFromRole(UserRoleBindingModel Model)
        {
            //var parameters = new Dictionary<string, string> { { "id", Model.UserId }, { "RoleName", Model.RoleName } };
            //var encodedContent = new FormUrlEncodedContent(parameters);
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<UserRoleBindingModel>("api/Role/RemoveUserFromRole", Model);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View();
            }
        }


    }
}