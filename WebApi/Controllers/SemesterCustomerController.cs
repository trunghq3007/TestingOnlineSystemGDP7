using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;
using Newtonsoft.Json;
using Services;
using WebApi.Commons;
using RouteAttribute = System.Web.Http.RouteAttribute;
namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
	[AllowAnonymous]
    public class SemesterCustomerController : ApiController
    {
        private SemesterCustomerSevice service;
        public SemesterCustomerController()
        {
            service = new SemesterCustomerSevice();
        }
        private ExamServices services;
        

        [HttpGet]
        [ValidateSSID(ActionId = 28)]
        public string Index()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                result.Data = service.getAll();
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);

            }
            catch (Exception E)
            {
                result.Message = "EXCEPTION " + E.Message + "Stack" + E.StackTrace;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }

        }
        [HttpGet]
        [ValidateSSID(ActionId = 29)]
        public string getExamById(int id)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                result.Data = service.getListExam(id).ToList();
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);

            }
            catch (Exception E)
            {
                result.Message = "EXCEPTION " + E.Message + "Stack" + E.StackTrace;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }

        }
        [Route("ExamDetails/{id}")]
        [HttpGet]
        [ValidateSSID(ActionId = 30)]
        public string getExam(int id)
        {
             var result = service.getDetailExam(id);
                return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
        [HttpGet]
        [ValidateSSID(ActionId = 31)]
        public string Get(string code)
        {
            ResultObject resultt = new ResultObject();

            try
            {
                 
                 resultt.Data = service.SeachCode(code).ToList();
                return JsonConvert.SerializeObject(resultt, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }

        }
    }
}