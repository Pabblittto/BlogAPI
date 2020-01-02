using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class ImgPost
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Src { get; set; }

}
}
