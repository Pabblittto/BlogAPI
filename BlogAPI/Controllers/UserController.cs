using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.ViewModel;
using Microsoft.AspNetCore.Identity;
using BlogAPI.Models;
using BlogAPI.Services;
using Newtonsoft.Json;

namespace BlogAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("MyDomain")]
    public class UserController : ControllerBase// all actions connected with users accounts- modyfing, updating
    {
        private readonly SignInManager<BlogUserIdentity> _signInManager;
        private readonly UserManager<BlogUserIdentity> _userManager;
        private readonly MyContext _appContext;
        private readonly FilesService _filesService;

        public UserController(SignInManager<BlogUserIdentity> signinmanager, UserManager<BlogUserIdentity> usermanager, MyContext context,FilesService fileservice)
        {
            _signInManager = signinmanager;
            _userManager = usermanager;
            _appContext = context;
            _filesService = fileservice;
        }



        public async Task<IActionResult> ChangeAvatar([FromBody]ChangeAvaterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserName = User.Identity.Name;// get id from jwt
                BlogUserIdentity user = await _userManager.FindByNameAsync(UserName);
                byte[] ImageBytes = Convert.FromBase64String(model.Picture);

                await _filesService.AddAvatarToUserAsync(user, ImageBytes);

                user.ProfilePic = "/src/profile/" + user.Id.ToString() + "/Avatar.jpg";
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return Ok();
                else
                    return Conflict();
            }
            else
            {
                return Conflict();
            }
        }

        public async Task<IActionResult> UserTags()
        {
            var UserName = User.Identity.Name;// get user name from jwt

            BlogUserIdentity _user = await _userManager.FindByNameAsync(UserName);

            
            var Tags = _appContext.Tags.Where(ob => ob.Owner == _user);// all tegs which have owner

            List<string> ReturnStringTags = new List<string>();

            foreach (Tag item in Tags)
            {
                ReturnStringTags.Add(item.Name);
            }

            string JsonListResult = JsonConvert.SerializeObject(ReturnStringTags);


            return Ok(JsonListResult);
        }

        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {// if model is valid

                var UserName= User.Identity.Name;// get id from jwt

                BlogUserIdentity _user  = await _userManager.FindByNameAsync(UserName);

                var result = _signInManager.CheckPasswordSignInAsync(_user, model.OldPassword, false);

                if (result.Result.Succeeded)
                {// password was good- change password of this account
                    var ChangingResult = await _userManager.ChangePasswordAsync(_user, model.OldPassword, model.NewPassword);

                    if (ChangingResult.Succeeded)// if it was successfull
                        return Ok();
                    else
                        return Conflict(); 
                }
                else
                {
                    return Unauthorized();
                }

            }
            else
                return Conflict();
        }

        public async Task<IActionResult> GetBlog()
        {
            var UserName = User.Identity.Name;// get user name from jwt

            BlogUserIdentity user = await _userManager.FindByNameAsync(UserName);

            var UsersBlog = _appContext.Blogs.FirstOrDefault(ob => ob.BlogUserIdentity == user);


            if (UsersBlog!=null)
            {
                BlogViewModel responseBlog = new BlogViewModel()
                {
                    BlogName = UsersBlog.BlogName,
                    DateOfCreated = UsersBlog.DateOfCreated,
                    Description = UsersBlog.Description
                };

                string JSONresponse = JsonConvert.SerializeObject(responseBlog);
                return Ok(JSONresponse);

            }
            else
            {
                return Ok();
            }



        }

        public async Task<IActionResult> ChangeDescription(SimpleString NewDescription)
        {
            if (NewDescription.Value.Length <= 1000)
            {
                BlogUserIdentity user = await _userManager.FindByNameAsync(User.Identity.Name);

                Blog UserBlog = user.Blog;

                UserBlog.Description = NewDescription.Value;

                _appContext.Blogs.Update(UserBlog);

                return Ok();
            }
            else
                return Conflict();
        }

    }

        
}