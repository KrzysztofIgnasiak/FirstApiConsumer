using CosumeApi.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosumeApi.Models.ViewModels
{
    public class ContactPeopleViewModel
    {
        public List<ContactPersonGetBindingModel> ContactPeople { get; set; }
    }
}