using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    [Table("Roles")]
    public class Role
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}