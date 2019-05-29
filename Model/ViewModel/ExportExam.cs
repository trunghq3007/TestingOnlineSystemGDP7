using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public class ExportExam
	{
		public long stt { get; set; }
		public string NameExam { get; set; }
		public int QuestionNumber { get; set; }
		public bool Status { get; set; }
		public string Content { get; set; }
		public int Level { get; set; }
		public string ContentAnswer { get; set; }
		public bool IsTrue { get; set; }
		public string Name { get; set; }
	}
}
