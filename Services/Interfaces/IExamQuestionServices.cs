using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface IExamQuestionServices<T> where T : class
    {
        IEnumerable<Model.ViewModel.ViewQuestionExam> GetListQuestionById(int id);
		int Insert(ExamQuestion model);
        Model.ViewModel.GetFill listFilters();
        int AddMutipleQuestion(List<ExamQuestion> ListModel);
        IEnumerable<T> GetAll();
        int RandomQuestion(ViewQuestionExam model);
        IEnumerable<Question> GetById(int id);
        int DeleteMutiple(List<ExamQuestion> ListModel);
        IEnumerable<T> Search(string searchString);
    }
}
