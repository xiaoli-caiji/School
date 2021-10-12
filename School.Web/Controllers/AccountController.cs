using Microsoft.AspNetCore.Mvc;
using SchoolCore.Dtos;
using SchoolCore.Service;
using System.Web;
using System.Threading.Tasks;
using School.Data;

namespace School.Web.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly ISchoolContracts _schoolContracts;
        public LoginController(ISchoolContracts schoolContracts)
        {
            _schoolContracts = schoolContracts;
        }
        [HttpPost]
        public async Task<AjaxResult> Login([FromBody] UserInputDto user)
        {
            AjaxResult userRoles = new();
            if (user.UserCode != null && user.Password != null)
            {
                userRoles = await _schoolContracts.UserLogin(user);
                //if (userRoles.Data.ToString().Length >= 2)
                //{
                //    return RedirectToAction("教师+办公室老师的主页");
                //}
                //else
                //{
                //    switch (userRoles.ToString())
                //    {
                //        case "TeachingTeacher":
                //            return RedirectToAction("教师主页");
                //        case "OfficeTeacher":
                //            return RedirectToAction("办公室老师主页");
                //        case "Student":
                //            return RedirectToAction("学生主页");
                //        case "OtherStuff":
                //            return RedirectToAction("其他职工主页");
                //    }
                //}
            }

            return userRoles;
        }

        [HttpPost]
        public async Task<AjaxResult> Logout()
        {
            var result = await _schoolContracts.UserLogout();
            return result;
        }

    }
}
