using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Model;
using Model.ViewModel;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Commons;

namespace WebApi.Controllers
{
	[AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExamQuestionsController : ApiController
	{
		private ExamQuestionServices examQuestion;
		private QuestionServices QuestionServices;
		public ExamQuestionsController()
		{
			QuestionServices = new QuestionServices();
			examQuestion = new ExamQuestionServices();
		}

        [HttpGet]
        [ValidateSSID(ActionId =74 )]
        public string Search(string searchString)
        {
            ResultObject resultt = new ResultObject();

            try
            {
                var result = examQuestion.Search(searchString);
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
        [ValidateSSID(ActionId = 74)]
        public string Get([FromUri] string action, [FromUri] int id)
        {

            if ("GetAll".Contains(action))
            {
                ResultObject resultt = new ResultObject();

                try
                {
                    var result = examQuestion.GetById(id).ToList();
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

            if ("GetById".Contains(action))
            {
                ResultObject resultt = new ResultObject();
                try
                {
                    var result = examQuestion.GetListQuestionById(id);
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

            if ("getfillter".Equals(action))
            {
                return JsonConvert.SerializeObject(examQuestion.listFilters());
            }

            return "true";

        }



        [HttpPost]
        [ValidateSSID(ActionId = 74)]
        public string Filter([FromBody] object value)
        {
            try
            {
                var filterObject = JsonConvert.DeserializeObject<ViewQuestionExam>(value.ToString());
                return JsonConvert.SerializeObject(examQuestion.Filter(filterObject), Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            }
            catch (Exception ex)
            {

                return "Object fillter not convert valid";
            }
        }
        
        [HttpPost]
        [ValidateSSID(ActionId = 75)]
        public string Post([FromUri]string action, [FromBody]object value)
		{
			

            if ("AddMutiple".Equals(action))
            {
                ResultObject resultt = new ResultObject();
                try
                {
                    var exam = JsonConvert.DeserializeObject<List<ExamQuestion>>(value.ToString());
                    var add = examQuestion.AddMutipleQuestion(exam);
                    return JsonConvert.SerializeObject(add);
                }
                catch (Exception e)
                {
                    resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                    return JsonConvert.SerializeObject(resultt);
                }
               
            }

            if ("DeleteMutiple".Equals(action))
            {
                ResultObject resultt = new ResultObject();

                try
                {
                    var exam = JsonConvert.DeserializeObject<List<ExamQuestion>>(value.ToString());
                    var delete = examQuestion.DeleteMutiple(exam);
                    return JsonConvert.SerializeObject(delete);
                }
                catch (Exception e)
                {
                    resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                    return JsonConvert.SerializeObject(resultt);
                }
               
            }
            if ("random".Equals(action))
            {
                ResultObject resultt = new ResultObject();

                try
                {
                    var exam = JsonConvert.DeserializeObject<ViewQuestionExam>(value.ToString());
                    var add = examQuestion.RandomQuestion(exam);
                    return JsonConvert.SerializeObject(add);
                }
                catch (Exception e)
                {
                    resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                    return JsonConvert.SerializeObject(resultt);
                }
               
            }
			
			var result = QuestionServices.GetAll().ToList();
			return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}
	
	}
}
