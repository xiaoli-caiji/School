using Microsoft.EntityFrameworkCore;
using School.Core.UserIndex.Dtos;
using School.Core.UserIndex.Dtos.StudentsDtos;
using School.Data;
using SchoolCore.Dtos;
using SchoolCore.Entities;
using SchoolCore.UserIndex.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Core.Common.Entities;
using AutoMapper;
using School.Core.Repository;
using EntityConfigurationBase;

namespace SchoolCore.Service
{
    public class SchoolService : ISchoolContracts
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Academic> _academicRepository;
        private readonly IRepository<AClass> _aClassRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<UserCourse> _userCourseRepository;
        private readonly IRepository<ReportCards> _reportCardsRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<AcademicCourse> _academicCourseRepository;

        public SchoolService(IMapper mapper, IRepository<User> userRepository, IRepository<UserRole> userRoleRepositoy,
            IRepository<Academic> academicRepository, IRepository<AClass> aClassRepository, IRepository<Course> courseRepository,
            IRepository<UserCourse> userCourseRepository, IRepository<ReportCards> reportCardsRepository, IRepository<Role> roleRepository,
            IRepository<Department> departmentRepository, IRepository<AcademicCourse> academicCourseRepository)
        {           
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepositoy;
            _academicRepository = academicRepository;
            _aClassRepository = aClassRepository;
            _courseRepository = courseRepository;
            _userCourseRepository = userCourseRepository;
            _reportCardsRepository = reportCardsRepository;
            _departmentRepository = departmentRepository;
            _academicCourseRepository = academicCourseRepository;
        }
        #region 锁定当前用户
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        public static class UserInfo
        {
            public static string UserCode { get; set; }
            public static string Password { get; set; }
        }
        #endregion

        #region 用户登录
        /// <summary>
        /// 用户登录界面只有账号密码
        /// 后面加验证码
        /// </summary>
        /// <param name="user"></param>
        /// <returns>有可能多重身份比如院长也讲课，所以返回的是array</returns>
        public async Task<AjaxResult> UserLogin(UserInputDto user)
        {
            
            string content = "登陆失败！";
            AjaxResultType resultType = AjaxResultType.Error;
            string[] roleTypes = { "" };
            var currentUserRole = _userRoleRepository.GetEntities<UserRole>(x => x.User.UserCode == user.UserCode && x.User.Password == user.Password);
            if (currentUserRole!=null)
            {
                var currentUser  = currentUserRole.Select(u => u.User).FirstOrDefault();
                //var currentUser = await _userRoleRepository.GetEntities<UserRole>(x => x.UserCode == user.UserCode && x.Password == user.Password).FirstOrDefaultAsync();
                roleTypes = currentUserRole.Select(x => x.Role.RoleName).ToArray();
                UserInfo.UserCode = user.UserCode;
                UserInfo.Password = user.Password;
                resultType = AjaxResultType.Success;
                content = "登陆成功！";
            }
            else
            {
                content += "账号或密码错误，请检查并重新输入！";
            }
            return new AjaxResult(content, roleTypes, resultType);
        }
        #endregion

