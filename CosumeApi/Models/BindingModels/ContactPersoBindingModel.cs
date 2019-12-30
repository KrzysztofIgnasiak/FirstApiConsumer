using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosumeApi.Models.BindingModels
{
    public class ContactPersonAddBindingModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Phone { get; set; }

        public string Mail { get; set; }

        public string Position { get; set; }

        public int? CompanyId { get; set; }
    }

    public class ContactPersonUpdateBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Phone { get; set; }

        public string Mail { get; set; }

        public string Position { get; set; }

        public int? CompanyId { get; set; }
    }
    public class ContactPersonGetBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Phone { get; set; }

        public string Mail { get; set; }

        public string Position { get; set; }

        public int? CompanyId { get; set; }

        public string UserId { get; set; }
    }
}