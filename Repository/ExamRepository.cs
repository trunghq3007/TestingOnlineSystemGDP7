using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using Repository.Interfaces;
using Model.ViewModel;
using System.Data.Entity;
using System.IO;
using ExporterObjects;
using ExportImplementation;
using Microsoft.Office.Interop.Word;
using System.Drawing;


namespace Repository
{
	public class ExamRepository : Interfaces.IExamRepository<Exam>, IDisposable
	{
		private DBEntityContext context;
		public ExamRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public int Delete(int id)
		{
			var item = context.Exams.Where(s => s.Id == id).SingleOrDefault();
			if (item != null)
			{
				if (item.Status != true)
				{
					context.Exams.Remove(item);
					return context.SaveChanges();
				}
				else
				{

				}


			}

			return 0;
		}



		public IEnumerable<Exam> Filter(Exam t)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Exam> Filter(ExamFilterModel fillterModel)
		{
            var result = context.Exams.ToList();

            if (fillterModel.CreateBy != null && !"".Equals(fillterModel.CreateBy))
            {
                result = result.Where(s => s.CreateBy.Equals(fillterModel.CreateBy)).ToList();
            }
            if (fillterModel.TimeTest > 0)
            {
                result = result.Where(s => s.Tests != null && s.Tests.Where(t => t.TestTime == fillterModel.TimeTest).Count() > 0).ToList();
            }
            //if (fillterModel.CreateAt != null)
            //{
            //    result = result.Where(s => s.CreateAt == fillterModel.CreateAt).ToList();
            //}
            if (fillterModel.QuestionNumber > 0)
            {
                result = result.Where(s => s.QuestionNumber == fillterModel.QuestionNumber).ToList();

            }

            if (fillterModel.Status >= 0)
            {
                var flag = fillterModel.Status == 1 ? true : false;
                result = result.Where(s => s.Status == flag).ToList();

            }

            return result;

        }

		public IEnumerable<Exam> GetAll()
		{
            return context.Exams.ToList();
        }

		public Exam GetById(int id)
		{
			return context.Exams.Where(s => s.Id == id).SingleOrDefault();
		}

		public int Insert(Exam exam)
		{

            //exam.Category = context.Categorys.Where(s => s.Category.Id == exam.Id).SingleOrDefault();
            context.Exams.Add(new Exam
            {
                NameExam = exam.NameExam,
                CreateBy = exam.CreateBy,
                QuestionNumber = exam.QuestionNumber,
                Status = exam.Status,
                SpaceQuestionNumber = exam.SpaceQuestionNumber,
                CreateAt = DateTime.Now,
                Note = exam.Note,
                Category = context.Categorys.SingleOrDefault(s => s.Id == exam.Category.Id)
        });
            return context.SaveChanges();
		}
		public IEnumerable<Exam> Search(string searchString)
		{
			if (!string.IsNullOrEmpty(searchString))
			{
				return context.Exams.Where(s => s.NameExam.Contains(searchString)).ToList();
			}

			return context.Exams.ToList();
		}
		public int Update(Exam exam)
		{
          
                    //exam.CreateAt=DateTime.Now;

                    context.Entry(exam).State = EntityState.Modified;
                    return context.SaveChanges();
               

		}

		private bool disposed = false;
		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public ListFilter listFilters()
		{
			ListFilter item = new ListFilter
			{
				Listtmetest = new HashSet<float>(),
				ListCreateBy = new HashSet<string>(),
				Listquestion = new HashSet<int>()
			};
			foreach (var it in context.Tests)
			{
				item.Listtmetest.Add(it.TestTime);
			}
			foreach (var itw in context.Exams)
			{
				item.ListCreateBy.Add(itw.CreateBy);

			}
			foreach (var item1 in context.Exams)
			{
				item.Listquestion.Add(item1.QuestionNumber);
			}

			return item;

		}

		public int Export_exam(int id)
		{
            
            Exam exam = (from e in context.Exams
                         where e.Id == id
                         select e).SingleOrDefault();
            string examName = "";

            examName = "Subject : "+exam.NameExam.ToString()+"\r";
            string nameFile = exam.NameExam.ToString();

            try
            {
                examName += "Question number: " + exam.QuestionNumber.ToString() + "\r";
                //titleExam += (from t in context.Tests
                //              where t.ExamId == id
                //              select new
                //              {
                //                  TestTime = t.TestTime
                //              }
                //              ).ToString() +"\r";

                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                object missing = System.Reflection.Missing.Value;
               
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                Microsoft.Office.Interop.Word.Range range = document.Range();
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.  
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                    headerRange.Font.Size = 24;
                    
                    headerRange.Text = examName;

                }
                string tempSave = string.Empty;

                foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.  
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 10;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = "Footer text goes here";

                    var exams = (from eq in context.ExamQuestions
                                 where eq.ExamId == id
                                 select new
                                 {
                                     QuestionId = eq.QuestionId
                                 }).ToList();
                    int countExam =  1;

                    foreach (var item in exams)
                    {
                        long temp = Convert.ToInt64(item.QuestionId);
                        List<Question> ques = (from q in context.Questions
                                               where q.Id == temp

                                               select q).ToList();
                        foreach (Question itemq in ques)
                        {

                            ///document.Content.CopyAsPicture();
                            string quesText = itemq.Content.ToString();
                            
                            //document.Content.Text = quesText + Environment.NewLine + Environment.NewLine;

                            tempSave += "Question "+countExam+" : "+itemq.Content + '\r';
                            List<Answer> answers = (from a in context.Answers
                                                    where a.Question.Id == temp
                                                    select a
                                                    ).ToList();
                            int characterAbc = 0;
                            char character = 'A';
                            characterAbc = (int)character;
                            foreach (var itemAns in answers)
                            {
                                
                                string answer = (char)(characterAbc) + ". "+ itemAns.Content.ToString();
                                
                                tempSave += answer + "\r";
                                characterAbc++;
                            }
                            countExam++;
                            tempSave += "\r";
                        }
                        
                    }
                    //object rangeABC = range.InlineShapes.AddPicture(@"C:\Users\LeCuong\OneDrive\Desktop\Untitled.jpg");


                    //Range rngPic = document.Tables[1].Range;

                    //rngPic.InlineShapes.AddPicture(@"C:\Users\LeCuong\Desktop\Untitled.jpg");

                    //float leftPosition = (float)this.Application.Selection.Information[WdInformation.wdHorizontalPositionRelativeToPage];


                    //var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
                    //string configLocation = requiredPath.ToString().Substring(6)+ "\\WebApi\\Content\\ConfigLocation\\ConfigLocation.txt";


                    //string configText = File.ReadAllText(configLocation);

                    //document.Content.Text = tempSave;
                    //object filename = configText+nameFile+"cuong"+id+".docx";
                    object filename = @"D:\"+nameFile + id + ".docx";
                    document.SaveAs2(ref filename);
                    
                }
                string abc = document.Content.Text;
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;

            }
            catch (Exception ex)
            {
                throw;
            }
            return 1;
			
		}

    }
}
