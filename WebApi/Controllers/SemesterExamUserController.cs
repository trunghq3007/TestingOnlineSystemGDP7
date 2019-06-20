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
using WebApi.Commons;

namespace WebApi.Controllers
{
    public class SemesterExamUserController : ApiController
    {
        private SemesterExamUserServices service;
        //DELETE


        // GET: SemesterExamUser
        public SemesterExamUserController()
        {
            service = new SemesterExamUserServices();
        }
        [System.Web.Http.HttpGet]
        [ValidateSSID(ActionId = 46)]
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

        //-----------------------------DELETE-------------------------------------------------------
        [System.Web.Http.HttpDelete]
        [ValidateSSID(ActionId = 47)]
        public string Delete(int id, [FromUri]int semesterid)
        {
            var result = service.DeleteCandidates(id, semesterid);
            return JsonConvert.SerializeObject(result);
        }
        //-----------------------------DELETE-------------------------------------------------------


        //-----------------------------Compare-SemesterId-----------------------------------------
        [System.Web.Http.HttpGet]
        [ValidateSSID(ActionId = 48)]
        public string GetUserOutSemester(int semesterid)
        {
            var result = service.GetUserOutSemester(semesterid);
            return JsonConvert.SerializeObject(result);
        }
        //-----------------------------Compare-SemesterId-----------------------------------------

        //------------------------------------------INSERT------------------------------------------
        [System.Web.Http.HttpPost]
        [ValidateSSID(ActionId = 49)]
        public string InsertUserGroup(int userid, int semesterid)
        {
            var result = service.InsertCandidates(userid, semesterid);
            return JsonConvert.SerializeObject(result);
        }
        //------------------------------------------INSERT------------------------------------------

        [System.Web.Http.HttpGet]
        [ValidateSSID(ActionId = 46)]
        public string Get(string searchString, int id, int type)
        {
            var result = service.Search(searchString, id, type).ToList();
            return JsonConvert.SerializeObject(result);
        }

        [System.Web.Http.HttpPost]
        [ValidateSSID(ActionId = 46)]
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