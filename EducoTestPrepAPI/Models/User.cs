using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EducoTestPrep.Api.User;
using System.Text;

namespace EducoTestPrepAPI.Models
{
    public class User : PageBase
    {
        Api_User objApi_User = null;
        StringBuilder spParam = null;
        DataSet dst = null;

        public User()
        {
            objApi_User = new Api_User();
        }

        public DataSet VerifyUserLogin(string userLogInName, string password)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userLogInName).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(password);

            dst = objApi_User.VerifyUserLogin(spParam.ToString());

            return dst;
        }

        public DataSet GetForgotPassword(string userLogInName)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(userLogInName);

            dst = objApi_User.GetForgotPassword(spParam.ToString());

            return dst;
        }

        public DataSet RegisterGuestUser(string FirstName, string LastName, string MobileNumber, string Email, string EncryptedUserName, string EncryptedPassword)
        {
            spParam = new StringBuilder();

            spParam.Append("1").Append(colSeperator).Append(FirstName).Append(colSeperator);
            spParam.Append("2").Append(colSeperator).Append(LastName).Append(colSeperator);
            spParam.Append("3").Append(colSeperator).Append(MobileNumber).Append(colSeperator);
            spParam.Append("4").Append(colSeperator).Append(Email).Append(colSeperator);
            spParam.Append("5").Append(colSeperator).Append(EncryptedUserName).Append(colSeperator);
            spParam.Append("6").Append(colSeperator).Append(EncryptedPassword);

            dst = objApi_User.RegisterGuestUser(spParam.ToString());

            return dst;
        }
    }
}