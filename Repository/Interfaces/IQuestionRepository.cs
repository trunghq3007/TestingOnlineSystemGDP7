 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    public interface IQuestionRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(SearchPaging item);
        IEnumerable<T> Filter(T t);
        int Insert(T t);
        int Update(T t);
        int Delete(int id);
        T GetById(int id);
        IEnumerable<T> Filter(QuestionFillterModel t);
        Category getCategoryByName(string cateName);
        string Import(List<T> list);
    }
}
