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

namespace WebApi.Controllers
{
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
        public string Get([FromUri] string action, [FromUri] int id)
        {
            if ("GetAll".Contains(action))
            {
                var result = examQuestion.GetById(id).ToList();
                return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }

            if ("GetById".Contains(action))
            {
                var result = examQuestion.GetListQuestionById(id);
                return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }

            return "true";

        }
       
		[HttpPost]
		public string Post([FromBody] object value)
		{
			if (value != null)
			{
				var exam = JsonConvert.DeserializeObject<ExamQuestion>(value.ToString());
				var result = examQuestion.Insert(exam);
				return JsonConvert.SerializeObject(result);

			}
			return "FALSE";
		}
        [HttpDelete]
        public string Delete(int id)
        {
            ResultObject resultt = new ResultObject();

            try
            {
                var result = examQuestion.Delete(id);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }

        }


        [HttpPost]
		public string Getfilter([FromUri]string action, [FromBody]object value)
		{
			if (value != null)
			{

				if ("fillter".Equals(action))
				{
					try
					{
						var filterObject = JsonConvert.DeserializeObject<QuestionFillterModel>(value.ToString());
						return JsonConvert.SerializeObject(QuestionServices.Filter(filterObject), Formatting.Indented, new JsonSerializerSettings
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

            if ("AddMutiple".Equals(action))
            {
                var exam = JsonConvert.DeserializeObject<List<ExamQuestion>>(value.ToString());
                var add = examQuestion.AddMutipleQuestion(exam);
                return JsonConvert.SerializeObject(add);
            }

            if ("random".Equals(action))
            {
                var exam = JsonConvert.DeserializeObject<ViewQuestionExam>(value.ToString());
                var add = examQuestion.RandomQuestion(exam);
                return JsonConvert.SerializeObject(add);
            }
			if ("getfillter".Equals(action))
			{
                return JsonConvert.SerializeObject(examQuestion.listFilters());
                //return "";


            }
			var result = QuestionServices.GetAll().ToList();
			return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}
	
	}
}
