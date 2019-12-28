using CosumeApi.Models;
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
        // GET: Companies
        public async Task<ActionResult> Users()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Company");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("Unauthorized");
            }
            if (response.IsSuccessStatusCode)
            {
                DisplayCompanyViewModel Model = new DisplayCompanyViewModel();
                Model.Companies = await response.Content.ReadAsAsync<List<CompanyViewPublicModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View(Model);
            }
            else
            {
                return View();
            }
        }
        // GET: Company
        public async Task<ActionResult> User()
        {
            CompanyViewPublicModel Company = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Company/1");
            if (response.IsSuccessStatusCode)
            {
                Company = await response.Content.ReadAsAsync<CompanyViewPublicModel>();
            }
            return View(Company);
        }

        // GET :Company/Delete
        public async Task<ActionResult> Delete()
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("Company/1");
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
                return View("Companies");
            }


        }
}