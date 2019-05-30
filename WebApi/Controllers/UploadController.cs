using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        public string UploadCkeditor()
        {
            dynamic result = new ExpandoObject();
            try
            {
                HttpPostedFileBase file;
                if (Request.Files.Count > 0)
                {
                    file = Request.Files[0];

                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string fileExtension = _FileName.Split('.').Last();
                        var supportExtensions = System.Configuration.ConfigurationManager.AppSettings["MediaExtensionsSupport"];
                        if (supportExtensions.Contains(fileExtension))
                        {
                            var url = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
                            var folder = System.Configuration.ConfigurationManager.AppSettings["MediaUploadFolder"];
                            string _path = Path.Combine(Server.MapPath(folder), _FileName);
                            file.SaveAs(_path);
                            result.uploaded = "true";
                            result.url = url + folder + _FileName;
                            return JsonConvert.SerializeObject(result);
                        }
                    }
                }
                result.uploaded = "false";
                result.url = "";
                return JsonConvert.SerializeObject(result);
            }
            catch
            {
                result.uploaded = "false";
                result.url = "";
                return JsonConvert.SerializeObject(result);
            }
        }
    }
}