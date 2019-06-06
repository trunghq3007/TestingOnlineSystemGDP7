using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ViewModel;

namespace Repository.Interfaces
{
         public interface IExamQuestion<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString);
        IEnumerable<T> Filter(T t);
        int Insert(ExamQuestion model);
        int Update(T t);
        int Delete(ExamQuestion model);
        IEnumerable<ViewQuestionExam> GetListQuestionById(int id);

        IEnumerable<Question> GetById(int id);
        GetFill listFilters();
        int AddMutipleQuestion(List<ExamQuestion> ListModel);
        int RandomQuestion(ViewQuestionExam model);
        int DeleteMutiple(List<ExamQuestion> ListModel);
    }
}
