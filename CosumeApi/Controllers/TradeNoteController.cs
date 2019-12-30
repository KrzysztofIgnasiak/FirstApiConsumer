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
    public class TradeNoteController : Controller
    {
        public async Task<ActionResult> TradeNotes(int Id)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("TradeNote/" +Id.ToString());
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
                TradeNoteViewModel Model = new TradeNoteViewModel();
                Model.TradeNotes = await response.Content.ReadAsAsync<List<TradeNoteViewBindingModel>>();
               // Model.Companies= await response.Content.ReadAsAsync<List<CompanyViewPublicModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View(Model);
            }
            else
            {
                return View("SomethingWrong");
            }
        }
    }
}