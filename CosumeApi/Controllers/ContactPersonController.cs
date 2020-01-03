using CosumeApi.Models;
using CosumeApi.Models.BindingModels;
using CosumeApi.Models.ViewModels;
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
    public class ContactPersonController : Controller
    {
        
        // GET: ContactPeople
        public async Task<ActionResult> ContactPeople()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/ContactPerson");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<ContactPersonGetBindingModel> ContactPeople = null;
         
                ContactPeople = await response.Content.ReadAsAsync<List<ContactPersonGetBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                
                 return View(ContactPeople);
              
            
            }
            else
            {
                return View("SomethingWrong");
            }
        }
        public async Task <ActionResult> ContactPeopleBySurnameAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ContactPeopleBySurnameAsync(ContactPersonBySurnameBindingModel Model)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/ContactPerson/BySurname?Surname=" + Model.Surname);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<ContactPersonGetBindingModel> ContactPeople = null;
               
                ContactPeople = await response.Content.ReadAsAsync<List<ContactPersonGetBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View("ContactPeople", ContactPeople);
            }
            else
            {
                return View("SomethingWrong");
            }

        }


        // GET: ContactPerson/1
        public async Task<ActionResult> ContactPerson(int Id)
        {
            ContactPersonGetBindingModel ContactPerson = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/ContactPerson/" + Id.ToString());
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            if (response.IsSuccessStatusCode)
            {
                ContactPerson = await response.Content.ReadAsAsync<ContactPersonGetBindingModel>();
                return View(ContactPerson);
            }
            else
            {
                return View("SomethingWrong");
            }
        }


        // POST : ContactPerson/Create
        public ActionResult CreateContactPerson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateContactPerson(ContactPersonGetBindingModel NewContactPerson)
        {
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<ContactPersonGetBindingModel>("api/ContactPerson", NewContactPerson);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("ContactPeople");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("NewContactPerson");
            }
        }

        public ActionResult UpdateContactPerson(int Id)
        {
            ContactPersonUpdateBindingModel ContactPerson = new ContactPersonUpdateBindingModel();
            var responseTask = ApiHelper.ApiClient.GetAsync("api/ContactPerson/" + Id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ContactPersonUpdateBindingModel>();
                readTask.Wait();

                ContactPerson = readTask.Result;
            }
            return View(ContactPerson);
        }

        [HttpPost]
        public ActionResult UpdateContactPerson(ContactPersonUpdateBindingModel ContactPerson)
        {

            var putTask = ApiHelper.ApiClient.PutAsJsonAsync("api/ContactPerson/" + ContactPerson.Id.ToString(), ContactPerson);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("ContactPeople");
            }
            else
            {
                return View("Error");
            }

            return View(ContactPerson);
        }

        // DELETE :ContactPerson/Delete
        public async Task<ActionResult> DeleteContactPerson(int Id)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("api/ContactPerson/" + Id.ToString());
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            else
            {
                return RedirectToAction("ContactPeople");
            }


        }
    } 
}