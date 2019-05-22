using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Services;

using Model;
using Model.ViewModel;

namespace WebApi.Controllers
{
    public class SemesterExamUserController : ApiController
    {
        private SemesterExamUserServices service;
        // GET: SemesterExamUser
        public SemesterExamUserController()
        {
            service = new SemesterExamUserServices();
        }
        [System.Web.Http.HttpGet]
        public string Get(int id)
        {
            var result = service.candidates(id);
            //string a = "";
            //foreach (SemesterExam_User item in result)
            //{
            //     a = item.SemesterExam.SemesterName;
            //}
            //string b = a;
            
            return JsonConvert.SerializeObject(result);
        }
        [System.Web.Http.HttpGet]
        public string Get(int id, string Getcandiates)
        {
            var result = service.candidates(id);
            //string a = "";
            //foreach (SemesterExam_User item in result)
            //{
            //     a = item.SemesterExam.SemesterName;
            //}
            //string b = a;

            return JsonConvert.SerializeObject(result);
        }
        [System.Web.Http.HttpDelete]
        public string Delete(int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result);
        }
        //DELETE BY ID
        [System.Web.Http.HttpDelete]
        public string DeleteByUserId([FromUri]int userId, [FromUri]int semesterid)
        {
            var result = service.DeleteUserInSemester(userId, semesterid);
            return JsonConvert.SerializeObject(result);
        }
        //DELETE ALL USER
        [System.Web.Http.HttpDelete]
        public string DeleteAllUser([FromUri] int id)
        {
            var result = service.DeleteAllUserInSemester(id);
            return JsonConvert.SerializeObject(result);
        }
        [System.Web.Http.HttpGet]
        public string Search(string searchString, int id, int type)
        {
            var result = service.Search(searchString, id, type).ToList();
            return JsonConvert.SerializeObject(result);
        }
        [System.Web.Http.HttpPost]
        public string Get([FromUri]string action, [FromBody] object value, int id)
        {
            if (value != null)
            {
                if ("filter".Equals(action))
                {
                    try
                    {

                        var filterObject = JsonConvert.DeserializeObject<Candidates>(value.ToString());
                        return JsonConvert.SerializeObject(service.Filter(filterObject));
                    }
                    catch (Exception)
                    {
                        return "Object fillter not convert valid";
                    }
                }
            }
            var result = service.candidates(id);
            return JsonConvert.SerializeObject(result);

        }

    }
}