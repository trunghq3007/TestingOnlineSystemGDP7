using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
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
        //public string UploadCkeditor(HttpPostedFileBase file)
        //{
        //    dynamic result = new ExpandoObject();
        //    try
        //    {
        //        // HttpPostedFileBase file;
        //        //if (Request.Files.Count > 0)
        //        //{
        //        //    file = Request.Files[0];

        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _fileNotExtension = _FileName.Split('.').First() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
        //            string fileExtension = _FileName.Split('.').Last();
        //            _FileName = _fileNotExtension + "." + fileExtension;
        //            var supportExtensions = ConfigurationManager.AppSettings.Get("MediaExtensionsSupport");
        //            if (supportExtensions.Contains(fileExtension))
        //            {
        //                var url = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
        //                var folder = ConfigurationManager.AppSettings["MediaUploadFolder"];
        //                var _pathForder = Server.MapPath(folder);
        //                string _path = Path.Combine(_pathForder, _FileName);
        //                if (!Directory.Exists(_pathForder))
        //                {
        //                    Directory.CreateDirectory(_pathForder);
        //                }
        //                file.SaveAs(_path);
        //                result.uploaded = "true";
        //                result.url = url + folder + _FileName;
        //                return JsonConvert.SerializeObject(result);
        //            }
        //        }
        //        //}
        //        return JsonConvert.SerializeObject(result);
        //    }
        //    catch (Exception e)
        //    {
        //        result.uploaded = "false";
        //        result.url = e.Message;
        //        return JsonConvert.SerializeObject(result);
        //    }
        //}
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
                        string _fileNotExtension = _FileName.Split('.').First() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        string fileExtension = _FileName.Split('.').Last();
                        _FileName = _fileNotExtension + "." + fileExtension;
                        var supportExtensions = ConfigurationManager.AppSettings.Get("MediaExtensionsSupport");
                        if (supportExtensions.Contains(fileExtension))
                        {
                            var url = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
                            var folder = ConfigurationManager.AppSettings["MediaUploadFolder"];
                            var _pathForder = Server.MapPath(folder);
                            string _path = Path.Combine(_pathForder, _FileName);
                            if (!Directory.Exists(_pathForder))
                            {
                                Directory.CreateDirectory(_pathForder);
                            }
                            file.SaveAs(_path);
                            result.uploaded = "true";
                            result.url = url + folder + _FileName;
                            return JsonConvert.SerializeObject(result);
                        }
                    }
                }
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                result.uploaded = "false";
                result.url = e.Message;
                return JsonConvert.SerializeObject(result);
            }
        }
    }
}