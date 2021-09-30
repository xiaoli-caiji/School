using School.Core.UserIndex.Dtos;
using School.Data;
using SchoolCore.Dtos;
using SchoolCore.Entities;
using SchoolCore.UserIndex.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCore.Service
{
    public interface ISchoolContracts
    {
        Task<AjaxResult> UserLogin(UserInputDto dto);
        Task<AjaxResult> StudentRegistration(StudentRegistrationDto student);
        Task<AjaxResult> TeachingTeacherRegistration(TeachingTeacherRegistrationDto teachingTeacher);
        Task<AjaxResult> OfficeTeacherRegistration(OfficeTeacherRegistrationDto officeTeacher);
        Task<AjaxResult> OtherStuffRegistration(OtherStuffRegistrationDto otherStuff);
        Task<AjaxResult> Settings(UserSelfSettingDto dto);
        Task<AjaxResult> GetCourses(CourseOutputDto course);
        Task<AjaxResult> ChooseCourses(string courseCode);
        Task<AjaxResult> GetReportCard();
        Task<List<AjaxResult>> InputReportCard(List<InputReportCardsDto> reportCards);
    }
}
