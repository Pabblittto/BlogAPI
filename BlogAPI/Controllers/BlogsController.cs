using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [EnableCors("MyDomain")]
    public class BlogsController : Controller// controller for viewing Blogs- its content and modyfing them by owners
    {

    }
}