using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Commons;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SemasterController : ApiController
    {
		private SemasterServices services;

		public SemasterController()
		{
			services = new SemasterServices();
		}
		// GET: 
		[HttpGet]
        [ValidateSSID(ActionId = 27)]
        public string Get()
		{
			var result = services.GetAll().ToList();
            return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
	}
}
