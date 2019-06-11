using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISemesterCustomer<T1, T2, T3>
    {
        IEnumerable<T1> getAll();
        IEnumerable<T2> getListExam(int id);
        IEnumerable<T3> getDetailExam(int id);
    }
}
