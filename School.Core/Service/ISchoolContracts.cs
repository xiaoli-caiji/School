using Microsoft.AspNetCore.Mvc;
using School.Core.UserIndex.Dtos;
using School.Core.UserIndex.Dtos.OfficeTeacherDtos;
using School.Core.UserIndex.Dtos.StudentsDtos;
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
        Task<AjaxResult> StudentRegistration(StudentRegistrationDto student);
        Task<AjaxResult> TeachingTeacherRegistration(TeachingTeacherRegistrationDto teachingTeacher);
        Task<AjaxResult> OfficeTeacherRegistration(OfficeTeacherRegistrationDto officeTeacher);
        Task<AjaxResult> OtherStuffRegistration(OtherStuffRegistrationDto otherStuff);
        Task<AjaxResult> Settings(UserSelfSettingDto dto, string path);
        Task<AjaxResult> GetCourses(CourseInputDto course);
        Task<AjaxResult> ChooseCourses(CourseChooseDto dto);
        Task<AjaxResult> GetReportCard(string userCode);
        Task<AjaxResult> HasChoosen(UserInputDto user);
        Task<AjaxResult> CourseAndStudent(string teacherCode);
        Task<List<AjaxResult>> InputReportCard([FromBody]List<InputReportCardsDto> reportCards);
        AjaxResult GetNewsTypes();
        Task<AjaxResult> NewsSave(NewsSaveDto newsDto, string pictureAddress, string newsFileAddress, string filImgsAddress);
        Task<AjaxResult> NewsSave(NewsSaveDto newsDto, string newsFileAddress, string htmlImgAddress);
        AjaxResult GetNews();
        AjaxResult ShowNews();
    }
}
