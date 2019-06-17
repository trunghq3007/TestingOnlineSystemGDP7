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
    public class ExamQuestionServices : Interfaces.IExamQuestionServices<Question>
    {
        private IExamQuestion<Question> repository;
        public ExamQuestionServices()
        {
            repository = new ExamQuestionResponsitory(new DBEntityContext());
        }

        public IEnumerable<ViewQuestionExam> GetListQuestionById(int id)
        {
            return repository.GetListQuestionById(id);
        }

        public int Delete(ExamQuestion model)
        {
            return repository.Delete(model);
        }
        public int Insert(Model.ExamQuestion model)
        {
            return repository.Insert(model);

        }
        public GetFill listFilters()
        {
            return repository.listFilters();
        }

        public int AddMutipleQuestion(List<ExamQuestion> ListModel)
        {
            return repository.AddMutipleQuestion(ListModel);
        }

        public IEnumerable<ViewQuestionExam> GetAll()
        {
            return repository.GetAll();
        }



        public int RandomQuestion(ViewQuestionExam model)
        {
            return repository.RandomQuestion(model);
        }

        public IEnumerable<ViewQuestionExam> GetById(int id)
        {
            return repository.GetById(id);
        }

        public int DeleteMutiple(List<ExamQuestion> ListModel)
        {
            return repository.DeleteMutiple(ListModel);
        }

        public IEnumerable<ViewQuestionExam> Search(string searchString)
        {
            return repository.Search(searchString);
        }

        public IEnumerable<ViewQuestionExam> Filter(ViewQuestionExam filterModel)
        {
            return repository.Filter(filterModel);
        }
    }
}
