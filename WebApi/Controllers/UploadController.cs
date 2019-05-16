using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;
using Model;
using Newtonsoft.Json;
using System.Web.Http;
using System.IO;
using System.Web.Mvc;
using System.Dynamic;

namespace WebApi.Controllers
{
    [AllowCrossSite]
    public class UploadController : Controller
    {





    public UploadController()
        {
        }
        public ActionResult Index()
        {
            string paths = Server.MapPath("~/UploadFiles/image.png");
            string fullPath = Path.Combine(paths);
            return View();
        }

        public string UploadSingle()
        {
            string result = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);
                        result += "http://localhost:65170" + "/UploadedFiles/" + _FileName;
                    }
                    
                }
                catch
                {
                }
            }
            return result;
        }
      
        public string UploadMuitiple()
    {
        try
        {
                foreach (HttpPostedFile postedFile in Request.Files)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
                }
            return "Content null";
        }
        catch
        {
            return "ERROR";
        }
    }

        public string ImageUpload()
        {
            dynamic result = new ExpandoObject();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName); 
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);
                        result.uploaded = "true";
                        result.url = "http://localhost:65170" + "/UploadedFiles/" + _FileName;
                    }
                    return JsonConvert.SerializeObject(result);
                }
                catch
                {
                }
            }
            result.uploaded = "false";
            result.url = "";
            return JsonConvert.SerializeObject(result);
        }
}
}
