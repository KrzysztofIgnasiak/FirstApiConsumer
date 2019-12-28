﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosumeApi.Models
{
    
        public class AccountDisplayBindingModel
        {
            public string NameOfUser { get; set; }
            public string Surname { get; set; }

            public DateTime DateofBirth { get; set; }
            public string Email { get; set; }

        }

        public class SetBindingModel
        {
            public string UserName { get; set; }
            public string NameOfUser { get; set; }
            public string Surname { get; set; }

            public DateTime? DateofBirth { get; set; }
            public string Email { get; set; }
        }
    
}