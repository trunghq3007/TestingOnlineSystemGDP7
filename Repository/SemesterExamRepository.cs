﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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
            return (IEnumerable<SemesterExam>)context.SemesterExams.Where(SE=>SE.status != 0).ToList();
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
            semesterDetail.ID = semesterExam.ID;
            semesterDetail.SemesterName = semesterExam.SemesterName;
            if (semesterExam.StartDay != null)
            {

            }
            semesterDetail.StartDay = semesterExam.StartDay.ToString().Substring(0, 9);
            semesterDetail.EndDay = semesterExam.EndDay.ToString();
            if(semesterExam_Users != null)
            semesterDetail.Creator = semesterExam_Users.User.FullName ;
            semesterDetail.Code = semesterExam.Code;
            if (semesterExam.status == 1)
                semesterDetail.status = "Public";
            if (semesterExam.status == 2)
                semesterDetail.status = "Draft";
            else
                semesterDetail.status = "Done";

            var QT = from TR in context.TestResults
                     join U in context.Users on TR.UserId equals U.UserId
                     join SU in context.SemesterExamUsers on U.UserId equals SU.User.UserId
                     where SU.Type == 2 && SU.SemesterExam.ID == id
                     select TR;
            int participation = QT.ToList().Count;
            semesterDetail.NumberInvite = Convert.ToString(participation);


            return semesterDetail;

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
                return context.SemesterExams.Where(SE => SE.SemesterName.Contains(searchString) && SE.status !=0).ToList();
            }
            return context.SemesterExams.Where(SE => SE.status != 0).ToList();
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
            int numCandiates = context.SemesterExamUsers.Where(SEU => SEU.SemesterExam.ID == id && SEU.Type==2).ToList().Count;
            List<SemesterExam_User> list = context.SemesterExamUsers.Where(SEU => SEU.SemesterExam.ID == id && SEU.Type == 2).ToList();

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
                if (item.Score >7 )
                {
                    good++;
                }
                else
                {
                    medium++;
                }
            }

            float avgScore = 0;
            foreach (TestResult item  in QT.ToList())
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
            if (exam!= null)
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
            if(semesterExam_Users != null)
            reportSemester.Creator = semesterExam_Users.User.FullName ;
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
            return context.Tests.Where(T => T.SemesterExam.ID == id).ToList();
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
            var query = from E in context.Exams join T in context.Tests on
                        E.Id equals T.ExamId 
                        where E.NameExam.Contains(examName) && T.SemasterExamId == id
                        select E;
            return query.ToList();

        }
    }

}
