using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogAPI.Models;
using BlogAPI.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyDomain")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SignInManager<BlogUserIdentity> _signInManager;
        private readonly UserManager<BlogUserIdentity> _userManager;
        private readonly MyContext _appContext;

        public ValuesController(SignInManager<BlogUserIdentity> signinmanager, UserManager<BlogUserIdentity> usermanager, MyContext context)
        {
            _signInManager = signinmanager;
            _userManager = usermanager;
            _appContext = context;

        }

        public Object Test()
        {
            string hello = "hello from controller";
            return hello;
        }



    }
}
