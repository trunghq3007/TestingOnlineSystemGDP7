using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http;

using Model;
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
        [Route("SemesterExam/detail/{id}")]
        [HttpGet]
        public string Get(int id)
        {
            var result = service.GetById(id);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string Get(string searchString)
        {
            var result = service.Search(searchString).ToList();
            return JsonConvert.SerializeObject(result);
        }
    }
}