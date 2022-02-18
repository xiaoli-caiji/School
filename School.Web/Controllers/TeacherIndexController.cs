using Microsoft.AspNetCore.Mvc;
using School.Core.UserIndex.Dtos;
using School.Core.UserIndex.Dtos.TeachingTeacherDtos;
using School.Data;
using SchoolCore.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Area.User.TeachingTeacher.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class TeacherIndexController : Controller
    {
        private readonly ISchoolContracts _schoolContracts;
        public TeacherIndexController(ISchoolContracts schoolContracts)
        {
            _schoolContracts = schoolContracts;
        }
        
        [HttpGet]
        public async Task<AjaxResult> CourseAndStudent(string teacherCode)
        {
            var result = await _schoolContracts.CourseAndStudent(teacherCode);
            return result;
        }

        /// <summary>
        /// 用户=》老师=》老师备注=》课=》学生
        /// 先把课上的学生都列出来，要有学号
        /// </summary>
        /// <returns>该课的学生列表(姓名、学号等、成绩)</returns>
        [HttpPost]
        public async Task<List<AjaxResult>> InputReportCard([FromBody]List<InputReportCardsDto> reportCards)
        {
            var result = await _schoolContracts.InputReportCard(reportCards);
            return result;
        }

        [HttpGet]
        public AjaxResult GetCoursesByTeacher()
        {
            var result = _schoolContracts.GetCoursesByTeacher();
            return result;
        }

        [HttpPost]
        public async Task<AjaxResult> WinCourse([FromBody] StartCourseSelectionDto dto)
        {
            AjaxResult result = new();
            result = await _schoolContracts.WinCourse(dto);
            return result;
        }

        [HttpPost]
        public async Task<AjaxResult> CourseSelectionClose()
        {
            var result = await _schoolContracts.CourseSelectionClose();
            return result;
        }
    }
}
