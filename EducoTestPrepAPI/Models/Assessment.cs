using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EducoTestPrep.Api.Assessment;
using System.Text;

namespace EducoTestPrepAPI.Models
{
    public class Assessment : PageBase
    {
        Api_TestPaper objApi_TestPaper = null;
        StringBuilder spParam = null;
        DataSet dst = null;

        public Assessment()
        {
            objApi_TestPaper = new Api_TestPaper();
        }

        public DataSet GetAssessmentList(string userLogInName, string password)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userLogInName).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(password).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append("1");

            dst = objApi_TestPaper.GetStudentMockTestList(spParam.ToString());

            return dst;
        }

        public DataSet User_Login(string userLogInName, string password)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userLogInName).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(password).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append("0");

            dst = objApi_TestPaper.GetStudentMockTestList(spParam.ToString());

            return dst;
        }

        public DataSet GetKdTestList(string userId, string sectionId, string kdId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(sectionId).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(kdId);

            dst = objApi_TestPaper.GetKdTestList(spParam.ToString());

            return dst;
        }

        public DataSet GetModuleTestList(string userId, string sectionId, string moduleId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(sectionId).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(moduleId);

            dst = objApi_TestPaper.GetModuleTestList(spParam.ToString());

            return dst;
        }

        public DataSet GetMockTestList(string userId, string sectionId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(sectionId);

            dst = objApi_TestPaper.GetMockTestList(spParam.ToString());

            return dst;
        }

        public DataSet GetTestInfo(string testId, string userId, string sectionId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(testId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(sectionId);

            dst = objApi_TestPaper.GetTestInfo(spParam.ToString());

            return dst;
        }

        public DataSet InitiateTestPaper(string testId, string userId, string sectionId, string lastTestUserId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(testId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(lastTestUserId).Append(colSeperator);
            spParam.Append("4").Append(colSeperator).Append(sectionId);

            dst = objApi_TestPaper.InitiateTestPaper(spParam.ToString());

            return dst;
        }

        public DataSet BuildTestPart(string testId, string userId, string contid, string testuserid, string sectionId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(testId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(contid).Append(colSeperator);
            spParam.Append("4").Append(colSeperator).Append(testuserid).Append(colSeperator);
            spParam.Append("5").Append(colSeperator).Append(sectionId);

            dst = objApi_TestPaper.BuildTestPart(spParam.ToString());

            return dst;
        }

        public DataSet DisplayTestQuestion(string testuserid, string contid, string pgno)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(testuserid).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(contid).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(pgno);

            dst = objApi_TestPaper.DisplayTestQuestion(spParam.ToString());

            return dst;
        }

        public DataSet SubmitTestQuestion(string testuserid, string contid, string testdata, string pgno, string timespent, string resdata,
            string totaltimespent, string sectionId, bool updateOnlyTime = false)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(testuserid).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(contid).Append(colSeperator);

            spParam.Append("3").Append(colSeperator).Append(testdata).Append(colSeperator);
            spParam.Append("4").Append(colSeperator).Append(pgno).Append(colSeperator);

            spParam.Append("5").Append(colSeperator).Append(timespent).Append(colSeperator);
            spParam.Append("6").Append(colSeperator).Append(resdata).Append(colSeperator);

            spParam.Append("7").Append(colSeperator).Append(totaltimespent).Append(colSeperator);
            spParam.Append("8").Append(colSeperator).Append(updateOnlyTime).Append(colSeperator);
            spParam.Append("9").Append(colSeperator).Append(sectionId);

            dst = objApi_TestPaper.SubmitTestQuestion(spParam.ToString());

            return dst;
        }

        public DataSet SubmitTestQuestion_AssessmentSubmit(string testuserid, string savemode, string timespent, string totaltimespent, string sectionId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(testuserid).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(savemode).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(timespent).Append(colSeperator);
            spParam.Append("4").Append(colSeperator).Append(totaltimespent).Append(colSeperator);
            spParam.Append("5").Append(colSeperator).Append(sectionId);

            dst = objApi_TestPaper.SubmitTestQuestion_AssessmentSubmit(spParam.ToString());

            return dst;
        }

        public DataSet GetTestAttemptResult(string TestUserID, string SectionID)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(TestUserID).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(SectionID);

            dst = objApi_TestPaper.GetTestAttemptResult(spParam.ToString());

            return dst;
        }

        public DataSet GetTestAttemptQuesStatus(string TestUserTestPartID)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(TestUserTestPartID);

            dst = objApi_TestPaper.GetTestAttemptQuesStatus(spParam.ToString());

            return dst;
        }

        public DataSet DispStudTestResults(string TestUserTestPartID, string intQid)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(TestUserTestPartID).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(intQid);

            dst = objApi_TestPaper.DispStudTestResults(spParam.ToString());

            return dst;
        }

    }
}