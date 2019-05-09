using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    [Table("SemesterExam",Schema ="dbo")]
    class SemesterExam
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string SemesterName { get; set; }
        [Required]
        public DateTime? StartDay { get; set; }
        [Required]
        public DateTime? EndDay { get; set; }
        
        public string Code { get; set; }
        public int status { get; set; }
        public ICollection<SemesterExam_User> semesterExam_Users { get; set; }
    }
}
