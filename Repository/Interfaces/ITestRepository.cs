using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Repository.Interfaces
{
 public	interface ITestRepository<T>
	{
		IEnumerable<ViewTest> GetAll();
		IEnumerable<ViewTest> Search(string searchString);
		IEnumerable<T> Filter(T t);
		int Insert(T t);
		int Update(T t);
		int Delete(int id);
		T Export_exam(int id);
		List<ViewDetailTest> GetById(int id);
        T GetByTestId(int id);
    }
}
