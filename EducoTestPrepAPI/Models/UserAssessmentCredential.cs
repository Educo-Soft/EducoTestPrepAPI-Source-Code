using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EducoTestPrepAPI.Models
{
    public class UserAssessmentKdTestListCredential
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string sectionId { get; set; }
        [Required]
        public string kdId { get; set; }
    }

    public class UserAssessmentModuleTestListCredential
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string sectionId { get; set; }
        [Required]
        public string moduleId { get; set; }
    }

    public class UserAssessmentMockTestListCredential
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string sectionId { get; set; }
    }

    public class UserAssessmentTestInfoCredential
    {
        [Required]
        public string testId { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public string sectionId { get; set; }
    }

    public class UserAssessmentInitiateTestPaperCredential
    {
        [Required]
        public string testId { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public string sectionId { get; set; }
        [Required]
        public string lastTestUserId { get; set; }        
    }

    public class UserAssessmentDisplayTestQuestionCredential
    {
        [Required]
        public string testuserid { get; set; }
        [Required]
        public string quescontid { get; set; }
        [Required]
        public string pgno { get; set; }
    }

    public class UserAssessmentSubmitTestQuestionCredential
    {
        [Required]
        public string testuserid { get; set; }
        [Required]
        public string contid { get; set; }
        [Required]
        public string testdata { get; set; }
        [Required]
        public string pgno { get; set; }
        [Required]
        public string timespent { get; set; }
        [Required]
        public string resdata { get; set; }
        [Required]
        public string totaltimespent { get; set; }
        [Required]
        public string sectionId { get; set; }
        [Required]
        public string CallReference { get; set; }
    }

    public class UserAssessmentTestAttemptResultCredential
    {
        [Required]
        public string TestUserID { get; set; }
        [Required]
        public string SectionID { get; set; }
    }

    public class UserAssessmentTestAttemptQuesStatusCredential
    {
        [Required]
        public string TestUserTestPartID { get; set; }
    }

    public class UserAssessmentDispStudTestResultsCredential
    {
        [Required]
        public string TestUserTestPartID { get; set; }
        [Required]
        public string TestQuesID { get; set; }
    }
}