using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EducoTestPrep.Api.Course;
using System.Text;

namespace EducoTestPrepAPI.Models
{
    public class UserCourse : PageBase
    {
        Api_Course objApi_UserCourse = null;
        StringBuilder spParam = null;
        DataSet dst = null;

        public UserCourse()
        {
            objApi_UserCourse = new Api_Course();
        }

        public DataSet GetUserCourseInfo(string userId, string sectionId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(sectionId);

            dst = objApi_UserCourse.GetUserCourseInfo(spParam.ToString());

            return dst;
        }

        public DataSet GetCourseKDList(string userId, string sectionId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(sectionId);

            dst = objApi_UserCourse.GetCourseKDList(spParam.ToString());

            return dst;
        }

        public DataSet GetKDModuleList(string userId, string sectionId, string kdId)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userId).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(sectionId).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(kdId);

            dst = objApi_UserCourse.GetKDModuleList(spParam.ToString());

            return dst;
        }       
    }
}