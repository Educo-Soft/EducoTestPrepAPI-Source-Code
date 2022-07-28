using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using EducoTestPrepAPI.Models;
using Newtonsoft.Json;

/// <summary>
/// Summary description for UserCredentialModelBinder
/// </summary>
public class UserCredentialModelBinder : IModelBinder
{	
    public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
    {
        try
        {
            bool flag = false;
            //if (bindingContext.ModelType != typeof(UserCredential))
            //{
            //    flag = false;
            //}

            //For User
            #region 
            if (bindingContext.ModelType == typeof(UserCredential) || bindingContext.ModelType == typeof(UserCredentialForgetPassword)
                 || bindingContext.ModelType == typeof(UserCredentialRegisterGuestUser))
            {
                if (bindingContext.ModelType == typeof(UserCredential)){
                    UserCredential result = JsonConvert.DeserializeObject<UserCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else  if (bindingContext.ModelType == typeof(UserCredentialForgetPassword)){
                    UserCredentialForgetPassword result = JsonConvert.DeserializeObject<UserCredentialForgetPassword>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserCredentialRegisterGuestUser))
                {
                    UserCredentialRegisterGuestUser result = JsonConvert.DeserializeObject<UserCredentialRegisterGuestUser>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
            }
            #endregion

            //For User Course
            #region
            else if (bindingContext.ModelType == typeof(UserCourseCredential) || bindingContext.ModelType == typeof(UserCourseKDModulesCredential))
            {
                if (bindingContext.ModelType == typeof(UserCourseCredential))
                {
                    UserCourseCredential result = JsonConvert.DeserializeObject<UserCourseCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserCourseKDModulesCredential))
                {
                    UserCourseKDModulesCredential result = JsonConvert.DeserializeObject<UserCourseKDModulesCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
            }
            #endregion

            //For User Assessment
            #region
            else if (bindingContext.ModelType == typeof(UserAssessmentKdTestListCredential) || bindingContext.ModelType == typeof(UserAssessmentModuleTestListCredential)
                || bindingContext.ModelType == typeof(UserAssessmentMockTestListCredential) || bindingContext.ModelType == typeof(UserAssessmentTestInfoCredential)
                || bindingContext.ModelType == typeof(UserAssessmentInitiateTestPaperCredential) || bindingContext.ModelType == typeof(UserAssessmentDisplayTestQuestionCredential)
                || bindingContext.ModelType == typeof(UserAssessmentSubmitTestQuestionCredential) || bindingContext.ModelType == typeof(UserAssessmentTestAttemptResultCredential)
                || bindingContext.ModelType == typeof(UserAssessmentTestAttemptQuesStatusCredential) || bindingContext.ModelType == typeof(UserAssessmentDispStudTestResultsCredential))
            {
                if (bindingContext.ModelType == typeof(UserAssessmentKdTestListCredential))
                {
                    UserAssessmentKdTestListCredential result = JsonConvert.DeserializeObject<UserAssessmentKdTestListCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentModuleTestListCredential))
                {
                    UserAssessmentModuleTestListCredential result = JsonConvert.DeserializeObject<UserAssessmentModuleTestListCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentMockTestListCredential))
                {
                    UserAssessmentMockTestListCredential result = JsonConvert.DeserializeObject<UserAssessmentMockTestListCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentTestInfoCredential))
                {
                    UserAssessmentTestInfoCredential result = JsonConvert.DeserializeObject<UserAssessmentTestInfoCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentInitiateTestPaperCredential))
                {
                    UserAssessmentInitiateTestPaperCredential result = JsonConvert.DeserializeObject<UserAssessmentInitiateTestPaperCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentDisplayTestQuestionCredential))
                {
                    UserAssessmentDisplayTestQuestionCredential result = JsonConvert.DeserializeObject<UserAssessmentDisplayTestQuestionCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentSubmitTestQuestionCredential))
                {
                    UserAssessmentSubmitTestQuestionCredential result = JsonConvert.DeserializeObject<UserAssessmentSubmitTestQuestionCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentTestAttemptResultCredential))
                {
                    UserAssessmentTestAttemptResultCredential result = JsonConvert.DeserializeObject<UserAssessmentTestAttemptResultCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentTestAttemptQuesStatusCredential))
                {
                    UserAssessmentTestAttemptQuesStatusCredential result = JsonConvert.DeserializeObject<UserAssessmentTestAttemptQuesStatusCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
                else if (bindingContext.ModelType == typeof(UserAssessmentDispStudTestResultsCredential))
                {
                    UserAssessmentDispStudTestResultsCredential result = JsonConvert.DeserializeObject<UserAssessmentDispStudTestResultsCredential>
                       (actionContext.Request.Content.ReadAsStringAsync().Result);
                    bindingContext.Model = result;
                    flag = true;
                }
            }
            #endregion

            //For Else region
            #region
            else
            {
                flag = false;
            }
            #endregion

            return flag;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}