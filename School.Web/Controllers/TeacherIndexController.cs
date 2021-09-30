using Microsoft.AspNetCore.Mvc;
using School.Core.UserIndex.Dtos;
using SchoolCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// 用户=》老师=》老师备注=》课=》学生
        /// 先把课上的学生都列出来，要有学号
        /// </summary>
        /// <returns>该课的学生列表(姓名、学号等、成绩)</returns>
        [HttpPost]
        public async Task<IActionResult> InputReportCard(List<InputReportCardsDto> reportCards)
        {
            var result = await _schoolContracts.InputReportCard(reportCards);
            var script = string.Format("<script>alert('{0}')</script>;location.href = self.location.href", result);
            return Content(script, "text/html");
        }
    }
}
