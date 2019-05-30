using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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
		// GET: Group
		[HttpGet]
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
