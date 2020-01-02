using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
    public class Tag// tags wchich help 
    {
        [Key]
        public int TagId { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        //public int OwnerId { get; set; }// admin's Tags can not be deleted
        [ForeignKey("OwnerID")]
        public BlogUserIdentity Owner { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

        [Required]
        public bool PublicTag { get; set; }
    }
}
