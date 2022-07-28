using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EducoTestPrepAPI.Models
{
    public class UserCredential
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserCredentialForgetPassword
    {
        [Required]
        public string Email { get; set; }        
    }

    public class UserCredentialRegisterGuestUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string strImage { get; set; }
        [Required]
        public string strImageType { get; set; }
    }
}