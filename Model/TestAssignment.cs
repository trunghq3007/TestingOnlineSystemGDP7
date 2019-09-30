using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class TestAssignment
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		[ForeignKey("Test")]
		public int TestId { get; set; }
		public Test Test { get; set; }
		public User User { get; set; }
	}
}
