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
        //GET : TradeNote/1
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

        // POST : TradeNote/Create

        public ActionResult CreateTradeNote()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTradeNote(TradeNoteAddBindingModel NewTradeNote)
        {
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<TradeNoteAddBindingModel>("TradeNote", NewTradeNote);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                //Coś z tym trza zrobić !!
                return RedirectToAction("TradeNotes");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View("NewCompany");
            }
        }

        // PUT : Company/Update
        public ActionResult UpdateTradeNote(int Id)
        {
            TradeNoteAddBindingModel Company = new TradeNoteAddBindingModel();
            var responseTask = ApiHelper.ApiClient.GetAsync("TradeNote/" + Id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TradeNoteAddBindingModel>();
                readTask.Wait();

                Company = readTask.Result;
            }
            return View(Company);
        }

        [HttpPost]
        public ActionResult UpdateTradeNote(TradeNoteUpdateBindingModel TradeNote)
        {

            var putTask = ApiHelper.ApiClient.PutAsJsonAsync<TradeNoteUpdateBindingModel>("TradeNote/" + TradeNote.Id.ToString(), TradeNote);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("TradeNotes");
            }
            else
            {
                return View("Error");
            }

            return View(TradeNote);
        }

        // DELETE :TradeNote/Delete
        public async Task<ActionResult> DeleteTradeNote(int Id)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("TradeNote/" + Id.ToString());
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
                return RedirectToAction("TradeNotes");
            }


        }

    }
}