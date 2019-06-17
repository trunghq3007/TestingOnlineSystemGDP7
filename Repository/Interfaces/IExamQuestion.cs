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
        IEnumerable<ViewQuestionExam> GetAll();
        IEnumerable<ViewQuestionExam> Search(string searchString);
        IEnumerable<ViewQuestionExam> Filter(ViewQuestionExam t);
        //int Insert(ExamQuestion model);
        //int Update(T t);
        //int Delete(ExamQuestion model);
        IEnumerable<ViewQuestionExam> GetListQuestionById(int id);

        IEnumerable<ViewQuestionExam> GetById(int id);
        GetFill listFilters();
        int AddMutipleQuestion(List<ExamQuestion> ListModel);
        int RandomQuestion(ViewQuestionExam model);
        int DeleteMutiple(List<ExamQuestion> ListModel);
       
    }
}
