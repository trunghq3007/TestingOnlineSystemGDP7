using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;
using Model;
using Newtonsoft.Json;
using System.Web.Http;
using System.Dynamic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Web.Http.Cors;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Net;
using WebApi.Commons;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuestionController : ApiController
    {
        private JsonSerializerSettings jsonSetting;
        private QuestionServices service;

        public QuestionController()
        {
            service = new QuestionServices();
            jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        [HttpPost]
        [ValidateSSID(ActionId = 16)]
        public string Post([FromUri]string action, [FromBody]object value)
        {

            ResultObject result = new ResultObject();
            if (value == null)
            {
                result.Message = "Data null";
                return JsonConvert.SerializeObject(result);
            }

            try
            {
                if ("search".Equals(action))
                {
                    var searchObj = JsonConvert.DeserializeObject<SearchPaging>(value.ToString());
                    result.Data = service.Search(searchObj);
                    if (result.Data != null) result.Success = 1;
                    return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
                }
                if ("fillter".Equals(action))
                {
                    var filterObject = JsonConvert.DeserializeObject<QuestionFillterModel>(value.ToString());
                    result.Data = service.Filter(filterObject);
                    if (result.Data != null) result.Success = 1;
                    return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
                }
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
            result.Message = "Action not allow";
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        
        [ValidateSSID(ActionId = 16)]
        public string GetAll()
        {

            ResultObject result = new ResultObject();
            try
            {
                var list = service.GetAll().ToList();
                result.Data = list;
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                result.Data = null;
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpGet]
       [ValidateSSID(ActionId = 18)]
        public string GetById(int id)
        {

            ResultObject result = new ResultObject();
            try
            {
                result.Data = service.GetById(id);
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                result.Data = null;
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpPost]
        [ValidateSSID(ActionId = 17)]
        public string Post([FromBody]object value)
        {

            ResultObject result = new ResultObject();
            try
            {
                if (value != null)
                {
                    var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                    result.Success = service.Insert(question);
                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    result.Message = "Null content";
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                result.Data = null;
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpPut]
        [ValidateSSID(ActionId = 18)]
        public string Put(int id, [FromBody]object value)
        {
            ResultObject result = new ResultObject();
            if (value != null)
            {
                try
                {
                    var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                    question.Id = id;
                    string userName = HttpContext.Current.Session["UserName"] as string;
                    result.Success = service.Update(question);
                    return JsonConvert.SerializeObject(result);
                }
                catch (Exception e)
                {
                    result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                    result.Data = null;
                    return JsonConvert.SerializeObject(result);
                }

            }
            result.Message = "Null content";
            return JsonConvert.SerializeObject(result);
        }


        [HttpDelete]
        [ValidateSSID(ActionId = 19)]
        public string Put(int id)
        {
            ResultObject result = new ResultObject();
            try
            {
                result.Success = service.Delete(id);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                result.Data = null;
                return JsonConvert.SerializeObject(result);
            }


        }




    }
}

