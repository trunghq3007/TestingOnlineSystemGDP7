using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Group Name is required"), MinLength(2), MaxLength(20)]
        public string GroupName { get; set; }
        [Required(ErrorMessage = "Creator is required"), MinLength(2), MaxLength(50)]
        public string Creator { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        //public bool IsActive { get; set; }
        public  ICollection<UserGroup> UserGroups { get; set; }

    }
}
