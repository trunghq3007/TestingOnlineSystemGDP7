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
   public class ExamQuestionServices:Interfaces.IExamQuestionServices<Question>
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

        public int Delete(int id)
        {
            return repository.Delete(id);
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

        public IEnumerable<Question> GetAll()
        {
            return repository.GetAll();
        }

        

        public int RandomQuestion(ViewQuestionExam model)
        {
            return repository.RandomQuestion(model);
        }

        public IEnumerable<Question> GetById(int id)
        {
            return repository.GetById(id);
        }
    }
}
