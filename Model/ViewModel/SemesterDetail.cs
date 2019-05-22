using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
  public  class SemesterDetail
    {
        public int ID { get; set; }

        public string SemesterName { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }

        public string Code { get; set; }
        public string status { get; set; }
        public string Creator { get; set; }
        public string NumberInvite { get; set; }
    }
}