        #region 用户注册系统
        /// <summary>
        /// 学籍注册，记得检查相关表的信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns>返回是string，对应error表看</returns>
        public async Task<AjaxResult> StudentRegistration(StudentRegistrationDto student)
        {
            User user = new();
            Role role =await _roleRepository.GetEntities<Role>(r => r.Id == 1).FirstOrDefaultAsync();            
            UserRole userRole = new();
            string errorReasonOrSuccess = "注册失败！";
            AjaxResultType resultType = AjaxResultType.Error;
            List<User> students = new();
            List<StudentRegistrationDto> studentsList = new();
            if (await _userRepository.GetEntities<User>(u => u.IdCardNumber == student.IdCardNumber).AnyAsync())
            {
                errorReasonOrSuccess = "该身份证号已存在，请检查并重新输入！";
            }
            else if (await _userRepository.GetEntities<User>(u => u.UserCode == student.UserCode).AnyAsync())
            {
                errorReasonOrSuccess = "该学号已存在，请检查并重新输入！";
            }
            else
            {
                int sexNumber = 0;
                user = _mapper.Map<User>(student);                
                user.Password = student.IdCardNumber.Substring(student.IdCardNumber.Length - 7, 6);
                sexNumber = int.Parse(student.IdCardNumber.Substring(student.IdCardNumber.Length - 2, 1));
                user.Sex = (sexNumber % 2 == 0) ? "女" : "男";
                user.Age = int.Parse(DateTime.Now.Year.ToString()) - int.Parse(student.IdCardNumber.Substring(student.IdCardNumber.Length - 12, 4));
                user.UserAcademic = _academicRepository.GetEntities<Academic>(a => a.AcademicName == student.Academic).FirstOrDefault();
                var aClass = _aClassRepository.GetEntities<AClass>(a => a.AClassName == student.Class).Include(a=>a.AclassUsers).FirstOrDefault();
                aClass.AclassUsers.Add(user);
                await _aClassRepository.ChangeEntitiesAsync(aClass);
                userRole.User = user;
                userRole.Role = role;
                //因为中间表有User实体，所以会直接注册一个,相当于直接从中间表把User注册了，
                //而role因为存在，所以不会注册
                await _userRoleRepository.AddEntitiesAsync(userRole);  
                errorReasonOrSuccess = "注册成功！";
                resultType = AjaxResultType.Success;
            }
            students = await _userRoleRepository.GetEntities<UserRole>(u => u.RoleId ==1).Select(ur=>ur.User).ToListAsync();
            foreach (var s in students)
            {
                var studenView = _mapper.Map<StudentRegistrationDto>(s);
                if(s.UserAcademic!=null)
                {
                    var sa = await _academicRepository.GetEntities<Academic>(a => a.Id == s.UserAcademic.Id).FirstOrDefaultAsync();
                    studenView.Academic = sa.AcademicName;
                }              
                studenView.Class = _aClassRepository.GetEntities<AClass>(a => a.AclassUsers.Contains(s)).FirstOrDefault().AClassName;
                studentsList.Add(studenView);
            }
            return new AjaxResult(errorReasonOrSuccess, studentsList, resultType);
        }

        /// <summary>
        /// 教师注册，检查关联表
        /// </summary>
        /// <param name="teachingTeacher"></param>
        /// <returns>和学籍注册大同小异</returns>
        public async Task<AjaxResult> TeachingTeacherRegistration(TeachingTeacherRegistrationDto teachingTeacher)
        {
            User user = new();
            Role role = await _roleRepository.GetEntities<Role>(r => r.Id == 2).FirstOrDefaultAsync();
            UserRole userRole = new();
            string errorReasonOrSuccess = "注册失败！";
            AjaxResultType resultType = AjaxResultType.Error;
            List<User> teachers = new();
            List<TeachingTeacherRegistrationDto> teachersList = new();
            if (await _userRepository.GetEntities<User>(u => u.IdCardNumber == teachingTeacher.IdCardNumber).AnyAsync())
            {
                errorReasonOrSuccess = "该身份证号已存在，请检查并重新输入！";
            }
            else if (await _userRepository.GetEntities<User>(u => u.UserCode == teachingTeacher.UserCode).AnyAsync())
            {
                errorReasonOrSuccess = "该教师编号已存在，请检查并重新输入！";
            }
            else
            {
                int sexNumber = 0;
                user = _mapper.Map<User>(teachingTeacher);
                user.Password = teachingTeacher.IdCardNumber.Substring(teachingTeacher.IdCardNumber.Length - 7, 6);
                sexNumber = int.Parse(teachingTeacher.IdCardNumber.Substring(teachingTeacher.IdCardNumber.Length - 2, 1));
                user.Sex = (sexNumber % 2 == 0) ? "女" : "男";
                user.Age = int.Parse(DateTime.Now.Year.ToString()) - int.Parse(teachingTeacher.IdCardNumber.Substring(teachingTeacher.IdCardNumber.Length - 12, 4));
                user.UserAcademic = _academicRepository.GetEntities<Academic>(a => a.AcademicName == teachingTeacher.Academic).FirstOrDefault();
                var course = _courseRepository.GetEntities<Course>(a => a.CourseName == teachingTeacher.Course).FirstOrDefault();
                course.TeachingTeacher = user;
                await _courseRepository.ChangeEntitiesAsync(course);
                userRole.User = user;
                userRole.Role = role;
                await _userRoleRepository.AddEntitiesAsync(userRole);
                errorReasonOrSuccess = "注册成功！";
                resultType = AjaxResultType.Success;
            }
            teachers = await _userRoleRepository.GetEntities<UserRole>(u => u.RoleId == 2).Select(ur => ur.User).ToListAsync();
            foreach (var s in teachers)
            {
                var teacher = _mapper.Map<TeachingTeacherRegistrationDto>(s);
                if (s.UserAcademic != null)
                {
                    var sa = await _academicRepository.GetEntities<Academic>(a => a.Id == s.UserAcademic.Id).FirstOrDefaultAsync();
                    teacher.Academic = sa.AcademicName;
                }
                teacher.Course = _courseRepository.GetEntities<Course>(c => c.TeachingTeacher == s).Select(c=>c.CourseName).ToString();
                teachersList.Add(teacher);
            }
            return new AjaxResult(errorReasonOrSuccess, teachersList, resultType);
        }

