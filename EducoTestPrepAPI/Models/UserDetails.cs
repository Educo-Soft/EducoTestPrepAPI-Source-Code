using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducoTestPrepAPI.Models
{
    public class UserDetails
    {
        public string userId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
    }

    public class UserLoginDetails
    {
        public string userId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string sectionid { get; set; }
    }

    public class UserForgetPasswordDetails
    {
        public string userId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}