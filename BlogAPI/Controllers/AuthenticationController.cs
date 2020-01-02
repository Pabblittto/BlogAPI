using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogAPI.Models;
using BlogAPI.Services;
using BlogAPI.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogAPI.Controllers
{
    [EnableCors("MyDomain")]
    public class AuthenticationController : Controller// controller responsible for regstration, loggin users and checking available blognames and login
    {
        private readonly SignInManager<BlogUserIdentity> _signInManager;
        private readonly UserManager<BlogUserIdentity> _userManager;
        private readonly MyContext _appContext;
        private readonly FilesService _filesService;

        public AuthenticationController(SignInManager<BlogUserIdentity> signInManager,UserManager<BlogUserIdentity> userManager, MyContext myContext,FilesService fileservice)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appContext = myContext;
            _filesService = fileservice;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);// pobiera obiekt uzytkowanika z bazy 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

            if (user != null && await _userManager.CheckPasswordAsync((BlogUserIdentity)user, model.Password))
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenDescripter = new SecurityTokenDescriptor
                {
                    Issuer = "http://localhost:61177",// kto stworzył token
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name,model.Login),
                        new Claim("ProfilePic",user.ProfilePic)
                    }),
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384Signature),
                    Expires = DateTime.Now.AddMinutes(60)
                };

                var token = tokenhandler.CreateJwtSecurityToken(tokenDescripter);

                return Ok(new { jwt = tokenhandler.WriteToken(token) });
            }

            return Unauthorized();
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var NewUser = new BlogUserIdentity()
                {
                    UserName = model.Login,
                    Email = model.Email,
                    ProfilePic="/src/system/defaultpic.png"
                };

                var ValidatePassword = _userManager.PasswordValidators.First().ValidateAsync(_userManager, NewUser, model.Password);

                if (ValidatePassword.Result.Succeeded)// if password is ok- create user
                {
                    if (model.CreateBlog)// if user wants its own Blog
                    {
                        var NewBlog = new Blog()
                        {
                            BlogName = model.Blogname,
                            Description = model.Description,
                            DateOfCreated = DateTime.Now,
                        };

                        NewBlog.BlogUserIdentity = NewUser;
                        _appContext.Add(NewBlog);
                        //_appContext.SaveChanges();
                       // NewBlog = _appContext.Blogs.FirstOrDefault(ob => ob.BlogUserIdentity == NewUser);
                        NewUser.Blog = NewBlog;
                        //_appContext.Add(NewBlog);

                    }

                    await _userManager.CreateAsync(NewUser, model.Password);

                    await _appContext.SaveChangesAsync();



                    return Ok();
                }
                else
                   return Conflict();

            }
            else
            {
                return Conflict();
            }

        }

        [HttpPost]
        public IActionResult BlogNameCheck([FromBody]SimpleString Name)
        {

            var Blog = _appContext.Blogs.Where(b => b.BlogName == Name.Value).FirstOrDefault();
            if (Blog == null)
                return Ok();
            else
                return Conflict();
        }

        [HttpPost]
        public IActionResult LoginCheck([FromBody]SimpleString Login)
        {
            var user = _userManager.Users.FirstOrDefault(ob => ob.UserName == Login.Value);

            if (user == null)
                return Ok();
            else
                return Conflict();
        }

    }
}