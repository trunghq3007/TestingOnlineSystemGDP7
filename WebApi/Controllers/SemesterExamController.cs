﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;

using System.Web.Http;
using System.Web.Http.Cors;
using Model;
using Model.ViewModel;
using Newtonsoft.Json;
using Services;
using WebApi.Commons;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowCrossSite]
    public class SemesterExamController : ApiController
    {
        private SemesterExamServices service;
        public SemesterExamController()
        {
            service = new SemesterExamServices();
        }

        // GET: SemesterExam
        [Route("SemesterExam")]
        [System.Web.Http.HttpGet]
        [ValidateSSID(ActionId = 32)]
        public string Index()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                 result.Data = service.GetAll();
                return JsonConvert.SerializeObject(result,Formatting.Indented,jsonSetting);

            }
            catch (Exception E)
            {
                result.Message = "EXCEPTION " + E.Message + "Stack" + E.StackTrace;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            
        }

        //[Route("SemesterExams")]
        //[System.Web.Http.HttpPost]
        //public string Insert(SemesterExam semesterExam)
        //{
        //    var result = service.Insert(semesterExam);
        //    return JsonConvert.SerializeObject(result);
        //}
        [Route("SemesterExam/Post/")]
        [HttpPost]
        [ValidateSSID(ActionId = 33)]
        public string Post([FromBody]object value)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                if (value != null)
                {
                    var E = JsonConvert.DeserializeObject<SemesterExam>(value.ToString());
                   
                     result.Data = service.Insert(E);
                    return JsonConvert.SerializeObject(result);
                }
                return "FALSE";

            }
            catch(Exception E)
            {
                result.Message = "EXCEPTION " + E.Message + "Stack" + E.StackTrace;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            
            
        }

        [Route("SemesterExam/{id}")]
        [System.Web.Http.HttpPut]
        [ValidateSSID(ActionId = 39)]
        public string Update(SemesterExam semesterExam, int id)
        {

            semesterExam.ID = id;
            var result = service.Update(semesterExam);
            return JsonConvert.SerializeObject(result);
        }
        [Route("SemesterExam/{id}")]
        [System.Web.Http.HttpDelete]
        [ValidateSSID(ActionId = 35)]
        public string Delete( int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result);
        }
        [System.Web.Http.HttpGet]
        [ValidateSSID(ActionId = 36)]
        public string Report(int id)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                result.Data = service.Report(id);
                //string a = "";
                //foreach (SemesterExam_User item in result)
                //{
                //     a = item.SemesterExam.SemesterName;
                //}
                //string b = a;

                return JsonConvert.SerializeObject(result);
            }
            catch(Exception E)
            {
                
                result.Message = "EXCEPTION " + E.Message + "Stack" + E.StackTrace;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            
        }
        //[Route("SemesterExam/detail/{id}")]
        //[HttpGet]
        //public string Get(int id)
        //{
        //    var result = service.GetById(id);
        //    return JsonConvert.SerializeObject(result);
        //}
        [HttpGet]
        [ValidateSSID(ActionId = 32)]
        public string Get(string searchString)
        {
            var result = service.Search(searchString).ToList();
            //return JsonConvert.SerializeObject(result);
            return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        [Route("SemesterExam/detail/{id}")]
        [HttpGet]
        [ValidateSSID(ActionId = 37)]
        public string Detail(int id)
        {
            var result = service.GetById(id);
            return JsonConvert.SerializeObject(result);
        }

        [Route("SemesterExam/result/{id}")]
        [ValidateSSID(ActionId = 38)]
        [HttpGet]
        public string Result(int id, int userid)
        {
            var result = service.GetResult(id,userid);
            return JsonConvert.SerializeObject(result);
        }
        [Route("SemesterExam/Update/")]
        [HttpPost]
        [ValidateSSID(ActionId = 34)]
        public string Update([FromBody]object value)
        {
            if (value != null)
            {
                var jsonSetting = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                ResultObject result = new ResultObject();
                try {
                    int a = 0;

                    var E = JsonConvert.DeserializeObject<SemesterDetail>(value.ToString());
                    SemesterExam SE = new SemesterExam();
                    SE.ID = E.ID;
                    SE.SemesterName = E.SemesterName;
                    SE.StartDay = Convert.ToDateTime(E.StartDay);
                    SE.EndDay = Convert.ToDateTime(E.EndDay);
                    SE.Code = E.Code;
                    if (E.status.Equals("Done"))
                        SE.status = 0;
                    if (E.status.Equals("Public"))
                        SE.status = 1;
                    if (E.status.Equals("Draft"))
                        SE.status = 2;


                    //SE.status = E.status.Equals("Public") ? 1 : 0;
                    //SE.status = E.status.Equals("Draft") ? 2 : 0;
                    //SE.EndDay = Convert.ToDateTime(E.EndDay);
                    //SE.Code = E.Code;

                    //SemesterExam E = new SemesterExam();
                    //E.SemesterName = "alo123";
                     result.Status = service.Update(SE);
                    return JsonConvert.SerializeObject(result);
                } 
                catch(Exception E)
                {
                    result.Message = "EXCEPTION " + E.Message + "Stack" + E.StackTrace;
                    return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
                }
            }
            //SemesterExam T = new SemesterExam();
            //T.SemesterName = "alo123";
            //T.ID = 5;
            //var resultt = service.Update(T, id);
            return "FALSE";

        }
        [HttpGet]
        [ValidateSSID(ActionId = 40)]
        public string Code(string code, string InputCode)
        {
            var result = service.InputCode(code).ToList();
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        [ValidateSSID(ActionId = 41)]
        public string GetByCandidateId(int candidateId, string SemesterExamAssign)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = service.GetByCandidateId(candidateId);
            return JsonConvert.SerializeObject(result,Formatting.Indented,jsonSetting
            
            );
        }
        [HttpGet]
        [ValidateSSID(ActionId = 42)]
        public string GetExams(int id, [FromUri] string IsGetExams)
        {
            var result = service.GetExams(id);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        [ValidateSSID(ActionId = 43)]
        public string GetTests(int id, [FromUri] string IsGetTests)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = service.GetTests(id);
            return JsonConvert.SerializeObject(result, Formatting.Indented,jsonSetting);
        }

        [HttpPost]
        [ValidateSSID(ActionId = 32)]
        public string Filter([FromUri]string action, [FromBody] object value)
        {
            if (value != null)
            {
                if ("filter".Equals(action))
                {
                    try
                    {
                        var filterObject = JsonConvert.DeserializeObject<SemesterExam>(value.ToString());
                        return JsonConvert.SerializeObject(service.Filter(filterObject));
                    }
                    catch (Exception)
                    {
                        return "Object fillter not convert valid";
                    }
                }
            }
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result);

        }
        [HttpGet]
        [ValidateSSID(ActionId = 32)]
        public string Search(string searchString, string isSearch)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                 result.Data = service.Search(searchString).ToList();
                return JsonConvert.SerializeObject(result);
            }
           catch(Exception E)
            {
                result.Message = "EXCEPTION " + E.Message + "Stack" + E.StackTrace;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
        }
        [HttpGet]
        [ValidateSSID(ActionId = 44)]
        public string SearchExams(string searchString, int id , string searchExams)
        {

            var result = service.SearchExams(searchString, id);
            return JsonConvert.SerializeObject(result);
        }
         [HttpGet]
        [ValidateSSID(ActionId = 45)]
        public string GetTestDetail(int id, string IsGetTestDetail)
        {
            var result = service.GetTestDetail(id);
            return JsonConvert.SerializeObject(result);
        }





        [HttpGet]
        public string Get(int id, string isGetTestsNotAdd)
        {
            var result = service.GetTestsNotAdd(id);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string GetTestProcessing(int id , string IsgetTestProcessing)
        {


            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = service.GeTestProcessings(id);
            return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
        }

        [HttpPost]
        public string Submit([FromBody] object value, int testId,int userId, string isSubmit)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            int userid = userId;
            var E = JsonConvert.DeserializeObject<List<Answer>>(value.ToString());
            int F = service.Submit(E, testId, userid);
            return JsonConvert.SerializeObject(F, Formatting.Indented, jsonSetting); 

        }
        [Route("SemesterExam/submid/{testId}")]
        [HttpPost]
        public string Submits( int testId, [FromBody] object value, int userID)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var E = JsonConvert.SerializeObject(value);
           
            int F = service.Submits(testId,E, userID);
            return JsonConvert.SerializeObject(F, Formatting.Indented, jsonSetting);

        }

    }
}