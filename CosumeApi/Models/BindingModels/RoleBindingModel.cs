using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosumeApi.Models.BindingModels
{
   public class RoleViewBindingModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class RoleAddBindingModel
    {
        public string Name { get; set; }
    }

}