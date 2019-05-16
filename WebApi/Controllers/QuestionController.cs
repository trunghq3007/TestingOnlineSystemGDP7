using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;
using Model;
using Newtonsoft.Json;
using System.Web.Http;

namespace WebApi.Controllers
{
    [AllowCrossSite]
    public class QuestionController : ApiController
    {
        private QuestionServices service;

        public QuestionController()
        {
            service = new QuestionServices();
        }
        [HttpPost]
        public string Get([FromUri]string action,[FromBody]object value)
        {
            if( value != null)
            {
                if ("search".Equals(action))
                {
                    var searchObj = JsonConvert.DeserializeObject<SearchPaging>(value.ToString());
                    return JsonConvert.SerializeObject(service.Search(searchObj));
                }
                if ("fillter".Equals(action))
                {
                    try
                    {
                        var filterObject = JsonConvert.DeserializeObject<QuestionFillterModel>(value.ToString());
                        return JsonConvert.SerializeObject(service.Filter(filterObject));
                    }catch(Exception)
                    {
                        return "Object fillter not convert valid";
                    }
                }
            }
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result);

        }
        //[HttpGet]
        //public string Get([FromUri]string action, [FromUri]string pageIndex, [FromUri]string pageSize, [FromUri]string search)
        //{
        //    SearchPaging searchItem = new SearchPaging { PageIndex = 1, PageSize = 10 };
        //    if( int.TryParse(pageIndex,out int pageIndexNumber))
        //    {
        //        if(pageIndexNumber>1) searchItem.PageIndex = pageIndexNumber;
        //    }
        //    if (int.TryParse(pageIndex, out int pageSizeNumber))
        //    {
        //        if (pageSizeNumber > 1) searchItem.PageSize = pageSizeNumber;
        //    }
        //    if ("search".Equals(action))
        //        {
                   
        //            return JsonConvert.SerializeObject(service.Search(searchItem));
        //        }
        //        if ("fillter".Equals(action))
        //        {
        //            try
        //            {
        //                var filterObject = JsonConvert.DeserializeObject<QuestionFillterModel>(value.ToString());
        //                return JsonConvert.SerializeObject(service.Filter(filterObject));
        //            }
        //            catch (Exception)
        //            {
        //                return "Object fillter not convert valid";
        //            }
        //        }
        //    }
        //    var result = service.GetAll().ToList();
        //    return JsonConvert.SerializeObject(result);

        //}

        [HttpGet]
        public string Get()
        {
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string Get(int id)
        {
            var result = service.GetById(id);
            return JsonConvert.SerializeObject(result);
        }
        
        [HttpPost]
        public string Post([FromBody]object value)
        {
            if (value != null)
            {
                var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                var result = service.Insert(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [HttpPut]
        public string Put(int id ,[FromBody]object value)
        {
            if (value != null)
            {
                var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                question.Id = id;
                var result = service.Update(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }
        [HttpDelete]
        public string Put(int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result);
        }


    }
}
