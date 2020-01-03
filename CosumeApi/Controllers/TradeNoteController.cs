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
        public async Task<ActionResult> TradeNotes(int? Id)
        {
            if(Id == null)
            {
                Id = RedirectInfo.LastCompany;
            }
            RedirectInfo.LastCompany = Id;
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/TradeNote/" +Id.ToString());
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
                IEnumerable<TradeNoteViewBindingModel> TradeNotes = null;
               
                TradeNotes = await response.Content.ReadAsAsync<List<TradeNoteViewBindingModel>>();
               // Model.Companies= await response.Content.ReadAsAsync<List<CompanyViewPublicModel>>();
                //Users = await response.Content.ReadAsAsync<List<AccountDisplayBindingModel>>();
                return View(TradeNotes);
            }
            else
            {
                return View("SomethingWrong");
            }
        }

        // GET :TradeNote/Particular/1
        public async Task<ActionResult> TradeNote(int Id)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("api/TradeNote/Particular/?id=" + Id.ToString());
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
                TradeNoteViewBindingModel Model = new TradeNoteViewBindingModel();
                Model = await response.Content.ReadAsAsync<TradeNoteViewBindingModel>();
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
            var postTask = ApiHelper.ApiClient.PostAsJsonAsync<TradeNoteAddBindingModel>("api/TradeNote", NewTradeNote);
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
                return View("NewCompany");// z tym coś trzeba zrobić
            }
        }

        // PUT : TradeNote/Update
        public ActionResult UpdateTradeNote(int Id)
        {
            TradeNoteUpdateBindingModel TradeNote = new TradeNoteUpdateBindingModel();
            var responseTask = ApiHelper.ApiClient.GetAsync("api/TradeNote/" + Id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TradeNoteUpdateBindingModel>();
                readTask.Wait();

                TradeNote = readTask.Result;
            }
            return View(TradeNote);
        }

        [HttpPost]
        public ActionResult UpdateTradeNote(TradeNoteUpdateBindingModel TradeNote)
        {

            var putTask = ApiHelper.ApiClient.PutAsJsonAsync<TradeNoteUpdateBindingModel>("api/TradeNote/" + TradeNote.Id.ToString(), TradeNote);
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
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("api/TradeNote/" + Id.ToString());
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