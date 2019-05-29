using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public	class ViewTest
	{
		
		public int Id { get; set; }
		public string TestName { get; set; }
		public string NameExam { get; set; }
		public string CreateBy { get; set; }
		public int Status { get; set; }
		public float PassScore { get; set; }
		public string SemesterName { get; set; }
		public int ExamId { get; set; }
		public int SemasterExamId { get; set; }
	}
}
