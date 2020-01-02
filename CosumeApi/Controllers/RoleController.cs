﻿using CosumeApi.Models;
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
                RoleViewModel Model = new RoleViewModel();
                Model.Roles = await response.Content.ReadAsAsync<List<RoleBindingModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View(Model);
            }
            else
            {
                return View("SomethingWorng");
            }
        }
    }
}