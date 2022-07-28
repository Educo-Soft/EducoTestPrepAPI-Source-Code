using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducoTestPrepAPI.Models
{
    public class UserCourseDetails
    {
        public string TermId { get; set; }
        public string Term_Name { get; set; }
        public string CourseId { get; set; }
        public string Course_Name { get; set; }
        public string SectionId { get; set; }
        public string Section_Name { get; set; }
        public string Term_StartDate { get; set; }
        public string Term_EndDate { get; set; }
        public string Duration { get; set; }
    }
}