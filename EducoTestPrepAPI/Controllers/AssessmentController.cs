using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Educo.ELS.Encryption;
using EducoTestPrepAPI.Models;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Text;
using System.Net.Http.Formatting;

namespace EducoTestPrepAPI.Controllers
{
    public class AssessmentController : ApiController
    {
        // GET api/Assessment
        [ApiExplorerSettings(IgnoreApi = true)]
        /// <summary>
        /// This Api call is for testing purpose only.
        /// </summary>
        /// <returns>EducoTestPrep Api testing</returns>
        public string GetAssessmentList()
        {
            return "EducoTestPrep Value: Indranarayan";
        }
                
        /// <summary>
        ///This api call takes user's credential i.e.,  'Email' and 'Password'  and 
        ///return user details like 'userId', 'first_name', 'last_name', 'email', 'mobile'  with 'status' and 'message' upon successful login.
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
        ///        "mobile": ""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "Username and password do not match/Error while login"
        ///}
        /// </returns>

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ActionName("user_login")]
        public HttpResponseMessage user_login([ModelBinder] UserCredential userCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EncryptDecrypt objEncrypt = new EncryptDecrypt();
                    Assessment objAssessment = new Assessment();
                    DataSet dst = objAssessment.User_Login(objEncrypt.Encrypt(userCredential.Email.ToUpper().Trim()), objEncrypt.Encrypt(userCredential.Password.Trim()));

                    if (dst != null && dst.Tables.Count > 0 && dst.Tables.Count > 0)
                    {
                        if (dst.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            UserDetails userdetails = new UserDetails();
                            foreach (DataRow dr in dst.Tables[0].Rows)
                            {
                                userdetails.userId = Convert.ToString(dr["UsersId"]);
                                userdetails.first_name = Convert.ToString(dr["Users_FirstName"]);
                                userdetails.last_name = Convert.ToString(dr["Users_LastName"]);
                                userdetails.email = Convert.ToString(dr["Users_Email"]);
                                userdetails.mobile = Convert.ToString(dr["Users_Mobile"]);
                            }

                            var resMessage = new
                            {
                                status = "1",
                                message = "succesfully login",
                                response = userdetails
                            };

                            return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
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
                    message = "Invalid user credential"
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


        // GET api/Assessment/?userLogInName=Name&password=password
        /// <summary>
        /// This Api call will fetch student's assessments list provided that the user is active.
        /// </summary>
        /// <param name="userLogInName">User Email id as login id.</param>
        /// <param name="password">User password.</param>
        /// <returns>If user exists, return a dataset of values (KDId, KDName, ModuleId, ModuleName, TestID, TestName, MaxAttempts, ActualAttempts, AllAttemptAvgTotScore,
        /// MaxTestUserID, InitFlag, TestNumQues, TestSettingsMode, LastAttemptedID, TestType, SortOrder, TestMaxScoreObt, TestModuleId, Test_ModulePercent, 
        /// LastAttemptPerScore, TestUser_IsEval, TestSettings_TestModeType, IsAutoPracticeTest, AutoPracticeTestID). 
        /// Else if user does not exist, return a message "User Doesnot Exists" with status code "-1" 
        /// in json, xml format.
        /// </returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [ActionName("AssessmentList")]
        public DataSet GetAssessmentList(string userLogInName, string password)
        {
            EncryptDecrypt objEncrypt = new EncryptDecrypt();
            Assessment objAssessment = new Assessment();
            return objAssessment.GetAssessmentList(objEncrypt.Encrypt(userLogInName.ToUpper().Trim()), objEncrypt.Encrypt(password.Trim()));
        }

        /// <summary>
        ///This api call takes user's course KDTest credential i.e.,  'userid', 'sectionid' and 'kdId'  and 
        ///return user course KDTest list details like 'KDId', 'KDName', 'ModuleId','ModuleName', 'TestID', 'TestName', 'MaxAttempts', 'ActualAttempts', 'AllAttemptAvgTotScore', 'MaxTestUserID', 'InitFlag', 'TestNumQues', 'TestSettingsMode', 'LastAttemptedID', 'TestType', 'SortOrder', 'TestMaxScoreObt', 'TestModuleId', 'Test_ModulePercent', 'LastAttemptPerScore', 'TestUser_IsEval', 'TestSettings_TestModeType', 'IsAutoPracticeTest', 'AutoPracticeTestID' with 'status' and 'message' upon successful retrival of course KDTest list information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of course KDTest information.
        /// </summary>
        /// <param name="userAssessmentKdTestListCredential">User course KDTest info retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived course KDTest list",
        ///   "response": 
        ///      {  
        ///        "KDId": "",
        ///        "KDName": "",
        ///        "ModuleId": "",
        ///        "ModuleName": "",
        ///        "TestID": "",
        ///        "TestName": "",
        ///        "MaxAttempts": "",
        ///        "ActualAttempts": "",
        ///        "AllAttemptAvgTotScore": "",
        ///        "MaxTestUserID": "",
        ///        "InitFlag": "",
        ///        "TestNumQues": "",
        ///        "TestSettingsMode": "",
        ///        "LastAttemptedID": "",
        ///        "TestType": "",
        ///        "SortOrder": "",
        ///        "TestMaxScoreObt": "",
        ///        "TestModuleId": "",
        ///        "Test_ModulePercent": "",
        ///        "LastAttemptPerScore": "",
        ///        "TestUser_IsEval": "",
        ///        "TestSettings_TestModeType": "",
        ///        "IsAutoPracticeTest": "",
        ///        "AutoPracticeTestID": ""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There are no tests for the specified course./No data found/Error while retriving course kd list information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetKdTestList")]
        public HttpResponseMessage GetKdTestList([ModelBinder] UserAssessmentKdTestListCredential userAssessmentKdTestListCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentKdTestListCredential.userId.Trim() != "" && userAssessmentKdTestListCredential.sectionId.Trim() != "" && userAssessmentKdTestListCredential.kdId.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.GetKdTestList(userAssessmentKdTestListCredential.userId.Trim(), userAssessmentKdTestListCredential.sectionId.Trim(), userAssessmentKdTestListCredential.kdId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                List<DataRow> listKDTestDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userKDTestDetails = from datarow in listKDTestDetails
                                                        select new
                                                        {
                                                            KDId = datarow["KDId"],
                                                            KDName = datarow["KDName"],
                                                            ModuleId = datarow["ModuleId"],

                                                            ModuleName = datarow["ModuleName"],
                                                            TestID = datarow["TestID"],
                                                            TestName = datarow["TestName"],

                                                            MaxAttempts = datarow["MaxAttempts"],
                                                            ActualAttempts = datarow["ActualAttempts"],
                                                            AllAttemptAvgTotScore = datarow["AllAttemptAvgTotScore"],

                                                            MaxTestUserID = datarow["MaxTestUserID"],
                                                            InitFlag = datarow["InitFlag"],
                                                            TestNumQues = datarow["TestNumQues"],

                                                            TestSettingsMode = datarow["TestSettingsMode"],
                                                            LastAttemptedID = datarow["LastAttemptedID"],
                                                            TestType = datarow["TestType"],

                                                            SortOrder = datarow["SortOrder"],

                                                            TestMaxScoreObt = datarow["TestMaxScoreObt"],
                                                            TestModuleId = datarow["TestModuleId"],
                                                            Test_ModulePercent = datarow["Test_ModulePercent"],

                                                            LastAttemptPerScore = datarow["LastAttemptPerScore"],
                                                            TestUser_IsEval = datarow["TestUser_IsEval"],
                                                            TestSettings_TestModeType = datarow["TestSettings_TestModeType"],

                                                            IsAutoPracticeTest = datarow["IsAutoPracticeTest"],
                                                            AutoPracticeTestID = datarow["AutoPracticeTestID"]
                                                        };

                                var resMessage = new
                                {
                                    status = "1",
                                    message = "succesfully retrived course KDTest list",
                                    response = userKDTestDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving course KDTest list information"
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
                            message = "Course KDTest info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving course KDTest list information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user course KDTest credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving course KDTest list information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's course ModuleTest credential i.e.,  'userid', 'sectionid' and 'moduleId'  and 
        ///return user course ModuleTest list details like 'KDId', 'KDName', 'ModuleId','ModuleName', 'TestID', 'TestName', 'MaxAttempts', 'ActualAttempts', 'AllAttemptAvgTotScore', 'MaxTestUserID', 'InitFlag', 'TestNumQues', 'TestSettingsMode', 'LastAttemptedID', 'TestType', 'SortOrder', 'TestMaxScoreObt', 'TestModuleId', 'Test_ModulePercent', 'LastAttemptPerScore', 'TestUser_IsEval', 'TestSettings_TestModeType', 'IsAutoPracticeTest', 'AutoPracticeTestID' with 'status' and 'message' upon successful retrival of course ModuleTest list information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of course ModuleTest information.
        /// </summary>
        /// <param name="userAssessmentModuleTestListCredential">User course ModuleTest info retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived course ModuleTest list",
        ///   "response": 
        ///      {  
        ///        "KDId": "",
        ///        "KDName": "",
        ///        "ModuleId": "",
        ///        "ModuleName": "",
        ///        "TestID": "",
        ///        "TestName": "",
        ///        "MaxAttempts": "",
        ///        "ActualAttempts": "",
        ///        "AllAttemptAvgTotScore": "",
        ///        "MaxTestUserID": "",
        ///        "InitFlag": "",
        ///        "TestNumQues": "",
        ///        "TestSettingsMode": "",
        ///        "LastAttemptedID": "",
        ///        "TestType": "",
        ///        "SortOrder": "",
        ///        "TestMaxScoreObt": "",
        ///        "TestModuleId": "",
        ///        "Test_ModulePercent": "",
        ///        "LastAttemptPerScore": "",
        ///        "TestUser_IsEval": "",
        ///        "TestSettings_TestModeType": "",
        ///        "IsAutoPracticeTest": "",
        ///        "AutoPracticeTestID": ""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There are no tests for the specified course./No data found/Error while retriving course ModuleTest list information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetModuleTestList")]
        public HttpResponseMessage GetModuleTestList([ModelBinder] UserAssessmentModuleTestListCredential userAssessmentModuleTestListCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentModuleTestListCredential.userId.Trim() != "" && userAssessmentModuleTestListCredential.sectionId.Trim() != "" && userAssessmentModuleTestListCredential.moduleId.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.GetModuleTestList(userAssessmentModuleTestListCredential.userId.Trim(), userAssessmentModuleTestListCredential.sectionId.Trim(), userAssessmentModuleTestListCredential.moduleId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                List<DataRow> listModuleTestDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userModuleTestDetails = from datarow in listModuleTestDetails
                                                            select new
                                                            {
                                                                KDId = datarow["KDId"],
                                                                KDName = datarow["KDName"],
                                                                ModuleId = datarow["ModuleId"],

                                                                ModuleName = datarow["ModuleName"],
                                                                TestID = datarow["TestID"],
                                                                TestName = datarow["TestName"],

                                                                MaxAttempts = datarow["MaxAttempts"],
                                                                ActualAttempts = datarow["ActualAttempts"],
                                                                AllAttemptAvgTotScore = datarow["AllAttemptAvgTotScore"],

                                                                MaxTestUserID = datarow["MaxTestUserID"],
                                                                InitFlag = datarow["InitFlag"],
                                                                TestNumQues = datarow["TestNumQues"],

                                                                TestSettingsMode = datarow["TestSettingsMode"],
                                                                LastAttemptedID = datarow["LastAttemptedID"],
                                                                TestType = datarow["TestType"],

                                                                SortOrder = datarow["SortOrder"],

                                                                TestMaxScoreObt = datarow["TestMaxScoreObt"],
                                                                TestModuleId = datarow["TestModuleId"],
                                                                Test_ModulePercent = datarow["Test_ModulePercent"],

                                                                LastAttemptPerScore = datarow["LastAttemptPerScore"],
                                                                TestUser_IsEval = datarow["TestUser_IsEval"],
                                                                TestSettings_TestModeType = datarow["TestSettings_TestModeType"],

                                                                IsAutoPracticeTest = datarow["IsAutoPracticeTest"],
                                                                AutoPracticeTestID = datarow["AutoPracticeTestID"]
                                                            };
                                var resMessage = new
                                {
                                    status = "1",
                                    message = "Succesfully retrived course ModuleTest list",
                                    response = userModuleTestDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving course ModuleTest list information"
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
                            message = "Course ModuleTest info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving course ModuleTest list information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user course ModuleTest credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving course ModuleTest list information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's Mock Test credential i.e.,  'userid', 'sectionid' and 
        ///return user Mock Test list details like 'TestID', 'TestName', 'TestDate', 'MaxAttempts', 'ActualAttempts', 'AllAttemptAvgTotScore', 'TestAnsMode', 'TestAnsDate', 'TestResultMode', 'TestResultDate', 'MaxTestUserID', 'TestEndDate', 'TestApplTo', 'InitFlag', 'TestNumQues', 'TestTimeAllotted', 'TestTimeAppl', 'TestStatus', 'TestSettingsMode', 'LastAttemptedID', 'InstID', 'TzID', 'TestType', 'SortOrder', 'AttHelpText', 'EndDtHelpText', 'AllotedTimeHelpText', 'TestMaxScoreObt', 'Test_AtptDate', 'IsManAtempt', 'TestModuleId', 'Test_ModulePercent', 'LastAttemptPerScore', 'TestUser_IsEval', 'TestSettings_TestModeType', 'PswdMode	IsEval', 'GlobalPswd', 'IsAutoPracticeTest', 'AutoPracticeTestID', 'TestSettings_IsActive', 'TotalTimeSpent', 'Test_IsLabTest' with 'status' and 'message' upon successful retrival of Mock Test list information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of mock test information.
        /// </summary>
        /// <param name="userAssessmentMockTestListCredential">User Mock Test info retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived Mock Test list",
        ///   "response": 
        ///      {  
        ///        "TestID": "",
        ///        "TestName": "",
        ///        "TestDate": "",
        ///        "MaxAttempts": "",
        ///        "ActualAttempts": "",
        ///        "AllAttemptAvgTotScore": "",
        ///        "TestAnsMode": "",
        ///        "TestAnsDate": "",
        ///        "TestResultMode": "",
        ///        "TestResultDate": "",
        ///        "MaxTestUserID": "",
        ///        "TestEndDate": "",
        ///        "TestApplTo": "",
        ///        "InitFlag": "",
        ///        "TestNumQues": "",
        ///        "TestTimeAllotted": "",
        ///        "TestTimeAppl": "",
        ///        "TestStatus": "",
        ///        "TestSettingsMode": "",
        ///        "LastAttemptedID": "",
        ///        "InstID": "",
        ///        "TzID": "",
        ///        "TestType": "",
        ///        "SortOrder": "",
        ///        "AttHelpText": "",
        ///        "EndDtHelpText": "",
        ///        "AllotedTimeHelpText": "",
        ///        "HasPreRequisites": "",
        ///        "TestMaxScoreObt": "",
        ///        "Test_AtptDate": "",
        ///        "IsManAtempt": "",
        ///        "TestModuleId": "",
        ///        "Test_ModulePercent": "",
        ///        "LastAttemptPerScore": "",
        ///        "TestUser_IsEval": "",
        ///        "TestSettings_TestModeType": "",
        ///        "PswdMode": "",
        ///        "IsEval": "",
        ///        "GlobalPswd": "",
        ///        "IsAutoPracticeTest": "",
        ///        "AutoPracticeTestID": "",
        ///        "TestSettings_IsActive": "",
        ///        "TotalTimeSpent": "",
        ///        "Test_IsLabTest": ""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There are no tests for the specified course./No data found/Error while retriving Mock Test list information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetMockTestList")]
        public HttpResponseMessage GetMockTestList([ModelBinder] UserAssessmentMockTestListCredential userAssessmentMockTestListCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentMockTestListCredential.userId.Trim() != "" && userAssessmentMockTestListCredential.sectionId.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.GetMockTestList(userAssessmentMockTestListCredential.userId.Trim(), userAssessmentMockTestListCredential.sectionId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                List<DataRow> listMockTestDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userMockTestDetails = from datarow in listMockTestDetails
                                                            select new
                                                            {
                                                                TestID = datarow["TestID"],
                                                                TestName = datarow["TestName"],
                                                                TestDate = datarow["TestDate"],

                                                                MaxAttempts = datarow["MaxAttempts"],
                                                                ActualAttempts = datarow["ActualAttempts"],
                                                                AllAttemptAvgTotScore = datarow["AllAttemptAvgTotScore"],

                                                                TestAnsMode = datarow["TestAnsMode"],
                                                                TestAnsDate = datarow["TestAnsDate"],
                                                                TestResultMode = datarow["TestResultMode"],

                                                                TestResultDate = datarow["TestResultDate"],
                                                                MaxTestUserID = datarow["MaxTestUserID"],
                                                                TestEndDate = datarow["TestEndDate"],

                                                                TestApplTo = datarow["TestApplTo"],
                                                                InitFlag = datarow["InitFlag"],
                                                                TestNumQues = datarow["TestNumQues"],

                                                                TestTimeAllotted = datarow["TestTimeAllotted"],
                                                                TestTimeAppl = datarow["TestTimeAppl"],
                                                                TestStatus = datarow["TestStatus"],

                                                                TestSettingsMode = datarow["TestSettingsMode"],
                                                                LastAttemptedID = datarow["LastAttemptedID"],
                                                                InstID = datarow["InstID"],

                                                                TzID = datarow["TzID"],
                                                                TestType = datarow["TestType"],
                                                                SortOrder = datarow["SortOrder"],
                                                                
                                                                AttHelpText = datarow["AttHelpText"],
                                                                EndDtHelpText = datarow["EndDtHelpText"],
                                                                AllotedTimeHelpText = datarow["AllotedTimeHelpText"],

                                                                HasPreRequisites = datarow["HasPreRequisites"],
                                                                TestMaxScoreObt = datarow["TestMaxScoreObt"],
                                                                Test_AtptDate = datarow["Test_AtptDate"],

                                                                IsManAtempt = datarow["IsManAtempt"],
                                                                TestModuleId = datarow["TestModuleId"],
                                                                Test_ModulePercent = datarow["Test_ModulePercent"],

                                                                LastAttemptPerScore = datarow["LastAttemptPerScore"],
                                                                TestUser_IsEval = datarow["TestUser_IsEval"],
                                                                TestSettings_TestModeType = datarow["TestSettings_TestModeType"],

                                                                PswdMode = datarow["PswdMode"],
                                                                IsEval = datarow["IsEval"],
                                                                GlobalPswd = datarow["GlobalPswd"],

                                                                IsAutoPracticeTest = datarow["IsAutoPracticeTest"],
                                                                AutoPracticeTestID = datarow["AutoPracticeTestID"],
                                                                TestSettings_IsActive = datarow["TestSettings_IsActive"],

                                                                TotalTimeSpent = datarow["TotalTimeSpent"],
                                                                Test_IsLabTest = datarow["Test_IsLabTest"]
                                                            };
                                var resMessage = new
                                {
                                    status = "1",
                                    message = "Succesfully retrived Mock Test list",
                                    response = userMockTestDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving Mock Test list information"
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
                            message = "Course Mock Test info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving course Mock Test list information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user course Mock Test credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving course Mock Test list information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's TestInfo credential i.e.,  'testid','userid', 'sectionid' and
        ///return user Test Info details like 'TestId', 'Test_Name', 'Test_HeadName', 'Test_HeadDesc', 'Test_Type', 'Test_IsComp', 'Test_TestSettingID', 'Test_UserID', 'Test_Instructions', 'TestSettings_ApplTo', 'TestSettings_TestDate', 'TestSettings_TestMode', 'TestSettings_LoadType', 'TestSettings_LoadValue', 'TestSettings_ShowAnsAtEnd', 'TestSettings_AnsDate', 'TestSettings_DispResImm', 'TestSettings_ResDate', 'TestSettings_TimeAppl', 'TestSettings_TimeType', 'TestSettings_TotalTime', 'TestSettings_MrksAppl', 'TestSettings_NegMrksAppl', 'TestSettings_NegMrks', 'TestSettings_AnsToFwd', 'TestSettings_AllowBack', 'TestSettings_NoAtmpts', 'TestSettings_DynDispMode', 'TestSettings_UserID', 'TestSettings_AddToGrade', 'TestSettings_GradeType', 'TestSettings_LevelOrder', 'TestSettings_ChoiceAppl', 'TestSettings_totmarks', 'Test_Attachflag', 'TestSecLink_PswdMode', 'TestSecLink_GlobalPswd', 'TestSettings_DynDispFlag', 'TestSettings_QuesType', 'Test_IsAutoPracticeTest', 'Test_IsQuesEdit', 'Test_HasPreRequisite', 'Test_PreRequisiteID', 'Test_ModeID', 'TestSettings_IsActive', 'Test_AutoPracticeTestID', 'StudentName', 'Test_ModulePercent', 'Test_AttendancePercent', 'TestSettings_TestModeType', 'TestSecLink_TimedPswd', 'TestSecLink_TimedPswdFromDate', 'TestSecLink_TimedPswdToDate', 'Test_ModuleId', 'Test_MaxScore', 'Test_TotNoQuestion', 'QuesMarks', 'QuesHasHindiVer' with 'status' and 'message' upon successful retrival of Test information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of Test information.
        /// </summary>
        /// <param name="userAssessmentTestInfoCredential">User' Test info retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived Test Info",
        ///   "response": 
        ///   {  
        ///   "TestId": "",
        ///   "Test_Name": "",
        ///   "Test_HeadName": "",
        ///   "Test_HeadDesc": "",
        ///   "Test_Type": "",
        ///   "Test_IsComp": "",
        ///   "Test_TestSettingID": "",
        ///   "Test_UserID": "",
        ///   "Test_Instructions": "",
        ///   "TestSettings_ApplTo": "",
        ///   "TestSettings_TestDate": "",
        ///   "TestSettings_TestMode": "",
        ///   "TestSettings_LoadType": "",
        ///   "TestSettings_LoadValue": "",
        ///   "TestSettings_ShowAnsAtEnd": "",
        ///   "TestSettings_AnsDate": "",
        ///   "TestSettings_DispResImm": "",
        ///   "TestSettings_ResDate": "",
        ///   "TestSettings_TimeAppl": "",
        ///   "TestSettings_TimeType": "",
        ///   "TestSettings_TotalTime": "",
        ///   "TestSettings_MrksAppl": "",
        ///   "TestSettings_NegMrksAppl": "",
        ///   "TestSettings_NegMrks": "",
        ///   "TestSettings_AnsToFwd": "",
        ///   "TestSettings_AllowBack": "",
        ///   "TestSettings_NoAtmpts": "",
        ///   "TestSettings_DynDispMode": "",
        ///   "TestSettings_UserID": "",
        ///   "TestSettings_AddToGrade": "",
        ///   "TestSettings_GradeType": "",
        ///   "TestSettings_LevelOrder": "",
        ///   "TestSettings_ChoiceAppl": "",
        ///   "TestSettings_totmarks": "",
        ///   "Test_Attachflag": "",
        ///   "TestSecLink_PswdMode": "",
        ///   "TestSecLink_GlobalPswd": "",
        ///   "TestSettings_DynDispFlag": "",
        ///   "TestSettings_QuesType": "",
        ///   "Test_IsAutoPracticeTest": "",
        ///   "Test_IsQuesEdit": "",
        ///   "Test_HasPreRequisite": "",
        ///   "Test_PreRequisiteID": "",
        ///   "Test_ModeID": "",
        ///   "TestSettings_IsActive": "",
        ///   "Test_AutoPracticeTestID": "",
        ///   "StudentName": "",
        ///   "Test_ModulePercent": "",
        ///   "Test_AttendancePercent": "",
        ///   "TestSettings_TestModeType": "",
        ///   "TestSecLink_TimedPswd": "",
        ///   "TestSecLink_TimedPswdFromDate": "",
        ///   "TestSecLink_TimedPswdToDate": "",
        ///   "Test_ModuleId": "",
        ///   "Test_MaxScore": "",
        ///   "Test_TotNoQuestion": "",
        ///   "QuesMarks": "",
        ///   "QuesHasHindiVer": ""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There is no test info for the specified TestId./No data found/Error while retriving Test information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetTestInfo")]
        public HttpResponseMessage GetTestInfo([ModelBinder] UserAssessmentTestInfoCredential userAssessmentTestInfoCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentTestInfoCredential.testId.Trim() != "" && userAssessmentTestInfoCredential.userId.Trim() != "" && userAssessmentTestInfoCredential.sectionId.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.GetTestInfo(userAssessmentTestInfoCredential.testId.Trim(), userAssessmentTestInfoCredential.userId.Trim(), userAssessmentTestInfoCredential.sectionId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                List<DataRow> listTestInfoDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userTestInfoDetails = from datarow in listTestInfoDetails
                                                          select new
                                                          {
                                                              TestId = datarow["TestId"],
                                                              Test_Name = datarow["Test_Name"],
                                                              Test_HeadName = datarow["Test_HeadName"],

                                                              Test_HeadDesc = datarow["Test_HeadDesc"],
                                                              Test_Type = datarow["Test_Type"],
                                                              Test_IsComp = datarow["Test_IsComp"],

                                                              Test_TestSettingID = datarow["Test_TestSettingID"],
                                                              Test_UserID = datarow["Test_UserID"],
                                                              Test_Instructions = datarow["Test_Instructions"],

                                                              TestSettings_ApplTo = datarow["TestSettings_ApplTo"],
                                                              TestSettings_TestDate = datarow["TestSettings_TestDate"],
                                                              TestSettings_TestMode = datarow["TestSettings_TestMode"],

                                                              TestSettings_LoadType = datarow["TestSettings_LoadType"],
                                                              TestSettings_LoadValue = datarow["TestSettings_LoadValue"],
                                                              TestSettings_ShowAnsAtEnd = datarow["TestSettings_ShowAnsAtEnd"],

                                                              TestSettings_AnsDate = datarow["TestSettings_AnsDate"],
                                                              TestSettings_DispResImm = datarow["TestSettings_DispResImm"],
                                                              TestSettings_ResDate = datarow["TestSettings_ResDate"],

                                                              TestSettings_TimeAppl = datarow["TestSettings_TimeAppl"],
                                                              TestSettings_TimeType = datarow["TestSettings_TimeType"],
                                                              TestSettings_TotalTime = datarow["TestSettings_TotalTime"],

                                                              TestSettings_MrksAppl = datarow["TestSettings_MrksAppl"],
                                                              TestSettings_NegMrksAppl = datarow["TestSettings_NegMrksAppl"],
                                                              TestSettings_NegMrks = datarow["TestSettings_NegMrks"],

                                                              TestSettings_AnsToFwd = datarow["TestSettings_AnsToFwd"],
                                                              TestSettings_AllowBack = datarow["TestSettings_AllowBack"],
                                                              TestSettings_NoAtmpts = datarow["TestSettings_NoAtmpts"],

                                                              TestSettings_DynDispMode = datarow["TestSettings_DynDispMode"],
                                                              TestSettings_UserID = datarow["TestSettings_UserID"],
                                                              TestSettings_AddToGrade = datarow["TestSettings_AddToGrade"],

                                                              TestSettings_GradeType = datarow["TestSettings_GradeType"],
                                                              TestSettings_LevelOrder = datarow["TestSettings_LevelOrder"],
                                                              TestSettings_ChoiceAppl = datarow["TestSettings_ChoiceAppl"],

                                                              TestSettings_totmarks = datarow["TestSettings_totmarks"],
                                                              Test_Attachflag = datarow["Test_Attachflag"],
                                                              TestSecLink_PswdMode = datarow["TestSecLink_PswdMode"],

                                                              TestSecLink_GlobalPswd = datarow["TestSecLink_GlobalPswd"],
                                                              TestSettings_DynDispFlag = datarow["TestSettings_DynDispFlag"],
                                                              TestSettings_QuesType = datarow["TestSettings_QuesType"],

                                                              Test_IsAutoPracticeTest = datarow["Test_IsAutoPracticeTest"],
                                                              Test_IsQuesEdit = datarow["Test_IsQuesEdit"],
                                                              Test_HasPreRequisite = datarow["Test_HasPreRequisite"],

                                                              Test_PreRequisiteID = datarow["Test_PreRequisiteID"],
                                                              Test_ModeID = datarow["Test_ModeID"],
                                                              TestSettings_IsActive = datarow["TestSettings_IsActive"],

                                                              Test_AutoPracticeTestID = datarow["Test_AutoPracticeTestID"],
                                                              StudentName = datarow["StudentName"],
                                                              Test_ModulePercent = datarow["Test_ModulePercent"],

                                                              Test_AttendancePercent = datarow["Test_AttendancePercent"],
                                                              TestSettings_TestModeType = datarow["TestSettings_TestModeType"],
                                                              TestSecLink_TimedPswd = datarow["TestSecLink_TimedPswd"],

                                                              TestSecLink_TimedPswdFromDate = datarow["TestSecLink_TimedPswdFromDate"],
                                                              TestSecLink_TimedPswdToDate = datarow["TestSecLink_TimedPswdToDate"],
                                                              Test_ModuleId = datarow["Test_ModuleId"],

                                                              Test_MaxScore = datarow["Test_MaxScore"],
                                                              Test_TotNoQuestion = datarow["Test_TotNoQuestion"],
                                                              QuesMarks = datarow["QuesMarks"],
                                                              QuesHasHindiVer = datarow["QuesHasHindiVer"]

                                                             
                                                          };
                                var resMessage = new
                                {
                                    status = "1",
                                    message = "Succesfully retrived Test information",
                                    response = userTestInfoDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving Test information"
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
                            message = "Test info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving Test information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user Test info credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving Test information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        #region
        /// <summary>
        ///This api call takes user's TestPaper Initialization credential i.e.,  'testid','userid', 'sectionid', 'lastTestUserId' and
        ///return TestPaper's info like 'TestUserID', 'TimeMode', 'TotalTime', 'ResDispMode', 'TimeLeft', 'ResumeAppl', 'HeadName', 'HeadDesc', 'FeedBackActive', 'TotalTimeSpent', 'DispSoln', 'DispSolnMode', 'UserName', 'DynDispFlag', 'TestType', 'TestModeType', 'IsMultiTestPartEnabled', 'TryLaterQuestList', 'TestDeliveryOption', 'TotalMkrs', 'TotalMkrsObt', 'DispHint', 'IsAutoPracticeTest', 'NegMrksPer' 
        ///and TestPart's info like 'QuesContID', 'QuesCont_Name', 'TotMarks, 'TotQuesCount', 'TotQuesResponded', 'PreAnsString', 'LastPageNo' with 'status' and 'message' upon successful retrival of TestPaper information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of TestPaper information.
        /// </summary>
        /// <param name="userAssessmentInitiateTestPaperCredential">User' TestPaper retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///"status":"1",
        ///"message":"Succesfully retrived TestPaper information",
        ///"response":{
        ///"TestPaper":[
        ///{"TestUserID":"",
        ///"TimeMode":"",
        ///"TotalTime":"",
        ///"ResDispMode":"",
        ///"TimeLeft":"",
        ///"ResumeAppl":"",
        ///"HeadName":"",
        ///"HeadDesc":"",
        ///"FeedBackActive":"",
        ///"TotalTimeSpent":"",
        ///"DispSoln":"",
        ///"DispSolnMode":"",
        ///"UserName":"",
        ///"DynDispFlag":"",
        ///"TestType":"",
        ///"TestModeType":"",
        ///"IsMultiTestPartEnabled":"",
        ///"TryLaterQuestList":"",
        ///"TestDeliveryOption":"",
        ///"TotalMkrs":"",
        ///"TotalMkrsObt":"",
        ///"DispHint":"",
        ///"IsAutoPracticeTest":"",
        ///"NegMrksPer":""}
        ///],
        ///"TestPart":[
        ///{"QuesContID":"","QuesCont_Name":"","TotMarks":"","TotQuesCount":"","TotQuesResponded":"","PreAnsString":"","LastPageNo":""}
        ///]
        ///}
        ///}
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There is no testpaper info for the specified TestId./No data found/Error while retriving TestPaper information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("InitiateTestPaper")]
        public HttpResponseMessage InitiateTestPaper([ModelBinder] UserAssessmentInitiateTestPaperCredential userAssessmentInitiateTestPaperCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentInitiateTestPaperCredential.testId == null || userAssessmentInitiateTestPaperCredential.userId == null
                        || userAssessmentInitiateTestPaperCredential.sectionId == null || userAssessmentInitiateTestPaperCredential.lastTestUserId == null)
                    {
                        if (userAssessmentInitiateTestPaperCredential.testId == null)
                            throw new ArgumentNullException("testId", "Parameter 'testId' is required & non-null");
                        if (userAssessmentInitiateTestPaperCredential.userId == null)
                            throw new ArgumentNullException("userId", "Parameter 'userId' is required & non-null");
                        if (userAssessmentInitiateTestPaperCredential.sectionId == null)
                            throw new ArgumentNullException("sectionId", "Parameter 'sectionId' is required & non-null");
                        if (userAssessmentInitiateTestPaperCredential.lastTestUserId == null)
                            throw new ArgumentNullException("LastTestUserId", "Parameter 'LastTestUserId' is required & non-null");
                    }
                    if (userAssessmentInitiateTestPaperCredential.testId.Trim() != "" && userAssessmentInitiateTestPaperCredential.userId.Trim() != "" && userAssessmentInitiateTestPaperCredential.sectionId.Trim() != "" && userAssessmentInitiateTestPaperCredential.lastTestUserId.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.InitiateTestPaper(userAssessmentInitiateTestPaperCredential.testId.Trim(), userAssessmentInitiateTestPaperCredential.userId.Trim(), userAssessmentInitiateTestPaperCredential.sectionId.Trim(), userAssessmentInitiateTestPaperCredential.lastTestUserId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                //Building TestPaper message object
                                string successmessage = "Succesfully retrived TestPaper information";
                                string testuserid = dst.Tables[0].Rows[0]["TestUserID"].ToString();
                                List<DataRow> listTestPaperInfoDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userTestPaperInfoDetails = from datarow in listTestPaperInfoDetails
                                                          select new
                                                          {
                                                              TestUserID = datarow["TestUserID"],
                                                              TimeMode = datarow["TimeMode"],
                                                              TotalTime = datarow["TotalTime"],
                                                              ResDispMode = datarow["ResDispMode"],
                                                              TimeLeft = datarow["TimeLeft"],
                                                              ResumeAppl = datarow["ResumeAppl"],
                                                              HeadName = datarow["HeadName"],
                                                              HeadDesc = datarow["HeadDesc"],
                                                              FeedBackActive = datarow["FeedBackActive"],
                                                              TotalTimeSpent = datarow["TotalTimeSpent"],
                                                              DispSoln = datarow["DispSoln"],
                                                              DispSolnMode = datarow["DispSolnMode"],
                                                              UserName = datarow["UserName"],
                                                              DynDispFlag = datarow["DynDispFlag"],
                                                              TestType = datarow["TestType"],
                                                              TestModeType = datarow["TestModeType"],
                                                              IsMultiTestPartEnabled = datarow["IsMultiTestPartEnabled"],
                                                              TryLaterQuestList = datarow["TryLaterQuestList"],
                                                              TestDeliveryOption = datarow["TestDeliveryOption"],
                                                              TotalMkrs = datarow["TotalMkrs"],
                                                              TotalMkrsObt = datarow["TotalMkrsObt"],
                                                              DispHint = datarow["DispHint"],
                                                              IsAutoPracticeTest = datarow["IsAutoPracticeTest"],
                                                              NegMrksPer = datarow["NegMrksPer"]
                                                          };
                                
                                //Building TestPart
                                var userTestPaperTestPartsDetails = new Object();
                                if (dst.Tables.Count > 1 && dst.Tables[1].Rows.Count > 0 && dst.Tables[1].Rows[0][0].ToString() == "0")
                                {
                                    List<DataRow> listTestPartInfoDetails = dst.Tables[1].AsEnumerable().ToList();
                                    userTestPaperTestPartsDetails = from datarow in listTestPartInfoDetails
                                                                    select new
                                                                    {
                                                                        QuesContID = datarow["QuesContID"],
                                                                        QuesCont_Name = datarow["QuesCont_Name"],
                                                                        TotMarks = datarow["TotMarks"],
                                                                        TotQuesCount = datarow["TotQuesCount"],
                                                                        TotQuesResponded = datarow["TotQuesResponded"],
                                                                        PreAnsString = datarow["PreAnsString"],
                                                                        LastPageNo = datarow["LastPageNo"]
                                                                    };
                                    if (userAssessmentInitiateTestPaperCredential.lastTestUserId.Trim() == "0")
                                    {
                                        foreach (DataRow datarow in dst.Tables[1].Rows)
                                        {
                                            try
                                            {
                                                objAssessment.BuildTestPart(userAssessmentInitiateTestPaperCredential.testId.Trim(), userAssessmentInitiateTestPaperCredential.userId.Trim(), datarow["QuesContID"].ToString(), testuserid, userAssessmentInitiateTestPaperCredential.sectionId.Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                successmessage += " but exception occured while creating testparts";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    successmessage += " but testparts are not generated";
                                }

                                //Constructing response message
                                var resMessage = new
                                {
                                    status = "1",
                                    message = successmessage,
                                    response = new
                                    {
                                        TestPaper = userTestPaperInfoDetails,
                                        TestPart = userTestPaperTestPartsDetails
                                    }
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving TestPaper information"
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
                            message = "TestPaper info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    string ExMessage = "Exception while retriving TestPaper information";
                    if (ex.GetType() == typeof(ArgumentNullException))
                        ExMessage = ex.Message;

                    var resMessage = new
                    {
                        status = "0",
                        message = ExMessage
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user's TestPaper info retrival credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving TestPaper information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }
        #endregion

        /// <summary>
        ///This api call takes user's TestQuestion retrival credential i.e.,  'testuserid','quescontid', 'pgno' and
        ///return TestQuestion's info like 'TestTotalMkrs', 'TestTotalMkrsObt', 'TotTestQuesResponded', 'TestQuesId', 'PageNo', 'DueTime', 'QuesTime', 'QuesConceptID', 'ConceptDesc', 'QuesId', 'QuesMarks', 'QuesMarksObt', 'EvalStatus', 'QuesType', 'HasImage', 'QuesDesc', 'QuesHint', 'DispHint', 'DispSoln', 'QuesSoln', 'QuesSolnVideo', 'DispSynonym'
        ///and Option's info like 'QuesMultiChId', 'MainQuesId', 'AnsDesc', 'AnsSrlNo', 'AnsLabel', 'AnsComment', 'IsValidAns', 'HasAnsImage', 'AnsFormat' with 'status' and 'message' upon successful retrival of TestQuestion information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of TestQuestion information.
        /// </summary>
        /// <param name="userAssessmentDisplayTestQuestionCredential">User' TestQuestion retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///"status":"1",
        ///"message":"Succesfully retrived TestQuestion information",
        ///"response":{
        ///"Question":[
        ///{"TestTotalMkrs":"",
        ///"TestTotalMkrsObt":"",
        ///"TotTestQuesResponded":"",
        ///"TestQuesId":"",
        ///"PageNo":"",
        ///"DueTime":"",
        ///"QuesTime":"",
        ///"QuesConceptID":"",
        ///"ConceptDesc":"",
        ///"QuesId":"",
        ///"QuesMarks":"",
        ///"QuesMarksObt":"",
        ///"EvalStatus":"",
        ///"QuesType":"",
        ///"HasImage":"",
        ///"QuesDesc":"",
        ///"QuesHint":"",
        ///"DispHint":"",
        ///"DispSoln":"",
        ///"QuesSoln":"",
        ///"QuesSolnVideo":"",
        ///"DispSynonym":""}
        ///],
        ///"Option":[
        ///{"QuesMultiChId":"","MainQuesId":"","AnsDesc":"","AnsSrlNo":"","AnsLabel":"","AnsComment":"","IsValidAns":"","HasAnsImage":"","AnsFormat":""}
        ///]
        ///}
        ///}
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There is no TestQuestion info for the specified testuserid, contid, pgno./No data found/Error while retriving TestQuestion information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("DisplayTestQuestion")]
        public HttpResponseMessage DisplayTestQuestion([ModelBinder] UserAssessmentDisplayTestQuestionCredential userAssessmentDisplayTestQuestionCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentDisplayTestQuestionCredential.testuserid.Trim() != "" && userAssessmentDisplayTestQuestionCredential.quescontid.Trim() != "" && userAssessmentDisplayTestQuestionCredential.pgno.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.DisplayTestQuestion(userAssessmentDisplayTestQuestionCredential.testuserid.Trim(), userAssessmentDisplayTestQuestionCredential.quescontid.Trim(), userAssessmentDisplayTestQuestionCredential.pgno.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                //Building Question message object
                                string successmessage = "Succesfully retrived TestQuestion information";
                                List<DataRow> listTestQuestionDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userTestQuestionDetails = from datarow in listTestQuestionDetails
                                                               select new
                                                               {
                                                                   TestTotalMkrs = datarow["TestTotalMkrs"],
                                                                   TestTotalMkrsObt = datarow["TestTotalMkrsObt"],
                                                                   TotTestQuesResponded = datarow["TotTestQuesResponded"],
                                                                   TestQuesId = datarow["TestQuesId"],
                                                                   PageNo = datarow["PageNo"],
                                                                   DueTime = datarow["DueTime"],
                                                                   QuesTime = datarow["QuesTime"],
                                                                   QuesConceptID = datarow["QuesConceptID"],
                                                                   ConceptDesc = datarow["ConceptDesc"],
                                                                   QuesId = datarow["QuesId"],
                                                                   QuesMarks = datarow["QuesMarks"],
                                                                   QuesMarksObt = datarow["QuesMarksObt"],
                                                                   EvalStatus = datarow["EvalStatus"],
                                                                   QuesType = datarow["QuesType"],
                                                                   HasImage = datarow["HasImage"],
                                                                   QuesDesc = datarow["QuesDesc"],
                                                                   QuesHint = datarow["QuesHint"],
                                                                   DispHint = datarow["DispHint"],
                                                                   DispSoln = datarow["DispSoln"],
                                                                   QuesSoln = datarow["QuesSoln"],
                                                                   QuesSolnVideo = datarow["QuesSolnVideo"],
                                                                   DispSynonym = datarow["DispSynonym"]
                                                               };

                                //Building Option message object
                                var userTestQuestionOptionDetails = new Object();
                                if (dst.Tables.Count > 1 && dst.Tables[1].Rows.Count > 0)
                                {
                                    try
                                    {
                                        List<DataRow> listTestQuestionOptionDetails = dst.Tables[1].AsEnumerable().ToList();
                                        userTestQuestionOptionDetails = from datarow in listTestQuestionOptionDetails
                                                                        select new
                                                                        {
                                                                            QuesMultiChId = datarow["QuesMultiChId"],
                                                                            MainQuesId = datarow["MainQuesId"],
                                                                            AnsDesc = datarow["AnsDesc"],
                                                                            AnsSrlNo = datarow["AnsSrlNo"],
                                                                            AnsLabel = datarow["AnsLabel"],
                                                                            AnsComment = datarow["AnsComment"],
                                                                            IsValidAns = datarow["IsValidAns"],
                                                                            HasAnsImage = datarow["HasAnsImage"],
                                                                            AnsFormat = datarow["AnsFormat"]
                                                                        };
                                    }
                                    catch (Exception ex)
                                    {
                                        userTestQuestionOptionDetails = null;
                                        successmessage += " but error occured while retriving options details.";
                                    }
                                }
                                else
                                {
                                    successmessage += " but options are not available";
                                }

                                //Constructing response message
                                var resMessage = new
                                {
                                    status = "1",
                                    message = successmessage,
                                    response = new
                                    {
                                        Question = userTestQuestionDetails,
                                        Option = userTestQuestionOptionDetails
                                    }
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving TestQuestion information"
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
                            message = "TestQuestion info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving TestQuestion information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user's TestQuestion info retrival credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving TestQuestion information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's TestQuestion submission credential i.e.,  'testuserid','contid', 'testdata','pgno', 'timespent','resdata', 'totaltimespent','sectionId', 'CallReference' and
        ///return 'status' and 'message' upon successful submission of question information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in submission of question.
        ///Note: CallReference = 0 for submission of question, CallReference = 1 for submission of question and assessment,  CallReference = 2 for submission of question and assessment in save and resume later mode.
        /// </summary>
        /// <param name="userAssessmentSubmitTestQuestionCredential">User' Question submission credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///"status":"1",
        ///"message":"Question has been successfully submited."
        ///}
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "Error message while submitting question information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("SubmitTestQuestion")]
        public HttpResponseMessage SubmitTestQuestion([ModelBinder] UserAssessmentSubmitTestQuestionCredential userAssessmentSubmitTestQuestionCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentSubmitTestQuestionCredential.testuserid.Trim() != "" && userAssessmentSubmitTestQuestionCredential.contid.Trim() != ""
                        && userAssessmentSubmitTestQuestionCredential.pgno.Trim() != ""
                         && userAssessmentSubmitTestQuestionCredential.timespent.Trim() != ""
                         && userAssessmentSubmitTestQuestionCredential.totaltimespent.Trim() != "" && userAssessmentSubmitTestQuestionCredential.sectionId.Trim() != ""
                         && userAssessmentSubmitTestQuestionCredential.CallReference.Trim() != "")
                    {
                        //begin --This block is used to determine if the question has been skipped/unattempeted.
                        bool updateOnlyTime = false; //Default updateOnlyTime = false => attempeted and updateOnlyTime = true => un-attempeted. 
                        if (string.IsNullOrEmpty(userAssessmentSubmitTestQuestionCredential.testdata) || string.IsNullOrEmpty(userAssessmentSubmitTestQuestionCredential.resdata)
                            || string.IsNullOrWhiteSpace(userAssessmentSubmitTestQuestionCredential.testdata) || string.IsNullOrWhiteSpace(userAssessmentSubmitTestQuestionCredential.resdata))
                        {
                            updateOnlyTime = true;
                        }
                        //end

                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.SubmitTestQuestion(userAssessmentSubmitTestQuestionCredential.testuserid.Trim(), userAssessmentSubmitTestQuestionCredential.contid.Trim(),
                            userAssessmentSubmitTestQuestionCredential.testdata, userAssessmentSubmitTestQuestionCredential.pgno.Trim(),
                            userAssessmentSubmitTestQuestionCredential.timespent.Trim(), userAssessmentSubmitTestQuestionCredential.resdata,
                            userAssessmentSubmitTestQuestionCredential.totaltimespent.Trim(), userAssessmentSubmitTestQuestionCredential.sectionId.Trim(),
                            updateOnlyTime);

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                //Building Question message object
                                string successmessage = "Question has been successfully submitted.";
                                if (updateOnlyTime == true)
                                {
                                    successmessage = "This question has been skipped successfully.";
                                }
                                string status_submission = "1";
                                if (userAssessmentSubmitTestQuestionCredential.CallReference.Trim() == "1")
                                {
                                    try
                                    {
                                       DataSet dst_AssSub =  objAssessment.SubmitTestQuestion_AssessmentSubmit(userAssessmentSubmitTestQuestionCredential.testuserid.Trim(), "1",
                                                                                          userAssessmentSubmitTestQuestionCredential.timespent.Trim(), 
                                                                                          userAssessmentSubmitTestQuestionCredential.totaltimespent.Trim(),
                                                                                          userAssessmentSubmitTestQuestionCredential.sectionId.Trim());

                                       if (dst_AssSub != null && dst_AssSub.Tables.Count > 0 && dst_AssSub.Tables[0].Rows.Count > 0 && dst_AssSub.Tables[0].Rows[0][0].ToString() == "0")
                                           successmessage += "Assessment has been successfully submitted.";
                                       else
                                       {
                                           successmessage += " but error occured while submitting the assessment.";
                                           status_submission = "0";
                                       }
                                    }
                                    catch (Exception ex)
                                    {
                                        successmessage += " but error occured while submitting the assessment.";
                                        status_submission = "0";
                                    }
                                }
                                else if (userAssessmentSubmitTestQuestionCredential.CallReference.Trim() == "2") 
                                {
                                    try
                                    {
                                        DataSet dst_AssSub = objAssessment.SubmitTestQuestion_AssessmentSubmit(userAssessmentSubmitTestQuestionCredential.testuserid.Trim(), "2",
                                                                                          userAssessmentSubmitTestQuestionCredential.timespent.Trim(),
                                                                                          userAssessmentSubmitTestQuestionCredential.totaltimespent.Trim(),
                                                                                          userAssessmentSubmitTestQuestionCredential.sectionId.Trim());

                                        if (dst_AssSub != null && dst_AssSub.Tables.Count > 0 && dst_AssSub.Tables[0].Rows.Count > 0 && dst_AssSub.Tables[0].Rows[0][0].ToString() == "0")
                                            successmessage += "Assessment has been successfully saved in resume mode.";
                                        else
                                        {
                                            successmessage += " but error occured while saving assessment in resume mode.";
                                            status_submission = "0";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        successmessage += " but error occured while submitting the assessment in save and resume later mode.";
                                        status_submission = "0";
                                    }
                                }

                                //Constructing response message
                                var resMessage = new
                                {
                                    status = status_submission,
                                    message = successmessage
                                };

                                if (status_submission =="1")
                                    return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200  
                                else
                                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while submitting Question."
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
                            message = "Question submission credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while submission Question"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user's Question submission credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while submitting question."
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's TestAttemptResult credential i.e., 'TestUserID', 'SectionID' and
        ///return TestAttemptResult Info that include TestResult and TestResultByPart. TestResult detail includes 'TestUserID', 'TestUser_TotMrks', 'TestUser_TotMrksObt', 'TestUser_TestDate', 'TimeSpent', 'TestSettings_NegMrksAppl', 'TestSettings_NegMrks', 'QuesMarks', 'Practice' and TestResultByPart detail includes 'TestUserTestPartID', 'TestUserTestPart_TestContID', 'TestUserTestPart_ContName', 'TestUserTestPart_TotMrks', 'TestUserTestPart_TotMrksObt', 'TestUserTestPart_TotQuesCount', 'TestUserTestPart_TotCurrQues', 'TestUserTestPart_TotInCurrQues', 'TestUserTestPart_TotParticalCurrQues' with 'status' and 'message' upon successful retrival of TestAttemptResult information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of TestAttemptResult information.
        /// </summary>
        /// <param name="userAssessmentTestAttemptResultCredential">User' TestAttemptResult info retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived TestAttemptResult information",
        ///   "response":
        ///   {  
        ///     "TestResult":[{
        ///         "TestUserID":"",
        ///         "TestUser_TotMrks":"",
        ///         "TestUser_TotMrksObt":"",
        ///         "TestUser_TestDate":"",
        ///         "TimeSpent":"",
        ///         "TestSettings_NegMrksAppl":"",
        ///         "TestSettings_NegMrks":"",
        ///         "QuesMarks":"",
        ///         "Practice":""
        ///         }],
        ///     "TestResultByPart":[{
        ///         "TestUserTestPartID":"",
        ///         "TestUserTestPart_TestContID":"",
        ///         "TestUserTestPart_ContName":"",
        ///         "TestUserTestPart_TotMrks":"",
        ///         "TestUserTestPart_TotMrksObt":"",
        ///         "TestUserTestPart_TotQuesCount":"",
        ///         "TestUserTestPart_TotCurrQues":"",
        ///         "TestUserTestPart_TotInCurrQues":"",
        ///         "TestUserTestPart_TotParticalCurrQues":"",
        ///         }]
        ///  }
        ///}
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There is no record for this test attempt./No data found/Error while retriving TestAttemptResult information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetTestAttemptResult")]
        public HttpResponseMessage GetTestAttemptResult([ModelBinder] UserAssessmentTestAttemptResultCredential userAssessmentTestAttemptResultCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentTestAttemptResultCredential.TestUserID.Trim() != "" && userAssessmentTestAttemptResultCredential.SectionID.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.GetTestAttemptResult(userAssessmentTestAttemptResultCredential.TestUserID.Trim(), userAssessmentTestAttemptResultCredential.SectionID.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                Object objTestResultDetails = null;
                                Object objTestPartResultDetails = null;
                                //----Test result
                                List<DataRow> listTestResultDetails = dst.Tables[0].AsEnumerable().ToList();
                                objTestResultDetails = from datarow in listTestResultDetails
                                                          select new
                                                          {
                                                              TestUserID = datarow["TestUserID"],
                                                              TestUser_TotMrks = datarow["TestUser_TotMrks"],
                                                              TestUser_TotMrksObt = datarow["TestUser_TotMrksObt"],

                                                              TestUser_TestDate = datarow["TestUser_TestDate"],
                                                              TimeSpent = datarow["TimeSpent"],

                                                              TestSettings_NegMrksAppl = datarow["TestSettings_NegMrksAppl"],
                                                              TestSettings_NegMrks = datarow["TestSettings_NegMrks"],
                                                              QuesMarks = datarow["QuesMarks"],

                                                              Practice = datarow["Practice"]
                                                          };

                                //----TestPart result
                                if (dst != null && dst.Tables.Count > 1 && dst.Tables[1].Rows.Count > 0)
                                {
                                    if (dst.Tables[1].Rows[0][0].ToString() == "0")
                                    {
                                        List<DataRow> listTestPartResultDetails = dst.Tables[1].AsEnumerable().ToList();
                                        objTestPartResultDetails = from datarow in listTestPartResultDetails
                                                                       select new
                                                                       {                                                                           
                                                                           TestUserTestPartID = datarow["TestUserTestPartID"],
                                                                           TestUserTestPart_TestContID = datarow["TestUserTestPart_TestContID"],
                                                                           TestUserTestPart_ContName = datarow["TestUserTestPart_ContName"],

                                                                           TestUserTestPart_TotMrks = datarow["TestUserTestPart_TotMrks"],
                                                                           TestUserTestPart_TotMrksObt = datarow["TestUserTestPart_TotMrksObt"],
                                                                           TestUserTestPart_TotQuesCount = datarow["TestUserTestPart_TotQuesCount"],

                                                                           TestUserTestPart_TotCurrQues = datarow["TestUserTestPart_TotCurrQues"],
                                                                           TestUserTestPart_TotInCurrQues = datarow["TestUserTestPart_TotInCurrQues"],
                                                                           TestUserTestPart_TotParticalCurrQues = datarow["TestUserTestPart_TotParticalCurrQues"],
                                                                       };
                                    }
                                }

                                //----Response object
                                var objTestAttemptResultDetails = new
                                {
                                    TestResult = objTestResultDetails,
                                    TestResultByPart = objTestPartResultDetails

                                };

                                var resMessage = new
                                {
                                    status = "1",
                                    message = "Succesfully retrived TestAttemptResult information",
                                    response = objTestAttemptResultDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving TestAttemptResult information"
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
                            message = "TestAttemptResult info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving TestAttemptResult information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user TestAttemptResult info credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving TestAttemptResult information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        #region
        /// <summary>
        ///This api call takes user's TestAttemptQuesStatus credential i.e., 'TestUserTestPartID' and
        ///return TestAttemptQuesStatus Info that include TestAttemptQuesStatus and TestAttemptQuesStatusSummary. TestAttemptQuesStatus detail includes 'RowID', 'TestQuesID', 'Ques_EvalStatus', 'Ques_MaxMarks', 'Ques_ObtMarks', 'Ques_Desc', 'Ques_LevelID', 'CRLevel_Name', 'Ques_ConceptDesc', 'ModuleName', 'Ques_SlnVedio', 'Ques_LanguageType'
        ///and TestAttemptQuesStatusSummary detail includes 'TestType', 'TotalQuesCount', 'CorrectAns', 'INCorrectAns', 'Notattempted', 'CRLevel_Name', 'CRLevelID', 'CRLevel_ParentId', 'IsParent', 'Sectionvideo' with 'status' and 'message' upon successful retrival of TestAttemptQuesStatus information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of TestAttemptQuesStatus information.
        /// </summary>
        /// <param name="userAssessmentTestAttemptQuesStatusCredential">User' TestAttemptQuesStatus info retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived TestAttemptQuesStatus information",
        ///   "response":
        ///   {  
        ///     "TestAttemptQuesStatus":[{              
        ///         "RowID": "",        
        ///         "TestQuesID": "",        
        ///         "Ques_EvalStatus": "",        
        ///         "Ques_MaxMarks": "",        
        ///         "Ques_ObtMarks": "",        
        ///         "Ques_Desc": "",        
        ///         "Ques_LevelID": "",        
        ///         "CRLevel_Name": "",        
        ///         "Ques_ConceptDesc": "",        
        ///         "ModuleName": "",        
        ///         "Ques_SlnVedio": "",        
        ///         "Ques_LanguageType": ""
        ///         }],
        ///     "TestAttemptQuesStatusSummary":[{
        ///         "TestType": "",
        ///			"TotalQuesCount": "",        
        ///			"CorrectAns": "",        
        ///			"INCorrectAns": "",        
        ///			"Notattempted": "",        
        ///			"CRLevel_Name": "",        
        ///			"CRLevelID": "",        
        ///			"CRLevel_ParentId": "",        
        ///			"IsParent": "",        
        ///			"Sectionvideo": ""
        ///         }]
        ///  }
        ///}
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There is no record for this test attempt./No data found/Error while retriving TestAttemptQuesStatus information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetTestAttemptQuesStatus")]
        public HttpResponseMessage GetTestAttemptQuesStatus([ModelBinder] UserAssessmentTestAttemptQuesStatusCredential userAssessmentTestAttemptQuesStatusCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentTestAttemptQuesStatusCredential.TestUserTestPartID == null)
                    {
                        throw new ArgumentNullException("TestUserTestPartID", "Parameter 'TestUserTestPartID' is required & non-null");
                    }
                    if (userAssessmentTestAttemptQuesStatusCredential.TestUserTestPartID.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.GetTestAttemptQuesStatus(userAssessmentTestAttemptQuesStatusCredential.TestUserTestPartID.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                Object objTestAttemptQuesStatusDetails = null;
                                Object objTestAttemptQuesStatusSummaryDetails = null;
                                //----TestAttemptQuesStatus
                                List<DataRow> listTestAttemptQuesStatusDetails = dst.Tables[0].AsEnumerable().ToList();
                                objTestAttemptQuesStatusDetails = from datarow in listTestAttemptQuesStatusDetails
                                                                  select new
                                                                  {
                                                                      RowID = datarow["RowID"],
                                                                      TestQuesID = datarow["TestQuesID"],
                                                                      Ques_EvalStatus = datarow["Ques_EvalStatus"],
                                                                      Ques_MaxMarks = datarow["Ques_MaxMarks"],
                                                                      Ques_ObtMarks = datarow["Ques_ObtMarks"],
                                                                      Ques_Desc = datarow["Ques_Desc"],
                                                                      Ques_LevelID = datarow["Ques_LevelID"],
                                                                      CRLevel_Name = datarow["CRLevel_Name"],
                                                                      Ques_ConceptDesc = datarow["Ques_ConceptDesc"],
                                                                      ModuleName = datarow["ModuleName"],
                                                                      Ques_SlnVedio = datarow["Ques_SlnVedio"],
                                                                      Ques_LanguageType = datarow["Ques_LanguageType"]
                                                                  };

                                //----TestAttemptQuesStatusSummary
                                if (dst != null && dst.Tables.Count > 1 && dst.Tables[1].Rows.Count > 0)
                                {
                                    if (dst.Tables[1].Rows[0][0].ToString() == "0")
                                    {
                                        List<DataRow> listTestAttemptQuesStatusSummaryDetails = dst.Tables[1].AsEnumerable().ToList();
                                        if (dst.Tables[1].Rows[0]["TestType"].ToString() == "25" || dst.Tables[1].Rows[0]["TestType"].ToString() == "17")
                                        {
                                            objTestAttemptQuesStatusSummaryDetails = from datarow in listTestAttemptQuesStatusSummaryDetails
                                                                                     select new
                                                                                     {
                                                                                         TestType = datarow["TestType"],
                                                                                         TotalQuesCount = datarow["TotalQuesCount"],
                                                                                         CorrectAns = datarow["CorrectAns"],
                                                                                         INCorrectAns = datarow["INCorrectAns"],
                                                                                         Notattempted = datarow["Notattempted"],
                                                                                         CRLevel_Name = datarow["CRLevel_Name"],
                                                                                         CRLevelID = datarow["CRLevelID"],
                                                                                         CRLevel_ParentId = datarow["CRLevel_ParentId"],
                                                                                         IsParent = datarow["IsParent"],
                                                                                         Sectionvideo = datarow["Sectionvideo"]
                                                                                     };
                                        }
                                        else
                                        {
                                            objTestAttemptQuesStatusSummaryDetails = from datarow in listTestAttemptQuesStatusSummaryDetails
                                                                                     select new
                                                                                     {
                                                                                         TestType = datarow["TestType"],
                                                                                         CRLevel_SrlNo = datarow["CRLevel_SrlNo"],
                                                                                         Ques_LevelID = datarow["Ques_LevelID"],
                                                                                         TotalQuesCount = datarow["TotalQuesCount"],
                                                                                         CorrectAns = datarow["CorrectAns"],
                                                                                         CRLevel_Name = datarow["CRLevel_Name"],
                                                                                         IsParent = datarow["IsParent"],
                                                                                         Sectionvideo = datarow["Sectionvideo"]
                                                                                     };
                                        }
                                    }
                                }

                                //----Response object
                                var objTestAttemptQuesStatusResponseDetails = new
                                {
                                    TestAttemptQuesStatus = objTestAttemptQuesStatusDetails,
                                    TestAttemptQuesStatusSummary = objTestAttemptQuesStatusSummaryDetails
                                };

                                var resMessage = new
                                {
                                    status = "1",
                                    message = "Succesfully retrived TestAttemptQuesStatus information",
                                    response = objTestAttemptQuesStatusResponseDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving TestAttemptQuesStatus information"
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
                            message = "TestAttemptQuesStatus info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    string ExMessage = "Exception while retriving TestAttemptQuesStatus information";
                    if (ex.GetType() == typeof(ArgumentNullException))
                        ExMessage = ex.Message;

                    var resMessage = new
                    {
                        status = "0",
                        message = ExMessage
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user TestAttemptQuesStatus info credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving TestAttemptQuesStatus information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }
        #endregion

        #region
        /// <summary>
        ///This api call takes user's DispStudTestResults credential i.e., 'TestUserTestPartID', 'TestQuesID' and
        ///return TestResults Info for the QuestionId that include Test_User, Test_Ques, Ques_Main, Ques_MultipleChoice, Test_UserAns, DispSolnRes,QuesLangType and QuesEvalStatus.
        ///Test_User detail includes 'RecID', 'TestQuesID', 'ContID', 'QuesType', 'Pgno', 'ContName', 'ContDesc', 'SecTime', 'QuesTime', 'Ques_ConceptDtlID', 'Ques_ConceptDesc', 'Ques_CRLevelID', 'Ques_DispCalc', 'QuesSubContID', 'QuesSubContName', 'Ques_ConceptType'.
        ///Test_Ques detail includes 'intPkVal', 'TestQues_ContID', 'TestQues_QuesID', 'TestQues_Marks', 'MarksObtained', 'EvalStatus', 'TestQues_Time', 'AnsDispMode', 'TestQues_ModuleId', 'TestQues_Qrclvl_Crleveid', 'TestQues_Qcrlvl_conceptid'.
        ///Ques_Main detail includes 'intPkVal', 'TestQues_ContID', 'TestQues_QuesID', 'TestQues_Marks', 'MarksObtained', 'EvalStatus', 'TestQues_Time', 'AnsDispMode', 'TestQues_ModuleId', 'TestQues_Qrclvl_Crleveid', 'TestQues_Qcrlvl_conceptid'.
        ///Ques_MultipleChoice detail includes 'QuesMultiChID', 'QuesMultiCh_QuesID', 'QuesMultiCh_AnsDesc', 'QuesMultiCh_SrlNo', 'QuesMultiCh_AnsLbl', 'QuesMultiCh_AnsComt', 'QuesMultiCh_ValidAns', 'QuesMultiCh_UpldAnsImg', 'QuesMultiCh_MultiValidAns', 'QuesMultiCH_AnsOptFormat', 'QuesMultiCh_MrksWeight', 'QuesMultiCh_HindiAnsDesc'.
        ///Test_UserAns detail includes 'TestQuesID', 'QuesMultiChID', 'ValidAns', 'AnsText'.
        ///DispSolnRes detail includes 'DispSolnRes'.
        ///QuesLangType detail includes 'QuesLangType' and
        ///QuesEvalStatus detail includes 'RowID', 'TestQuesID', 'Ques_EvalStatus'
        ///with 'status' and 'message' upon successful retrival of DispStudTestResults information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of DispStudTestResults information.
        /// </summary>
        /// <param name="userAssessmentDispStudTestResultsCredential">User' DispStudTestResults info retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived DispStudTestResults information",
        ///   "response":
        ///   {  
        ///     "Test_User":[{              
        ///			"RecID": "",        
        ///			"TestQuesID": "",        
        ///			"ContID": "",        
        ///			"QuesType": "",        
        ///			"Pgno": "",        
        ///			"ContName": "",        
        ///			"ContDesc": "",        
        ///			"SecTime": "",        
        ///			"QuesTime": "",        
        ///			"Ques_ConceptDtlID": "",        
        ///			"Ques_ConceptDesc": "",        
        ///			"Ques_CRLevelID": "",        
        ///			"Ques_DispCalc": "",        
        ///			"QuesSubContID": "",        
        ///			"QuesSubContName": "",        
        ///			"Ques_ConceptType": ""
        ///         }],
        ///     "Test_Ques":[{
        ///			"intPkVal": "",        
        ///			"TestQues_ContID": "",        
        ///			"TestQues_QuesID": "",        
        ///			"TestQues_Marks": "",        
        ///			"MarksObtained": "",        
        ///			"EvalStatus": "",        
        ///			"TestQues_Time": "",        
        ///			"AnsDispMode": "",        
        ///			"TestQues_ModuleId": "",        
        ///			"TestQues_Qrclvl_Crleveid": "",        
        ///			"TestQues_Qcrlvl_conceptid": ""
        ///         }],
        ///     "Ques_Main":[{
        ///			"Ques_HasImg": "",        
        ///			"Ques_Desc": "",        
        ///			"Ques_ResDesc": "",        
        ///			"Ques_UserID": "",        
        ///			"HintHTML": "",        
        ///			"DispHint": "",        
        ///			"DispSoln": "",        
        ///			"slnHTML": "",        
        ///			"SlnVedio": "",        
        ///			"IsSynonym": "",        
        ///			"Ques_IsMultiLingual": "",        
        ///			"Ques_HindiDesc": ""
        ///         }],
        ///     "Ques_MultipleChoice":[{
        ///			"QuesMultiChID": "",        
        ///         "QuesMultiCh_QuesID": "",        
        ///         "QuesMultiCh_AnsDesc": "",        
        ///         "QuesMultiCh_SrlNo": "",        
        ///         "QuesMultiCh_AnsLbl": "",        
        ///         "QuesMultiCh_AnsComt": "",        
        ///         "QuesMultiCh_ValidAns": "",        
        ///         "QuesMultiCh_UpldAnsImg": "",        
        ///         "QuesMultiCh_MultiValidAns": "",        
        ///         "QuesMultiCH_AnsOptFormat": "",        
        ///         "QuesMultiCh_MrksWeight": "",        
        ///         "QuesMultiCh_HindiAnsDesc": ""
        ///         }],
        ///     "Test_UserAns":[{
        ///			"TestQuesID": "",        
        ///			"QuesMultiChID": "",       
        ///			"ValidAns": "",        
        ///			"AnsText": ""
        ///         }],
        ///     "DispSolnRes":[{
        ///			"DispSolnRes": ""
        ///         }],
        ///     "QuesLangType":[{
        ///			"QuesLangType": ""
        ///         }],
        ///     "QuesEvalStatus":[{
        ///			"RowID": "",        
        ///			"TestQuesID": "",        
        ///			"Ques_EvalStatus": ""
        ///         }]
        ///  }
        ///}
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "There is no record for this test attempt./No data found/Error while retriving DispStudTestResults information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetDispStudTestResults")]
        public HttpResponseMessage GetDispStudTestResults([ModelBinder] UserAssessmentDispStudTestResultsCredential userAssessmentDispStudTestResultsCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userAssessmentDispStudTestResultsCredential.TestUserTestPartID == null || userAssessmentDispStudTestResultsCredential.TestQuesID == null)
                    {
                        if (userAssessmentDispStudTestResultsCredential.TestUserTestPartID == null)
                            throw new ArgumentNullException("TestUserTestPartID", "Parameter 'TestUserTestPartID' is required & non-null");
                        if (userAssessmentDispStudTestResultsCredential.TestQuesID == null)
                            throw new ArgumentNullException("TestQuesID", "Parameter 'TestQuesID' is required & non-null");
                    }
                    if (userAssessmentDispStudTestResultsCredential.TestUserTestPartID.Trim() != "" && userAssessmentDispStudTestResultsCredential.TestQuesID.Trim() != "")
                    {
                        Assessment objAssessment = new Assessment();
                        DataSet dst = objAssessment.DispStudTestResults(userAssessmentDispStudTestResultsCredential.TestUserTestPartID.Trim(), userAssessmentDispStudTestResultsCredential.TestQuesID.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                Object objTest_UserDetails = null;
                                Object objTest_QuesDetails = null;

                                Object objQues_MainDetails = null;
                                Object objQues_MultipleChoiceDetails = null;

                                Object objTest_UserAnsDetails = null;
                                Object objDispSolnResDetails = null;

                                Object objQuesLangTypeDetails = null;
                                Object objQuesEvalStatusDetails = null;


                                //---Retriving each table's data as anynomous object
                                objTest_UserDetails = GetDispStudTestResults_PartResultData(dst.Tables[0].Rows[0]["TableName"].ToString(), dst);
                                for (int tblno = 1; tblno <= 7; tblno++)
                                {
                                    Object tempObj = null;

                                    if (dst != null && dst.Tables.Count > tblno && dst.Tables[tblno].Rows.Count > 0 && dst.Tables[tblno].Rows[0][0].ToString() == "0")
                                        tempObj = GetDispStudTestResults_PartResultData(dst.Tables[tblno].Rows[0]["TableName"].ToString(), dst);
                                    else if (dst != null && dst.Tables.Count > tblno && dst.Tables[tblno].Rows.Count > 0 && dst.Tables[tblno].Rows[0][0].ToString() == "-2")
                                        tempObj = new { message = dst.Tables[tblno].Rows[0]["VarErrorMsg"].ToString() };
                                    else
                                        tempObj = new { message = dst.Tables[tblno].Rows[0]["2"].ToString() };

                                    if (tblno == 1)
                                        objTest_QuesDetails = tempObj;
                                    else if (tblno == 2)
                                        objQues_MainDetails = tempObj;
                                    else if (tblno == 3)
                                        objQues_MultipleChoiceDetails = tempObj;
                                    else if (tblno == 4)
                                        objTest_UserAnsDetails = tempObj;
                                    else if (tblno == 5)
                                        objDispSolnResDetails = tempObj;
                                    else if (tblno == 6)
                                        objQuesLangTypeDetails = tempObj;
                                    else if (tblno == 7)
                                        objQuesEvalStatusDetails = tempObj;

                                    tempObj = null;
                                }

                                //----Response object
                                var objDispStudTestResultsResponseDetails = new
                                {
                                    Test_User = objTest_UserDetails,
                                    Test_Ques = objTest_QuesDetails,
                                    Ques_Main = objQues_MainDetails,
                                    Ques_MultipleChoice = objQues_MultipleChoiceDetails,
                                    Test_UserAns = objTest_UserAnsDetails,
                                    DispSolnRes = objDispSolnResDetails,
                                    QuesLangType = objQuesLangTypeDetails,
                                    QuesEvalStatus = objQuesEvalStatusDetails
                                };

                                var resMessage = new
                                {
                                    status = "1",
                                    message = "Succesfully retrived DispStudTestResults information",
                                    response = objDispStudTestResultsResponseDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving DispStudTestResults information"
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
                            message = "DispStudTestResults info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    string ExMessage = "Exception while retriving DispStudTestResults information";
                    if (ex.GetType() == typeof(ArgumentNullException))
                        ExMessage = ex.Message;

                    var resMessage = new
                    {
                        status = "0",
                        message = ExMessage
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user DispStudTestResults info credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving DispStudTestResults information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }
        #endregion

        #region
        /// <summary>
        /// GetDispStudTestResults_PartResultData(string TestResultPartName, DataSet dst)
        /// </summary>
        /// <param name="TestResultPartName"></param>
        /// <param name="dst"></param>
        /// <returns>anynomous object</returns>
        private object GetDispStudTestResults_PartResultData(string TestResultPartName, DataSet dst)
        {
            switch (TestResultPartName)
            {
                case "Test_User":
                    List<DataRow> listobjTest_UserDetails = dst.Tables[0].AsEnumerable().ToList();
                    return from datarow in listobjTest_UserDetails
                           select new
                           {
                               RecID = datarow["RecID"],
                               TestQuesID = datarow["TestQuesID"],
                               ContID = datarow["ContID"],
                               QuesType = datarow["QuesType"],
                               Pgno = datarow["Pgno"],
                               ContName = datarow["ContName"],
                               ContDesc = datarow["ContDesc"],
                               SecTime = datarow["SecTime"],
                               QuesTime = datarow["QuesTime"],
                               Ques_ConceptDtlID = datarow["Ques_ConceptDtlID"],
                               Ques_ConceptDesc = datarow["Ques_ConceptDesc"],
                               Ques_CRLevelID = datarow["Ques_CRLevelID"],
                               Ques_DispCalc = datarow["Ques_DispCalc"],
                               QuesSubContID = datarow["QuesSubContID"],
                               QuesSubContName = datarow["QuesSubContName"],
                               Ques_ConceptType = datarow["Ques_ConceptType"]
                           };
                    break;

                case "Test_Ques":
                    List<DataRow> listobjTest_QuesDetails = dst.Tables[1].AsEnumerable().ToList();
                    return from datarow in listobjTest_QuesDetails
                           select new
                           {
                               intPkVal = datarow["intPkVal"],
                               TestQues_ContID = datarow["TestQues_ContID"],
                               TestQues_QuesID = datarow["TestQues_QuesID"],
                               TestQues_Marks = datarow["TestQues_Marks"],
                               MarksObtained = datarow["MarksObtained"],
                               EvalStatus = datarow["EvalStatus"],
                               TestQues_Time = datarow["TestQues_Time"],
                               AnsDispMode = datarow["AnsDispMode"],
                               TestQues_ModuleId = datarow["TestQues_ModuleId"],
                               TestQues_Qrclvl_Crleveid = datarow["TestQues_Qrclvl_Crleveid"],
                               TestQues_Qcrlvl_conceptid = datarow["TestQues_Qcrlvl_conceptid"]
                           };
                    break;

                case "Ques_Main":
                    List<DataRow> listobjQues_MainDetails = dst.Tables[2].AsEnumerable().ToList();
                    return from datarow in listobjQues_MainDetails
                           select new
                           {
                               Ques_HasImg = datarow["Ques_HasImg"],
                               Ques_Desc = datarow["Ques_Desc"],
                               Ques_ResDesc = datarow["Ques_ResDesc"],
                               Ques_UserID = datarow["Ques_UserID"],
                               HintHTML = datarow["HintHTML"],
                               DispHint = datarow["DispHint"],
                               DispSoln = datarow["DispSoln"],
                               slnHTML = datarow["slnHTML"],
                               SlnVedio = datarow["SlnVedio"],
                               IsSynonym = datarow["IsSynonym"],
                               Ques_IsMultiLingual = datarow["Ques_IsMultiLingual"],
                               Ques_HindiDes = datarow["Ques_HindiDesc"]
                           };
                    break;

                case "Ques_MultipleChoice":
                    List<DataRow> listobjQues_MultipleChoiceDetails = dst.Tables[3].AsEnumerable().ToList();
                    return from datarow in listobjQues_MultipleChoiceDetails
                           select new
                           {
                               QuesMultiChID = datarow["QuesMultiChID"],
                               QuesMultiCh_QuesID = datarow["QuesMultiCh_QuesID"],
                               QuesMultiCh_AnsDesc = datarow["QuesMultiCh_AnsDesc"],
                               QuesMultiCh_SrlNo = datarow["QuesMultiCh_SrlNo"],
                               QuesMultiCh_AnsLbl = datarow["QuesMultiCh_AnsLbl"],
                               QuesMultiCh_AnsComt = datarow["QuesMultiCh_AnsComt"],
                               QuesMultiCh_ValidAns = datarow["QuesMultiCh_ValidAns"],
                               QuesMultiCh_UpldAnsImg = datarow["QuesMultiCh_UpldAnsImg"],
                               QuesMultiCh_MultiValidAns = datarow["QuesMultiCh_MultiValidAns"],
                               QuesMultiCH_AnsOptFormat = datarow["QuesMultiCH_AnsOptFormat"],
                               QuesMultiCh_MrksWeight = datarow["QuesMultiCh_MrksWeight"],
                               QuesMultiCh_HindiAnsDesc = datarow["QuesMultiCh_HindiAnsDesc"]
                           };
                    break;

                case "Test_UserAns":
                    List<DataRow> listobjTest_UserAnsDetails = dst.Tables[4].AsEnumerable().ToList();
                    return from datarow in listobjTest_UserAnsDetails
                           select new
                           {
                               TestQuesID = datarow["TestQuesID"],
                               QuesMultiChID = datarow["QuesMultiChID"],
                               ValidAns = datarow["ValidAns"],
                               AnsText = datarow["AnsText"],
                           };
                    break;

                case "DispSolnRes":
                    List<DataRow> listobjDispSolnResDetails = dst.Tables[5].AsEnumerable().ToList();
                    return from datarow in listobjDispSolnResDetails
                           select new
                           {
                               DispSolnRes = datarow["DispSolnRes"]
                           };
                    break;

                case "QuesLangType":
                    List<DataRow> listobjQuesLangTypeDetails = dst.Tables[6].AsEnumerable().ToList();
                    return from datarow in listobjQuesLangTypeDetails
                           select new
                           {
                               QuesLangType = datarow["QuesLangType"]
                           };
                    break;

                case "QuesEvalStatus":
                    List<DataRow> listobjQuesEvalStatusDetails = dst.Tables[7].AsEnumerable().ToList();
                    return from datarow in listobjQuesEvalStatusDetails
                           select new
                           {
                               RowID = datarow["RowID"],
                               TestQuesID = datarow["TestQuesID"],
                               Ques_EvalStatus = datarow["Ques_EvalStatus"]
                           };
                    break;

                default:
                    return new { Message = "No data found" };
                    break;
            }
        }
        #endregion
    }
}
