using BlogAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BlogAPI.ViewModel;

namespace BlogAPI.Services
{
    public class FilesService
    {
        string currentPath;

        public FilesService()
        {
            currentPath = Directory.GetCurrentDirectory();
        }

        public async Task AddAvatarToUser(BlogUserIdentity user,Byte[] ImageBytes)// creates dictionary, updates user image, 
        {
            //string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);// get current path
            string PersonalPath = currentPath + "\\wwwroot\\Src\\Profile\\"+ user.Id.ToString();// add needed directories + user directory 

            Directory.CreateDirectory(PersonalPath);//create all needed directories- if dont exists
            PersonalPath += "\\Avatar.jpg";
            await File.WriteAllBytesAsync(PersonalPath, ImageBytes);
        }

        public Task AddAvatarToUserAsync(BlogUserIdentity user, Byte[] ImageBytes)
        {
            return Task.Run(() => AddAvatarToUser(user, ImageBytes));
        }

         public async Task AddImagesToPostAsynch(BlogUserIdentity user,string PostName,List<SimpleImageObject> Images, long NumberOfTicks)
        {
            string TmpPath = currentPath + "\\wwwroot\\Src\\Profile\\" + user.Id.ToString() + "\\" + PostName + NumberOfTicks.ToString();
            List<Task> ListOfTasks = new List<Task>();

            Directory.CreateDirectory(TmpPath);
            
                    foreach (var item in Images)
                    {
                        ListOfTasks.Add(SaveImg(TmpPath, item));
                    }

            await Task.WhenAll(ListOfTasks);
        }
    

        public async Task DeletePostDirectory(BlogUserIdentity user, string PostName,long NumberOfTicks )
        {
            string DeletePath = currentPath + "\\wwwroot\\Src\\Profile\\" + user.Id.ToString() + "\\" + PostName + NumberOfTicks.ToString();
            // path to directory containing all images for post

            await Task.Run(() => Directory.Delete(DeletePath, true));
        } 

        /// <summary>
        /// Save Thumbnail for certain post
        /// </summary>
        /// <param name="user"></param>
        /// <param name="PostName"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
        public async Task AddThumbnailToPostAsynch(BlogUserIdentity user, string PostName, SimpleImageObject Image,long NumberOfTicks)
        {
            string Path = currentPath + "\\wwwroot\\Src\\Profile\\" + user.Id.ToString()+"\\"+PostName+ NumberOfTicks.ToString();// path to users folder

            Directory.CreateDirectory(Path);// create needed directories
            Path += "\\" + Image.name;
            await File.WriteAllBytesAsync(Path, DecodeImage(Image));
        }


        private Task SaveImg(string basepath,SimpleImageObject ImgObj)
        {
            basepath += "\\" + ImgObj.name;
            File.WriteAllBytesAsync(basepath, DecodeImage(ImgObj));
            return Task.CompletedTask;
        }


        /// <summary>
        /// Returns bytes which represent a image file
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] DecodeImage(SimpleImageObject obj)
        {
            string PureBlob = obj.value.Split(',')[1];

            return Convert.FromBase64String(PureBlob);
        }
    }
}
