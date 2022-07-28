using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace EducoTestPrepAPI.Areas.HelpPage
{
    /// <summary>
    /// Use this class to customize the Help Page.
    /// For example you can set a custom <see cref="System.Web.Http.Description.IDocumentationProvider"/> to supply the documentation
    /// or you can provide the samples for the requests/responses.
    /// </summary>
    public static class HelpPageConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Uncomment the following to use the documentation from XML documentation file.
            config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

            //// Uncomment the following to use "sample string" as the sample for all actions that have string as the body parameter or return type.
            //// Also, the string arrays will be used for IEnumerable<string>. The sample objects will be serialized into different media type 
            //// formats by the available formatters.
            //config.SetSampleObjects(new Dictionary<Type, object>
            //{
            //    {typeof(string), "sample string"},
            //    {typeof(IEnumerable<string>), new string[]{"sample 1", "sample 2"}}
            //});

            //// Uncomment the following to use "[0]=foo&[1]=bar" directly as the sample for all actions that support form URL encoded format
            //// and have IEnumerable<string> as the body parameter or return type.
            //config.SetSampleForType("[0]=foo&[1]=bar", new MediaTypeHeaderValue("application/x-www-form-urlencoded"), typeof(IEnumerable<string>));

            //// Uncomment the following to use "1234" directly as the request sample for media type "text/plain" on the controller named "Values"
            //// and action named "Put".
            //config.SetSampleRequest("1234", new MediaTypeHeaderValue("text/plain"), "Values", "Put");

            //// Uncomment the following to use the image on "../images/aspNetHome.png" directly as the response sample for media type "image/png"
            //// on the controller named "Values" and action named "Get" with parameter "id".
            //config.SetSampleResponse(new ImageSample("../images/aspNetHome.png"), new MediaTypeHeaderValue("image/png"), "Values", "Get", "id");

            //// Uncomment the following to correct the sample request when the action expects an HttpRequestMessage with ObjectContent<string>.
            //// The sample will be generated as if the controller named "Values" and action named "Get" were having string as the body parameter.
            //config.SetActualRequestType(typeof(string), "Values", "Get");

            //// Uncomment the following to correct the sample response when the action returns an HttpResponseMessage with ObjectContent<string>.
            //// The sample will be generated as if the controller named "Values" and action named "Post" were returning a string.
            //config.SetActualResponseType(typeof(string), "Values", "Post");

            //For UserAssessment
            #region
            string sampleForAssessmentListJSONFormat = "{\n\"Table\":[{\n\"KDId\":1204,\n\"KDName\":\"General Intelligence(GI)\",\n\"ModuleId\":1225,\n\"ModuleName\":\"Module 1: GI - U1\",\n\"TestID\":3515,\n\"TestName\":\"Quiz on Section Section 2: GI - U1 - M1\",\n\"MaxAttempts\":99,\n\"ActualAttempts\":0,\n\"AllAttemptAvgTotScore\":null,\n\"MaxTestUserID\":0,\n\"InitFlag\":null,\n\"TestNumQues\":4,\n\"TestSettingsMode\":\"Practice\",\n\"LastAttemptedID\":0,\n\"TestType\":1,\n\"SortOrder\":7,\n\"TestMaxScoreObt\":0.0,\n\"TestModuleId\":1225,\n\"Test_ModulePercent\":0.0,\n\"LastAttemptPerScore\":0.0,\n\"TestUser_IsEval\":null,\n\"TestSettings_TestModeType\":2,\n\"IsAutoPracticeTest\":2,\n\"AutoPracticeTestID\":0\n}]\n}";
            string sampleForAssessmentListXMLFormat = "<DataSet>\n<xs:schema id=\"NewDataSet\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\n<xs:element name=\"NewDataSet\" msdata:IsDataSet=\"true\" msdata:UseCurrentLocale=\"true\">\n<xs:complexType>\n<xs:choice minOccurs=\"0\" maxOccurs=\"unbounded\">\n<xs:element name=\"Table\">\n<xs:complexType>\n<xs:sequence>\n<xs:element name=\"KDId\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"KDName\" type=\"xs:string\" minOccurs=\"0\" />\n<xs:element name=\"ModuleId\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"ModuleName\" type=\"xs:string\" minOccurs=\"0\" />\n<xs:element name=\"TestID\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"TestName\" type=\"xs:string\" minOccurs=\"0\" />\n<xs:element name=\"MaxAttempts\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"ActualAttempts\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"AllAttemptAvgTotScore\" type=\"xs:float\" minOccurs=\"0\" />\n<xs:element name=\"MaxTestUserID\" type=\"xs:long\" minOccurs=\"0\" />\n<xs:element name=\"InitFlag\" type=\"xs:unsignedByte\" minOccurs=\"0\" />\n<xs:element name=\"TestNumQues\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"TestSettingsMode\" type=\"xs:string\" minOccurs=\"0\" />\n<xs:element name=\"LastAttemptedID\" type=\"xs:long\" minOccurs=\"0\" />\n<xs:element name=\"TestType\" type=\"xs:short\" minOccurs=\"0\" />\n<xs:element name=\"SortOrder\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"TestMaxScoreObt\" type=\"xs:float\" minOccurs=\"0\" />\n<xs:element name=\"TestModuleId\" type=\"xs:int\" minOccurs=\"0\" />\n<xs:element name=\"Test_ModulePercent\" type=\"xs:double\" minOccurs=\"0\" />\n<xs:element name=\"LastAttemptPerScore\" type=\"xs:float\" minOccurs=\"0\" />\n<xs:element name=\"TestUser_IsEval\" type=\"xs:short\" minOccurs=\"0\" />\n<xs:element name=\"TestSettings_TestModeType\" type=\"xs:unsignedByte\" minOccurs=\"0\" />\n<xs:element name=\"IsAutoPracticeTest\" type=\"xs:short\" minOccurs=\"0\" />\n<xs:element name=\"AutoPracticeTestID\" type=\"xs:long\" minOccurs=\"0\" />\n</xs:sequence>\n</xs:complexType>\n</xs:element>\n</xs:choice>\n</xs:complexType>\n</xs:element>\n</xs:schema>\n<diffgr:diffgram xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\" xmlns:diffgr=\"urn:schemas-microsoft-com:xml-diffgram-v1\">\n<NewDataSet>\n<Table diffgr:id=\"Table1\" msdata:rowOrder=\"0\">\n<KDId>1204</KDId>\n<KDName>General Intelligence(GI)</KDName>\n<ModuleId>1225</ModuleId>\n<ModuleName>Module 1: GI - U1</ModuleName>\n<TestID>3515</TestID>\n<TestName>Quiz on Section Section 2: GI - U1 - M1</TestName>\n<MaxAttempts>99</MaxAttempts>\n<ActualAttempts>0</ActualAttempts>\n<MaxTestUserID>0</MaxTestUserID>\n<TestNumQues>4</TestNumQues>\n<TestSettingsMode>Practice</TestSettingsMode>\n<LastAttemptedID>0</LastAttemptedID>\n<TestType>1</TestType>\n<SortOrder>7</SortOrder>\n<TestMaxScoreObt>0</TestMaxScoreObt>\n<TestModuleId>1225</TestModuleId>\n<Test_ModulePercent>0</Test_ModulePercent>\n<LastAttemptPerScore>0</LastAttemptPerScore>\n<TestSettings_TestModeType>2</TestSettings_TestModeType>\n<IsAutoPracticeTest>2</IsAutoPracticeTest>\n<AutoPracticeTestID>0</AutoPracticeTestID>\n</Table>\n</NewDataSet>\n</diffgr:diffgram>\n</DataSet>";
            
            config.SetSampleForType(sampleForAssessmentListJSONFormat, new MediaTypeHeaderValue("application/json"), typeof(System.Data.DataSet));
            config.SetSampleForType(sampleForAssessmentListJSONFormat, new MediaTypeHeaderValue("text/json"), typeof(System.Data.DataSet));

            config.SetSampleForType(sampleForAssessmentListXMLFormat, new MediaTypeHeaderValue("application/xml"), typeof(System.Data.DataSet));
            config.SetSampleForType(sampleForAssessmentListXMLFormat, new MediaTypeHeaderValue("text/xml"), typeof(System.Data.DataSet));

            string sampleRequestBodyLogin = "{\"Email\":\"useremail@educo.com\",\"Password\":\"welcome\",\"os\":\"android\",\"device_id\":\"\"}";
            string sampleResponseBodyLogin = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"succesfully login\",\n\"response\": {\n \"userId\":\"2355\", \"first_name\":\"Student717\", \"last_name\":\"717\", \"email\":\"stu717@educosoft.com\", \"mobile\":\"7975399026\"\n  }\n} \n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"Username and password do not match\"\n}";
            config.SetSampleRequest(sampleRequestBodyLogin, new MediaTypeHeaderValue("application/json"), "Assessment", "user_login");
            config.SetSampleResponse(sampleResponseBodyLogin, new MediaTypeHeaderValue("application/json"), "Assessment", "user_login");

            string sampleRequestBodyGetKdTestList = "{\"userId\":\"2355\",\"sectionId\":\"18\",\"kdId\":\"1204\"}";
            string sampleResponseBodyGetKdTestList = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived course KDTest information\",\n\"response\": " +
                "{\n \"KDId\":\"1204\", \"KDName\":\"General Intelligence(GI)\", \"ModuleId\":\"1225\", \"ModuleName\":\"Module 1: GI - U1\", \"TestID\":\"3515\", "+
                "\"TestName\":\"Quiz on Section Section 2: GI - U1 - M1\", \"MaxAttempts\":\"99\", \"ActualAttempts\":\"1\", \"AllAttemptAvgTotScore\":\"0\", " +
                "\"MaxTestUserID\":\"44826\", \"InitFlag\":\"1\", \"TestNumQues\":\"4\", \"TestSettingsMode\":\"Practice\", " +
                "\"LastAttemptedID\":\"44826\", \"TestType\":\"1\", \"SortOrder\":\"17\", \"TestMaxScoreObt\":\"0\", \"TestModuleId\":\"1225\", " +
                "\"Test_ModulePercent\":\"0\", \"LastAttemptPerScore\":\"0\", \"TestUser_IsEval\":\"1\", \"TestSettings_TestModeType\":\"2\", " +
                "\"IsAutoPracticeTest\":\"2\", \"AutoPracticeTestID\":\"0\"\n  }\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"There are no tests for the specified course/No data found/Error while retriving course KDTest information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetKdTestList, new MediaTypeHeaderValue("application/json"), "Assessment", "GetKdTestList");
            config.SetSampleResponse(sampleResponseBodyGetKdTestList, new MediaTypeHeaderValue("application/json"), "Assessment", "GetKdTestList");

            string sampleRequestBodyGetModuleTestList = "{\"userId\":\"2355\",\"sectionId\":\"18\",\"moduleId\":\"1225\"}";
            string sampleResponseBodyGetModuleTestList = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived course ModuleTest information\",\n\"response\": " +
                "{\n \"KDId\":\"1204\", \"KDName\":\"General Intelligence(GI)\", \"ModuleId\":\"1225\", \"ModuleName\":\"Module 1: GI - U1\", \"TestID\":\"3515\", " +
                "\"TestName\":\"Quiz on Section Section 2: GI - U1 - M1\", \"MaxAttempts\":\"99\", \"ActualAttempts\":\"1\", \"AllAttemptAvgTotScore\":\"0\", " +
                "\"MaxTestUserID\":\"44826\", \"InitFlag\":\"1\", \"TestNumQues\":\"4\", \"TestSettingsMode\":\"Practice\", " +
                "\"LastAttemptedID\":\"44826\", \"TestType\":\"1\", \"SortOrder\":\"17\", \"TestMaxScoreObt\":\"0\", \"TestModuleId\":\"1225\", " +
                "\"Test_ModulePercent\":\"0\", \"LastAttemptPerScore\":\"0\", \"TestUser_IsEval\":\"1\", \"TestSettings_TestModeType\":\"2\", " +
                "\"IsAutoPracticeTest\":\"2\", \"AutoPracticeTestID\":\"0\"\n  }\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"There are no tests for the specified course/No data found/Error while retriving course ModuleTest information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetModuleTestList, new MediaTypeHeaderValue("application/json"), "Assessment", "GetModuleTestList");
            config.SetSampleResponse(sampleResponseBodyGetModuleTestList, new MediaTypeHeaderValue("application/json"), "Assessment", "GetModuleTestList");

            string sampleRequestBodyGetMockTestList = "{\"userId\":\"2355\",\"sectionId\":\"18\"}";
            string sampleResponseBodyGetMockTestList = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived Mock Test list information\",\n\"response\": " +
                "{\n \"TestID\": \"3467\",  \"TestName\": \"MtestMock4\",  \"TestDate\": \"2016-04-29 00:00:00.000\",  \"MaxAttempts\": \"99\"," +  
                "\"ActualAttempts\": \"0\",  \"AllAttemptAvgTotScore\": \"NULL\",  \"TestAnsMode\": \"2\",  \"TestAnsDate\": \"NULL\",  " +
                "\"TestResultMode\": \"2\",  \"TestResultDate\": \"1900-01-01 00:00:00.000\",  \"MaxTestUserID\": \"0\",  \"TestEndDate\": \"2018-04-30 00:00:00.000\",  " +
                "\"TestApplTo\": \"1\",  \"InitFlag\": \"NULL\",  \"TestNumQues\": \"4\",  \"TestTimeAllotted\": \"0\",  \"TestTimeAppl\": \"0\",  " +
                "\"TestStatus\": \"5\",  \"TestSettingsMode\": \"Test MC (RMA English Course)\",  \"LastAttemptedID\": \"0\",  \"InstID\": \"22\",  " +
                "\"TzID\": \"190\",  \"TestType\": \"17\",  \"SortOrder\": \"73\",  \"AttHelpText\": \"NULL\",  \"EndDtHelpText\": \"NULL\",  " +
                "\"AllotedTimeHelpText\": \"NULL\",  \"HasPreRequisites\": \"0\",  \"TestMaxScoreObt\": \"0\",  \"Test_AtptDate\": \"NULL\",  \"IsManAtempt\": \"NULL\",  " +
                "\"TestModuleId\": \"0\",  \"Test_ModulePercent\": \"0\",  \"LastAttemptPerScore\": \"0\",  \"TestUser_IsEval\": \"NULL\",  " +
                "\"TestSettings_TestModeType\": \"1\",  \"PswdMode\": \"0\",  \"IsEval\": \"-1\",  \"GlobalPswd\": \"NULL\",  \"IsAutoPracticeTest\": \"0\",  " +
                "\"AutoPracticeTestID\": \"0\",  \"TestSettings_IsActive\": \"1\",  \"TotalTimeSpent\": \"0\",  \"Test_IsLabTest\": \"0\"\n  }\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"There are no tests for the specified course/No data found/Error while retriving Mock Test list information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetMockTestList, new MediaTypeHeaderValue("application/json"), "Assessment", "GetMockTestList");
            config.SetSampleResponse(sampleResponseBodyGetMockTestList, new MediaTypeHeaderValue("application/json"), "Assessment", "GetMockTestList");

            string sampleRequestBodyGetTestInfo = "{\"testId\":\"3480\", \"userId\":\"2567\",\"sectionId\":\"18\"}";
            string sampleResponseBodyGetTestInfo = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived Test Info\",\n\"response\": " +
                "{\n \"TestId\": \"3480\", \"Test_Name\": \"Mtest Sub Prat Test1 mock\", \"Test_HeadName\": \"Mtest Sub Prat Test1 mock\", " +
                "\"Test_HeadDesc\": \"NULL\", \"Test_Type\": \"17\", \"Test_IsComp\": \"1\", \"Test_TestSettingID\": \"3482\", " +
                "\"Test_UserID\": \"22\", \"Test_Instructions\": \"NULL\", \"TestSettings_ApplTo\": \"1\", " +
                "\"TestSettings_TestDate\": \"2016-04-29 00:00:00.000\", \"TestSettings_TestMode\": \"0\", \"TestSettings_LoadType\": \"3\", " +
                "\"TestSettings_LoadValue\": \"1\", \"TestSettings_ShowAnsAtEnd\": \"2\", \"TestSettings_AnsDate\": \"NULL\", \"TestSettings_DispResImm\": \"1\", " +
                "\"TestSettings_ResDate\": \"1900-01-01 00:00:00.000\", \"TestSettings_TimeAppl\": \"0\", \"TestSettings_TimeType\": \"0\", " +
                "\"TestSettings_TotalTime\": \0\", \"TestSettings_MrksAppl\": \"1\", \"TestSettings_NegMrksAppl\": \"1\", \"TestSettings_NegMrks\": \"50\", " +
                "\"TestSettings_AnsToFwd\": \"0\", \"TestSettings_AllowBack\": \"0\", \"TestSettings_NoAtmpts\": \"99\", \"TestSettings_DynDispMode\": \"2\", " +
                "\"TestSettings_UserID\": \"22\", \"TestSettings_AddToGrade\": \"0\", \"TestSettings_GradeType\": \"0\", \"TestSettings_LevelOrder\": \"0\", " +
                "\"TestSettings_ChoiceAppl\": \"0\", \"TestSettings_totmarks\": \"28\", \"Test_Attachflag\": \"1\", \"TestSecLink_PswdMode\": \"0\", " +
                "\"TestSecLink_GlobalPswd\": \"NULL\", \"TestSettings_DynDispFlag\": \"4\", \"TestSettings_QuesType\": \"$1$1$20$1$\", \"Test_IsAutoPracticeTest\": \"1\", " +
                "\"Test_IsQuesEdit\": \"0\", \"Test_HasPreRequisite\": \"0\", \"Test_PreRequisiteID\": \"0\", \"Test_ModeID\": \"2009\", " +
                "\"TestSettings_IsActive\": \"1\", \"Test_AutoPracticeTestID\": \"3520\", \"StudentName\": \"821, Student821\", \"Test_ModulePercent\": \"0\", " +
                "\"Test_AttendancePercent\": \"0\", \"TestSettings_TestModeType\": \"1\", \"TestSecLink_TimedPswd\": \"NULL\"," +
                "\"TestSecLink_TimedPswdFromDate\": \"NULL\", \"TestSecLink_TimedPswdToDate\": \"NULL\", \"Test_ModuleId\": \"0\", \"Test_MaxScore\": \"NULL\", " +
                "\"Test_TotNoQuestion\": \"18\", \"QuesMarks\": \"1\", \"QuesHasHindiVer\": \"0\"\n  }\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"There is no test info for the specified TestId./No data found/Error while retriving Test information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetTestInfo, new MediaTypeHeaderValue("application/json"), "Assessment", "GetTestInfo");
            config.SetSampleResponse(sampleResponseBodyGetTestInfo, new MediaTypeHeaderValue("application/json"), "Assessment", "GetTestInfo");

            string sampleRequestBodyInitiateTestPaper = "{\"testId\":\"3481\", \"userId\":\"2558\",\"sectionId\":\"18\",\"lastTestUserId\":\"0\"}";
            string sampleResponseBodyInitiateTestPaper = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived TestPaper information\","+
                "\n\"response\":{"+
                "\n   \"TestPaper\":[\n   {\"TestUserID\":45458,\"TimeMode\":0,\"TotalTime\":0,\"ResDispMode\":1,\"TimeLeft\":0,\"ResumeAppl\":false," +
                "\"HeadName\":\"Mtest Sub Prat Test2 mock Quiz\",\n   \"HeadDesc\":\"\",\"FeedBackActive\":true,\"TotalTimeSpent\":0,\"DispSoln\":2," +
                "\"DispSolnMode\":2,\"UserName\":\"e 101\",\"DynDispFlag\":3,\"TestType\":17,\n   \"TestModeType\":1,\"IsMultiTestPartEnabled\":true," +
                "\"TryLaterQuestList\":\"\",\"TestDeliveryOption\":1,\"TotalMkrs\":18.0,\"TotalMkrsObt\":0.0,\n   \"DispHint\":1,\"IsAutoPracticeTest\":1," +
                "\"NegMrksPer\":0.0}\n   ]," +
                "\n   \"TestPart\":[\n   {\"QuesContID\":3583,\"QuesCont_Name\":\"Knowledge Domain - I\",\"TotMarks\":13.0,\"TotQuesCount\":7,\"TotQuesResponded\":0, \"PreAnsString\":, \"LastPageNo\":0}," +
                "\n   {\"QuesContID\":3586,\"QuesCont_Name\":\"Knowledge Domain - 2\",\"TotMarks\":5.0,\"TotQuesCount\":5,\"TotQuesResponded\":0, \"PreAnsString\":, \"LastPageNo\":0}\n   ]\n  }\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"There is no TestPaper info for the specified TestId./No data found/Error while retriving TestPaper information\"\n}";
            config.SetSampleRequest(sampleRequestBodyInitiateTestPaper, new MediaTypeHeaderValue("application/json"), "Assessment", "InitiateTestPaper");
            config.SetSampleResponse(sampleResponseBodyInitiateTestPaper, new MediaTypeHeaderValue("application/json"), "Assessment", "InitiateTestPaper");

            string sampleRequestBodyDisplayTestQuestion = "{\"testuserid\":\"45403\", \"quescontid\":\"3948\",\"pgno\":\"1\"}";
            string sampleResponseBodyDisplayTestQuestion = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived TestQuestion information\"," +
                "\n\"response\":{"+
                "\n   \"Question\":[ {\"TestTotalMkrs\":\"4\", \"TestTotalMkrsObt\":\"1\", \"TotTestQuesResponded\":\"1\", \"TestQuesId\":\"332728\"," +
                "\"PageNo\":\"1\", \"DueTime\":\"-419131\", \"QuesTime\":\"0\", \"QuesConceptID\":\"1540\", \"ConceptDesc\":\"GI_U1_M1_S3_Con60;\"," +
                "\"QuesId\":\"3079\", \"QuesMarks\":\"1\", \"QuesMarksObt\":\"0\", \"EvalStatus\":\"0\", \"QuesType\":\"1\", \"HasImage\":\"0\","+
                "\"QuesDesc\":\"GI_U1_M1_S3_Con60_Description1\", \"QuesHint\":\"\", \"DispHint\":\"1\", \"DispSoln\":\"1\","+
                "\"QuesSoln\":\"Html content string as solution\", \"QuesSolnVideo\":\"\", \"DispSynonym\":\"0\"} ],"+
                "\n   \"Option\":[      {\"QuesMultiChId\":\"12316\",\"MainQuesId\":\"3079\",\"AnsDesc\":\"Opt4\",\"AnsSrlNo\":\"1\",\"AnsLabel\":\"a\","+
                "\"AnsComment\":\"\",\"IsValidAns\":\"0\",\"HasAnsImage\":\"0\",\"AnsFormat\":\"0\"}\n   ]\n }\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"There is no TestQuestion info for the specified testuserid, contid, pgno./No data found/Error while retriving TestQuestion information\"\n}";
            config.SetSampleRequest(sampleRequestBodyDisplayTestQuestion, new MediaTypeHeaderValue("application/json"), "Assessment", "DisplayTestQuestion");
            config.SetSampleResponse(sampleResponseBodyDisplayTestQuestion, new MediaTypeHeaderValue("application/json"), "Assessment", "DisplayTestQuestion");

            string sampleRequestBodySubmitTestQuestion = "{\"testuserid\":\"45448\", \"contid\":\"3947\",\"testdata\":\"1Åopt_12307Å332606Å12307Å0\",\"pgno\":\"1\", \"timespent\":\"1191\",\"resdata\":\"332606Å2Å0.0\",\"totaltimespent\":\"9\", \"sectionId\":\"67\",\"CallReference\":\"0\"}";
            string sampleResponseBodySubmitTestQuestion = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Question has been successfully submited\"," +
                "\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"Error message while submitting question information\"\n}";
            config.SetSampleRequest(sampleRequestBodySubmitTestQuestion, new MediaTypeHeaderValue("application/json"), "Assessment", "SubmitTestQuestion");
            config.SetSampleResponse(sampleResponseBodySubmitTestQuestion, new MediaTypeHeaderValue("application/json"), "Assessment", "SubmitTestQuestion");
            
            string sampleRequestBodyGetTestAttemptResult = "{\"TestUserID\":\"45738\",\"SectionID\":\"18\"}";
            string sampleResponseBodyGetTestAttemptResult = "Response Type: Json  Success \n\n{\n \"status\":\"1\",\n \"message\":\"Succesfully retrived TestAttemptResult information\"," +
                "\n \"response\":{\n            \"TestResult\":[\n                        {\"TestUserID\":45738,\"TestUser_TotMrks\":20.0,\"TestUser_TotMrksObt\":1.0,"+
                "\"TestUser_TestDate\":\"2018-07-06T17:06:27.673\",\"TimeSpent\":\" 10 Min.\",\n                         \"TestSettings_NegMrksAppl\":false,"+
                "\"TestSettings_NegMrks\":0.0,\"QuesMarks\":1.0,\"Practice\":2}\n                        ],\n            \"TestResultByPart\":[\n                        " +
                "{\"TestUserTestPartID\":9334,\"TestUserTestPart_TestContID\":3763,\"TestUserTestPart_ContName\":\"GeneralIntelligence(GI)\","+
                "\n                        \"TestUserTestPart_TotMrks\":10.0,\"TestUserTestPart_TotMrksObt\":0.0,\"TestUserTestPart_TotQuesCount\":10," +
                "\n                        \"TestUserTestPart_TotCurrQues\":0,\"TestUserTestPart_TotInCurrQues\":2,\"TestUserTestPart_TotParticalCurrQues\":0},\n"+
                "\n                        {\"TestUserTestPartID\":9335,\"TestUserTestPart_TestContID\":3764,\"TestUserTestPart_ContName\":\"English\","+
                "\n                        \"TestUserTestPart_TotMrks\":10.0,\"TestUserTestPart_TotMrksObt\":1.0,\"TestUserTestPart_TotQuesCount\":10," +
                "\n                        \"TestUserTestPart_TotCurrQues\":1,\"TestUserTestPart_TotInCurrQues\":2,\"TestUserTestPart_TotParticalCurrQues\":0}"+
                "\n                        ]\n            }\n}" +
                "\n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"Error message while retriving TestAttemptResult information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetTestAttemptResult, new MediaTypeHeaderValue("application/json"), "Assessment", "GetTestAttemptResult");
            config.SetSampleResponse(sampleResponseBodyGetTestAttemptResult, new MediaTypeHeaderValue("application/json"), "Assessment", "GetTestAttemptResult");

            string sampleRequestBodyGetTestAttemptQuesStatus = "{\"TestUserTestPartID\":\"9529\"}";
            string sampleResponseBodyGetTestAttemptQuesStatus = "Response Type: Json  Success \n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived TestAttemptQuesStatus information\"," +
                "\n\"response\":{\n          \"TestAttemptQuesStatus\":[\n                    {\"RowID\":1,\"TestQuesID\":275444,\"Ques_EvalStatus\":1,\"Ques_MaxMarks\":1.0,\"Ques_ObtMarks\":1.0," + 
                "\"Ques_Desc\":\"Sample question description in plain text or html format\",\"Ques_LevelID\":1281,\"CRLevel_Name\":\"Section 1: GI - U1 - M1\"," + 
                "\"Ques_ConceptDesc\":\"GI_U1_M1_S1_Con1;\",\"ModuleName\":0,\"Ques_SlnVedio\":\"<div class='SolVedioBtn SolVedioBtnDispNone'title='Click here to view solution video' onclick = \"DispSolutionVedio('5MagF1y_JnY',1);\" data-filename=5MagF1y_JnY data-langtype=0><span>Solution Video</span></div> \",\"Ques_LanguageType\":1}]," +
                "\n         \"TestAttemptQuesStatusSummary\":[\n                    {\"TestType\":17,\"TotalQuesCount\":10,\"CorrectAns\":3,\"INCorrectAns\":0,\"Notattempted\":7,\"CRLevel_Name\":\"Module 1: GI - U1\",\"CRLevelID\":1225,\"CRLevel_ParentId\":1225,\"IsParent\":1,\"Sectionvideo\":0}," +
                "{\"TotalQuesCount\":10,\"CorrectAns\":3,\"INCorrectAns\":0,\"Notattempted\":7,\"CRLevel_Name\":\"Section 1: GI - U1 - M1\",\"CRLevelID\":1281,\"CRLevel_ParentId\":1225,\"IsParent\":0,\"Sectionvideo\":1}]\n      }\n}" +
                "\n\nResponse Type: Json  Error \n{\n \"status\": 0,\n \"message\": \"Error message while retriving TestAttemptQuesStatus information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetTestAttemptQuesStatus, new MediaTypeHeaderValue("application/json"), "Assessment", "GetTestAttemptQuesStatus");
            config.SetSampleResponse(sampleResponseBodyGetTestAttemptQuesStatus, new MediaTypeHeaderValue("application/json"), "Assessment", "GetTestAttemptQuesStatus");

            string sampleRequestBodyGetDispStudTestResults = "{\"TestUserTestPartID\":\"9529\", \"TestQuesID\":\"275444\"}";
            string sampleResponseBodyGetDispStudTestResults = "Response Type: Json  Success \n{\n\"status\":\"1\", \n\"message\":\"Succesfully retrived DispStudTestResults information\","+
                "\n\"response\":{\n         \"Test_User\":[{\"RecID\":\"1\", \"TestQuesID\":\"275444\", \"ContID\":\"3760\", \"QuesType\":\"1\", \"Pgno\":\"1\", \"ContName\":\"General Intelligence(GI)\", \"ContDesc\":\"\", \"SecTime\":\"0\", \"QuesTime\":\"0\", \"Ques_ConceptDtlID\":\"1361\", \"Ques_ConceptDesc\":\"GI_U1_M1_S1_Con1;\", \"Ques_CRLevelID\":\"1281\", \"Ques_DispCalc\":\"0\", \"QuesSubContID\":\"359\", \"QuesSubContName\":\"Part 1\", \"Ques_ConceptType\":\"0\"}], \n         \"Test_Ques\":[{\"intPkVal\":\"275444\", \"TestQues_ContID\":\"3760\", \"TestQues_QuesID\":\"2722\", \"TestQues_Marks\":\"1\", \"MarksObtained\":\"1\", \"EvalStatus\":\"1\", \"TestQues_Time\":\"0\", \"AnsDispMode\":\"2\", \"TestQues_ModuleId\":\"0\", \"TestQues_Qrclvl_Crleveid\":\"1281\", \"TestQues_Qcrlvl_conceptid\":\"1361\"}],"+
                "\n         \"Ques_Main\":[{\"Ques_HasImg\":\"1\", \"Ques_Desc\":\"Sample question description in plain text or html format\", \"Ques_ResDesc\":\"\", \"Ques_UserID\":\"21\", \"HintHTML\":\"\", \"DispHint\":\"1\", \"DispSoln\":\"1\", \"slnHTML\":\"<div class='SolutionBtn SolutionBtnDispNone'title='Click here to view solution' onclick = \"DispSolutionHint('004FE958-5301-4470-9C66-560884E47823.html',0,0);\" data-filename=004FE958-5301-4470-9C66-560884E47823.html data-filewidth=0 data-fileheight=0 data-langtype=0>Solution</div>\", \"SlnVedio\":\"<div class='SolVedioBtn SolVedioBtnDispNone'title='Click here to view solution video' onclick = \"DispSolutionVedio('5MagF1y_JnY',1);\" data-filename=5MagF1y_JnY data-langtype=0><span>Solution Video</span></div> \", \"IsSynonym\":\"1\", \"Ques_IsMultiLingual\":\"1\", \"Ques_HindiDes\":\"Question description in hindi language\"}],"+
                "\n         \"Ques_MultipleChoice\":[{\"QuesMultiChID\":\"10886\", \"QuesMultiCh_QuesID\":\"2722\", \"QuesMultiCh_AnsDesc\":\"Opt2\", \"QuesMultiCh_SrlNo\":\"1\", \"QuesMultiCh_AnsLbl\":\"a\", \"QuesMultiCh_AnsComt\":\"\", \"QuesMultiCh_ValidAns\":\"0\", \"QuesMultiCh_UpldAnsImg\":\"0\", \"QuesMultiCh_MultiValidAns\":\"0\", \"QuesMultiCH_AnsOptFormat\":\"1\", \"QuesMultiCh_MrksWeight\":\"0.0000000e+000\", \"QuesMultiCh_HindiAnsDesc\":\"Option description in hindi language\"},\n{\"QuesMultiChID\":\"10888\", \"QuesMultiCh_QuesID\":\"2722\", \"QuesMultiCh_AnsDesc\":\"Opt4\", \"QuesMultiCh_SrlNo\":\"2\", \"QuesMultiCh_AnsLbl\":\"b\", \"QuesMultiCh_AnsComt\":\"\", \"QuesMultiCh_ValidAns\":\"0\", \"QuesMultiCh_UpldAnsImg\":\"0\", \"QuesMultiCh_MultiValidAns\":\"0\", \"QuesMultiCH_AnsOptFormat\":\"1\", \"QuesMultiCh_MrksWeight\":\"0.0000000e+000\", \"QuesMultiCh_HindiAnsDesc\":\"Opt4?????\"},\n{\"QuesMultiChID\":\"10885\", \"QuesMultiCh_QuesID\":\"2722\", \"QuesMultiCh_AnsDesc\":\"Opt1\", \"QuesMultiCh_SrlNo\":\"3\", \"QuesMultiCh_AnsLbl\":\"c\", \"QuesMultiCh_AnsComt\":\"\", \"QuesMultiCh_ValidAns\":\"1\", \"QuesMultiCh_UpldAnsImg\":\"0\", \"QuesMultiCh_MultiValidAns\":\"0\", \"QuesMultiCH_AnsOptFormat\":\"1\", \"QuesMultiCh_MrksWeight\":\"0.0000000e+000\", \"QuesMultiCh_HindiAnsDesc\":\"Opt1?????\"},\n{\"QuesMultiChID\":\"10887\", \"QuesMultiCh_QuesID\":\"2722\", \"QuesMultiCh_AnsDesc\":\"Opt3\", \"QuesMultiCh_SrlNo\":\"4\", \"QuesMultiCh_AnsLbl\":\"d\", \"QuesMultiCh_AnsComt\":\"\", \"QuesMultiCh_ValidAns\":\"0\", \"QuesMultiCh_UpldAnsImg\":\"0\", \"QuesMultiCh_MultiValidAns\":\"0\", \"QuesMultiCH_AnsOptFormat\":\"1\", \"QuesMultiCh_MrksWeight\":\"0.0000000e+000\", \"QuesMultiCh_HindiAnsDesc\":\"Opt3?????\"}],"+
                "\n         \"Test_UserAns\":[{\"TestQuesID\":\"275444\", \"QuesMultiChID\":\"10885\", \"ValidAns\":\"1\", \"AnsText\":\"None\"}],"+
                "\n         \"DispSolnRes\":[{\"DispSolnRes\":\"1\"}],"+
                "\n         \"QuesLangType\":[{\"QuesLangType\":\"0\"}]," +
                "\n         \"QuesEvalStatus\":[{\"RowID\":1, \"TestQuesID\":275444, \"Ques_EvalStatus\":1},{\"RowID\":2, \"TestQuesID\":275443, \"Ques_EvalStatus\":1},{\"RowID\":3, \"TestQuesID\":275446, \"Ques_EvalStatus\":1},{\"RowID\":4, \"TestQuesID\":275445, \"Ques_EvalStatus\":0},{\"RowID\":5, \"TestQuesID\":275448, \"Ques_EvalStatus\":0},{\"RowID\":6, \"TestQuesID\":275447, \"Ques_EvalStatus\":0},{\"RowID\":7, \"TestQuesID\":275449, \"Ques_EvalStatus\":0},{\"RowID\":8, \"TestQuesID\":275450, \"Ques_EvalStatus\":0},{\"RowID\":9, \"TestQuesID\":275451, \"Ques_EvalStatus\":0},{\"RowID\":10, \"TestQuesID\":275452, \"Ques_EvalStatus\":0}]}}" +
                "\n\nResponse Type: Json  Error \n{\n \"status\": 0, \n \"message\": \"Error message while retriving DispStudTestResults information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetDispStudTestResults, new MediaTypeHeaderValue("application/json"), "Assessment", "GetDispStudTestResults");
            config.SetSampleResponse(sampleResponseBodyGetDispStudTestResults, new MediaTypeHeaderValue("application/json"), "Assessment", "GetDispStudTestResults");

            #endregion

            //For User
            #region
            string sampleRequestBodyVerifyUserLogin = "{\"Email\":\"useremail@educo.com\",\"Password\":\"welcome\",\"os\":\"android\",\"device_id\":\"\"}";
            string sampleResponseBodyVerifyUserLogin = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"succesfully login\",\n\"response\": {\n \"userId\":\"2355\", \"first_name\":\"Student717\", \"last_name\":\"717\", \"email\":\"stu717@educosoft.com\", \"mobile\":\"9036192208\", \"sectionid\":\"18\"\n  }\n} \n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"Username and password do not match\"\n}";
            config.SetSampleRequest(sampleRequestBodyVerifyUserLogin, new MediaTypeHeaderValue("application/json"), "Users", "VerifyUserLogin");
            config.SetSampleResponse(sampleResponseBodyVerifyUserLogin, new MediaTypeHeaderValue("application/json"), "Users", "VerifyUserLogin");

            string sampleRequestBodyGetForgotPassword = "{\"Email\":\"useremail@educo.com\", \"os\":\"android\", \"device_id\":\"\"}";
            string sampleResponseBodyGetForgotPassword = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"succesfully retrived password\",\n\"response\": {\n \"userId\":\"2355\", \"first_name\":\"Student717\", \"last_name\":\"717\", \"email\":\"stu717@educosoft.com\", \"password\":\"user password\"\n  }\n} \n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"Username does not exist/Error message \"\n}";
            config.SetSampleRequest(sampleRequestBodyGetForgotPassword, new MediaTypeHeaderValue("application/json"), "Users", "GetForgotPassword");
            config.SetSampleResponse(sampleResponseBodyGetForgotPassword, new MediaTypeHeaderValue("application/json"), "Users", "GetForgotPassword");

            string sampleRequestBodyRegisterGuestUser = "{\"FirstName\":\"user first name\",\"LastName\":\"user last name\",\"MobileNumber\":\"user mobile no\",\"Email\":\"useremail@educo.com\",\"Password\":\"welcome\",\"strImageType\":\"jpg\",\"strImage\":\"Image in Base64String format\"}";
            string sampleResponseBodyRegisterGuestUser = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"succesfully registered and login as guest. Registration mail status: Email has been sent to the registered email id\",\n\"response\": {\n \"userId\":\"2355\", \"first_name\":\"Student717\", \"last_name\":\"717\", \"email\":\"stu717@educosoft.com\", \"mobile\":\"9036192208\", \"sectionid\":\"18\"\n  }\n} \n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"Error message while registering the user's info and login\"\n}";
            config.SetSampleRequest(sampleRequestBodyRegisterGuestUser, new MediaTypeHeaderValue("application/json"), "Users", "RegisterGuestUser");
            config.SetSampleResponse(sampleResponseBodyRegisterGuestUser, new MediaTypeHeaderValue("application/json"), "Users", "RegisterGuestUser");

            #endregion

            //For UserCourse
            #region
            string sampleRequestBodyGetUserCourseInfo = "{\"userId\":\"2355\",\"sectionId\":\"18\"}";
            string sampleResponseBodyGetUserCourseInfo = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived course information\",\n\"response\": {\n \"Course_Name\":\"SSC Center 1\", \"CourseId\":\"1023\", \"Duration\":\"321\", \"Section_Name\":\"Section 1\", \"SectionId\":\"18\", \"Term_EndDate\":\"30-05-2018 00:00:00\", \"Term_Name\":\"Term1 Center1\", \"Term_StartDate\":\"15-12-2016 00:00:00\", \"TermId\":\"6\"\n  }\n} \n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"User does not have access to section/No data found/Error while retriving course information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetUserCourseInfo, new MediaTypeHeaderValue("application/json"), "UserCourse", "GetUserCourseInfo");
            config.SetSampleResponse(sampleResponseBodyGetUserCourseInfo, new MediaTypeHeaderValue("application/json"), "UserCourse", "GetUserCourseInfo");

            string sampleRequestBodyGetCourseKDList = "{\"userId\":\"2355\",\"sectionId\":\"18\"}";
            string sampleResponseBodyGetCourseKDList = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived course kd list information\",\n\"response\": {\n    \"CourseName\" : \"SSC Center 1\", \n    \"CourseList\":{\n        \"CRLevelId\":\"1204\", \"CRLevel_Name\":\"General Intelligence(GI)\"\n    }\n  }\n} \n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"User does not have access to section/No data found/Error while retriving course kd information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetCourseKDList, new MediaTypeHeaderValue("application/json"), "UserCourse", "GetCourseKDList");
            config.SetSampleResponse(sampleResponseBodyGetCourseKDList, new MediaTypeHeaderValue("application/json"), "UserCourse", "GetCourseKDList");

            string sampleRequestBodyGetKDModuleList = "{\"userId\":\"2355\",\"sectionId\":\"18\",\"kdId\":\"1204\"}";
            string sampleResponseBodyGetKDModuleList = "Response Type: Json  Success \n\n{\n\"status\":\"1\",\n\"message\":\"Succesfully retrived course kd module list\",\n\"response\": {\n \"CRModule_LevelId\":\"1225\", \"CRModule_Name\":\"Module 1: GI - U1\"\n  }\n} \n\nResponse Type: Json  Error \n\n{\n \"status\": 0,\n \"message\": \"User does not have access to section/No data found/Error while retriving course kd module information\"\n}";
            config.SetSampleRequest(sampleRequestBodyGetKDModuleList, new MediaTypeHeaderValue("application/json"), "UserCourse", "GetKDModuleList");
            config.SetSampleResponse(sampleResponseBodyGetKDModuleList, new MediaTypeHeaderValue("application/json"), "UserCourse", "GetKDModuleList");

            #endregion
        }
    }
}