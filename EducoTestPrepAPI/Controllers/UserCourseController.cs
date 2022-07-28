using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Educo.ELS.Encryption;
using EducoTestPrepAPI.Models;
using System.Data;

namespace EducoTestPrepAPI.Controllers
{
    public class UserCourseController : ApiController
    {
        /// <summary>
        ///This api call takes user's course credential i.e.,  'userid' and 'sectionid'  and 
        ///return user course details like 'TermId', 'Term_Name', 'CourseId', 'Course_Name', 'SectionId', 'Section_Name', 'Term_StartDate', 'Term_EndDate', 'Duration'  with 'status' and 'message' upon successful retrival of course information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retriving course information.
        /// </summary>
        /// <param name="userCourseCredential">User course retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived course information",
        ///   "response": 
        ///      {  
        ///        "TermId": "",
        ///        "Term_Name": "",
        ///        "CourseId": "",
        ///        "Course_Name": "",
        ///        "SectionId": "",
        ///        "Section_Name":"",
        ///        "Term_StartDate": "",
        ///        "Term_EndDate": "",
        ///        "Duration":""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "User does not have access to section/Error while retriving course information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetUserCourseInfo")]
        public HttpResponseMessage GetUserCourseInfo([ModelBinder] UserCourseCredential userCourseCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userCourseCredential.userId.Trim() != "" && userCourseCredential.sectionId.Trim() != "")
                    {
                        UserCourse objUserCourse = new UserCourse();
                        DataSet dst = objUserCourse.GetUserCourseInfo(userCourseCredential.userId.Trim(), userCourseCredential.sectionId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                UserCourseDetails userCourseDetails = new UserCourseDetails();
                                foreach (DataRow dr in dst.Tables[0].Rows)
                                {
                                    userCourseDetails.TermId = Convert.ToString(dr["TermId"]);
                                    userCourseDetails.Term_Name = Convert.ToString(dr["Term_Name"]);
                                    userCourseDetails.CourseId = Convert.ToString(dr["CourseId"]);
                                    userCourseDetails.Course_Name = Convert.ToString(dr["Course_Name"]);
                                    userCourseDetails.SectionId = Convert.ToString(dr["SectionId"]);
                                    userCourseDetails.Section_Name = Convert.ToString(dr["Section_Name"]);
                                    userCourseDetails.Term_StartDate = Convert.ToString(dr["Term_StartDate"]);
                                    userCourseDetails.Term_EndDate = Convert.ToString(dr["Term_EndDate"]);
                                    userCourseDetails.Duration = Convert.ToString(dr["Duration"]);
                                }

                                var resMessage = new
                                {
                                    status = "1",
                                    message = "succesfully retrived course information",
                                    response = userCourseDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving course information"
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
                            message = "Course info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving course information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user course credential)"
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
        ///This api call takes user's course kd credential i.e.,  'userid' and 'sectionid'  and 
        ///returns user's course kdlist details like 'CourseName', 'KdList' info as 'CRLevelId', 'CRLevel_Name' with 'status' and 'message' upon successful retrival of course kd information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of course kd information.
        /// </summary>
        /// <param name="userCourseCredential">User's course kd retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived course kd list",
        ///   "response": 
        ///      {  
        ///         "CourseName" : "",
        ///         "CourseList":
        ///             {
        ///                 "CRLevelId": "",
        ///                 "CRLevel_Name": ""
        ///             }
        ///      }
        ///}
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "User does not have access to section/Error while retriving course kd list information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetCourseKDList")]
        public HttpResponseMessage GetCourseKDList([ModelBinder] UserCourseCredential userCourseCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userCourseCredential.userId.Trim() != "" && userCourseCredential.sectionId.Trim() != "")
                    {
                        UserCourse objUserCourse = new UserCourse();
                        DataSet dst = objUserCourse.GetCourseKDList(userCourseCredential.userId.Trim(), userCourseCredential.sectionId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                List<DataRow> listKDDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userCourseKDDetails = from datarow in listKDDetails select new { CRLevelId = datarow["CRLevelId"].ToString(), CRLevel_Name = datarow["CRLevel_Name"].ToString() };

                                var resMessage = new
                                {
                                    status = "1",
                                    message = "succesfully retrived course kd list",
                                    response = new {
                                        CourseName = Convert.ToString(dst.Tables[0].Rows[0]["CourseName"]),
                                        CourseList = userCourseKDDetails
                                    }
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving course kd list information"
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
                            message = "Course kd info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving course kd list information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user course kd credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving course kd list information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }

        /// <summary>
        ///This api call takes user's course kd module credential i.e.,  'userid', 'sectionid' and 'kdid'  and 
        ///return user course kd module list details like 'CRModule_LevelId', 'CRModule_Name' with 'status' and 'message' upon successful retrival of course kd module information.
        ///Incase of failure, it returns only 'status' and 'message'.
        ///'Status':'1'  means Success and  'Status':'0'  means failure/error in retrival of course kd module information.
        /// </summary>
        /// <param name="userCourseKDModulesCredential">User course kd module retrival credential to specify in request body in json format.</param>
        /// <returns>
        ///Success:
        ///{
        ///   "status": 1,
        ///   "message": "Succesfully retrived course kd module list",
        ///   "response": 
        ///      {  
        ///        "CRModule_LevelId": "",
        ///        "CRModule_Name": ""
        ///   }
        ///Error:
        ///{
        ///"status": 0,
        ///"message": "User does not have access to section/Error while retriving course kd module list information"
        ///}
        /// </returns>
        [HttpPost]
        [ActionName("GetKDModuleList")]
        public HttpResponseMessage GetKDModuleList([ModelBinder] UserCourseKDModulesCredential userCourseKDModulesCredential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userCourseKDModulesCredential.userId.Trim() != "" && userCourseKDModulesCredential.sectionId.Trim() != "" && userCourseKDModulesCredential.kdId.Trim() != "")
                    {                        
                        UserCourse objUserCourse = new UserCourse();
                        DataSet dst = objUserCourse.GetKDModuleList(userCourseKDModulesCredential.userId.Trim(), userCourseKDModulesCredential.sectionId.Trim(), userCourseKDModulesCredential.kdId.Trim());

                        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                List<DataRow> listKDModuleDetails = dst.Tables[0].AsEnumerable().ToList();
                                var userCourseKDModulesDetails = from datarow in listKDModuleDetails select new { CRModule_LevelId = datarow["CRModule_LevelId"].ToString(), CRModule_Name = datarow["CRModule_Name"].ToString() };

                                var resMessage = new
                                {
                                    status = "1",
                                    message = "succesfully retrived course kd module list",
                                    response = userCourseKDModulesDetails
                                };

                                return Request.CreateResponse(HttpStatusCode.OK, resMessage); //response code = 200                             
                            }
                            else if (dst.Tables[0].Rows[0][0].ToString() == "-1")
                            {
                                var resMessage = new
                                {
                                    status = "0",
                                    message = "Error while retriving course kd module list information"
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
                            message = "Course kd module info retrival credentials are required"
                        };

                        return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                    }
                }
                catch (Exception ex)
                {
                    var resMessage = new
                    {
                        status = "0",
                        message = "Exception while retriving course kd module list information"
                    };

                    return Request.CreateResponse(HttpStatusCode.Created, resMessage); //response code = 201
                }
            }
            else
            {
                var resMessage = new
                {
                    status = "0",
                    message = "Invalid/malformed input(user course kd module credential)"
                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, resMessage);  //response code = 400                 
            }

            var resMessageUnKnown = new
            {
                status = "0",
                message = "Unknown error while retriving course kd module list information"
            };
            return Request.CreateResponse(HttpStatusCode.Created, resMessageUnKnown);  //response code = 201

        }
    }
}