        /// <summary>
        /// 校内其他教师注册
        /// </summary>
        /// <param name="officeTeacher"></param>
        /// <returns></returns>
        public async Task<AjaxResult> OfficeTeacherRegistration(OfficeTeacherRegistrationDto officeTeacher)
        {
            User user = new();
            Role role = await _roleRepository.GetEntities<Role>(r => r.Id == 3).FirstOrDefaultAsync();
            UserRole userRole = new();
            string errorReasonOrSuccess = "注册失败！";
            AjaxResultType resultType = AjaxResultType.Error;
            List<User> teachers = new();
            List<OfficeTeacherRegistrationDto> teachersList = new();
            if (await _userRepository.GetEntities<User>(u => u.IdCardNumber == officeTeacher.IdCardNumber).AnyAsync())
            {
                errorReasonOrSuccess = "该身份证号已存在，请检查并重新输入！";
            }
            else if (await _userRepository.GetEntities<User>(u => u.UserCode == officeTeacher.UserCode).AnyAsync())
            {
                errorReasonOrSuccess = "该职工编号已存在，请检查并重新输入！";
            }
            else
            {
                int sexNumber = 0;
                user = _mapper.Map<User>(officeTeacher);
                user.Password = officeTeacher.IdCardNumber.Substring(officeTeacher.IdCardNumber.Length - 7, 6);
                sexNumber = int.Parse(officeTeacher.IdCardNumber.Substring(officeTeacher.IdCardNumber.Length - 2, 1));
                user.Sex = (sexNumber % 2 == 0) ? "女" : "男";
                user.Age = int.Parse(DateTime.Now.Year.ToString()) - int.Parse(officeTeacher.IdCardNumber.Substring(officeTeacher.IdCardNumber.Length - 12, 4));
                user.UserAcademic = _academicRepository.GetEntities<Academic>(a => a.AcademicName == officeTeacher.Academic).FirstOrDefault();
                var department = _departmentRepository.GetEntities<Department>(a => a.DepartmentName == officeTeacher.Department).Include(d=>d.DepartmentUsers).FirstOrDefault();
                department.DepartmentUsers.Add(user);
                await _departmentRepository.ChangeEntitiesAsync(department);
                userRole.User = user;
                userRole.Role = role;
                await _userRoleRepository.AddEntitiesAsync(userRole);
                errorReasonOrSuccess = "注册成功！";
                resultType = AjaxResultType.Success;
            }
            teachers = await _userRoleRepository.GetEntities<UserRole>(u => u.RoleId == 3).Select(ur => ur.User).ToListAsync();
            foreach (var s in teachers)
            {
                var teacher = _mapper.Map<OfficeTeacherRegistrationDto>(s);
                if (s.UserAcademic != null)
                {
                    var sa = await _academicRepository.GetEntities<Academic>(a => a.Id == s.UserAcademic.Id).FirstOrDefaultAsync();
                    teacher.Academic = sa.AcademicName;
                }
                teacher.Department = _departmentRepository.GetEntities<Department>(d => d.DepartmentUsers.Contains(s)).FirstOrDefault().DepartmentName;
                teachersList.Add(teacher);
            }
            return new AjaxResult(errorReasonOrSuccess, teachersList, resultType);
        }

