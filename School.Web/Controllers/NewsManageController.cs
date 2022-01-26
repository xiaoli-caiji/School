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
        public AjaxResult UpLoadHtmlPictures([FromForm]List<IFormFile> pictureUrls)
        {
            AjaxResult result = new();
            List<string> imgUrls = new();
            var webRootPath = _hostingEnvironment.WebRootPath;
            var now = DateTime.Now;
            var pictureUrlsPath = string.Format("/Resource/News/HtmlFiles/HtmlPictures/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            
            const string imgFit = ".jpg|.jpeg|.png|.gif" ;

            if(!Directory.Exists(webRootPath + pictureUrlsPath))
            {
                Directory.CreateDirectory(webRootPath + pictureUrlsPath);
            }
            foreach (var pu in pictureUrls)
            {
                string imgName = null;
                var dateStr = DateTime.Now.ToString("yyMMddhhmmssfff");
                var numStr = Convert.ToString(new Random().Next(10, 99));
                var pE = Path.GetExtension(pu.FileName);                

                if(imgFit.IndexOf(pE.ToLower(), StringComparison.Ordinal) <= -1)
                {
                    result.Type = AjaxResultType.Error;
                    result.Content = "请上传.jpg|.jpeg|.png|.gif格式的图片";
                }
                else
                {
                    imgName = dateStr + numStr + pE;
                }
                if(imgName != null)
                {
                    using (FileStream fs = System.IO.File.Create(webRootPath + pictureUrlsPath + imgName))
                    {
                        pu.CopyTo(fs);
                        fs.Flush();
                    }
                    imgUrls.Add("https://localhost:13001" + pictureUrlsPath + imgName);
                } 
            }
            result.Data = imgUrls;
            return result;
        }

        [HttpGet]
        public AjaxResult GetNewsTypes()
        {
            var result = _schoolContracts.GetNewsTypes();
            return result;
        }

        /// <summary>
        /// 新增新闻实体
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
            
            string strRandom = null;

            var FilePath = string.Format("/Resource/News/NewsFiles/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            var FileImgsPath = string.Format("/Resource/News/FileImgs/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            //在线稿件插入图片可以直接看到，因此省略单独存的步骤
            // var HtmlImgsPath = string.Format("/Resource/News/HtmlImgs/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"));
            var filePath = webRootPath + FilePath;
            var fileImgsPath = webRootPath + FileImgsPath;

            string fileName = null;
            string imgsNames = null;
            List<string> imgNames = new();
            Dictionary<IFormFile,string> Imgs = new();

            const string fileFilt = ".docx|.doc|.pdf";
            const string pictureFilt = ".png|.jpg|.jpeg|.gif";

            string pictureFinalPath = null;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if(!Directory.Exists(fileImgsPath))
            {
                Directory.CreateDirectory(fileImgsPath);
            }

            // 判断文稿类型是上传还是在线,对上传的图片列表进行处理
            // 上传文稿
            if (dto.NewsFile != null && dto.NewsContent == null)
            {
                var fE = Path.GetExtension(dto.NewsFile.FileName);
                strRandom = Convert.ToString(new Random().Next(10, 99));
                if (fileFilt.IndexOf(fE.ToLower(), StringComparison.Ordinal) <= -1)
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
                        strRandom = Convert.ToString(new Random().Next(10, 99));
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
                    imgsNames = string.Join(",", imgNames.ToArray());
                }
                
            }
            else if (dto.NewsContent != null && dto.NewsFile == null)
            {
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
                            imgNames.Add(FileImgsPath + strDateTime + strRandom + pE);
                        }
                    }
                    imgsNames = string.Join(",", imgNames.ToArray());
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
                if (fileName != null)
                {
                    result = await _schoolContracts.NewsSave(dto, PicturePath + pictureName, FilePath + fileName, imgsNames);
                }
                else
                {
                    result = await _schoolContracts.NewsSave(dto, PicturePath + pictureName, null, imgsNames);
                }

                // 根据返回内容判断是否存图片等数据
                if (result.Type == AjaxResultType.Success)
                {
                    byte[] b = Convert.FromBase64String(dto.NewsCover.Substring(dto.NewsCover.IndexOf(",") + 1));
                    MemoryStream ms = new(b);
                    ms.Position = 0;
                    Bitmap bmp = new(ms);
                    bmp.Save(pictureFinalPath, ImageFormat.Bmp);
                    ms.Close();
                }
            }
            else
            {
                if (fileName != null)
                {
                    result = await _schoolContracts.NewsSave(dto, FilePath + fileName, imgsNames);
                }
                else
                {
                    result = await _schoolContracts.NewsSave(dto, null, imgsNames);
                }
            }
            
            if (result.Type == AjaxResultType.Success)
            {
                if (fileName != null)
                {
                    using (FileStream fs = System.IO.File.Create(filePath + fileName))
                    {
                        dto.NewsFile.CopyTo(fs);
                        fs.Flush();
                    }
                }
                if(Imgs != null)
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

        ///<summary>
        ///编辑修改已有新闻
        ///</summary>
        [HttpPost]
        public async Task<AjaxResult> NewsEdit( [FromForm] NewsSaveDto dto)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            AjaxResult result = new();
            var newsId = dto.NewsId;
            if(dto.NewsCoverType == "图片")
            {

            }
            result = await _schoolContracts.NewsEdit(dto, newsId, webRootPath);
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

        [HttpPost]
        public AjaxResult DeleteNews(int? newsId)
        {
            AjaxResult result = new();

            return result;
        }
    }
}
