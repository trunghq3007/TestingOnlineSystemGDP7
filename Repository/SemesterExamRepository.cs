using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository.Interfaces;

namespace Repository
{
    public class SemesterExamRepository : Interfaces.ISemesterExamRepository<SemesterExam>, IDisposable
    {
        private DBEntityContext context;

        public SemesterExamRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            SemesterExam item = context.SemesterExams.Where(s => s.ID == id).SingleOrDefault();
            SemesterExam olditem = item;
            item.status = 0;

            context.Entry(item).CurrentValues.SetValues(olditem);
            return context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam> Filter(SemesterExam t)
        {
            var result = context.SemesterExams.ToList();
            if (t.ID > 0)
            {
                result = result.Where(x => x.ID == t.ID).ToList();
            }

            if (t.StartDay != null)
            {
                result = result.Where(x => x.StartDay >= t.StartDay).ToList();
            }

            if (t.EndDay != null)
            {
                result = result.Where(x => x.EndDay <= t.EndDay).ToList();
            }

            return result;
        }

        public IEnumerable<Question> Filter(object t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam> GetAll()
        {
            return (IEnumerable<SemesterExam>)context.SemesterExams.ToList();
        }

        public SemesterDetail GetById(int id)
        {
            //SemesterExam semesterExam = context.SemesterExams.Include(SE => SE.);
            //var query = from SE in context.SemesterExams.Include(SE.)
            //            where SE.ID == id
            //            select SE;
            context.Configuration.LazyLoadingEnabled = false;
            List<User> users = context.Users.ToList();
            List<SemesterExam> semesterExams = context.SemesterExams.ToList();
            SemesterExam_User semesterExam_Users =
                context.SemesterExamUsers.Where(SU => SU.SemesterExam.ID == id && SU.Type == 1).FirstOrDefault();
            SemesterExam semesterExam = context.SemesterExams.Find(id);
            SemesterDetail semesterDetail = new SemesterDetail();
            try
            {
                semesterDetail.ID = semesterExam.ID;
                semesterDetail.SemesterName = semesterExam.SemesterName;
                if (semesterExam.StartDay != null)
                {

                }

                semesterDetail.StartDay = semesterExam.StartDay.ToString();
                semesterDetail.EndDay = semesterExam.EndDay.ToString();
                if (semesterExam_Users != null)
                    semesterDetail.Creator = semesterExam_Users.User.FullName;
                semesterDetail.Code = semesterExam.Code;
                if (semesterExam.status == 1)
                    semesterDetail.status = "Public";
                if (semesterExam.status == 2)
                    semesterDetail.status = "Draft";
                if (semesterExam.status == 0)
                    semesterDetail.status = "Done";

                var QT = from TR in context.TestResults
                         join U in context.Users on TR.UserId equals U.UserId
                         join SU in context.SemesterExamUsers on U.UserId equals SU.User.UserId
                         where SU.Type == 2 && SU.SemesterExam.ID == id
                         select TR;
                int participation = QT.ToList().Count;
                semesterDetail.NumberInvite = Convert.ToString(participation);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }



            return semesterDetail;

        }

        public Result GetResult(int id, int userId)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<TestResult> testResults = context.TestResults.ToList();
            //List<User> users = context.Users.ToList();
            List<Test> tests = context.Tests.ToList();
            List<SemesterExam> semesterExams = context.SemesterExams.ToList();

            Test test = context.Tests.Find(id);

            // User user = context.Users.Find(id);
            // TestResult testResult = context.TestResults.Find(id);

            Result result = new Result();
            try
            {
                result.ID = test.Id;

                var query1 = from TR in context.TestResults
                             join T in context.Tests
                             on TR.TestId equals T.Id
                             select T.TestName;
                result.TestName = query1.FirstOrDefault();


                var query2 = from TR in context.TestResults
                             join T in context.Tests
                             on TR.TestId equals T.Id
                             join SE in context.SemesterExams
                             on T.SemasterExamId equals SE.ID
                             select new { T.Id, T.TestName, SE.SemesterName };

                result.SemesterName = query2.FirstOrDefault().SemesterName;

                //var query3 = from TR in context.TestResults
                //             join U in context.Users
                //             on TR.UserId equals U.UserId
                //             select new { U.FullName, U.Email };
                //var tesst = query3.ToList();
                var user = context.Users.Find(userId);
                result.FullName = user.FullName;
                result.Email = user.Email;

                var query4 = from TR in context.TestResults

                             where TR.UserId == userId
                             && TR.TestTimeNo == 0
                             && TR.TestId == id
                             select TR.Score;
                var query5 = context.TestResults.Where(s=>s.TestId==id&&s.UserId==userId).GroupBy(x => new { x.TestTimeNo }).OrderByDescending(x => x.Key.TestTimeNo).Select(x => new { dep = x.Key.TestTimeNo, sum = x.Sum(c => c.Score) }).FirstOrDefault();
                var list = query5.sum;
                result.Score = Convert.ToInt32(list);



                // select PassScore from tests t,Exams e where t.examid = e.id and t.id=1021

                var totalQuestion = context.Exams.FirstOrDefault(s => context.Tests.Any(t => t.Id == id && s.Id == t.ExamId))?.QuestionNumber;

                //    result.Score = Convert.ToInt32(testResult.Score);
                result.Category = test.PassScore <= result.Score ? "Đỗ" : "Trượt";
                
                //if (result.Score <= 4)
                //    result.Category = "Trượt";
                //else
                //    result.Category = "Đỗ";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }





            return result;


        }

        public int Insert(SemesterExam t)
        {
            context.SemesterExams.Add(t);
            return context.SaveChanges();
        }

        public IEnumerable<SemesterExam> Search(string searchString)
        {
            //List<SemesterExam> semesterExams = new List<SemesterExam>();
            //try {
            //    if (!string.IsNullOrEmpty(searchString))
            //    {
            //        semesterExams = context.SemesterExams.Where(SE => SE.SemesterName.Contains(searchString)).ToList();
            //    }
            //    return context.SemesterExams.ToList();
            //}
            //catch
            //{

            //    semesterExams = new List<SemesterExam>();
            //}
            //return semesterExams;
            if (!string.IsNullOrEmpty(searchString))
            {
                return context.SemesterExams.Where(SE => SE.SemesterName.Contains(searchString)).ToList();
            }

            return context.SemesterExams.ToList();
        }

        public Model.ViewModel.ReportSemester Report(int id)
        {
            SemesterExam semesterExam = context.SemesterExams.Find(id);
            SemesterExam_User semesterExam_Users =
                context.SemesterExamUsers.Where(SU => SU.SemesterExam.ID == id && SU.Type == 1).FirstOrDefault();
            var query = from E in context.Exams
                        join T in context.Tests on E.Id equals T.ExamId
                        join SE in context.SemesterExamUsers on T.ExamId equals SE.ID
                        select E;
            //var queryCandiates = from SEU in context.SemesterExamUsers
            //    join SE in context.SemesterExams on SEU.SemesterExam.ID equals id
            //    //where SEU.Type == 2
            //    select SEU;
            int numCandiates = context.SemesterExamUsers.Where(SEU => SEU.SemesterExam.ID == id && SEU.Type == 2)
                .ToList().Count;
            List<SemesterExam_User> list = context.SemesterExamUsers
                .Where(SEU => SEU.SemesterExam.ID == id && SEU.Type == 2).ToList();

            var QT = from TR in context.TestResults
                     join U in context.Users on TR.UserId equals U.UserId
                     join SU in context.SemesterExamUsers on U.UserId equals SU.User.UserId
                     where SU.Type == 2 && SU.SemesterExam.ID == id
                     select TR;
            int low = 0;
            int medium = 0;
            int good = 0;
            foreach (TestResult item in QT.ToList())
            {
                if (item.Score < 4)
                {
                    low++;
                }

                if (item.Score > 7)
                {
                    good++;
                }
                else
                {
                    medium++;
                }
            }

            float avgScore = 0;
            foreach (TestResult item in QT.ToList())
            {
                avgScore += item.Score;
            }

            avgScore = avgScore / QT.ToList().Count;
            int participation = QT.ToList().Count;
            int count = 0;
            if (query.ToList() != null)
                count = query.ToList().Count;
            Exam exam = query.ToList().FirstOrDefault();
            int numQuestion = 0;
            if (exam != null)
            {
                numQuestion = exam.QuestionNumber;

            }



            ReportSemester reportSemester = new ReportSemester();

            User user = new User();
            //user.SemesterExam_Users.Where(S => S.ID == 1).ToList();
            //SemesterExam semesterExam= new SemesterExam();
            List<User> users = context.Users.ToList();
            List<SemesterExam> semesterExamsterExams = context.SemesterExams.ToList();
            reportSemester.SemesterName = semesterExam.SemesterName;
            if (semesterExam_Users != null)
                reportSemester.Creator = semesterExam_Users.User.FullName;
            reportSemester.StartDay = semesterExam.StartDay.ToString();
            reportSemester.EndDay = semesterExam.EndDay.ToString();
            reportSemester.NumEXams = count;
            reportSemester.NumQuestions = numQuestion;
            reportSemester.NumCandiates = numCandiates;
            reportSemester.NotParticipation = numCandiates - participation;
            reportSemester.AvgScore = avgScore;
            reportSemester.Good = good;
            reportSemester.Medium = medium;
            reportSemester.Low = low;
            if (semesterExam.status == 1)
                reportSemester.Status = "Public";
            if (semesterExam.status == 2)
                reportSemester.Status = "Draft";
            else
                reportSemester.Status = "Done";
            //ReportSemester reportSemester = new ReportSemester(semesterExam.SemesterName,semesterExam_Users.User.UserName,semesterExam.StartDay.ToString(),semesterExam.EndDay.ToString(),count);
            return reportSemester;

        }

        public int Update(SemesterExam t)
        {
            SemesterExam oldItem = context.SemesterExams.Where(s => s.ID == t.ID).SingleOrDefault();



            context.Entry(oldItem).CurrentValues.SetValues(t);
            return context.SaveChanges();
        }

        public IEnumerable<Exam> GetExams(int id)
        {
            var query = from E in context.Exams
                        join T in context.Tests on E.Id equals T.ExamId
                        where T.SemesterExam.ID == id
                        select E;
            List<Exam> list = query.ToList();
            int a = 0;
            return query.ToList();
        }

        public IEnumerable<SemesterExam> InputCode(string code)
        {
            return context.SemesterExams.Where(SE => SE.Code.Equals(code));
        }

        public IEnumerable<SemesterExam> GetByCandidateId(int candidateId)
        {
            var query = from SE in context.SemesterExams
                        join
                            SEU in context.SemesterExamUsers on
                            SE.ID equals SEU.SemesterExam.ID
                        where SEU.Type == 2 && SEU.User.UserId == candidateId
                        select SE;
            return query.ToList();
        }

        public IEnumerable<Test> GetTests(int id)
        {
            List<Test> list = context.Tests.Where(T => T.SemesterExam.ID == id).ToList();
            return list;
        }

        public IEnumerable<Exam> GetExamsNotAdd(int id)
        {
            var query = from E in context.Exams
                        join T in context.Tests on E.Id equals T.ExamId
                        where T.SemesterExam.ID == id
                        select E;
            List<Exam> list1 = query.ToList();
            //var query2 = from E in context.Exams
            //             where !E.Equals(from Ex in context.Exams
            //                             join T in context.Tests on Ex.Id equals T.ExamId
            //                             where T.SemesterExam.ID == id
            //                             select Ex)
            //             select E;
            //var query2 = from E in context.Exams
            //             where !E.Equals(from Ex in context.Exams
            //                             //join T in context.Tests on Ex.Id equals T.ExamId
            //                             //where T.SemesterExam.ID == id
            //                             select Ex)
            //             select E;
            var query2 = from Ex in context.Exams select Ex;
            List<Exam> list2 = query2.ToList();
            //List<Exam> list3 = (from E in list2 where ! E.Id.c (from EX in list1 select EX.Id) select E).ToList();

            foreach (Exam item in list1)
            {
                if (list2.Contains(item))
                    list2.Remove(item);

            }


            //List<Exam> list2 = query2.ToList();z

            //List<Exam> list3=

            //var query3 = from E in context.Exams
            //             join T in context.Tests on person equals pet.Owner into gj
            //             from subpet in gj.DefaultIfEmpty()
            //             select new { person.FirstName, PetName = subpet?.Name ?? String.Empty };
            int a = 0;

            return list2;

        }

        public int AddMany(int[] listId, int semesterId)
        {
            int a = 0;
            List<Test> list = context.Tests.Where(T => T.SemesterExam.ID == semesterId).ToList();
            foreach (Test item in list)
            {
                foreach (int item2 in listId)
                {

                    item.ExamId = item2;

                    context.Entry(context.Tests.Find(item.Id)).CurrentValues.SetValues(item);
                    a = context.SaveChanges();
                }
            }

            return a;
        }

        public IEnumerable<Exam> SearchExams(string examName, int id)
        {
            var query = from E in context.Exams
                        join T in context.Tests on
                            E.Id equals T.ExamId
                        where E.NameExam.Contains(examName) && T.SemasterExamId == id
                        select E;
            return query.ToList();

        }

        public IEnumerable<Model.Test> GetTestsNotAdd(int id)
        {
            return context.Tests.Where(T => T.SemesterExam.ID != id);
        }

        public Model.ViewModel.TestProcessing GeTestProcessings(int id)
        {
            TestProcessing testProcessing = new TestProcessing();
            Test test = context.Tests.Find(id);
            testProcessing.Id = id;
            testProcessing.TestName = test.TestName;
            testProcessing.TestTime = test.TestTime;
            Exam exam = test.Exam;
            var queryQuestions = from Q in context.Questions
                                 join EQ in context.ExamQuestions on Q.Id equals EQ.QuestionId
                                 join E in context.Exams on EQ.ExamId equals E.Id
                                 join T in context.Tests on E.Id equals T.ExamId
                                 where T.Id == id && E.Category.Id == Q.Category.Id
                                 //where E.Category.Id == Q.Category.Id
                                 select Q;



            List<Question> questions = queryQuestions.ToList();
            List<Question> listRandom = new List<Question>();
            Random random = new Random();
            int b = questions.Count;
            if (b > 0)
            {
                for (int i = 0; i < test.Exam.QuestionNumber; i++)
                {

                    int a = random.Next(0, b);
                    if (b != 0)
                        b--;
                    if (questions.Count > 0)
                    {
                        listRandom.Add(questions[a]);
                        questions.Remove(questions[a]);
                    }
                }
            }


            testProcessing.Questions = listRandom;
            int asdafas = 0;
            return testProcessing;



        }

        public IEnumerable<ExamInformation> GetTestDetail(int id)
        {
            var query = from T in context.Tests
                        join E in context.Exams on T.ExamId equals E.Id
                        join C in context.Categorys
                            on E.Category.Id equals C.Id
                        select new
                        {
                            T.TestName,
                            T.TestTime,
                            E.QuestionNumber,
                            C.Name,
                        };
            ExamInformation examInformation = new ExamInformation();
            examInformation.TestName = query.FirstOrDefault().TestName;
            examInformation.NumberChoiceQuestion = query.FirstOrDefault().QuestionNumber * 3 / 4;
            examInformation.NumberStatementQuestion =
                query.FirstOrDefault().QuestionNumber - examInformation.NumberChoiceQuestion;
            examInformation.TestTime = query.FirstOrDefault().TestTime;
            examInformation.CategoryName = query.FirstOrDefault().Name;
            examInformation.QuestionNumber = query.FirstOrDefault().QuestionNumber;
            examInformation.TotalScore = 100;
            return GetTestDetail(id);
        }

        ExamInformation ISemesterExamRepository<SemesterExam>.GetTestDetail(int id)
        {
            var query = from T in context.Tests
                        join E in context.Exams on T.ExamId equals E.Id
                        join C in context.Categorys
                            on E.Category.Id equals C.Id
                        select new
                        {
                            T.TestName,
                            T.TestTime,
                            E.QuestionNumber,
                            C.Name,
                        };
            ExamInformation examInformation = new ExamInformation();
            examInformation.TestName = query.FirstOrDefault().TestName;
            examInformation.NumberChoiceQuestion = query.FirstOrDefault().QuestionNumber * 3 / 4;
            examInformation.NumberStatementQuestion =
                query.FirstOrDefault().QuestionNumber - examInformation.NumberChoiceQuestion;
            examInformation.TestTime = query.FirstOrDefault().TestTime;
            examInformation.CategoryName = query.FirstOrDefault().Name;
            examInformation.QuestionNumber = query.FirstOrDefault().QuestionNumber;
            examInformation.TotalScore = 10;
            return examInformation;
        }
        private void SubmitAnswer(int testId, string listId, int userID)
        {
            try
            {


                var arrr = listId.Replace('[', ' ');
                var arrrr = arrr.Replace(']', ' ');
                //var arrr = listId.Remove(0, 0);
                var arr = arrrr.Trim().Split(',');
                List<Answer> answers = new List<Answer>();
                foreach (var item in arr)
                {
                    if (item != null && !"".Equals(item))
                    {
                        int ids = int.Parse(item);
                        var query = (from Q in context.Questions
                                     join A in context.Answers
                                     on Q.Id equals A.Question.Id
                                     where A.Id == ids
                                     select A
                                     ).SingleOrDefault();
                        answers.Add(query);


                    }
                    else
                    {
                        TestResult testResult = new TestResult();
                        testResult.UserId = userID;
                        testResult.TestId = testId;
                        testResult.Score = 0;
                        context.TestResults.Add(testResult);
                        context.SaveChanges();
                    }
                }
                //  var questions = answers.Where(s=>s.Question.Id==answers.c).ToList();
                //foreach(var item in answers)
                // {

                // }

                string listQ = "";
                for (var i = 0; i < answers.Count(); i++)
                {
                    for (var j = i + 1; j < answers.Count(); j++)
                    {
                        if (answers[i].Question.Id == answers[j].Question.Id && answers[i].Id != answers[j].Id)
                        {


                            listQ += answers[i].Id + ",";

                        }
                    }
                }

                var listqs = listQ.Split(',');
                HashSet<string> listhashset = new HashSet<string>();
                foreach (var i in listqs)
                {
                    listhashset.Add(i);
                }
                foreach (var item in listhashset)
                {
                    if (item != null && !"".Equals(item))
                    {
                        var listQuetions_1 = answers.Where(s => s.Id == int.Parse(item)).SingleOrDefault();
                        TestResult testResult = new TestResult();
                        testResult.AnwserId = listQuetions_1.Id;
                        testResult.QuestionId = listQuetions_1.Question.Id;
                        testResult.UserId = userID;
                        testResult.TestId = testId;
                        testResult.Score = 0;
                        context.TestResults.Add(testResult);
                        context.SaveChanges();
                        answers.Remove(answers.Where(s => s.Id == int.Parse(item)).SingleOrDefault());
                    }

                }
                foreach (var item in answers)
                {
                    TestResult testResult = new TestResult();
                    testResult.AnwserId = item.Id;
                    testResult.QuestionId = item.Question.Id;
                    testResult.UserId = userID;
                    testResult.TestId = testId;

                    if (item.IsTrue == true)
                    {
                        testResult.Score = 1;
                    }
                    context.TestResults.Add(testResult);
                    context.SaveChanges();
                }
            }
            catch { }
        }
        public int Submit(List<Answer> answers, int testId, int userID)
        {

            foreach (Answer item in answers)
            {
                var query = from Q in context.Questions
                            join A in context.Answers
                            on Q.Id equals A.Question.Id
                            where A.Id == item.Id
                            select Q;
                item.Question = query.FirstOrDefault();

            }
            foreach (Answer item in answers)
            {
                TestResult testResult = new TestResult();
                testResult.AnwserId = item.Id;
                testResult.QuestionId = item.Question.Id;
                testResult.UserId = userID;
                testResult.TestId = testId;
                if (item.IsTrue == true) testResult.Score = 1;
                context.TestResults.Add(testResult);
                context.SaveChanges();

            }

            return 1;
        }

        public int Submits(int testId, string listId, int userID)
        {
            var listTest = context.TestResults.Where(s => s.UserId == userID && s.TestId == testId).Count();
            if(listTest==0)
            {
                SubmitAnswer(testId, listId, userID);
            }
            else
            {
                var listTrue = context.TestResults.OrderByDescending(s => s.Id).Where(s => s.UserId == userID && s.TestId == testId).FirstOrDefault();
                try
                {


                    var arrr = listId.Replace('[', ' ');
                    var arrrr = arrr.Replace(']', ' ');
                    //var arrr = listId.Remove(0, 0);
                    var arr = arrrr.Trim().Split(',');
                    List<Answer> answers = new List<Answer>();
                    foreach (var item in arr)
                    {
                        if (item != null && !"".Equals(item))
                        {
                            int ids = int.Parse(item);
                            var query = (from Q in context.Questions
                                         join A in context.Answers
                                         on Q.Id equals A.Question.Id
                                         where A.Id == ids
                                         select A
                                         ).SingleOrDefault();
                            answers.Add(query);


                        }
                        else
                        {
                            TestResult testResult = new TestResult();
                            testResult.UserId = userID;
                            testResult.TestId = testId;
                            testResult.Score = 0;
                            testResult.TestTimeNo = listTrue.TestTimeNo + 1;
                            context.TestResults.Add(testResult);
                            context.SaveChanges();
                        }
                    }
                    //  var questions = answers.Where(s=>s.Question.Id==answers.c).ToList();
                    //foreach(var item in answers)
                    // {

                    // }

                    string listQ = "";
                    for (var i = 0; i < answers.Count(); i++)
                    {
                        for (var j = i + 1; j < answers.Count(); j++)
                        {
                            if (answers[i].Question.Id == answers[j].Question.Id && answers[i].Id != answers[j].Id)
                            {


                                listQ += answers[i].Id + ",";

                            }
                        }
                    }

                    var listqs = listQ.Split(',');
                    HashSet<string> listhashset = new HashSet<string>();
                    foreach (var i in listqs)
                    {
                        listhashset.Add(i);
                    }
                    foreach (var item in listhashset)
                    {
                        if (item != null && !"".Equals(item))
                        {
                            var listQuetions_1 = answers.Where(s => s.Id == int.Parse(item)).SingleOrDefault();
                            TestResult testResult = new TestResult();
                            testResult.AnwserId = listQuetions_1.Id;
                            testResult.QuestionId = listQuetions_1.Question.Id;
                            testResult.UserId = userID;
                            testResult.TestId = testId;
                            testResult.Score = 0;
                            testResult.TestTimeNo = listTrue.TestTimeNo + 1;
                            context.TestResults.Add(testResult);
                            context.SaveChanges();
                            answers.Remove(answers.Where(s => s.Id == int.Parse(item)).SingleOrDefault());
                        }

                    }
                    foreach (var item in answers)
                    {
                        TestResult testResult = new TestResult();
                        testResult.AnwserId = item.Id;
                        testResult.QuestionId = item.Question.Id;
                        testResult.UserId = userID;
                        testResult.TestId = testId;

                        if (item.IsTrue == true)
                        {
                            testResult.Score = 1;
                        }
                        testResult.TestTimeNo = listTrue.TestTimeNo + 1;
                        context.TestResults.Add(testResult);
                        context.SaveChanges();
                    }
                }
                catch { }
            }
          
            //finally
            //{
            //    var listLastest = context.TestResults.OrderByDescending(s => s.Id).Where(s => s.TestId == testId && s.UserId == userID&&s.TestTimeNo>0).FirstOrDefault();
            //    var listFirst = context.TestResults.Where(s => s.TestId == testId && s.UserId == userID && s.TestTimeNo == 0).ToList();
            //    if(listLastest!=null)
            //    foreach (var item in listFirst)
            //    {
            //        item.TestTimeNo = listLastest.TestTimeNo + 1;
            //        context.Entry(item).State = EntityState.Modified;
            //        context.SaveChanges();
            //    }
            //}

            return 1;
        }


    }



}