        /// <summary>
        /// 其他职工注册
        /// </summary>
        /// <param name="otherStuff"></param>
        /// <returns></returns>
        public async Task<AjaxResult> OtherStuffRegistration(OtherStuffRegistrationDto otherStuff)
        {
            User user = new();
            Role role = await _roleRepository.GetEntities<Role>(r => r.Id == 4).FirstOrDefaultAsync();
            UserRole userRole = new();
            string errorReasonOrSuccess = "注册失败！";
            AjaxResultType resultType = AjaxResultType.Error;
            List<User> stuffs = new();
            List<OtherStuffRegistrationDto> stuffsList = new();
            if (await _userRepository.GetEntities<User>(u => u.IdCardNumber == otherStuff.IdCardNumber).AnyAsync())
            {
                errorReasonOrSuccess = "该身份证号已存在，请检查并重新输入！";
            }
            else if (await _userRepository.GetEntities<User>(u => u.UserCode == otherStuff.UserCode).AnyAsync())
            {
                errorReasonOrSuccess = "该职工编号已存在，请检查并重新输入！";
            }
            else
            {
                int sexNumber = 0;
                user = _mapper.Map<User>(otherStuff);
                user.Password = otherStuff.IdCardNumber.Substring(otherStuff.IdCardNumber.Length - 6, 6);
                sexNumber = int.Parse(otherStuff.IdCardNumber.Substring(otherStuff.IdCardNumber.Length - 1, 1));
                user.Sex = (sexNumber % 2 == 0) ? "女" : "男";
                user.Age = int.Parse(DateTime.Now.Year.ToString()) - int.Parse(otherStuff.IdCardNumber.Substring(otherStuff.IdCardNumber.Length - 11, 4));
                user.UserAcademic = _academicRepository.GetEntities<Academic>(a => a.AcademicName == otherStuff.Academic).FirstOrDefault();
                var department = _departmentRepository.GetEntities<Department>(a => a.DepartmentName == otherStuff.Department).Include(d => d.DepartmentUsers).FirstOrDefault();
                department.DepartmentUsers.Add(user);
                await _departmentRepository.ChangeEntitiesAsync(department);
                userRole.User = user;
                userRole.Role = role;
                await _userRoleRepository.AddEntitiesAsync(userRole);
                errorReasonOrSuccess = "注册成功！";
                resultType = AjaxResultType.Success;
            }
            stuffs = await _userRoleRepository.GetEntities<UserRole>(u => u.RoleId == 1).Select(ur => ur.User).ToListAsync();
            foreach (var s in stuffs)
            {
                var stuff = _mapper.Map<OfficeTeacherRegistrationDto>(s);
                if (s.UserAcademic != null)
                {
                    var sa = await _academicRepository.GetEntities<Academic>(a => a.Id == s.UserAcademic.Id).FirstOrDefaultAsync();
                    stuff.Academic = sa.AcademicName;
                }
                stuff.Department = _departmentRepository.GetEntities<Department>(d => d.DepartmentUsers.Contains(s)).FirstOrDefault().DepartmentName;
                stuffsList.Add(_mapper.Map< OtherStuffRegistrationDto>(s));
            }
            return new AjaxResult(errorReasonOrSuccess, stuffsList, resultType);
        }
        #endregion

