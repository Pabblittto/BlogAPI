using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Models;
using BlogAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BlogAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controllers
{
    [EnableCors("MyDomain")]
    public class PostsController : Controller//controller for getting posts, adding comments to them etc.
    {

        private readonly SignInManager<BlogUserIdentity> _signInManager;
        private readonly UserManager<BlogUserIdentity> _userManager;
        private readonly MyContext _appContext;
        private readonly FilesService _fileService;


        public PostsController(SignInManager<BlogUserIdentity> signinmanager, UserManager<BlogUserIdentity> usermanager, MyContext context,FilesService fileService)
        {
              _signInManager = signinmanager;
            _userManager = usermanager;
            _appContext = context;
            _fileService = fileService;
        }



        
        public IActionResult CertainPost([FromBody] SimpleString _id)
        {
            int id = Int32.Parse(_id.Value);

            var post =  _appContext.Posts.FirstOrDefault(ob => ob.Blog.BlogId==2).Include(ob=>ob.Comments).FirstOrDefault(Obpost => Obpost.PostId == id);

            if (post == null)
                return Conflict();
            else
            {
                var DateOfPost = new DateTime(post.DateOfPost);

                var ResponsePost = new PostViewModel()
                {
                    DateOfPost = DateOfPost,
                    PostId = post.PostId,
                    Thumbnail = post.Thumbnail,
                    Title = post.Title,
                    ContentOne = post.ContentOne,
                    ContentTwo = post.ContentTwo,
                    BlogId = post.Blog.BlogId,
                    BlogName = post.Blog.BlogName,
                    Comments = post.Comments.ToList()
                };

                if (post.Images != null)
                {
                    foreach(var item in post.Images)
                    {
                        ResponsePost.Images.Add(item.Src);
                    }
                }

                if (post.PostTags != null)
                {
                    foreach (var item in post.PostTags)
                    {
                        ResponsePost.PostTags.Add(item.Tag.Name);
                    }
                }

                string jsonResult = JsonConvert.SerializeObject(ResponsePost);

                return Ok(jsonResult);
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePost([FromBody]SimpleString id)
        {
            int PostID = Int32.Parse(id.Value);

            BlogUserIdentity user = await _userManager.FindByNameAsync(User.Identity.Name);

            var UsersBlog = await _appContext.Blogs.FirstOrDefaultAsync(ob => ob.BlogUserIdentity == user);


            var postToRemove = _appContext.Posts.FirstOrDefault(ob => ob.PostId == PostID);
                
               

            if (postToRemove != null && postToRemove.Blog==UsersBlog)
            {
                _appContext.Posts.Remove(postToRemove);
                await _appContext.SaveChangesAsync();
                await  _fileService.DeletePostDirectory(user, postToRemove.Title, postToRemove.DateOfPost);
                return Ok();
            }
            else// if post was not found or 
            {
                return Conflict();
            }
        }

 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddPost([FromBody] AddPostModelView model)
        {
            var ValidationContext = new ValidationContext(model.NewPost);


            if (Validator.TryValidateObject(model.NewPost, ValidationContext, null))// zamiast null można dać liste która zostanie zapełniona błędami
            {

                var DateNow = DateTime.Now;
                BlogUserIdentity user = await _userManager.FindByNameAsync(User.Identity.Name);

                var UserBlog = _appContext.Blogs.FirstOrDefault(ob=>ob.BlogUserIdentity==user);

                Post NewPost = new Post()
                {
                    
                    Title = model.NewPost.Title,
                    Blog = UserBlog,
                    ContentOne = model.NewPost.ContentOne,
                    ContentTwo = model.NewPost.ContentTwo,
                    Comments = null,
                    DateOfPost = DateNow.Ticks,
                    Images = new List<ImgPost>(),
                    PostTags = new List<PostTag>()
                };

                if (model.Thumbnail.name != "")
                {
                    NewPost.Thumbnail = "/Src/Profile/" + user.Id.ToString() + "/" + model.NewPost.Title + DateNow.Ticks.ToString() + "/" + model.Thumbnail.name;
                    await _fileService.AddThumbnailToPostAsynch(user, model.NewPost.Title, model.Thumbnail, DateNow.Ticks);
                }
                else
                    NewPost.Thumbnail = "/Src/System/noimage.png";


                //_appContext.Add(NewPost);// add new 
                //_appContext.SaveChanges();// save changes so post will get id

                //NewPost = _appContext.Posts.FirstOrDefault(post => post.Title == model.NewPost.Title);// get post from database
                
                await _fileService.AddImagesToPostAsynch(user, model.NewPost.Title, model.Images, DateNow.Ticks);

                List<Task> CreatingTags = new List<Task>();
                foreach (var item in model.NewPost.PostTags)
                {
                    CreatingTags.Add(CheckTag(item, user));
                }
                await Task.WhenAll(CreatingTags);
                // tags was created

                List<Tag> TagLists = new List<Tag>();
                foreach (var item in model.NewPost.PostTags)
                {
                    var tagFromBase = _appContext.Tags.FirstOrDefault(ob => ob.Name == item.ToLower());
                    if (tagFromBase != null)
                        TagLists.Add(tagFromBase);
                }
                // get all tags from  database 

                List<PostTag> PostTagConnectorList = new List<PostTag>();

                foreach (var item in TagLists)
                {
                    var NewConnector = new PostTag();
                    NewConnector.Post = NewPost;
                    NewConnector.Tag = item;

                    item.PostTags = new List<PostTag>();


                    NewPost.PostTags.Add(NewConnector);
                    item.PostTags.Add(NewConnector);
                    PostTagConnectorList.Add(NewConnector);
                }
                // objects which connects Tags with posts (many to many connection)


                //Creatng img objects
                List<ImgPost> ImageList = new List<ImgPost>();
                var path = "/Src/Profile/" + user.Id + "/" + model.NewPost.Title + "/";// base of all images in post

                foreach (var item in model.Images)
                {
                    var imagepath = path + item.name;
                    var TmpImgPost = new ImgPost();

                    TmpImgPost.Src = imagepath;
                    TmpImgPost.Post = NewPost;

                    NewPost.Images.Add(TmpImgPost);
                    ImageList.Add(TmpImgPost);// add to list which will save it to database
                }


                try
                {
                    await _appContext.AddAsync(NewPost);//add post to database
                    await _appContext.AddRangeAsync(PostTagConnectorList);
                    await _appContext.AddRangeAsync(ImageList);
                }
                catch(Exception e){
                    return Conflict();
                }

                await _appContext.SaveChangesAsync();
                
                return Ok();
            }
            else
                return Conflict();





        }

        /// <summary>
        /// Checks if certain tag exists in base, if so - create Tag element in base and save the base
        /// </summary>
        /// <returns></returns>
        private Task CheckTag(string TagName, BlogUserIdentity creator)
        {
            var result = new List<Tag>();
            result =_appContext.Tags.Where(ob => ob.Name == TagName.ToLower()).ToList<Tag>();
            if(result.Count==0)// if there is no tag create one and save it in database
            {
                Tag NewTag = new Tag()
                {
                    Name=TagName.ToLower(),
                    Owner=creator,
                    PublicTag=true
                };

                _appContext.Add(NewTag);
                _appContext.SaveChanges();
            }
            return Task.CompletedTask;

        }

        // in Users Blog panel
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPartOfPosts([FromBody] RangeNumbers numbers)
        {
            BlogUserIdentity user = await _userManager.FindByNameAsync(User.Identity.Name);

            var UsersBlog = _appContext.Blogs.FirstOrDefault(ob=>ob.BlogUserIdentity==user);

            //List<Post> PartOfPosts = _appContext.Posts.Where(ob => ob.Blog == UsersBlog);
            List<Post> PartOfPosts = _appContext.Posts.Where(ob=>ob.Blog== UsersBlog)
                                        .Include(img=> img.Images)
                                        .Include(pt=> pt.PostTags)
                                        .ThenInclude(t=>t.Tag)
                                        .ToList();


            if (PartOfPosts != null)
            {

                PartOfPosts.Sort((p, q) => p.DateOfPost.CompareTo(q.DateOfPost)*-1);

                if(PartOfPosts.Count> numbers.end - numbers.start)// if there is more posts than can fit on site
                {
                    if(PartOfPosts.Count> numbers.end)
                        PartOfPosts = PartOfPosts.GetRange(numbers.start, numbers.end - numbers.start+1);
                    else
                        PartOfPosts = PartOfPosts.GetRange(numbers.start,PartOfPosts.Count-numbers.start);

                }

                // one need here simplified posts without comments and images

                List<PostViewModel> SimplifiedPosts = new List<PostViewModel>();

                foreach (var item in PartOfPosts)
                {
                    var DateOfPost = new DateTime(item.DateOfPost);

                    PostViewModel NewPostModel= new PostViewModel()
                        {
                            BlogId = item.Blog.BlogId,
                            BlogName = item.Blog.BlogName,
                            ContentOne = item.ContentOne,
                            ContentTwo = item.ContentTwo,
                            DateOfPost = DateOfPost,
                            Thumbnail = item.Thumbnail,
                            Title = item.Title,
                            PostTags = new List<string>(),
                            PostId=item.PostId
                        };
                    
                    foreach(var tag in item.PostTags)
                    {
                        NewPostModel.PostTags.Add(tag.Tag.Name);
                    }

                    SimplifiedPosts.Add(NewPostModel);

                }

                string jsonResult = JsonConvert.SerializeObject(SimplifiedPosts);

                return Ok(jsonResult);
            }
            else
            {
                return Ok();
            }


        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetNumberOfPosts()
        {
            BlogUserIdentity user = await _userManager.FindByNameAsync(User.Identity.Name);
            var USersBlog= await _appContext.Blogs.FirstOrDefaultAsync(ob => ob.BlogUserIdentity == user);

            int ammount = _appContext.Posts.Where(ob => ob.Blog == USersBlog).Count();// counts number of all posts
            return Ok(ammount);

        }




    }

}