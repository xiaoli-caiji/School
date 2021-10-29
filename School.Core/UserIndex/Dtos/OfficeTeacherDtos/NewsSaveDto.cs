using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace School.Core.UserIndex.Dtos.OfficeTeacherDtos
{
    public class NewsSaveDto
    {
        
        public string NewsName { get; set; }
        public string NewsType { get; set; }
        public string NewsWriter { get; set; }
        public string NewsStartTime { get; set; }
        public string NewsEndTime { get; set; }
        public string NewsUploadTime { get; set; }
        public string NewsCover { get; set; }
        public string NewsCoverType { get; set; }
        public string NewsContent { get; set; }
        public List<IFormFile> NewsPictures { get; set; }
        public IFormFile NewsFile { get; set; }
    }
}
