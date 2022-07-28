using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding.Binders;
using EducoTestPrepAPI.Models;
using System.Web.Http.ModelBinding;

namespace EducoTestPrepAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    //routeTemplate: "api/{controller}/{id}",
            //    routeTemplate: "EducoTestPrepAPI/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "EducoTestPrepAPI/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ////For User
            #region
            //Model Binder Configuration for UserCredential
            var provider = new SimpleModelBinderProvider(
            typeof(UserCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);

            //Model Binder Configuration for UserCredentialForgetPassword
            var providerForgetPassword = new SimpleModelBinderProvider(
            typeof(UserCredentialForgetPassword), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerForgetPassword);

            //Model Binder Configuration for UserCredentialRegisterGuestUser
            var providerRegisterGuestUser = new SimpleModelBinderProvider(
            typeof(UserCredentialRegisterGuestUser), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerRegisterGuestUser);

            #endregion

            ////For UserCourse
            #region
            //Model Binder Configuration for UserCourseCredential
            var providerUserCourseCredential = new SimpleModelBinderProvider(
            typeof(UserCourseCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserCourseCredential);

            //Model Binder Configuration for UserCourseKDModulesCredential
            var providerUserCourseKDModulesCredential = new SimpleModelBinderProvider(
            typeof(UserCourseKDModulesCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserCourseKDModulesCredential);

            #endregion

            ////For User Assessment
            #region
            //Model Binder Configuration for UserAssessmentKdTestListCredential
            var providerUserAssessmentKdTestListCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentKdTestListCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentKdTestListCredential);

            //Model Binder Configuration for UserAssessmentModuleTestListCredential
            var providerUserAssessmentModuleTestListCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentModuleTestListCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentModuleTestListCredential);

            //Model Binder Configuration for UserAssessmentMockTestListCredential
            var providerUserAssessmentMockTestListCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentMockTestListCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentMockTestListCredential);

            //Model Binder Configuration for UserAssessmentMockTestListCredential
            var providerUserAssessmentTestInfoCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentTestInfoCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentTestInfoCredential);

            //Model Binder Configuration for UserAssessmentInitiateTestPaperCredential
            var providerUserAssessmentInitiateTestPaperCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentInitiateTestPaperCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentInitiateTestPaperCredential);
            
            //Model Binder Configuration for UserAssessmentInitiateTestPaperCredential
            var providerUserAssessmentDisplayTestQuestionCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentDisplayTestQuestionCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentDisplayTestQuestionCredential);

            //Model Binder Configuration for UserAssessmentInitiateTestPaperCredential
            var providerUserAssessmentSubmitTestQuestionCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentSubmitTestQuestionCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentSubmitTestQuestionCredential);

            //Model Binder Configuration for UserAssessmentInitiateTestPaperCredential
            var providerUserAssessmentTestAttemptResultCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentTestAttemptResultCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentTestAttemptResultCredential);

            //Model Binder Configuration for UserAssessmentInitiateTestPaperCredential
            var providerUserAssessmentTestAttemptQuesStatusCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentTestAttemptQuesStatusCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentTestAttemptQuesStatusCredential);

            //Model Binder Configuration for UserAssessmentInitiateTestPaperCredential
            var providerUserAssessmentDispStudTestResultsCredential = new SimpleModelBinderProvider(
            typeof(UserAssessmentDispStudTestResultsCredential), new UserCredentialModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, providerUserAssessmentDispStudTestResultsCredential);

            #endregion
        }
    }
}
