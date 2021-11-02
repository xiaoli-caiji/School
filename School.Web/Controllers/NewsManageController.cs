using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Core.UserIndex.Dtos.OfficeTeacherDtos;
using School.Data;
using SchoolCore.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Web.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class NewsManageController : Controller
    {
        private readonly ISchoolContracts _schoolContracts;
        private readonly IHostingEnvironment _hostingEnvironment;
        public NewsManageController(ISchoolContracts schoolContracts, IHostingEnvironment hostingEnvironment)
        {
            _schoolContracts = schoolContracts;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public AjaxResult UpLoadHtmlPictures(List<string> pictureUrls)
        {
            AjaxResult result = new();
            var webRootPath = _hostingEnvironment.WebRootPath;
            var now = DateTime.Now;
            var pictureUrlsPath = string.Format("/Resource/News/HtmlFiles/HtmlPictures/{0}/{1}/{2}", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            foreach (var pu in pictureUrls)
            {

            }
            return result;
        } 

        [HttpGet]
        public AjaxResult GetNewsTypes()
        {
            var result = _schoolContracts.GetNewsTypes();
            return result;
        }

        /// <summary>
        /// 默认在线编辑和投稿二选一，不具备在线稿件+附件功能
        /// 默认NewsCover不为空，前端判断非图片就传标题给后端
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> NewsSave([FromForm] NewsSaveDto dto)
        {
            AjaxResult result = new();
            var webRootPath = _hostingEnvironment.WebRootPath;
            // 为生成随机名字的文件做准备
            var now = DateTime.Now;
            var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff");
            var strRandom = Convert.ToString(new Random().Next(10, 99));

            var FilePath = string.Format("/Resource/News/NewsFiles/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            var FileImgsPath = string.Format("/Resource/News/FileImgs/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            var HtmlPath = string.Format("/Resource/News/HtmlFiles/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            // var HtmlImgsPath = string.Format("/Resource/News/HtmlImgs/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            var filePath = webRootPath + FilePath;
            var fileImgsPath = webRootPath + FileImgsPath;
            var htmlPath = webRootPath + HtmlPath;
            // var htmlImgsPath = webRootPath + HtmlImgsPath;

            string fileName = null;
            string imgsNames = null;
            string htmlName = null;
            List<string> imgNames = new();
            Dictionary<IFormFile,string> Imgs = new();

            const string fileFilt = ".docx|.doc|.pdf";
            const string pictureFilt = ".png|.jpg|.jpeg|.gif";

            string pictureFinalPath = null;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            if (!Directory.Exists(htmlPath))
            {
                Directory.CreateDirectory(htmlPath);
            }
            if(!Directory.Exists(fileImgsPath))
            {
                Directory.CreateDirectory(fileImgsPath);
            }

            // 判断文稿类型是上传还是在线,对上传的图片列表进行处理
            if (dto.NewsFile != null && dto.NewsContent == null)
            {
                var fE = Path.GetExtension(dto.NewsFile.FileName);
                if(fileFilt.IndexOf(fE.ToLower(), StringComparison.Ordinal) <= -1)
                {
                    result.Content = "请上传.dox/.docx/.pdf格式的文件！";
                }
                else
                {
                    fileName = strDateTime + strRandom + fE;
                }

                if (dto.NewsPictures != null)
                {
                    foreach (var p in dto.NewsPictures)
                    {
                        var pE = Path.GetExtension(p.FileName);
                        if (pictureFilt.IndexOf(pE.ToLower(), StringComparison.Ordinal) <= -1)
                        {
                            result.Content = "请上传.jpg/.jpeg/.png/.gif格式的文件！";
                        }
                        else
                        {
                            // fileAndName.Add(p, strDateTime + strRandom + pE);
                            Imgs.Add(p, fileImgsPath + strDateTime + strRandom + pE);
                            imgNames.Add(FileImgsPath + strDateTime + strRandom + pE);
                        }
                    }
                    imgsNames = imgNames.ToString();
                }
                
            }
            else if (dto.NewsContent != null && dto.NewsFile == null)
            {
                htmlName = strDateTime + strRandom + ".html";
                // System.IO.File.AppendAllText(htmlPath + htmlName, dto.NewsContent, Encoding.Default);
                if (dto.NewsPictures != null)
                {
                    foreach (var p in dto.NewsPictures)
                    {
                        var pE = Path.GetExtension(p.FileName);
                        if (pictureFilt.IndexOf(pE.ToLower(), StringComparison.Ordinal) <= -1)
                        {
                            result.Content = "请上传.jpg/.jpeg/.png/.gif格式的文件！";
                        }
                        else
                        {
                            // fileAndName.Add(p, strDateTime + strRandom + pE);
                            imgNames.Add(strDateTime + strRandom + pE);
                        }
                    }
                    imgsNames = imgNames.ToString();
                }
            }
            else
            {
                result.Type = AjaxResultType.Error;
                result.Content = "未发现新闻稿件/在线内容！请确认是否上传！";
            }

            //判断封面内容
            if (dto.NewsCoverType == "图片")
            {
                var PicturePath = string.Format("/Resource/News/CoverPicture/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
                var picturePath = webRootPath + PicturePath;
                string pictureExtensionName = null;
                
                if (!Directory.Exists(picturePath))
                {
                    Directory.CreateDirectory(picturePath);
                }
                var sr = dto.NewsCover.Substring(dto.NewsCover.IndexOf("/") + 1, 5);
                if (sr.Contains(".jpg"))
                {
                    pictureExtensionName = ".jpg";
                }
                else if (sr.Contains("jpeg"))
                {
                    pictureExtensionName = ".jpeg";
                }
                else if (sr.Contains("gif"))
                {
                    pictureExtensionName = ".gif";
                }
                else if (sr.Contains("png"))
                {
                    pictureExtensionName = ".png";
                }
                else
                {
                    result.Content = "图片非JPG/PNG/JPEG/GIF格式，请检查上传图片格式！";
                }
                var pictureName = strDateTime + strRandom + pictureExtensionName;
                pictureFinalPath = picturePath + pictureName;

                //判断传在线地址还是文件地址
                if (htmlName == null)
                {
                    result = await _schoolContracts.NewsSave(dto, PicturePath + pictureName, FilePath + fileName, imgsNames);
                }
                else
                {
                    result = await _schoolContracts.NewsSave(dto, PicturePath + pictureName, HtmlPath + htmlName, imgsNames);
                }

                // 根据返回内容判断是否存图片等数据
                if (result.Type == AjaxResultType.Success)
                {
                    byte[] b = Convert.FromBase64String(dto.NewsCover.Substring(dto.NewsCover.IndexOf(",") + 1));
                    MemoryStream ms = new(b);
                    Bitmap bmp = new(ms);
                    bmp.Save(pictureFinalPath, ImageFormat.Bmp);
                    ms.Close();
                }
            }
            else
            {
                if (htmlName == null)
                {
                    result = await _schoolContracts.NewsSave(dto, FilePath + fileName, imgsNames);
                }
                else
                {
                    result = await _schoolContracts.NewsSave(dto, HtmlPath + htmlName, imgsNames);
                }
            }
            
            if (result.Type == AjaxResultType.Success)
            {
                if (htmlName == null)
                {
                    using (FileStream fs = System.IO.File.Create(filePath + fileName))
                    {
                        dto.NewsFile.CopyTo(fs);
                        fs.Flush();
                    }
                }
                else
                {
                    System.IO.File.AppendAllText(htmlPath + htmlName, dto.NewsContent, Encoding.UTF8);
                }
                //result.Data = bmp;
                if(Imgs!=null)
                {
                    foreach (var img in Imgs)
                    {
                        using (FileStream fs2 = System.IO.File.Create(img.Value))
                        {
                            img.Key.CopyTo(fs2);
                            fs2.Flush();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据封面内容分成两种，展示在页面的不同位置
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public AjaxResult GetNews()
        {
            AjaxResult result;
            result = _schoolContracts.GetNews();
            return result;
        }

        [HttpGet]
        public AjaxResult ShowNews()
        {
            AjaxResult result;
            result = _schoolContracts.ShowNews();
            return result;
        }
    }
}
