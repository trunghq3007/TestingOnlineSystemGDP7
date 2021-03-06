﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Services;
using Model;
using System.Web.Http.Cors;
using Model.ViewModel;
using WebApi.Commons;

namespace WebApi.Controllers
{
	[AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExamController : ApiController
    {
		private ExamServices services;

		public ExamController()
		{
			services = new ExamServices();
		}
	
         [HttpGet]
        [ValidateSSID(ActionId = 5)]
        public string Getall()
        {
            ResultObject resultt = new ResultObject();
            try
            {
                var result = services.GetAll().ToList();
                return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
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

		[HttpGet]
        [ValidateSSID(ActionId = 10)]
		public string GetExam(int id)
		{
            ResultObject resultt = new ResultObject();

            try
            {
                resultt.Data = services.GetDetailExams(id);
                
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
		[HttpPost]
        [ValidateSSID(ActionId = 5)]
        public string Filter([FromUri]string action, [FromBody]object value)
		{
            ResultObject resultt = new ResultObject();
            try
            {
                if (value != null)
                {

                    if ("filter".Equals(action))
                    {
                        try
                        {
                            var filterObject = JsonConvert.DeserializeObject<ExamFilterModel>(value.ToString());
                            return JsonConvert.SerializeObject(services.Filter(filterObject), Formatting.Indented, new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });

                        }
                        catch (Exception ex)
                        {
                            return "Object fillter not convert valid";
                        }
                    }

                }
                if ("getfilter".Equals(action))
                {
                    return JsonConvert.SerializeObject(services.listFilters());
                }
               
                var result = services.GetAll().ToList();

                return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
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
		[HttpPost]
        [ValidateSSID(ActionId = 7)]
        public string InsertExam([FromBody] object value)
		{
            ResultObject resultt = new ResultObject();

            try
            {
                if (value != null && value.ToString().Count() > 0)
                {
                    var exam = JsonConvert.DeserializeObject<Exam>(value.ToString());
                    var result = services.Insert(exam);
                    return JsonConvert.SerializeObject(result);

                }
                return "FALSE";
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }
			
		}
		[HttpPut]
        [ValidateSSID(ActionId = 6)]
        public string Update(int id, [FromBody]object value)
		{
            ResultObject resultt = new ResultObject();
            try
            {
                if (value != null && value.ToString().Count() > 0)
                {
                    var exam = JsonConvert.DeserializeObject<Exam>(value.ToString());
                    exam.Id = id;
                    var result = services.Update(exam);
                    return JsonConvert.SerializeObject(result);
                }
                return "FALSE";
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }
            
		}
		[HttpGet]
        [ValidateSSID(ActionId = 5)]
        public string Get(string searchString)
		{
            ResultObject resultt = new ResultObject();

            try
            {
                var result = services.Search(searchString).ToList();
                return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
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
		[HttpDelete]
        [ValidateSSID(ActionId = 8)]
        public string Delete(int id)
		{
            ResultObject resultt = new ResultObject();

            try
            {
                var result = services.Delete(id);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }
            
		}


		[System.Web.Http.HttpGet]
        [ValidateSSID(ActionId = 9)]

        public string Export_Exam(int id, [FromUri] string action)
		{
            ResultObject resultt = new ResultObject();

            if ("exportExam".Equals(action))
            {
                try
                {
                    resultt.Message = services.Export_exam(id);
                    return JsonConvert.SerializeObject(resultt);
                }
                catch (Exception e)
                {
                    resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                    return JsonConvert.SerializeObject(resultt);
                }

            }
            return "false";
        }
        [HttpGet]
        [ValidateSSID(ActionId = 6)]
        public string Get(int idExam)
        {
            return services.GetCategoryName(idExam);
        }
	}
}
