using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace School.Core.UserIndex.Dtos.OfficeTeacherDtos
{
    public class NewsSaveDto
    {
        public int? NewsId { get; set; }
        public string NewsName { get; set; }
        public string NewsType { get; set; }
        public string NewsWriter { get; set; }
        public DateTime NewsStartTime { get; set; }
        public DateTime NewsEndTime { get; set; }
        public DateTime NewsUploadTime { get; set; }
        public string NewsCover { get; set; }
        public string NewsCoverType { get; set; }
        public string NewsContent { get; set; }
        public List<IFormFile> NewsPictures { get; set; }
        public IFormFile NewsFile { get; set; }
        public List<string> DeletePicture { get; set; }
    }
}
