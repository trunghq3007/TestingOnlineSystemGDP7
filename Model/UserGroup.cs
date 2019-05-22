using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserGroup
    {
        [Key]
        public int UserGroupId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