        #region 用户自定义系统
        /// <summary>
        /// 用户修改信息
        /// 注意检查数据库对应数据是否完成修改
        /// </summary>
        /// <param name="user"></param>
        /// <returns>返回修改结果字符串</returns>
        public async Task<AjaxResult> Settings(UserSelfSettingDto dto)
        {
            //dto.UserCode = UserInfo.UserCode;
            var user = _userRepository.GetEntities<User>(u => u.UserCode == UserInfo.UserCode).FirstOrDefault();
            if(dto.Password!=null)
            {
                user.Password = dto.Password;
            }
            if (dto.PhoneNumber != null)
            {
                user.PhoneNumber = dto.PhoneNumber;
            }           
            if(dto.UserClaims!=null)
            {
                foreach (var uc in dto.UserClaims)
                {
                    user.UserClaims.Add(uc);
                }
            }                    
            var result = await _userRepository.ChangeEntitiesAsync(user);            
            return result;
        }
        #endregion

        #region 查选课系统
        /// <summary>
        /// 查课
        /// </summary>
        /// <param name="course"></param>
        /// <returns>返回列表</returns>
        public async Task<AjaxResult> GetCourses(CourseOutputDto course)
        {
            List<Course> browseCourse = new();//查询结果
            List<CourseOutputDto> coursesList = new();//返回数据结果
            CourseOutputDto course1 = new()
            {
                Academics = new List<string>()
            };
            AcademicCourse academicCourse = new();
            //按照从左到右的顺序分别写三个if语句，就可以避免写6种情况了
            if (!string.IsNullOrEmpty(course.AcademicName))
            {
                //academicCourse.Academic = await _academicRepository.GetEntities<Academic>(a => a.AcademicName == course.AcademicName).FirstOrDefaultAsync();
                var academicID = _academicRepository.GetEntities<Academic>(a => a.AcademicName == course.AcademicName).FirstOrDefault().Id;
                var courseIds = _academicCourseRepository.GetEntities<AcademicCourse>(ac => ac.AcademicId == academicID).Select(c => c.CourseId).ToList();
                browseCourse.AddRange(_courseRepository.GetEntities<Course>(c => courseIds.Contains(c.Id)).Include(c=>c.TeachingTeacher));
                //browseCourse.AddRange(await _courseRepository.GetEntities<Course>(c => c.CourseAcademic.Where(ca=>ca.Academic.AcademicName==course.AcademicName)).ToListAsync());
            }
            if (!string.IsNullOrEmpty(course.TeachingTeacher))
            {
                browseCourse.AddRange(await _courseRepository.GetEntities<Course>(c => c.TeachingTeacher.Name == course.TeachingTeacher).ToListAsync());
            }
            if (!string.IsNullOrEmpty(course.CourseName))
            {
                browseCourse.AddRange(await _courseRepository.GetEntities<Course>(c => c.CourseName == course.CourseName).ToListAsync());
            }
            //还是要从数据库查询，规避掉传入参数为空的情况，不许偷懒！aCourse是Course对象
            foreach (var aCourse in browseCourse)
            {
                course1 = _mapper.Map<CourseOutputDto>(aCourse);
                var academicIds = _academicCourseRepository.GetEntities<AcademicCourse>(ac => ac.CourseId == aCourse.Id).Select(ac => ac.AcademicId).ToList();
                course1.Academics = _academicRepository.GetEntities<Academic>(a => academicIds.Contains(a.Id)).Where(a => a.AcademicName != null).Select(a => a.AcademicName).ToList();
                
                course1.TeachingTeacher = aCourse.TeachingTeacher.Name;
                coursesList.Add(course1);
            }
            return new AjaxResult("查询成功！", coursesList, AjaxResultType.Success);
        }

