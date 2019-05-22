using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http;

using Model;
using Model.ViewModel;
using Newtonsoft.Json;
using Services;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace WebApi.Controllers
{
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
        public string Index()
        {
            var result = service.GetAll();
            return JsonConvert.SerializeObject(result);
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
        public string Post([FromBody]object value)
        {
            if (value != null)
            {
                var E= JsonConvert.DeserializeObject<SemesterExam>(value.ToString());
                var result = service.Insert(E);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [Route("SemesterExam/{id}")]
        [System.Web.Http.HttpPut]
        public string Update(SemesterExam semesterExam, int id)
        {
            semesterExam.ID = id;
            var result = service.Update(semesterExam);
            return JsonConvert.SerializeObject(result);
        }
        [Route("SemesterExam/{id}")]
        [System.Web.Http.HttpDelete]
        public string Delete( int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result);
        }
        [System.Web.Http.HttpGet]
        public string Report(int id)
        {
            var result = service.Report(id);
            //string a = "";
            //foreach (SemesterExam_User item in result)
            //{
            //     a = item.SemesterExam.SemesterName;
            //}
            //string b = a;

            return JsonConvert.SerializeObject(result);
        }
        //[Route("SemesterExam/detail/{id}")]
        //[HttpGet]
        //public string Get(int id)
        //{
        //    var result = service.GetById(id);
        //    return JsonConvert.SerializeObject(result);
        //}
        [HttpGet]
        public string Get(string searchString)
        {
            var result = service.Search(searchString).ToList();
            return JsonConvert.SerializeObject(result);
        }
        [Route("SemesterExam/detail/{id}")]
        [HttpGet]
        public string Detail(int id)
        {
            var result = service.GetById(id);
            return JsonConvert.SerializeObject(result);
        }
        [Route("SemesterExam/Update/")]
        [HttpPost]
        public string Update([FromBody]object value)
        {
            if (value != null)
            {
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
                var result = service.Update(SE);
                return JsonConvert.SerializeObject(result);
            }
            //SemesterExam T = new SemesterExam();
            //T.SemesterName = "alo123";
            //T.ID = 5;
            //var resultt = service.Update(T, id);
            return "FALSE";

        }
        [HttpGet]
        public string Code(string code, string InputCode)
        {
            var result = service.InputCode(code).ToList();
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string GetByCandidateId(int candidateId, string SemesterExamAssign)
        {
            var result = service.GetByCandidateId(candidateId);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string GetExams(int id, [FromUri] string IsGetExams)
        {
            var result = service.GetExams(id);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string GetTests(int id, [FromUri] string IsGetTests)
        {
            var result = service.GetTests(id);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
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
        public string Search(string searchString, string isSearch)
        {
            var result = service.Search(searchString).ToList();
            return JsonConvert.SerializeObject(result);
        }


    }
}