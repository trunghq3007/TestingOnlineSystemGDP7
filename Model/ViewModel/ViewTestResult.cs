using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public class ViewTestResult
	{
		public int Id { get; set; }
		public string TestName { get; set; }
		public float TestTime { get; set; }
		public string Content { get; set; }
		public int Level { get; set; }
		public string Suggestion { get; set; }
		public int Type { get; set; }
		public string Media { get; set; }
		public int Status { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public AnswerModel Answer { get; set; }
	}
}
