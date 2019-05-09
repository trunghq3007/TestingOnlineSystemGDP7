using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [Table("Tag",Schema ="dbo")]
    class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Status { get; set; } 
    }
}
