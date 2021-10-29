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
using School.Core;
using IdentityServer4.Validation;
using School.Core.UserIndex.Entities;
using School.Core.UserIndex.Dtos.OfficeTeacherDtos;

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
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<NewsType> _newsTypeRepository;
        public const string secret = "JskduadUHLsdgasdUID1zkaosjx3h2JHdasdwef";
        public SchoolService(IMapper mapper, IRepository<User> userRepository, IRepository<UserRole> userRoleRepositoy,
            IRepository<Academic> academicRepository, IRepository<AClass> aClassRepository, IRepository<Course> courseRepository,
            IRepository<UserCourse> userCourseRepository, IRepository<ReportCards> reportCardsRepository, IRepository<Role> roleRepository,
            IRepository<Department> departmentRepository, IRepository<AcademicCourse> academicCourseRepository, IRepository<News> newsRepository,
            IRepository<NewsType> newsTypeRepository)
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
            _newsRepository = newsRepository;
            _newsTypeRepository = newsTypeRepository;
        }
        #region 锁定当前用户 已采用token，该部分注销
        ///// <summary>
        ///// 获取当前登录用户信息
        ///// </summary>
        //public static class UserInfo
        //{
        //    public static string UserCode { get; set; }
        //    public static string Password { get; set; }
        //}

        ////public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        ////{
        ////    UserInputDto dto = new();
        ////    dto.UserCode = context.UserName;
        ////    dto.Password = context.Password;
        ////    var loginReult = await UserLogin(dto);
        ////    if (loginReult.Type == AjaxResultType.Success)
        ////    {
        ////        context.Result = new GrantValidationResult(context.UserName, "password", null, "local", (Dictionary<string, object>)loginReult.Data);
        ////    }
        ////    else
        ////    {
        ////        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, loginReult.Content);
        ////    }
        ////}
        //public static string GetToken(UserTokenDto userTokenDto)
        //{
        //    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        //    IJsonSerializer serializer = new JsonNetSerializer();
        //    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        //    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        //    DateTime time = DateTime.Now.AddMinutes(15);
        //    userTokenDto.Exp = time.ToString();
        //    Dictionary<string, object> dict = new()
        //    {
        //        { "姓名：", userTokenDto.Name },
        //        { "账号：", userTokenDto.Code },
        //        { "角色：", userTokenDto.Role }
        //    };
        //    var token = encoder.Encode(userTokenDto, secret);
        //    return token;
        //}

        //public static IDictionary<string, object> DecodeToken(string token)
        //{
        //    IJsonSerializer serializer = new JsonNetSerializer();
        //    IDateTimeProvider provider = new UtcDateTimeProvider();
        //    IJwtValidator validator = new JwtValidator(serializer, provider);
        //    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        //    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        //    IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
        //    var dicInfo = decoder.DecodeToObject(token, secret, verify: true);//token为之前生成的字符串
        //    return dicInfo;
        //}
        #endregion

        #region 用户登录/登出,登出在前端重置了
        /// <summary>
        /// 用户登录界面只有账号密码
        /// 后面加验证码
        /// </summary>
        /// <param name="user"></param>
        /// <returns>有可能多重身份比如院长也讲课，所以返回的是array</returns>
        public async Task<AjaxResult> UserLogin(UserInputDto user)
        {
            ResourceOwnerPasswordValidationContext context = new();
            string content = "登陆失败！";
            AjaxResultType resultType = AjaxResultType.Error;
            //string[] roleTypes = { "" };
            UserTokenDto userTokenDto = new();
            string data = null;
            var currentUserRole = _userRoleRepository.GetEntities<UserRole>(x => x.User.UserCode == user.UserCode && x.User.Password == user.Password);
            var currentUser = currentUserRole.Select(u => u.User).FirstOrDefault();
            if (currentUser != null)
            {         
                //var currentUser = await _userRoleRepository.GetEntities<UserRole>(x => x.UserCode == user.UserCode && x.Password == user.Password).FirstOrDefaultAsync();
                userTokenDto.Role = currentUserRole.Select(x => x.Role.RoleName).ToArray();
                userTokenDto.Name = currentUser.Name;
                userTokenDto.Code = user.UserCode;
                // data = GetToken(userTokenDto);
                // UserInfo.UserCode = user.UserCode;
                // UserInfo.Password = user.Password;
                resultType = AjaxResultType.Success;
                content = "登陆成功！";
                // context.UserName = currentUser.Name;
                // context.Result = new GrantValidationResult(context.UserName, "password", null, "local", data);
            }
            else
            {
                content += "账号或密码错误，请检查并重新输入！";
                // context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, content);
            }
            return new AjaxResult(content, data, resultType);
        }
        public async Task<AjaxResult> UserLogout()
        {
            return new AjaxResult("登出成功！");
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
            else
            {
                int sexNumber = 0;
                user = _mapper.Map<User>(student);
                var studentNum = GetStudentType(student.Academic, student.Class, student.StudentType);
                user.UserCode = DateTime.Now.Year.ToString() + studentNum + student.Num;
                if (await _userRepository.GetEntities<User>(u => u.UserCode == user.UserCode).AnyAsync())
                {
                    errorReasonOrSuccess = "该学号已存在，请检查并重新输入！";
                }
                user.Password = student.IdCardNumber.Substring(student.IdCardNumber.Length - 7, 6);
                sexNumber = int.Parse(student.IdCardNumber.Substring(student.IdCardNumber.Length - 2, 1));
                user.Sex = (sexNumber % 2 == 0) ? "女" : "男";
                user.Age = int.Parse(DateTime.Now.Year.ToString()) - int.Parse(student.IdCardNumber.Substring(student.IdCardNumber.Length - 12, 4));
                var birthDate = student.IdCardNumber.Substring(6, 8);
                user.BirthDate = Convert.ToDateTime(birthDate.Substring(0, 4) + '-' + birthDate.Substring(4, 2) + '-' + birthDate.Substring(6, 2));
                user.UserAcademic = _academicRepository.GetEntities<Academic>(a => a.AcademicName == student.Academic).FirstOrDefault();
                var className = DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length-2,2) + "级" + student.Academic + student.Class + "班";
                var aClass = _aClassRepository.GetEntities<AClass>(a => a.AClassName == className).Include(a=>a.AclassUsers).FirstOrDefault();
                if (aClass != null)
                {
                    aClass.AclassUsers.Add(user);
                    await _aClassRepository.ChangeEntitiesAsync(aClass);
                }           
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
        /// 根据信息自动填充中间6位学号
        /// </summary>
        /// <param name="academic"></param>
        /// <param name="aClass"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static string GetStudentType(string academic,string aClass,string type)
        {
            string studentType = null;
            switch (type)
            {
                case "学硕全日制":
                    studentType = "21";
                    break;
                case "专硕全日制":
                    studentType = "22";
                    break;
                case "专硕非全日制":
                    studentType = "52";
                    break;
                case "硕博连读":
                    studentType = "23";
                    break;
                default:
                    break;
            }
            switch (academic)
            {
                case "信通":
                    studentType += "01";
                    break;
                case "电子":
                    studentType += "02";
                    break;
                case "机械":
                    studentType += "03";
                    break;
                case "航空":
                    studentType += "04";
                    break;
                case "资环":
                    studentType += "05";
                    break;
                case "计算机":
                    studentType += "06";
                    break;
                case "经管":
                    studentType += "07";
                    break;
                case "信软":
                    studentType += "08";
                    break;
                case "自动化":
                    studentType += "09";
                    break;
                case "通信抗干扰":
                    studentType += "10";
                    break;
                case "物理":
                    studentType += "11";
                    break;
            }
            studentType += aClass;
            return studentType;
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
                var birthDate = teachingTeacher.IdCardNumber.Substring(6, 8);
                user.BirthDate = Convert.ToDateTime(birthDate.Substring(0, 4) + '-' + birthDate.Substring(4, 2) + '-' + birthDate.Substring(6, 2));
                var department = _departmentRepository.GetEntities<Department>(a => a.DepartmentName == "教学").Include(d => d.DepartmentUsers).FirstOrDefault();
                department.DepartmentUsers.Add(user);
                user.UserAcademic = _academicRepository.GetEntities<Academic>(a => a.AcademicName == teachingTeacher.Academic).FirstOrDefault();
                if (teachingTeacher.Course != null)
                {
                    foreach (var c in teachingTeacher.Course)
                    {
                        var course = _courseRepository.GetEntities<Course>(a => a.CourseName == c).FirstOrDefault();
                        course.TeachingTeacher = user;
                        await _courseRepository.ChangeEntitiesAsync(course);
                    }
                }                                    
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
                teacher.Course = _courseRepository.GetEntities<Course>(c => c.TeachingTeacher == s).Select(c=>c.CourseName).ToList();
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
            else if (await _userRepository.GetEntities<User>(u => u.UserCode == officeTeacher.UserCode).AnyAsync() && officeTeacher.UserCode!=null)
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
                var birthDate = officeTeacher.IdCardNumber.Substring(6, 8);
                user.BirthDate = Convert.ToDateTime(birthDate.Substring(0, 4) + '-' + birthDate.Substring(4, 2) + '-' + birthDate.Substring(6, 2));
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
                user.Password = otherStuff.IdCardNumber.Substring(otherStuff.IdCardNumber.Length - 7, 6);
                sexNumber = int.Parse(otherStuff.IdCardNumber.Substring(otherStuff.IdCardNumber.Length - 2, 1));
                user.Sex = (sexNumber % 2 == 0) ? "女" : "男";
                user.Age = int.Parse(DateTime.Now.Year.ToString()) - int.Parse(otherStuff.IdCardNumber.Substring(otherStuff.IdCardNumber.Length - 12, 4));
                var birthDate = otherStuff.IdCardNumber.Substring(6, 8);
                user.BirthDate = Convert.ToDateTime(birthDate.Substring(0, 4) + '-' + birthDate.Substring(4, 2) + '-' + birthDate.Substring(6, 2));
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
            stuffs = await _userRoleRepository.GetEntities<UserRole>(u => u.RoleId == 4).Select(ur => ur.User).ToListAsync();
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
        public async Task<AjaxResult> Settings(UserSelfSettingDto dto, string path)
        {
            //dto.UserCode = UserInfo.UserCode;
            var user = _userRepository.GetEntities<User>(u => u.UserCode == dto.UserCode).FirstOrDefault();
            if (dto.PhoneNumber != null)
            {
                user.PhoneNumber = dto.PhoneNumber;
            }
            if (dto.Name != null)
            {
                user.Name = dto.Name;
            }
            if (dto.Gender != null)
            {
                user.Sex = dto.Gender;
            }
            if (dto.BirthDate != null)
            {
                var t = Convert.ToDateTime(dto.BirthDate);
                user.BirthDate = t;
            }
            if (dto.HeadImg != null)
            {
                user.HeadPictureAddress = path;
            }
            //if (dto.UserClaims!=null)
            //{
            //    foreach (var uc in dto.UserClaims)
            //    {
            //        user.UserClaims.Add(uc);
            //    }
            //}                    
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
        public async Task<AjaxResult> GetCourses(CourseInputDto course)
        {
            List<Course> browseCourse = new();//查询结果
            List<CourseOutputDto> coursesList = new();//返回数据结果
            CourseOutputDto course1 = new()
            {
                Academics = new List<string>()
            };
            AcademicCourse academicCourse = new();
            List<string> content = new();
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
                browseCourse.AddRange(await _courseRepository.GetEntities<Course>(c => c.TeachingTeacher.Name == course.TeachingTeacher).Include(c=>c.TeachingTeacher).ToListAsync());
            }
            if (!string.IsNullOrEmpty(course.CourseName))
            {
                browseCourse.AddRange(await _courseRepository.GetEntities<Course>(c => c.CourseName == course.CourseName).Include(c => c.TeachingTeacher).ToListAsync());
            }
            //还是要从数据库查询，规避掉传入参数为空的情况，不许偷懒！aCourse是Course对象
            foreach (var aCourse in browseCourse)
            {
                course1 = _mapper.Map<CourseOutputDto>(aCourse);
                var academicIds = _academicCourseRepository.GetEntities<AcademicCourse>(ac => ac.CourseId == aCourse.Id).Select(ac => ac.AcademicId).ToList();
                var user = _userRepository.GetEntities<User>(u => u.UserCode == course.UserCode).FirstOrDefault();
                course1.Academics = _academicRepository.GetEntities<Academic>(a => academicIds.Contains(a.Id)).Where(a => a.AcademicName != null).Select(a => a.AcademicName).ToList();                
                course1.TeachingTeacher = aCourse.TeachingTeacher.Name;                
                if(_userCourseRepository.GetEntities<UserCourse>(uc=>uc.UserId == user.Id && uc.CourseId == aCourse.Id).Any())
                {
                    course1.ChoosenOrNot = "已选";
                }
                else
                {
                    course1.ChoosenOrNot = "未选";
                }
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
        public async Task<AjaxResult> ChooseCourses(CourseChooseDto dto)
        {
            //选课成功，选课人数+1
            AjaxResult result = new();
            if (await _courseRepository.GetEntities<Course>(c => c.CourseCode == dto.CourseCode).AnyAsync())
            {
                var course = await _courseRepository.GetEntities<Course>(c => c.CourseCode == dto.CourseCode).FirstOrDefaultAsync();
                var user = await _userRepository.GetEntities<Course>(u => u.UserCode == dto.UserCode).FirstOrDefaultAsync();
                UserCourse userCourse = new();
                course.CourseChoosenNumber += 1;
                userCourse.User = user;
                userCourse.Course = course;
                if(!await _userCourseRepository.GetEntities<UserCourse>(uc=>uc.UserId == user.Id && uc.CourseId == course.Id).AnyAsync())
                {
                    result = await _userCourseRepository.ChangeEntitiesAsync(userCourse);
                }
                else
                {
                    result.Content = "已选！";
                }
            }
            return result;
        }

        ///<summary>
        ///查看已选课程
        ///</summary>
        
        public async Task<AjaxResult> HasChoosen(UserInputDto user)
        {
            var myChoosenCourse = await _userCourseRepository.GetEntities<UserCourse>(uc => uc.User.UserCode == user.UserCode).ToListAsync();
            return new AjaxResult("查询成功！", AjaxResultType.Success, myChoosenCourse);
        }
        #endregion

        #region 成绩录入系统

        ///<summary>
        ///把该老师所带的不同的课以及每个课内的学生给到前端
        ///</summary>
        public async Task<AjaxResult> CourseAndStudent(string teacherCode)
        {
            AjaxResult result = new("查询成功！");
            CourseAndStudentListDto dto = new()
            {
                CourseAndStudents = new(),
                Courses = new()
            };
            List<CourseAndStudenDto> courseAndStudent = new();           
            var courseList = await _courseRepository.GetEntities<Course>(c => c.TeachingTeacher.UserCode == teacherCode).ToListAsync();
            if (courseList.Count != 0)
            {
                foreach (var c in courseList)
                {
                    CourseAndStudenDto dto1 = new()
                    {
                        Students = new()
                    };
                    dto1.CourseName = c.CourseName;
                    dto1.Students = await _userCourseRepository.GetEntities<UserCourse>(uc => uc.CourseId == c.Id).Include(c => c.User).Select(uc => uc.User).ToListAsync();
                    dto.CourseAndStudents.Add(dto1);
                    dto.Courses.Add(dto1.CourseName);
                }
            }
            result.Data = dto;
            return result;
        }
        
        /// <summary>
        /// 成绩录入或修改？
        /// 根据传入的学号直接在用户=》学生=》学生备注=》成绩单添加该课成绩
        /// </summary>
        /// <returns>带成绩的学生列表(课程名称、编号、学生编号、姓名、成绩)</returns>
        public async Task<List<AjaxResult>> InputReportCard(List<InputReportCardsDto> reportCards)
        {
            List<AjaxResult> result = new();
            ReportCards report = new();
            if (reportCards != null)
            {
                foreach (var reportCard in reportCards)
                {
                    var userID = _userRepository.GetEntities<User>(u => u.Name == reportCard.StudentName).FirstOrDefault().Id;
                    var coursrID = _courseRepository.GetEntities<Course>(c => c.CourseName == reportCard.CourseName).FirstOrDefault().Id;
                    var findReportcard = _reportCardsRepository.GetEntities<ReportCards>(rc => rc.CourseId == coursrID && rc.UserId == userID).FirstOrDefault();
                    if (findReportcard != null)
                    {
                        findReportcard.Report = reportCard.Grades;
                        result.Add(await _reportCardsRepository.ChangeEntitiesAsync(findReportcard));
                    }
                    else
                    {
                        report.CourseId = coursrID;
                        report.UserId = userID;
                        report.Report = reportCard.Grades;
                        result.Add(await _reportCardsRepository.ChangeEntitiesAsync(report));
                    }
                    
                }
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
        public async Task<AjaxResult> GetReportCard(string userCode)
        {
            GetReportCardsDto studentReportCard = new() { 
            Course = new(),
            GotGrades = 0};//返回数据结果
            
            double GPASum = 0; double CreditsSum = 0;
            var reportCards = await _reportCardsRepository.GetEntities<ReportCards>(r => r.Student.UserCode == userCode).ToListAsync();
            foreach (var reportCard in reportCards)
            {
                int pointCount = 0;
                var student = await _userRepository.GetEntities<User>(u => u.UserCode == userCode).FirstOrDefaultAsync();
                var course = await _courseRepository.GetEntities<Course>(c => c.Id== reportCard.CourseId).FirstOrDefaultAsync();
                CourseReportDto courseReport = new();
                //所有学生的成绩都存在一张表里，找出来，
                courseReport.CourseCode = course.CourseCode;
                courseReport.CourseName = course.CourseName;
                courseReport.CourseCredits = course.CourseCredit;
                courseReport.Grades = reportCard.Report;
                studentReportCard.Course.Add(courseReport);
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
            return new AjaxResult("查询成功！", studentReportCard);
        }
        #endregion

        #region 新闻管理系统
        ///<summary>
        ///获取数据库新闻类型
        ///</summary>
        
        public AjaxResult GetNewsTypes()
        {
            var types = _newsTypeRepository.GetEntities<NewsType>(nt => nt.NewsTypeType != null).Select(nt => nt.NewsTypeName).ToList();
            return new AjaxResult("查询成功！", AjaxResultType.Success, types);
        }

        ///<summary>
        ///保存新闻
        ///两个重载，有无图片封面
        ///</summary>
        public async Task<AjaxResult> NewsSave(NewsSaveDto newsDto, string pictureAddress, string newsFileAddress, string fileImgAddress)
        {
            var t = newsDto;
            News aNew = new();
            //aNew = _mapper.Map<News>(newsDto);
            aNew.NewsName = newsDto.NewsName;
            aNew.NewsWriter = newsDto.NewsWriter;
            if (newsDto.NewsContent == null)
            {
                aNew.NewsFileAddress = newsFileAddress;
            }
            else
            {
                aNew.NewsContentAddress = newsFileAddress;
            }
            aNew.NewsCoverType = newsDto.NewsCoverType;
            aNew.NewsCoverAddressOrTitle = pictureAddress;
            aNew.NewsImgsAddress = fileImgAddress;
            aNew.NewsShowStartTime = Convert.ToDateTime(newsDto.NewsStartTime);
            aNew.NewsShowEndTime = Convert.ToDateTime(newsDto.NewsEndTime);
            aNew.NewsWriteTime = Convert.ToDateTime(newsDto.NewsUploadTime);
            var newsType = _newsTypeRepository.GetEntities<NewsType>(nt => nt.NewsTypeName == newsDto.NewsType).FirstOrDefault();
            if (newsType != null)
            {
                aNew.NewsType = newsType;
            }
            var result = await _newsRepository.AddEntitiesAsync(aNew);
            return result;
        }
        public async Task<AjaxResult> NewsSave(NewsSaveDto newsDto, string newsFileAddress, string htmlImgAddress)
        {
            var s = newsDto;
            News news = new();
            //news = _mapper.Map<News>(newsDto);
            news.NewsName = newsDto.NewsName;
            news.NewsWriter = newsDto.NewsWriter;
            news.NewsCoverType = newsDto.NewsCoverType;
            if (newsDto.NewsContent == null)
            {
                news.NewsFileAddress = newsFileAddress;
            }
            else
            {
                news.NewsContentAddress = newsFileAddress;
            }
            if (newsDto.NewsCover != null)
            {
                news.NewsCoverAddressOrTitle = newsDto.NewsCover;
            }
            news.NewsImgsAddress = htmlImgAddress;
            news.NewsShowStartTime = Convert.ToDateTime(newsDto.NewsStartTime);
            news.NewsShowEndTime = Convert.ToDateTime(newsDto.NewsEndTime);
            news.NewsWriteTime = Convert.ToDateTime(newsDto.NewsUploadTime);
            var newsType = _newsTypeRepository.GetEntities<NewsType>(nt => nt.NewsTypeName == newsDto.NewsType).FirstOrDefault();
            if (newsType != null)
            {
                news.NewsType = newsType;
            }
            var result = await _newsRepository.AddEntitiesAsync(news);
            return result;
        }

        ///<summary>
        ///获取新闻
        ///</summary>
        public AjaxResult GetNews()
        {
            AjaxResult result = new();
            var news = _newsRepository.GetEntities<News>(n => n.NewsContentAddress != null).ToList();
            if (news.Count != 0)
            {
                result.Content = "查询成功！";
                result.Data = news;
            }
            return result;
        }

        #endregion
    }
}
