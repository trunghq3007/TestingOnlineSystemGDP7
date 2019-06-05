using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
  public  class SemesterExamServices : ISemesterExamServices<SemesterExam>
    {
      private ISemesterExamRepository<SemesterExam> SemesterExamRepository;
        public SemesterExamServices()
        {
            SemesterExamRepository = new SemesterExamRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return SemesterExamRepository.Delete(id);
        }

        public IEnumerable<SemesterExam> Filter(SemesterExam t)
        {
            return SemesterExamRepository.Filter(t);
        }

        public IEnumerable<SemesterExam> GetAll()
        {
            return SemesterExamRepository.GetAll();
        }

        public SemesterDetail GetById(int id)
        {
            return SemesterExamRepository.GetById(id);
        }

        public int Insert(SemesterExam t)
        {
            return SemesterExamRepository.Insert(t);
        }

        public ReportSemester Report(int id)
        {
            return SemesterExamRepository.Report(id);
        }

        public IEnumerable<SemesterExam> Search(string searchString)
        {
            return SemesterExamRepository.Search(searchString);
        }

        public int Update(SemesterExam t)
        {
            return SemesterExamRepository.Update(t);
        }

        //SemesterExam ISemesterExamServices<SemesterExam>.GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}
        public IEnumerable<Exam> GetExamsNotAdd(int id)
        {
            return SemesterExamRepository.GetExamsNotAdd(id);
        }

        public int AddMany(int[] listId, int semesterId)
        {
            return SemesterExamRepository.AddMany(listId, semesterId);
        }
        public IEnumerable<Exam> GetExams(int id)
        {
            return SemesterExamRepository.GetExams(id);
        }

        public IEnumerable<Test> GetTests(int id)
        {
            return SemesterExamRepository.GetTests(id);
        }

        public IEnumerable<SemesterExam> InputCode(string code)
        {
            return SemesterExamRepository.InputCode(code);
        }
        public IEnumerable<SemesterExam> GetByCandidateId(int candidateId)
        {
            return SemesterExamRepository.GetByCandidateId(candidateId);
        }

        public IEnumerable<Exam> SearchExams(string examName, int id)
        {
            return SemesterExamRepository.SearchExams(examName, id);
        }
public IEnumerable<Test> GetTestsNotAdd(int id)
        {
            return SemesterExamRepository.GetTestsNotAdd(id);
        }

        public TestProcessing GeTestProcessings(int id)
        {
            return SemesterExamRepository.GeTestProcessings(id);
        }
 public ExamInformation GetTestDetail(int id)
        {
            return SemesterExamRepository.GetTestDetail(id);
        }

<<<<<<< .mine

        public ExamInformation GetTestDetail(int id)
        {
            return SemesterExamRepository.GetTestDetail(id);
        }







        public IEnumerable<Test> GetTestsNotAdd(int id)
        {
            return SemesterExamRepository.GetTestsNotAdd(id);
        }

        public TestProcessing GeTestProcessings(int id)
        {
            return SemesterExamRepository.GeTestProcessings(id);
        }

    }
}
