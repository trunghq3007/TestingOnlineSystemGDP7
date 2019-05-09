using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public class test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Exam_id { get; set; }
        [Required]
        public int Semaster_exam_id { get; set; }
        
        public int Status { get; set; }
        [Required]
        public DateTime Start_date { get; set; }
        [Required]
        public DateTime End_date { get; set; }
        public string Create_by { get; set; }
        public float Pass_score { get; set; }
        public string Test_name  { get; set; }
        public int number_time { get; set; }
        public float time_test { get; set; }
        
    }
}
