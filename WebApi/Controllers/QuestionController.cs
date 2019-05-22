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
        public string Get([FromUri]string action, [FromBody]object value)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
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

                if ("import".Equals(action.ToLower()))
                {
                    string _path = "";
                    if (HttpContext.Current.Request.Files.Count < 1)
                    {
                        result.Message = "Not file upload";
                        return JsonConvert.SerializeObject(result);
                    }
                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    List<Question> listFromFiles = new List<Question>();

                    if (file.ContentLength <= 0)
                    {
                        result.Message = "content file null";
                        return JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        _path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);
                        string extension = Path.GetExtension(_path);
                        if (!".xls".Equals(extension.ToLower()) && !".xlsx".Equals(extension.ToLower()))
                        {
                            result.Message = "File extenstion not valid excel file (.xls, .xlsx)";
                            ClearFile(_path);
                            return JsonConvert.SerializeObject(result);
                        }
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
                        ISheet sheet = workbook.GetSheetAt(1);
                        Question ques = null;
                        string FILEERROR = "";
                        int questionRow = 0;

                        for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                        {
                            var currentRow = sheet.GetRow(rowIndex);
                            string flag = GetValueCell(currentRow.GetCell(0));
                            if ("".Equals(flag) || "END".Equals(flag.ToUpper()))
                            {
                                if (ques != null) listFromFiles.Add(ques);
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
                                    if (!hasIsTrue) FILEERROR += "Row " + (questionRow - 1) + " not has true answer";
                                }
                                break;
                            }
                            if ("1".Equals(flag))
                            {

                                string err = "";
                                if (ques != null) listFromFiles.Add(ques);
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
                                    if (!hasIsTrue) err += "Row " + (questionRow - 1) + " not has true answer";
                                }
                                questionRow = rowIndex;
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
                                    ques = new Question
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
                                    FILEERROR += "Row " + rowIndex + ": " + err + "\n";
                                }
                            }
                            if ("2".Equals(flag))
                            {
                                string err = "";
                                string content = GetValueCell(currentRow.GetCell(1));
                                if (content == null || "".Equals(content)) err += "Content is null";
                                var statusIsNumber = int.TryParse(GetValueCell(currentRow.GetCell(4)), out int status);
                                var isTrue = "1".Equals(GetValueCell(currentRow.GetCell(7))) ? true : false;
                                if (!statusIsNumber) err += " Status is not number,";
                                if ("".Equals(err))
                                {
                                    ques.Answers.Add(new Answer
                                    {
                                        Content = content,
                                        Status = status,
                                        CreatedBy = "anonymous user import",
                                        CreatedDate = DateTime.Now,
                                        IsTrue = isTrue
                                    });
                                }
                                else
                                {
                                    FILEERROR = "Row " + rowIndex + ": " + err + "\n";
                                }
                            }
                        }
                        if ("".Equals(FILEERROR))
                        {
                            result.Success = service.Import(listFromFiles);
                            ClearFile(_path);
                            return JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            result.Success = 0;
                            ClearFile(_path);
                            result.Message = FILEERROR;
                            return JsonConvert.SerializeObject(result);
                        }
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
                result.Data = service.GetAll().ToList();
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
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
                var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                question.Id = id;
                result.Success = service.Update(question);
                return JsonConvert.SerializeObject(result);
            }
            result.Message = "Null content";
            return JsonConvert.SerializeObject(result);
        }
        [HttpDelete]
        public string Put(int id)
        {
            ResultObject result = new ResultObject
            {
                Success = service.Delete(id)
            };
            return JsonConvert.SerializeObject(result);

        }

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
    }
}
