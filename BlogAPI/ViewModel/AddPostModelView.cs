using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.ViewModel
{
    public class AddPostModelView
    {
        public PostViewModel NewPost { get; set; }
        public SimpleImageObject Thumbnail { get; set; }
        public List<SimpleImageObject> Images { get; set; }
    }
}
