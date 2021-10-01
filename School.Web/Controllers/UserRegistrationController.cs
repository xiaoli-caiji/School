using Microsoft.AspNetCore.Mvc;
using School.Data;
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
        public async Task<AjaxResult> StudentRegistration(StudentRegistrationDto student)
        {//弹窗的内容：Content，数据：data
            var registrationResult = await _schoolContracts.StudentRegistration(student);

            return registrationResult;

        }

        [HttpPost]
        public async Task<AjaxResult> TeachingTeacherRegistration(TeachingTeacherRegistrationDto teachingTeacher)
        {
            var registrationResult = await _schoolContracts.TeachingTeacherRegistration(teachingTeacher);
            return registrationResult;

        }

        [HttpPost]
        public async Task<AjaxResult> OfficeTeacherRegistration(OfficeTeacherRegistrationDto officeTeacher)
        {
            var registrationResult = await _schoolContracts.OfficeTeacherRegistration(officeTeacher);
            return registrationResult;

        }

        [HttpPost]
        public async Task<AjaxResult> OtherStuffRegistration(OtherStuffRegistrationDto otherStuff)
        {
            var registrationResult = await _schoolContracts.OtherStuffRegistration(otherStuff);
            return registrationResult;
        }

    }
}
