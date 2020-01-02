using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [MaxLength(200)]
        public string BlogName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOfCreated { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("User")]
        public BlogUserIdentity BlogUserIdentity { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
