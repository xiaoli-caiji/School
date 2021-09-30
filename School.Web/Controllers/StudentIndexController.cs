using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Data;
using SchoolCore.Dtos;
using SchoolCore.Service;

namespace School.Web.Area.Students.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class StudentIndexController : Controller
    {
        /// <summary>
        /// 结合菜单给出各个业务模块
        /// 暂拟：查询+选课、录入成绩、成绩单+绩点、个人信息查看+修改（密码）、
        /// </summary>
        private readonly ISchoolContracts _schoolContracts;
        public StudentIndexController(ISchoolContracts schoolContracts)
        {
            _schoolContracts = schoolContracts;
        }

        ///<summary>
        ///查询课表
        ///有下拉选择学院、输入老师名、输入课程名三种查询框，
        ///注意传进来的是实体，返回的是列表
        ///</summary>
        [HttpGet]
        public async Task<AjaxResult> BrowseCourse(CourseOutputDto course)
        {
            var courses = await _schoolContracts.GetCourses(course);
            return courses;
        }

        /// <summary>
        /// 选课，页面要有课程和可选控件     
        /// </summary>
        /// <param name="course">Course类的实体</param>
        /// <returns>选课结果弹框+页面</returns>
        [HttpPost]
        public async Task<AjaxResult> ChoosenCourse(string courseCode)
        {
            AjaxResult result = new(); 
            if(courseCode!=null)
            {
                result = await _schoolContracts.ChooseCourses(courseCode);
            }         
            return result;
        }
        /// <summary>
        /// 获取成绩单
        /// 课程相关信息都是List,可以遍历
        /// </summary>
        /// <returns>成绩单页面</returns>
        [HttpGet]
        public async Task<AjaxResult> GetReportCard()
        {
            var reportsCards = await _schoolContracts.GetReportCard();
            return reportsCards;
        }


    }
}
