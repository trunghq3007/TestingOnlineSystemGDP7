﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class ExamCategory
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int CategoryId { get; set; }
        public int Level { get; set; }
        public int NumBerQuestion { get; set; }
        public int MyProperty { get; set; }
    }
}
