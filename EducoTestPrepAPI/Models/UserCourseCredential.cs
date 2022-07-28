using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EducoTestPrepAPI.Models
{
    public class UserCourseCredential
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string sectionId { get; set; }
    }

    public class UserCourseKDModulesCredential
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string sectionId { get; set; } 
        [Required]
        public string kdId { get; set; }
    }
}