using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Core.UserIndex.Dtos.StudentsDtos;
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
        [HttpPost]
        public async Task<AjaxResult> BrowseCourse([FromBody] CourseInputDto course)
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
        public async Task<AjaxResult> ChoosenCourse([FromBody]CourseChooseDto dto)
        {
            AjaxResult result = new(); 
            if(dto.CourseCode != null)
            {
                result = await _schoolContracts.ChooseCourses(dto);
            }           
            return result;
        }

        ///<summary>
        ///获取已选课程
        ///</summary>
        [HttpGet]
        public async Task<AjaxResult> HasChoosen([FromBody] UserInputDto user)
        {
            AjaxResult result = new();
            if(user.UserCode!= null)
            {
                result = await _schoolContracts.HasChoosen(user);
            }
            return result;
        }


        /// <summary>
        /// 获取成绩单
        /// 课程相关信息都是List,可以遍历
        /// </summary>
        /// <returns>成绩单页面</returns>
        [HttpGet]
        public async Task<AjaxResult> GetReportCard(string urlCode)
        {
            var reportsCards = await _schoolContracts.GetReportCard(urlCode);
            return reportsCards;
        }


    }
}
