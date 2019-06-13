using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
 public	class ExamFilterModel
	{

		public float? TimeTest { get; set; }
		public int? QuestionNumber { get; set; }

		public string CreateBy { get; set; }
		public DateTime? CreateAt { get; set; }
         public string TypeExam { get; set; }
		public int? Status { get; set; }
	}
}
