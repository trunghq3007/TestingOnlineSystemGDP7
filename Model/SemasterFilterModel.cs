using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class SemasterFilterModel
    {
        public int ID { get; set; }
        public string SemesterName { get; set; }
        public DateTime? StarDay { get; set; }
        public DateTime? EndDay { get; set; }
        public string Code { get; set; }
        public int status { get; set; }
    }
}
