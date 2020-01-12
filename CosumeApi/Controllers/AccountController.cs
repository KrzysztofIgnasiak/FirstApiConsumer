using CosumeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using CosumeApi.Models.BindingModels;

namespace CosumeApi.Controllers
{
    public class AccountController : Controller
    {
        //GET : Account/Users
        public async Task<ActionResult> Users()
        {
            PagingInfo.UserPage = 1;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Account");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<AccountDisplayBindingModel> Users = null;
                Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                if(UserInfo.IsAdmin == true)
                {
                    return View(Users);
                }
                else
                {
                    return View("UsersNormal", Users);
                }
                
            }
            else
            {
                return View("SomethingWorng");
            } 
        }
        public async Task<ActionResult> GoUp()
        {
            PagingInfo.UserPage += 1;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Account?pageNumber="+PagingInfo.UserPage);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<AccountDisplayBindingModel> Users = null;
                Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                if (UserInfo.IsAdmin == true)
                {
                    return View("Users",Users);
                }
                else
                {
                    return View("UsersNormal", Users);
                }

            }
            else
            {
                return View("SomethingWorng");
            }
        }
        public async Task<ActionResult> GoDown()
        {
            if(PagingInfo.UserPage >1)
            {
                PagingInfo.UserPage -= 1;
            }
            else
            {
                PagingInfo.UserPage = 1;
            }
            
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Account?pageNumber=" + PagingInfo.UserPage.ToString());
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<AccountDisplayBindingModel> Users = null;
                Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                if (UserInfo.IsAdmin == true)
                {
                    return View("Users", Users);
                }
                else
                {
                    return View("UsersNormal", Users);
                }

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
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Account/" +Id);
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

        //GET : Account/IsAdmin
        public async Task<ActionResult> IsAdmin()
        {
            IsAdminBindingModel Model = new IsAdminBindingModel();
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Role/IsAdmin");
            if (response.IsSuccessStatusCode)
            {
                Model = await response.Content.ReadAsAsync<IsAdminBindingModel>();
                UserInfo.IsAdmin = Model.IsAdmin;
                return RedirectToAction("Users");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
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
            var responseTask = ApiHelper.ApiClient.GetAsync("api/Account/" + Id);
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

            var putTask = ApiHelper.ApiClient.PutAsJsonAsync<AccountSetBindingModel>("api/Account/" + User.Id, User);
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

        public ActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn(LogInBindingModel User)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                 new KeyValuePair<string, string>("username", User.username),
                 new KeyValuePair<string, string>("password", User.password)
            });
            var responseTask = ApiHelper.ApiClient.PostAsync("token", content);
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TokenBindingModel>();
                readTask.Wait();
                var TokenBinding = readTask.Result;
                ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //give me json only
                ApiHelper.ApiClient.DefaultRequestHeaders.Authorization
                              = new AuthenticationHeaderValue("Bearer", TokenBinding.access_token);
                return RedirectToAction("IsAdmin");
            }
            else
            {
                return View("Error");
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountAddBindingModel NewUser)
        {
            var content = new FormUrlEncodedContent(new[]
           {
                 new KeyValuePair<string, string>("username", NewUser.UserName),
                 new KeyValuePair<string, string>("NameOfUser", NewUser.NameOfUser),
                 new KeyValuePair<string, string>("Password", NewUser.Password),
                 new KeyValuePair<string, string>("ConfirmPassword", NewUser.ConfirmPasword),
                 new KeyValuePair<string, string>("Surname", NewUser.Surname),
                 new KeyValuePair<string, string>("DateOfBirth", NewUser.DateofBirth.ToString()),
                 new KeyValuePair<string, string>("Email", NewUser.Email),
            });
            var responseTask = ApiHelper.ApiClient.PostAsync("api/account/register", content);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("LogIn");
            }
            else
            {
                return View("Error");
            }

        }

        // GET :Account/Delete
        public async Task<ActionResult> DeleteUser(string Id)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("api/Account/" +Id);
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
        
    }
}