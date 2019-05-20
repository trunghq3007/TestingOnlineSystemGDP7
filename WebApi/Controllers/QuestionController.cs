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

            if ("search".Equals(action))
            {
                if (value != null) return "Data null";
                var searchObj = JsonConvert.DeserializeObject<SearchPaging>(value.ToString());
                return JsonConvert.SerializeObject(service.Search(searchObj));
            }
            if ("fillter".Equals(action))
            {
                if (value != null) return "Data null";
                try
                {
                    var filterObject = JsonConvert.DeserializeObject<QuestionFillterModel>(value.ToString());
                    return JsonConvert.SerializeObject(service.Filter(filterObject));
                }
                catch (Exception)
                {
                    return "Object fillter not convert valid";
                }
            }
            string _path = "";
            if ("import".Equals(action.ToLower()))
            {
                if (HttpContext.Current.Request.Files.Count < 1) return ClearFile(_path, "Content file null");
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                List<Question> listFromFiles = new List<Question>();
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        _path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);
                        string extension = Path.GetExtension(_path);
                        if (!".xls".Equals(extension.ToLower()) && !".xlsx".Equals(extension.ToLower())) return ClearFile(_path, "File extenstion not valid excel file (.xls, .xlsx)");

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
                            else return ClearFile(_path, "File extenstion not valid excel file (.xls, .xlsx)");
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
                            return ClearFile(_path, service.Import(listFromFiles));
                        }
                        else
                        {
                            return ClearFile(_path, FILEERROR);
                        }

                    }
                    else
                    {
                        return ClearFile(_path, "CONTENT LENGTH FILE NULL");
                    }
                }
                catch (Exception e)
                {
                    return ClearFile(_path, "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace);
                }
            }
            return "action not support";

        }

        [HttpGet]
        public string Get()
        {
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        [HttpGet]
        public string Get(int id)
        {
            var result = service.GetById(id);
            return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        [HttpPost]
        public string Post([FromBody]object value)
        {
            if (value != null)
            {
                var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                question.CreatedBy = "anonymous user";
                question.CreatedDate = DateTime.Now;
                var result = service.Insert(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [HttpPut]
        public string Put(int id, [FromBody]object value)
        {
            if (value != null)
            {
                var question = JsonConvert.DeserializeObject<Question>(value.ToString());
                question.Id = id;
                question.UpdatedBy = "anonymous user";
                question.UpdatedDate = DateTime.Now;
                var result = service.Update(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }
        [HttpDelete]
        public string Put(int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private string ClearFile(string _path, string result)
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }

            return result;
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
