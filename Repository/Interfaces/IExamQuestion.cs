using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Repository.Interfaces
{
         public interface IExamQuestion<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString);
        IEnumerable<T> Filter(T t);
        int Insert(ExamQuestion model);
        int Update(T t);
        int Delete(int id);
        IEnumerable<Model.ViewModel.ViewQuestionExam> GetListQuestionById(int id);
        T GetById(int id);

    }
}
