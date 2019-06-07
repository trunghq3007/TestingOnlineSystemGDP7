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

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuestionController : ApiController
    {
        private QuestionServices service;

        public QuestionController()
        {
            service = new QuestionServices();
        }

        [HttpPost]
        public string Post([FromUri]string action, [FromBody]object value)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            if (value == null && !"import".Equals(action.ToLower()))
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
                if ("export".Equals(action.ToLower()))
                {
                    result.Message = Export(service.GetAll().ToList());
                    if (!"".Equals(result.Message)) result.Success = 1;
                    return JsonConvert.SerializeObject(result);
                }
                if ("import".Equals(action.ToLower()))
                {
                   
                    string _tempUploadFolder = ConfigurationManager.AppSettings["MediaTempUploadFolder"];
                    string _storeFolder = ConfigurationManager.AppSettings["MediaUploadFolder"];
                    if (HttpContext.Current.Request.Files.Count < 1)
                    {
                        result.Message = "Not file upload";
                        return JsonConvert.SerializeObject(result);
                    }
                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    if (file.ContentLength <= 0)
                    {
                        result.Message = "content file null";
                        return JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        result = importZip(file, _tempUploadFolder, _storeFolder);
                        return JsonConvert.SerializeObject(result);
                    }
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
        public string Get()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                var list = service.GetAll().ToList();
                result.Data = list;
                GetFileName(list.SingleOrDefault(s => s.Id == 11).Content);
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
        }

        [HttpGet]
        public string Get(int id)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
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
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpPost]
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
                return JsonConvert.SerializeObject(result);
            }
            //  return JsonConvert.SerializeObject(result);
        }

        [HttpPut]
        public string Put(int id, [FromBody]object value)
        {
            ResultObject result = new ResultObject();
            if (value != null)
            {
                try
                {
                    var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                    question.Id = id;
                    result.Success = service.Update(question);
                    return JsonConvert.SerializeObject(result);
                }
                catch (Exception e)
                {
                    result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                    return JsonConvert.SerializeObject(result);
                }

            }
            result.Message = "Null content";
            return JsonConvert.SerializeObject(result);
        }


        [HttpDelete]
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
                return JsonConvert.SerializeObject(result);
            }


        }



        // private method
        private void ClearFile(string _path)
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }

        private string GetValueCell(ICell cell)
        {
            if (cell != null)
            {
                return cell.StringCellValue;
            }
            else
            {
                return "";
            }
        }

        private IRow FilltoRow(IRow row, Question q)
        {
            row.CreateCell(0);
            row.CreateCell(1);
            row.CreateCell(2);
            row.CreateCell(3);
            row.CreateCell(4);
            row.CreateCell(5);
            row.CreateCell(6);
            row.GetCell(0).SetCellValue("1");
            row.GetCell(1).SetCellValue(q.Content);
            row.GetCell(2).SetCellValue(q.Level.ToString());
            row.GetCell(3).SetCellValue(q.Type.ToString());
            row.GetCell(4).SetCellValue(q.Status.ToString());
            row.GetCell(5).SetCellValue(q.Category == null ? "" : q.Category.Name);
            row.GetCell(6).SetCellValue(q.Suggestion);
            return row;
        }

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

        private string Export(List<Question> questions)
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
                for (int i = 0; i < questions.Count; i++)
                {
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
                var stream = new MemoryStream();
                workbook.Write(stream);
                string fileName = "Export_Question_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                string FilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                //Write to file using file stream  
                FileStream file = new FileStream(FilePath, FileMode.CreateNew, FileAccess.Write);
                stream.WriteTo(file);
                file.Close();
                stream.Close();
                var url = (Request.RequestUri.GetLeftPart(UriPartial.Authority));
                var folder = ConfigurationManager.AppSettings["MediaUploadFolder"];
                return url + folder + fileName;
            }
            catch (Exception e)
            {
                throw e;
            }

        }







        private void AcceptFile(string content, string srcFolder, string destFolder)
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
                if (!Directory.Exists(destFolder + fileName))
                {
                    Directory.Move(srcFolder + fileName, destFolder + fileName);
                }
                else
                {
                    File.Delete(destFolder + fileName);
                    Directory.Move(srcFolder + fileName, destFolder + fileName);
                }
            }
        }

        private List<string> GetFileName(string content)
        {

            List<string> result = new List<string>();

            Regex reg = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>");
            foreach (Match m in reg.Matches(content))
            {
                result.Add(m.Value.Split('/').Last().Split('\"').First());
            }

            return result;
        }

        private ResultObject importZip(HttpPostedFile file, string _tempUploadFolder, string _storePath)
        {
            try
            {
                var result = new ResultObject { Success = -1 };

                string _FileName = Path.GetFileName(file.FileName);
                string zipPath = Path.Combine(HttpContext.Current.Server.MapPath(_tempUploadFolder), _FileName);
                string _tempZipPath = Path.Combine(HttpContext.Current.Server.MapPath(_tempUploadFolder + "/_temp" + DateTime.Now.ToString("yyyyMMddHHmmss")));
                string _pathExcel = Path.Combine(_tempZipPath + ConfigurationManager.AppSettings["ExcelUploadName"]);
                string _tempImagepath = Path.Combine(_tempZipPath + ConfigurationManager.AppSettings["ImageUploadPath"]);
                // save file zip to zip path
                file.SaveAs(zipPath);

                if (!".zip".Equals(Path.GetExtension(zipPath).ToLower()))
                {
                    result.Message = "Upload file is not zip format";
                    return result;
                }

                // extract to temp zip path
                ZipFile.ExtractToDirectory(zipPath, _tempZipPath);
                if (Directory.Exists(_pathExcel + ".xls"))
                {
                    _pathExcel += ".xls";
                }
                else if (Directory.Exists(_pathExcel + ".xlsx"))
                {
                    _pathExcel += ".xls";
                }
                else
                {
                    result.Message = _pathExcel + ".xls or " + _pathExcel + ".xlsx NOT FOUND!";
                    return result;
                }
                if (Directory.Exists(_tempImagepath))
                {
                    result.Message = "Folder " + _tempImagepath + " NOT FOUND!";
                    return result;
                }
                result = ImportExcel(_pathExcel, _tempImagepath, _storePath);
                if (Directory.Exists(_tempZipPath))
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(_tempZipPath);

                    foreach (FileInfo _file in di.GetFiles())
                    {
                        _file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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
                    result.Success = service.Import(listFromFiles);
                    if (result.Success >= 1)
                    {
                        foreach (var question in listFromFiles)
                        {
                            AcceptFile(question.Content, _tempUploadFolder, _storeFolder);
                        }
                    }
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

        private Question GetQuestionFromRow(IRow currentRow, int rowIndex, ref string errMesage)
        {
            string err = "";
            string content = GetValueCell(currentRow.GetCell(1));
            if (content == null || "".Equals(content)) err += "Content is null";
            var leveIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(2)), out int level);
            var typeIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(3)), out int type);
            var statusIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(4)), out int status);
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
                    Content = content,
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
                    Content = content,
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
    }
}

