using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public bool CreateBlog { get; set; }

        [MaxLength(200)]
        public string Blogname { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

    }
}
