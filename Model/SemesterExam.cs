﻿using System;
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
    public class SemesterExam
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Semester Name is required")]
        public string SemesterName { get; set; }      
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        
        public string Code { get; set; }      
        public int status { get; set; }
        public ICollection<SemesterExam_User> semesterExam_Users { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        
    }
}