        /// <summary>
        /// 选课系统
        /// 注意检查选课人数是否增加
        /// </summary>
        /// <param name="courseName"></param>
        /// <returns>返回的是选课结果字符串</returns>
        public async Task<AjaxResult> ChooseCourses(string courseName)
        {
            //选课成功，选课人数+1
            AjaxResult result = new();
            if (await _courseRepository.GetEntities<Course>(c => c.CourseName == courseName).AnyAsync())
            {
                var course = await _courseRepository.GetEntities<Course>(c => c.CourseName == courseName).FirstOrDefaultAsync();
                var user = await _userRepository.GetEntities<Course>(u => u.UserCode == UserInfo.UserCode).FirstOrDefaultAsync();
                UserCourse userCourse = new();
                course.CourseChoosenNumber += 1;
                userCourse.User = user;
                userCourse.Course = course;
                result = await _userCourseRepository.ChangeEntitiesAsync(userCourse);               
            }
            return result;
        }
        #endregion

        #region 成绩录入系统
        /// <summary>
        /// 成绩录入或修改？
        /// 根据传入的学号直接在用户=》学生=》学生备注=》成绩单添加该课成绩
        /// </summary>
        /// <returns>带成绩的学生列表(课程名称、编号、学生编号、姓名、成绩)</returns>
        public async Task<List<AjaxResult>> InputReportCard(List<InputReportCardsDto> reportCards)
        {
            List<AjaxResult> result = new();
            foreach (var reportCard in reportCards)
            {
                var userID = _userRepository.GetEntities<User>(u => u.Name == reportCard.StudentName).FirstOrDefault().Id;
                var coursrID = _courseRepository.GetEntities<Course>(c => c.CourseName == reportCard.CourseName).FirstOrDefault().Id;
                var findReportcard = _reportCardsRepository.GetEntities<ReportCards>(rc => rc.CourseId == coursrID && rc.UserId == userID).FirstOrDefault();
                if (findReportcard!=null)
                {
                    findReportcard.Report = reportCard.Grades;  
                }
                result.Add(await _reportCardsRepository.ChangeEntitiesAsync(findReportcard));                              
            }
            return result;
        }
        #endregion

        #region 成绩查看系统
        /// <summary>
        /// 获取学生的成绩单
        /// 在成绩单的表格里全搜出来就完了
        /// 课程名称、成绩
        /// </summary>
        /// <returns>成绩单（课程编号、课程名、学分、成绩）</returns>
        public async Task<AjaxResult> GetReportCard()
        {
            GetReportCardsDto studentReportCard = new() { 
            CourseCode = new(),
            CourseName = new(),
            CourseCredits = new(),
            Grades = new()};//返回数据结果
            double GPASum = 0; double CreditsSum = 0;
            var reportCards = await _reportCardsRepository.GetEntities<ReportCards>(r => r.Student.UserCode == UserInfo.UserCode).ToListAsync();
            foreach (var reportCard in reportCards)
            {
                int pointCount = 0;
                var student = await _userRepository.GetEntities<User>(u => u.UserCode == UserInfo.UserCode).FirstOrDefaultAsync();
                var course = await _courseRepository.GetEntities<Course>(c => c.Id== reportCard.CourseId).FirstOrDefaultAsync();
                //所有学生的成绩都存在一张表里，找出来，然后单独放在当前学生的成绩单里
                studentReportCard.CourseCode.Add(course.CourseCode);
                studentReportCard.CourseCredits.Add(course.CourseCredit);
                studentReportCard.CourseName.Add(course.CourseName);
                studentReportCard.Grades.Add(reportCard.Report);
                if (reportCard.Report >= 60)
                {
                    studentReportCard.GotGrades += course.CourseCredit;
                }
                pointCount = (((int)(reportCard.Report / 10)) - 5) switch
                {
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    4 => 4,
                    5 => 4,
                    _ => 0,
                };
                GPASum += pointCount * course.CourseCredit;
                CreditsSum += course.CourseCredit;
                studentReportCard.GPA = GPASum / CreditsSum;
            }
            return new AjaxResult("", studentReportCard);
        }
        #endregion

    }
}
