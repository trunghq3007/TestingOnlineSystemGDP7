using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using DataAccessLayer;
using Repository.Interfaces;
using Model.ViewModel;
using Services.Interfaces;

namespace Services
{
    public class ExamServices : Interfaces.IExamServices<Exam>
	{

		private IExamRepository<Exam> examRepository;
		public ExamServices()
		{
			examRepository = new ExamRepository(new DBEntityContext());
		}

		public int Delete(int id)
		{
			return examRepository.Delete(id);
		}

		public int Export_exam(int id)
		{
			return examRepository.Export_exam(id);
			//throw new System.NotImplementedException();
		}

		

		public IEnumerable<Exam> Filter(ExamFilterModel filterModel)
		{
			return examRepository.Filter(filterModel);
		}

		

		public IEnumerable<Exam> GetAll()
		{
			return examRepository.GetAll();
		}

		public Exam GetById(int id)
		{
			return examRepository.GetById(id);
		}

		public int Insert(Exam exam)
		{
			return examRepository.Insert(exam);
		}

		public ListFilter listFilters()
		{
			return examRepository.listFilters();
		}

		public IEnumerable<Exam> Search(string searchString)
		{
			return examRepository.Search(searchString);
		}

		public int Update(Exam exam)
		{
			return examRepository.Update(exam);
		}

		ListFilter IExamServices<Exam>.listFilters()
		{
			throw new NotImplementedException();
		}
	}
}
