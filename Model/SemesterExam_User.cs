using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("SemesterExam_User",Schema ="dbo")]
    public class SemesterExam_User
    {
        [Key]
        public int ID { get; set; }
        
        
        public int Type { get; set; }
        public virtual User User { get; set; }
        public virtual SemesterExam SemesterExam { get; set; }
    }
}
