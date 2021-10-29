using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using School.Data;
using SchoolCore.Service;
using SchoolCore.UserIndex.Dtos;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace School.Web.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserSelfSettingController : Controller
    {
        private readonly ISchoolContracts _schoolContracts;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UserSelfSettingController(ISchoolContracts schoolContracts, IHostingEnvironment hostingEnvironment)
        {
            _schoolContracts = schoolContracts;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 用户修改信息
        /// </summary>
        /// <returns></returns>
        
        [HttpPost]
        public AjaxResult ReceiveHeadImg()
        {
            var data = HttpContext.Request.Form.Files;
            return new AjaxResult("图片上传成功！", AjaxResultType.Success,data);
        }

        [HttpPost]
        //[Authorize] //默认所有角色都可以访问
        //[Authorize(Roles = "办公老师")]
        public async Task<AjaxResult> Settings([FromBody]UserSelfSettingDto dto)
        {
            AjaxResult result = new();
            var webRootPath = _hostingEnvironment.WebRootPath;
            var now = DateTime.Now;
            var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff");
            var strRandom = Convert.ToString(new Random().Next(1000, 9999));
            var filePath = string.Format("/Resource/HeadImgs/{0}/{1}/{2}/",now.ToString("yyyy"),now.ToString("MM"),now.ToString("dd"));
            var path = webRootPath + filePath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            if (dto != null)
            {
                string fileExtensionName = null;
                string finalPath = null;
                if (dto.HeadImg != null)
                {
                    var sr = dto.HeadImg.Substring(dto.HeadImg.IndexOf("/") + 1, 5);
                    if (sr.Contains("jpg"))
                    {
                        fileExtensionName = ".jpg";
                    }
                    else if (sr.Contains("jpeg"))
                    {
                        fileExtensionName = ".jpeg";
                    }
                    else if (sr.Contains("gif"))
                    {
                        fileExtensionName = ".gif";
                    }
                    else if (sr.Contains("png"))
                    {
                        fileExtensionName = ".png";
                    }
                    else
                    {
                        result.Content = "图片非JPG/PNG/JPEG/GIF格式，请检查上传图片格式！";
                    }
                    var fileName = strDateTime + strRandom + fileExtensionName;
                    finalPath = path + fileName;
                    byte[] b = Convert.FromBase64String(dto.HeadImg.Substring(dto.HeadImg.IndexOf(",") + 1));
                    result = await _schoolContracts.Settings(dto, filePath + fileName);
                    if (result.Type == AjaxResultType.Success)
                    {
                        MemoryStream ms = new(b);
                        Bitmap bmp = new(ms);
                        bmp.Save(finalPath, ImageFormat.Bmp);
                        ms.Close();
                        result.Data = bmp;
                    }
                }
                else
                {
                    result = await _schoolContracts.Settings(dto, null);
                }
            }
            return result;
        }
    }
}
