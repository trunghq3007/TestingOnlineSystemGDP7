using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class ExamFilterModel
	{
		public int Id { get; set; }


		public float TimeTest { get; set; }
		public int QuestionNumber { get; set; }

		public string CreateBy { get; set; }
		public DateTime? CreateAt { get; set; }

		public bool Status { get; set; }
	}
}
