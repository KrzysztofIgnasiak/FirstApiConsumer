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
    public class CompanyController : Controller
    {
        // GET: Company
        public async Task<ActionResult> Companies()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Company");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<CompanyViewPublicModel> Companies = null;
                DisplayCompanyViewModel Model = new DisplayCompanyViewModel();
                //Model.Companies = await response.Content.ReadAsAsync<List<CompanyViewPublicModel>>();
                Companies = await response.Content.ReadAsAsync<List<CompanyViewPublicModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                if(UserInfo.IsAdmin == true)
                {
                    return View(Companies);
                }
                else
                {
                    return View("CompaniesNormal");
                }
                
            }
            else
            {
                return View("SomethingWrong");
            }
        }
        // GET: Company/1
        public async Task<ActionResult> Company(int Id)
        {
            CompanyViewPublicModel Company = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Company/" +Id.ToString());
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
                Company = await response.Content.ReadAsAsync<CompanyViewPublicModel>();
                return View(Company);
            }
           else
            {
                return View("SomethingWrong");
            }
        }
        public async Task<ActionResult> GetCompaniesByIndustryAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetCompaniesByIndustryAsync(IndustrySearchBindingModel Industry)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/Company/ByIndustry?IndustryId="+Industry.Id);
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
                DisplayCompanyViewModel Model = new DisplayCompanyViewModel();
                Model.Companies = await response.Content.ReadAsAsync<List<CompanyViewPublicModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View("Companies",Model);
            }
            else
            {
                return View("SomethingWrong");
            }
        }

        // POST : Company/Create

        public ActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCompany(CompanyAddBindingModel NewCompany)
        {
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<CompanyAddBindingModel>("api/Company", NewCompany);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Companies");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("NewCompany");
            }
        }

        // PUT : Company/Update
        public ActionResult UpdateCompany(int Id)
        {
            CompanyUpdateBindingModel Company = new CompanyUpdateBindingModel();
            var responseTask = ApiHelper.ApiClient.GetAsync("api/Company/"+ Id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<CompanyUpdateBindingModel>();
                readTask.Wait();

                Company = readTask.Result;
            }
            return View(Company);
        }

        [HttpPost]
        public ActionResult UpdateCompany(CompanyUpdateBindingModel Company)
        {
           
                var putTask = ApiHelper.ApiClient.PutAsJsonAsync<CompanyUpdateBindingModel>("api/Company/" +Company.Id.ToString(), Company);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Companies");
                }
                else
                {
                return View("Error");
                }
            
            return View(Company);
        }
    

            // DELETE :Company/Delete
            public async Task<ActionResult> DeleteCompany(int Id)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("api/Company/" +Id.ToString());
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("Not Found");
            }
            else
            {
                return RedirectToAction("Companies");
            }


        }
    }
}