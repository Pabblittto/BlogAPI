using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.ViewModel
{
    public class PostViewModel
    {
        public  int PostId { get; set; }
        public DateTime DateOfPost { get; set; }
        public string Thumbnail { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string ContentOne { get; set; }
        public string ContentTwo { get; set; }
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public List<string> PostTags { get; set; }
        public List<Comment> Comments { get; set; }
        public List<string> Images { get; set; }

    }
}
