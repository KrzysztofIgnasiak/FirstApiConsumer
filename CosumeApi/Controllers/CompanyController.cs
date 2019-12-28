﻿using CosumeApi.Models;
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
        public async Task<ActionResult> Companies()
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
                return View("SomethingWrong");
            }
        }
        // GET: Company
        public async Task<ActionResult> Company()
        {
            CompanyViewPublicModel Company = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("Company/1");
            if (response.IsSuccessStatusCode)
            {
                Company = await response.Content.ReadAsAsync<CompanyViewPublicModel>();
            }
            return View(Company);
        }

        // POSt : Company
        
        public ActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCompany(CompanyAddBindingModel NewCompany)
        {
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<CompanyAddBindingModel>("NewCompany", NewCompany);
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

        // POST : Company
        public ActionResult UpdateCompany(int Id)
        {
            CompanyUpdateBindingModel Company = new CompanyUpdateBindingModel();
            var responseTask = ApiHelper.ApiClient.GetAsync(""+ Id.ToString());
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64189/api/student");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<CompanyUpdateBindingModel>("Company", Company);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(Company);
        }
    

            // DELETE :Company/Delete
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
}