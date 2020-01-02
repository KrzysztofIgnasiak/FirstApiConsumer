using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosumeApi.Models
{
    public class TokenBindingModel
    {
        public string access_token { get; set; }
    }
    public class LogInBindingModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    
        public class AccountDisplayBindingModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string NameOfUser { get; set; }
            public string Surname { get; set; }
            
   
            public DateTime DateofBirth { get; set; }
            public string Email { get; set; }

        }

        public class AccountSetBindingModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string NameOfUser { get; set; }
            public string Surname { get; set; }

            public DateTime? DateofBirth { get; set; }
            public string Email { get; set; }
        }

        public class AccountAddBindingModel
        {
        public string UserName { get; set; }
        public string NameOfUser { get; set; }
        public string Surname { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPasword { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Email { get; set; }
        }
    
}