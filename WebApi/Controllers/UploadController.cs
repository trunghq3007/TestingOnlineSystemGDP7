using Model;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Services;
using Simple.ImageResizer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using WebApi.Commons;

namespace WebApi.Controllers
{
    [AllowCrossSite]
    public class UploadController : Controller
    {
        public UploadController()
        {
            service = new QuestionServices();
        }
        private QuestionServices service;

        /// <summary>
        /// Allow upload image, using for CKEDITOR
        /// </summary>
        /// <returns>object containt uploaded save status(true,false) and url image if susscess </returns>
        [ValidateSSID(ActionId = 59)]
        public string UploadCkeditor()
        {
            dynamic result = new ExpandoObject();
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _fileNotExtension = _FileName.Split('.').First() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        string fileExtension = _FileName.Split('.').Last();
                        _FileName = _fileNotExtension + "." + fileExtension;
                        var supportExtensions = ConfigurationManager.AppSettings.Get("MediaExtensionsSupport");
                        if (supportExtensions.Contains(fileExtension.ToLower()))
                        {
                            var url = (Request.Url.GetLeftPart(UriPartial.Authority));
                            var _pathForder = ConfigurationManager.AppSettings["ImagesStorePath"];
                            //var _pathForder = Server.MapPath(folder);
                            string _path = Path.Combine(_pathForder, _FileName);
                            if (!Directory.Exists(_pathForder))
                            {
                                Directory.CreateDirectory(_pathForder);
                            }
                            file.SaveAs(_path);
                            result.uploaded = "true";
                            result.url = url + "/images/" + _FileName;
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

        /// <summary>
        /// Import Question from client
        /// </summary>
        /// <returns>json string of ResultObject</returns>
		[ValidateSSID(ActionId = 60)]
        public string ImportQuestion()
        {
            var result = new ResultObject();
            try
            {
                string _tempUploadFolder = ConfigurationManager.AppSettings["MediaTempUploadFolder"];
                string _storeFolder = ConfigurationManager.AppSettings["ImagesStorePath"];
                if (HttpContext.Request.Files.Count < 1)
                {
                    result.Message = "Not file upload";
                    return JsonConvert.SerializeObject(result);
                }
                HttpPostedFileBase file = HttpContext.Request.Files[0];
                if (file.ContentLength <= 0)
                {
                    result.Message = "content file null";
                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    result = importZip(file, _tempUploadFolder,_storeFolder);

                    return JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception e)
            {
                result.Success = -1;
                result.Message = e.Message;
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// Export Question from client
        /// </summary>
        /// <returns>json string of ResultObject containt file name save in server</returns>
		[ValidateSSID(ActionId = 61)]
        public string ExportQuestion()
        {
            if (("OPTIONS").Equals(HttpContext.Request.HttpMethod))
            {
                Response.StatusCode = 200;
                return "";
            }
            var result = new ResultObject();
            try
            {
                string _tempUploadFolder = ConfigurationManager.AppSettings["MediaTempUploadFolder"];
                string _storeFolder = ConfigurationManager.AppSettings["ImagesStorePath"];
                string fileName = "export_"+"" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string _tempStoreFolder = Server.MapPath(_tempUploadFolder + "/" + fileName);
                if (!Directory.Exists(_tempStoreFolder)) Directory.CreateDirectory(_tempStoreFolder);
                var question = service.GetAll().ToList();
                result = Export(question, _storeFolder, _tempStoreFolder + "/");
                ZipFile.CreateFromDirectory(_tempStoreFolder + "/", _tempStoreFolder + ".zip", CompressionLevel.Optimal, false);
                DeleteDirectory(_tempStoreFolder);
                result.Message = fileName + ".zip";
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                result.Success = -1;
                result.Message = e.Message;
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// Using download file export from server
        /// </summary>
        /// <param name="fileName">file name need to download</param>
        /// <returns>a File if found</returns>
		[ValidateSSID(ActionId = 62)]
        public ActionResult DownloadFileExport(string fileName)
        {
            //get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["MediaTempUploadFolder"]), fileName);
            if (!System.IO.File.Exists(fullPath))
            {
                //Response.StatusCode = 404;
                return null;
            }

            //return the file for download
            return File(fullPath, "application/zip", fileName);
        }

        /// <summary>
        /// Action get Link image show in HTML code
        /// </summary>
        /// <param name="fileName">file image name</param>
        /// <returns>File image if founded</returns>
        [Route("images/{fileName}")]
        public async Task<ActionResult> Images(string fileName)
        {
            var a = Request.Url;

            string fullPath = Path.Combine(ConfigurationManager.AppSettings["ImagesStorePath"], fileName);
            if (!System.IO.File.Exists(fullPath))
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }


        }














        /// <summary>
        /// remove file
        /// </summary>
        /// <param name="_path">path file</param>
        private void ClearFile(string _path)
        {
            if (System.IO.File.Exists(_path))
            {
                System.IO.File.Delete(_path);
            }
        }
        /// <summary>
        /// Get Value String from Cell
        /// </summary>
        /// <param name="cell">Cell contain data</param>
        /// <returns>string value in cell, return empty string if cell null or type not string</returns>
        private string GetValueCell(ICell cell)
        {
            if (cell != null && CellType.String == cell.CellType)
            {
                return cell.StringCellValue;
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Fill question to row excel
        /// </summary>
        /// <param name="row">Row containt question data</param>
        /// <param name="a">answer object</param>
        /// <returns>Row excel include question data</returns>
        private IRow FilltoRow(IRow row, Question q)
        {
            row.CreateCell(0);
            row.CreateCell(1);
            row.CreateCell(2);
            row.CreateCell(3);
            row.CreateCell(4);
            row.CreateCell(5);
            row.CreateCell(6);
            row.CreateCell(8);
            row.GetCell(0).SetCellValue("1");
            row.GetCell(1).SetCellValue(q.Content);
            row.GetCell(2).SetCellValue(q.Level.ToString());
            row.GetCell(3).SetCellValue(q.Type.ToString());
            row.GetCell(4).SetCellValue(q.Status.ToString());
            row.GetCell(5).SetCellValue(q.Category == null ? "" : q.Category.Name);
            row.GetCell(6).SetCellValue(q.Suggestion);
            row.GetCell(8).SetCellValue(q.Id);
            return row;
        }

        /// <summary>
        /// Fill answer to row excel
        /// </summary>
        /// <param name="row">Row containt answer data</param>
        /// <param name="a">answer object</param>
        /// <returns>Row excel include answer data</returns>
        private IRow FilltoRow(IRow row, Answer a)
        {
            row.CreateCell(0);
            row.CreateCell(1);
            row.CreateCell(4);
            row.CreateCell(7);
            row.GetCell(0).SetCellValue("2");
            row.GetCell(1).SetCellValue(a.Content);
            row.GetCell(4).SetCellValue(a.Status.ToString());
            row.GetCell(7).SetCellValue(a.IsTrue ? "1" : "0");
            return row;
        }
        /// <summary>
        /// export list question 
        /// </summary>
        /// <param name="questions">List question to export</param>
        /// <param name="_localStoreFolder">folder store image</param>
        /// <param name="_tempStoreFolder">temp store file zip</param>
        /// <returns></returns>
        private ResultObject Export(List<Question> questions, string _localStoreFolder, string _tempStoreFolder)
        {
            var result = new ResultObject();
            try
            {
                var workbook = new HSSFWorkbook();
                var sheet = workbook.CreateSheet("Data");
                var headerRow = sheet.CreateRow(0);
                // fill header
                var headers = new[] { "Type", "Content", "Level", "Type Question", "Status", "Category Name", "Suggestion", "Is true" };
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = headerRow.CreateCell(i);
                    cell.SetCellValue(headers[i]);
                }
                //Below loop is fill content  
                var rowIndex = 1;
                string imagePath = Path.Combine(_tempStoreFolder + "Images/");
                if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);
                for (int i = 0; i < questions.Count; i++)
                {
                    AcceptFile(questions[i].Content, _localStoreFolder, imagePath);
                    var row = sheet.CreateRow(rowIndex);
                    FilltoRow(row, questions[i]);
                    rowIndex++;
                    if (questions[i].Answers != null)
                    {
                        foreach (var item in questions[i].Answers)
                        {
                            var rowA = sheet.CreateRow(rowIndex);
                            FilltoRow(rowA, item);
                            rowIndex++;
                        }
                    }
                }
                var finishRow = sheet.CreateRow(rowIndex);
                finishRow.CreateCell(0).SetCellValue("END");
                string fileName = "Questions.xls";
                string FilePath = Path.Combine(_tempStoreFolder, fileName);
                FileStream zipToOpen = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
                var stream = new MemoryStream();
                workbook.Write(stream);
                stream.WriteTo(zipToOpen);
                zipToOpen.Close();
                stream.Close();
                result.Success = 1;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        /// <summary>
        /// accept file from content question in src img tag. Move file from srcFolder to destFolder
        /// </summary>
        /// <param name="content">Content Question with html code</param>
        /// <param name="srcFolder">folder include image file</param>
        /// <param name="destFolder">folder save image file</param>
        private void AcceptFile(string content, string srcFolder, string destFolder)
        {
            try
            {

                if (!Directory.Exists(srcFolder))
                {
                    throw new FileNotFoundException("Thư mục chứa ảnh không tồn tại", srcFolder);
                }
                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                }
                var files = GetFileName(content);

                foreach (var fileName in files)
                {
                    if (System.IO.File.Exists(srcFolder + fileName))
                    {
                        if (System.IO.File.Exists(destFolder + fileName)) System.IO.File.Delete(destFolder + fileName);
                        System.IO.File.Copy(srcFolder + fileName, destFolder + fileName);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// accept file from content question in src img tag. Move file from srcFolder to destFolder
        /// </summary>
        /// <param name="content">Content Question with html code</param>
        /// <param name="srcFolder">folder include image file</param>
        /// <param name="destFolder">folder save image file</param>
        /// <param name="w">with image when resize</param>
        /// <param name="h">height image when resize</param>
        /// <param name="encoding">image encoding</param>
        private void AcceptFile(string content, string srcFolder, string destFolder, int w, int h, ImageEncoding encoding)
        {
            try
            {
                if (w <= 0 || h <= 0) AcceptFile(content, srcFolder, destFolder);

                if (!Directory.Exists(srcFolder))
                {
                    throw new FileNotFoundException("Thư mục chứa ảnh không tồn tại", srcFolder);
                }
                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                }
                var files = GetFileName(content);

                foreach (var fileName in files)
                {
                    if (System.IO.File.Exists(srcFolder + fileName))
                    {
                        if (System.IO.File.Exists(destFolder + fileName)) System.IO.File.Delete(destFolder + fileName);
                        System.IO.File.Copy(srcFolder + fileName, destFolder + fileName);
                        var resize = new ImageResizer(destFolder + fileName);
                        resize.Resize(w, h, encoding);
                        resize.SaveToFile(destFolder + fileName);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Get file name in src img tag from string Htmlcode, with media extension config in webconfig key="MediaExtensionsSupport"
        /// </summary>
        /// <param name="content">string html code</param>
        /// <returns>List string file name pass</returns>
        private List<string> GetFileName(string content)
        {
            string acceptExtension = ConfigurationManager.AppSettings["MediaExtensionsSupport"];
            List<string> result = new List<string>();
            if (acceptExtension == null || content == null) return result;

            Regex reg = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>");
            foreach (Match m in reg.Matches(content))
            {
                if (m.Value != null)
                {
                    var fileName = m.Value.Split('/').Last().Split('\"').First();
                    var extension = fileName.Split('.').Last();
                    if (acceptExtension.Contains(extension)) result.Add(fileName);
                }
            }
            return result;
        }
        /// <summary>
        /// import question from zip file
        /// </summary>
        /// <param name="file">file upload</param>
        /// <param name="_tempUploadFolder">temp folder save file to unzip</param>
        /// <param name="_storePath">folder store when complete</param>
        /// <returns>Result object</returns>
        private ResultObject importZip(HttpPostedFileBase file, string _tempUploadFolder, string _storePath)
        {
            try
            {
                var result = new ResultObject { Success = -1 };

                string _FileName = Path.GetFileName(file.FileName);
                string zipPath = Path.Combine(HttpContext.Server.MapPath(_tempUploadFolder), _FileName);
                string _tempZipPath = Path.Combine(HttpContext.Server.MapPath(_tempUploadFolder + "/_temp" + DateTime.Now.ToString("yyyyMMddHHmmss")));
                string _pathExcel = Path.Combine(_tempZipPath + "\\" + ConfigurationManager.AppSettings["ExcelUploadName"]);
                string _tempImagepath = Path.Combine(_tempZipPath + ConfigurationManager.AppSettings["ImageUploadPath"]);
                if (!Directory.Exists(HttpContext.Server.MapPath(_tempUploadFolder)))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath(_tempUploadFolder));
                }
                // save file zip to zip path
                file.SaveAs(zipPath);

                if (!".zip".Equals(Path.GetExtension(zipPath).ToLower()))
                {
                    result.Message = "Upload file is not zip format";
                    return result;
                }

                // extract to temp zip path
                ZipFile.ExtractToDirectory(zipPath, _tempZipPath);
                if (System.IO.File.Exists(_pathExcel + ".xls"))
                {
                    _pathExcel += ".xls";
                }
                else if (System.IO.File.Exists(_pathExcel + ".xlsx"))
                {
                    _pathExcel += ".xls";
                }
                else
                {
                    result.Message = "File excel NOT FOUND!!!!! Question.xls or Question.xlsx NOT FOUND!";
                    return result;
                }
                if (!Directory.Exists(_tempImagepath))
                {
                    result.Message = "Folder " + _tempImagepath + " NOT FOUND!";
                    return result;
                }

                result = ImportExcel(_pathExcel, _tempImagepath, _storePath);
                DeleteDirectory(_tempZipPath);
                if (System.IO.File.Exists(zipPath)) System.IO.File.Delete(zipPath);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// import question from excel file
        /// </summary>
        /// <param name="_path">path file excel</param>
        /// <param name="_tempUploadFolder">temp folder save file to unzip</param>
        /// <param name="_storePath">folder store when complete</param>
        /// <returns>Result object</returns>
        private ResultObject ImportExcel(string _path, string _tempUploadFolder, string _storeFolder)
        {

            try
            {
                var result = new ResultObject { Success = -1 };

                List<Question> listFromFiles = new List<Question>();

                string extension = Path.GetExtension(_path);

                IWorkbook workbook = null;
                using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
                {
                    if (extension == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (extension == ".xls")
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                }
                ISheet sheet = workbook.GetSheetAt(0);
                Question ques = null;
                string FILEERROR = "";
                int questionRow = 0;

                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    var currentRow = sheet.GetRow(rowIndex);
                    string flag = GetValueCell(currentRow.GetCell(0));
                    if ("".Equals(flag) || "END".Equals(flag.ToUpper()))
                    {
                        if (ValidateQuestion(ques, questionRow, ref FILEERROR)) listFromFiles.Add(ques);
                        break;
                    }
                    if ("1".Equals(flag))
                    {
                        if (ValidateQuestion(ques, questionRow, ref FILEERROR)) listFromFiles.Add(ques);
                        ques = GetQuestionFromRow(currentRow, rowIndex, ref FILEERROR);

                    }
                    if ("2".Equals(flag))
                    {
                        var ans = GetAnswerFromRow(currentRow, rowIndex, ref FILEERROR);
                        if (ans != null) ques.Answers.Add(ans);
                    }
                }
                if ("".Equals(FILEERROR))
                {
                    foreach (var question in listFromFiles)
                    {

                        AcceptFile(question.Content, _tempUploadFolder, _storeFolder);
                    }
                    result.Success = service.Import(listFromFiles);
                    ClearFile(_path);
                    return result;
                }
                else
                {
                    ClearFile(_path);
                    result.Message = FILEERROR;
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// get question from row excel
        /// </summary>
        /// <param name="currentRow">IRow include question data</param>
        /// <param name="rowIndex">row index in sheet</param>
        /// <param name="errMesage">ref error message if throw</param>
        /// <returns>Question from row</returns>
        private Question GetQuestionFromRow(IRow currentRow, int rowIndex, ref string errMesage)
        {
            string err = "";
            string content = GetValueCell(currentRow.GetCell(1));
            if (content == null || "".Equals(content)) err += "Content is null";
            var leveIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(2)), out int level);
            var typeIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(3)), out int type);
            var statusIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(4)), out int status);
            var idIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(8)), out int idQuestion);
            string categoryName = GetValueCell(currentRow.GetCell(5));
            string suggestion = GetValueCell(currentRow.GetCell(6));
            var cate = service.getCategoryByName(categoryName);
            var category = cate ?? new Category { Name = categoryName };
            if (!leveIsNumber) err += " Level is not number";
            if (!typeIsNumber) err += " TypeQuestion is not number";
            if (!statusIsNumber) err += " Status is not number,";
            if ("".Equals(err))
            {
                return new Question
                {
                    Id = idQuestion,
                    Content = RemoveXSS(content),
                    Level = level,
                    Type = type,
                    Status = status,
                    Category = category,
                    Suggestion = suggestion,
                    Answers = new List<Answer>(),
                    CreatedBy = "anonymous user import",
                    CreatedDate = DateTime.Now
                };
            }
            else
            {
                errMesage = "Row " + rowIndex + ": " + err + "\n";
                return null;
            }
        }
        /// <summary>
        /// get answer from row excel
        /// </summary>
        /// <param name="currentRow">IRow include answer data</param>
        /// <param name="rowIndex">row index in sheet mark to error message</param>
        /// <param name="errMesage">ref error message if throw</param>
        /// <returns>answer from row</returns>
        private Answer GetAnswerFromRow(IRow currentRow, int rowIndex, ref string errMesage)
        {
            string err = "";
            string content = GetValueCell(currentRow.GetCell(1));
            if (content == null || "".Equals(content)) err += "Content is null";
            var statusIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(4)), out int status);
            var isTrue = "1".Equals(GetValueCell(currentRow.GetCell(7))) ? true : false;
            if (!statusIsNumber) err += " Status is not number,";
            if ("".Equals(err))
            {
                return new Answer
                {
                    Content = RemoveXSS(content),
                    Status = status,
                    CreatedBy = "anonymous user import",
                    CreatedDate = DateTime.Now,
                    IsTrue = isTrue
                };
            }
            else
            {
                errMesage = "Row " + rowIndex + ": " + err + "\n";
                return null;
            }
        }
        /// <summary>
        /// validate question
        /// </summary>
        /// <param name="ques">object question</param>
        /// <param name="rowIndex">row index in sheet mark to error message</param>
        /// <param name="errMesage">ref error message if throw</param>
        /// <returns>true if has least one answer is true, false if no answer is true</returns>
        private bool ValidateQuestion(Question ques, int rowIndex, ref string err)
        {
            var hasIsTrue = false;
            if (ques != null && ques.Answers != null)
            {
                foreach (var ans in ques.Answers)
                {
                    if (ans.IsTrue == true)
                    {
                        hasIsTrue = true;
                        break;
                    }
                }
                if (!hasIsTrue) err += "Row " + (rowIndex - 1) + " not has true answer";
            }
            return ques != null;
        }

        /// <summary>
        /// delete directory(using with temp folder)
        /// </summary>
        /// <param name="target_dir">directory need delete</param>
        private void DeleteDirectory(string target_dir)
        {
            try
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// remove script tag from html string to block XSS
        /// </summary>
        /// <param name="input">string containt html code</param>
        /// <returns></returns>
        private string RemoveXSS(string input)
        {
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            return rRemScript.Replace(input, "");
        }
    }
}