using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
    public class BlogUserIdentity : IdentityUser<string>
    {
        public Blog Blog { get; set; }
       
        
        public string ProfilePic { get; set; }

        public static implicit operator Task<object>(BlogUserIdentity v)
        {
            throw new NotImplementedException();
        }
    }
}
