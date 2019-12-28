using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosumeApi.Models
{
    public class CompanyAddBindingModel
    {
        public string Name { get; set; }
        public int Nip { get; set; }
        public int? IndustryId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

    }

    public class CompanyUpdateBindingModel
    {
        public string Name { get; set; }
        public int? Nip { get; set; }
        public int? IndustryId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }

    public class CompanyViewPublicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Nip { get; set; }
        public int? IndustryId { get; set; }
        public string IdustryName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string userID { get; set; }
    }
}