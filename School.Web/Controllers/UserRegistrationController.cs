using Microsoft.AspNetCore.Mvc;
using SchoolCore.Service;
using SchoolCore.UserIndex.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserRegistrationController : Controller
    {
        private readonly ISchoolContracts _schoolContracts;
        public UserRegistrationController(ISchoolContracts schoolContracts)
        {
            _schoolContracts = schoolContracts;
        }
        [HttpPost]
        public async Task<IActionResult> StudentRegistration(StudentRegistrationDto student)
        {//弹窗的内容：Content，数据：data
            var registrationResult = await _schoolContracts.StudentRegistration(student);
            return View(registrationResult);

        }

        [HttpPost]
        public async Task<IActionResult> TeachingTeacherRegistration(TeachingTeacherRegistrationDto teachingTeacher)
        {
            var registrationResult = await _schoolContracts.TeachingTeacherRegistration(teachingTeacher);
            string script = string.Format("<script>aleart('{0}');location.href = self.location.href", registrationResult);
            return Content(script, "text/html");

        }

        [HttpPost]
        public async Task<IActionResult> OfficeTeacherRegistration(OfficeTeacherRegistrationDto officeTeacher)
        {
            var registrationResult = await _schoolContracts.OfficeTeacherRegistration(officeTeacher);
            string script = string.Format("<script>aleart('{0}');location.href = self.location.href", registrationResult);
            return Content(script, "text/html");

        }

        [HttpPost]
        public async Task<IActionResult> OtherStuffRegistration(OtherStuffRegistrationDto otherStuff)
        {
            var registrationResult = await _schoolContracts.OtherStuffRegistration(otherStuff);
            string script = string.Format("<script>aleart('{0}');location.href = self.location.href", registrationResult);
            return Content(script, "text/html");
        }

    }
}
