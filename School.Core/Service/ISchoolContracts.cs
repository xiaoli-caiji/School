using Microsoft.AspNetCore.Mvc;
using School.Core.UserIndex.Dtos;
using School.Core.UserIndex.Dtos.OfficeTeacherDtos;
using School.Core.UserIndex.Dtos.StudentsDtos;
using School.Core.UserIndex.Dtos.TeachingTeacherDtos;
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
        Task<AjaxResult> UserLogout();
        AjaxResult GetUnits();
        Task<AjaxResult> StudentRegistration(StudentRegistrationDto student);
        Task<AjaxResult> TeachingTeacherRegistration(TeachingTeacherRegistrationDto teachingTeacher);
        Task<AjaxResult> OfficeTeacherRegistration(OfficeTeacherRegistrationDto officeTeacher);
        Task<AjaxResult> OtherStuffRegistration(OtherStuffRegistrationDto otherStuff);
        Task<AjaxResult> Settings(UserSelfSettingDto dto, string path);
        Task<AjaxResult> BrowseCourse(CourseInputDto course);
        Task<AjaxResult> ChooseCourses(CourseChooseDto dto);
        Task<AjaxResult> GetCourses(string userCode);
        Task<AjaxResult> ModifyPercentage(List<CourseChooseDto> dtos);
        Task<AjaxResult> DeleteCourse(string courseCode, string userCode);
        Task<AjaxResult> GetReportCard(string userCode);
        Task<AjaxResult> CourseAndStudent(string teacherCode);
        AjaxResult GetCoursesByTeacher();
        Task<AjaxResult> WinCourse(StartCourseSelectionDto dto);
        Task<AjaxResult> CourseSelectionClose();
        Task<List<AjaxResult>> InputReportCard([FromBody]List<InputReportCardsDto> reportCards);
        AjaxResult GetNewsTypes();
        Task<AjaxResult> NewsSave(NewsSaveDto newsDto, string pictureAddress, string newsFileAddress, string filImgsAddress);
        Task<AjaxResult> NewsSave(NewsSaveDto newsDto, string newsFileAddress, string htmlImgAddress);
        AjaxResult GetNews();
        AjaxResult ShowNews();
        Task<AjaxResult> NewsEdit(NewsSaveDto newsDto, int? newsId, string rootPath);
    }
}
