using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EducoTestPrepAPI.Models;
using System.Web.Http.ModelBinding;
using Educo.ELS.Encryption;
using System.Data;
using System.IO;
using Educo.ELS.SystemSettings;
using System.Text;

namespace EducoTestPrepAPI.Controllers
{
    public class UsersController : ApiController
    {
        /// <summary>
        ///This api call takes user's credential i.e.,  'Email' and 'Password'  and 
        ///return user details like 'userId', 'first_name', 'last_name', 'email', 'mobile', 'sectionid'  with 'status' and 'message' upon successful login.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in login.
        /// </summary>
        /// <param name="userCredential">User credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully Login",
        ///   "response": 
        ///      {  
        ///        "userId": "",
        ///        "first_name": "",
        ///        "last_name": "",
        ///        "email": "",
        ///        "mobile": "",
        ///        "sectionid":""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "Username and password do not match/Error while login"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("VerifyUserLogin")]
        public HttpResponseMessage VerifyUserLogin([ModelBinder] UserCredential userCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EncryptDecrypt objEncrypt = new EncryptDecrypt();
                    User objUser = new User();
                    DataSet dst = objUser.VerifyUserLogin(objEncrypt.Encrypt(userCredential.Email.ToUpper().Trim()), objEncrypt.Encrypt(userCredential.Password.Trim()));

                    if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                    {
                        if (dst.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            if (userCredential.Password.Trim().ToUpper() == objEncrypt.Decrypt(Convert.ToString(dst.Tables[0].Rows[0]["Password"])).ToUpper())
                            {
                            UserLoginDetails userLoginDetails = new UserLoginDetails();
                            foreach (DataRow dr in dst.Tables[0].Rows)
                            {
                                userLoginDetails.userId = Convert.ToString(dr["UsersId"]);
                                userLoginDetails.first_name = Convert.ToString(dr["Users_FirstName"]);
                                userLoginDetails.last_name = Convert.ToString(dr["Users_LastName"]);
                                userLoginDetails.email = Convert.ToString(dr["Users_Email"]);
                                userLoginDetails.mobile = Convert.ToString(dr["Users_Mobile"]);
                                userLoginDetails.sectionid = Convert.ToString(dr["SectionID"]);
                            }

                            var resMessage = new
                            {
                                status = "1",
                                message = "succesfully login",
                                response = userLoginDetails
                            };

                            return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200 

                            }
                            else  //if password does not matched
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Password does not matched"
                                };
                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200
                            }
                        }
                        else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                        {
                            var resMessage = new
                            {
                                status = "0",
                                message = "Error While Login"
                            };

                            return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                        }
                        else if (dst.Tables[0].Rows[0][0].ToString() == "-2")
                        {
                            var resMessage = new
                            {
                                status = "0",
                                message = dst.Tables[0].Rows[0]["VarErrorMsg"].ToString()
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                        }
                    }
                    else
                    {
                        var resMessage = new
                        {
                            status = "0",
                            message = "No data found"
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception While Login"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed user credential"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while login"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's login id i.e.,  'Email' to retrive forgotten password and 
        ///return user details like 'userId', 'first_name', 'last_name', 'email', 'password' with 'status' and 'message' upon successful login.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in login.
        /// </summary>
        /// <param name="userCredentialForgetPassword">User credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully Login",
        ///   "response": 
        ///      {  
        ///        "userId": "",
        ///        "first_name": "",
        ///        "last_name": "",
        ///        "email": "",
        ///        "password": ""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "Username does not exist/Error message"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetForgotPassword")]
        public HttpResponseMessage GetForgotPassword([ModelBinder] UserCredentialForgetPassword userCredentialForgetPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EncryptDecrypt objEncrypt = new EncryptDecrypt();
                    User objUser = new User();
                    DataSet dst = objUser.GetForgotPassword(objEncrypt.Encrypt(userCredentialForgetPassword.Email.ToUpper().Trim()));

                    if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                    {
                        if (dst.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            string msgForgetPasswordExternalMailStatus = "";
                            UserForgetPasswordDetails userForgetPasswordDetails = new UserForgetPasswordDetails();
                            foreach (DataRow dr in dst.Tables[0].Rows)
                            {
                                userForgetPasswordDetails.userId = Convert.ToString(dr["UsersId"]);
                                userForgetPasswordDetails.first_name = Convert.ToString(dr["Users_FirstName"]);
                                userForgetPasswordDetails.last_name = Convert.ToString(dr["Users_LastName"]);
                                userForgetPasswordDetails.email = Convert.ToString(dr["Users_Email"]);
                                userForgetPasswordDetails.password = objEncrypt.Decrypt(Convert.ToString(dr["Resource_Password"]));
                            }

                            //Begin -- Sending external mail to registered email
                            try
                            {
                                SendForgetPasswordExternalMail(userForgetPasswordDetails.email, userForgetPasswordDetails.password, userForgetPasswordDetails.first_name + " " + userForgetPasswordDetails.last_name);

                                msgForgetPasswordExternalMailStatus += "Your password has been sent to your mail.";
                            }
                            catch (Exception ex)
                            {
                                msgForgetPasswordExternalMailStatus += "Exception in sending external mail to registered mail: " + ex.Message;
                            }
                            //End -- Sending external mail to registered email

                            var resMessage = new
                            {
                                status = "1",
                                message = msgForgetPasswordExternalMailStatus,
                                response = userForgetPasswordDetails
                            };

                            return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                        }
                        else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                        {
                            var resMessage = new
                            {
                                status = "0",
                                message = "Error while retriving the password."
                            };

                            return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                        }
                        else if (dst.Tables[0].Rows[0][0].ToString() == "-2")
                        {
                            var resMessage = new
                            {
                                status = "0",
                                message = dst.Tables[0].Rows[0]["VarErrorMsg"].ToString()
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                        }
                    }
                    else
                    {
                        var resMessage = new
                        {
                            status = "0",
                            message = "No data found"
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200  
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception occured while retriving the password."
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed user credentials"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error occured while retriving the password."
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's data i.e.,  'First Name', 'Last Name', 'Mobile Number', 'Email', 'Password', 'strImage', 'strImageType'  and 
        ///return user details like 'userId', 'first_name', 'last_name', 'email', 'mobile', 'sectionid'  with 'status' and 'message' upon successful registration.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in registration.
        ///Note: 'strImage' is in Base64String format and 'strImageType' is the extension of image i.e, jpg, png etc.
        /// </summary>
        /// <param name="userCredentialRegisterGuestUser">User credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "succesfully registered and login as guest. Also include message for image saving status and mail firing status.",
        ///   "response": 
        ///      {  
        ///        "userId": "",
        ///        "first_name": "",
        ///        "last_name": "",
        ///        "email": "",
        ///        "mobile": "",
        ///        "sectionid":""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "Error message while registering the user's info and login"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("RegisterGuestUser")]
        public HttpResponseMessage RegisterGuestUser([ModelBinder] UserCredentialRegisterGuestUser userCredentialRegisterGuestUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userCredentialRegisterGuestUser.FirstName.Trim() != "" && userCredentialRegisterGuestUser.LastName.Trim() != "" 
                        && userCredentialRegisterGuestUser.Email.Trim() != "" && userCredentialRegisterGuestUser.MobileNumber.Trim() != "" 
                        && userCredentialRegisterGuestUser.Password.Trim() != "" && userCredentialRegisterGuestUser.strImage.Trim() != ""
                        && userCredentialRegisterGuestUser.strImageType.Trim() != "")
                    {
                        EncryptDecrypt objEncrypt = new EncryptDecrypt();
                        User objUser = new User();
                        DataSet dst = objUser.RegisterGuestUser(userCredentialRegisterGuestUser.FirstName, userCredentialRegisterGuestUser.LastName, 
                            userCredentialRegisterGuestUser.MobileNumber.Trim(), userCredentialRegisterGuestUser.Email, 
                            objEncrypt.Encrypt(userCredentialRegisterGuestUser.Email.ToUpper().Trim()), 
                            objEncrypt.Encrypt(userCredentialRegisterGuestUser.Password.Trim()));

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                if (userCredentialRegisterGuestUser.Password.Trim().ToUpper() == objEncrypt.Decrypt(Convert.ToString(dst.Tables[0].Rows[0]["Password"])).ToUpper())
                                {
                                    string userId = "";
                                    string imagePath = "";
                                    string courseName = "";
                                    string imgExtn = ".jpg";
                                    string msg_ImageSaveStatus = "";
                                    UserLoginDetails userLoginDetails = new UserLoginDetails();

                                    foreach (DataRow dr in dst.Tables[0].Rows)
                                    {
                                        userLoginDetails.userId = Convert.ToString(dr["UsersId"]);
                                        userLoginDetails.first_name = Convert.ToString(dr["Users_FirstName"]);
                                        userLoginDetails.last_name = Convert.ToString(dr["Users_LastName"]);
                                        userLoginDetails.email = Convert.ToString(dr["Users_Email"]);
                                        userLoginDetails.mobile = Convert.ToString(dr["Users_Mobile"]);
                                        userLoginDetails.sectionid = Convert.ToString(dr["SectionID"]);

                                        userId = Convert.ToString(dr["UsersId"]);
                                        imagePath = Convert.ToString(dr["ImagePath"]);
                                        courseName = Convert.ToString(dr["Course_Name"]);
                                    }

                                    //Begin -- Saving the user profile image
                                    try
                                    {
                                        if (userCredentialRegisterGuestUser.strImage.Length > 100)
                                        {
                                            if (!Directory.Exists(imagePath))
                                                Directory.CreateDirectory(imagePath);

                                            imgExtn = "." + userCredentialRegisterGuestUser.strImageType.Trim().ToLower();
                                            //byte[] newImage = ResizeImageFile(Convert.FromBase64String(userCredentialRegisterGuestUser.strImage), 60);
                                            File.WriteAllBytes(imagePath + "\\" + "BB_" + userId + imgExtn, Convert.FromBase64String(userCredentialRegisterGuestUser.strImage));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        msg_ImageSaveStatus = " Image save status: " + ex.Message;
                                    }                                    
                                    //End -- Saving the user profile image

                                    //Begin -- Sending external mail for registration info
                                    try
                                    {
                                        SendGuestStudentExternalMail(userCredentialRegisterGuestUser.Email, userCredentialRegisterGuestUser.Password, courseName,
                                            userCredentialRegisterGuestUser.FirstName + " " + userCredentialRegisterGuestUser.LastName);

                                        msg_ImageSaveStatus += " Registration mail status: " + "Email has been sent to the registered email id.";
                                    }
                                    catch (Exception ex)
                                    {
                                        msg_ImageSaveStatus += " Registration mail status: " + ex.Message;
                                    }
                                    //End -- Sending external mail for registration info

                                    var resMessage = new
                                    {
                                        status = "1",
                                        message = "succesfully registered and login as guest. " + msg_ImageSaveStatus,
                                        response = userLoginDetails
                                    };

                                    return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200 

                                }
                                else  //if password does not matched
                                {
                                    var resMessage = new
                                    {
                                        status = "0",
                                        message = "Password does not matched"
                                    };
                                    return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200
                                }
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error While registering as guest. Error message is :" + dst.Tables[0].Rows[0]["VarErrorMsg"].ToString()
                                };

                                return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-2")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = dst.Tables[0].Rows[0]["VarErrorMsg"].ToString()
                                };
                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                        }
                        else
                        {
                            var resMessage = new
                            {
                                status = "0",
                                message = "No data found"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200 
                        }
                    }
                    else
                    {
                        var resMessage = new
                        {
                            status = "0",
                            message = "Guest registration form data are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while registering the user"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user's RegisterGuestUser credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while registering the guest user information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        private void SendGuestStudentExternalMail(string strUsername, string password, string CourseName, string display_name)
        {
            AutomatedMails objSendExternalMail = new AutomatedMails();
            string msgSubject = "Login Information";
            string msgbody = "";
            string msgText = "";

            msgbody = "Dear " + display_name + ",<BR><BR>" +
                      "Welcome to EducoTestPrep, a Unique and highly effective approach to prepare you for Success in the Government Job-Placement Tests. " +
                      "<BR><BR>";

            msgbody = msgbody + "You are registered as Guest user into following course:" + "<BR><BR>" + "<b>Course:</b>&nbsp;&nbsp;&nbsp;" + CourseName + "<BR>" +
                      "<b>Course Duration:</b>&nbsp;&nbsp;&nbsp;14 days<BR><BR>";

            msgbody = msgbody + "You can go to <a href =#> www.educotestprep.com </a> and enter the following username and " +
                     "password to login : " + "<BR><BR>" +

                     "Your user name is: <a href =#>" + strUsername + "</a><BR>" +
                     "Your password is: " + password + "<BR><BR>" +

                     "This is a system generated e-mail so please do not reply to this e-mail. If you " + "<BR>" +
                     "have any questions about your account then please send an e-mail to  " + "<BR>" +
                     "<a href = #>info@educosoft.com</a><BR><BR>EducoTestPrep Team ";

            msgText = msgbody.ToString();

            objSendExternalMail.ExternalMail(strUsername, msgSubject.ToString(), msgText.ToString(), "Strings-En");

            objSendExternalMail = null;
        }

        private void SendForgetPasswordExternalMail(string username, string password, string display_name)
        {
            //Object
            AutomatedMails objAutomatedMails = new AutomatedMails();
            DataSet dstForgotPassword = new DataSet();
            StringBuilder paramList = new StringBuilder();

            //Variables
            string msgbody = "";
            string msgsalutation = "";
            string msgEnding = "";
            string msgSubject = "";
            string msgText = "";            
            char colSep = (char)195;
            string langFile = "Strings-En";

            //ParamList
            paramList.Length = 0;
            paramList.Append("1").Append(colSep).Append("73");

            dstForgotPassword = objAutomatedMails.getExternalMailDetails(paramList.ToString(), langFile);
            if (dstForgotPassword.Tables[0].Rows[0][0].ToString() == "0")
            {
                msgbody = dstForgotPassword.Tables[0].Rows[0][6].ToString();
                msgsalutation = dstForgotPassword.Tables[0].Rows[0][10].ToString();
                msgEnding = dstForgotPassword.Tables[0].Rows[0][11].ToString();
                msgSubject = dstForgotPassword.Tables[0].Rows[0][5].ToString();
            }
            msgbody = msgbody.Replace("DISP_NAME", display_name.ToString());
            msgbody = msgbody.Replace("USER_NAME", username.ToLower().ToString());
            msgbody = msgbody.Replace("PASS_WORD", password.ToString());
            msgText = msgsalutation.ToString() + "<BR>" + msgbody.ToString() + "<BR>" + msgEnding.ToString();

            //Send Email
            objAutomatedMails.ExternalMail(username, msgSubject.ToString(), msgText.ToString(), langFile);

            objAutomatedMails = null;
            dstForgotPassword = null;
            paramList = null;
        }
    }
}