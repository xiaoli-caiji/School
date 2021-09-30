//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using SchoolCore;
//using SchoolCore.Entities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//namespace School.Controllers
//{
//    /// <summary>
//    /// 增删改对每个类都适用，可以写成泛型
//    /// </summary>
//    public class SchoolController : Controller
//    {
//        private readonly MyDbContext _context;
//        public SchoolController(MyDbContext context)
//        {
//            _context = context;
//        }
//        public IActionResult Index()
//        {

//            return View();
//        }        
//        public IActionResult CareTakerIndexAll()
//        {
//            return View();
//        }
//        public async Task<IActionResult> StudentRegistration(TStudent student, int? teacherId)
//        {
//            if (student.StudentNumber != null)
//            {

//                //student.StudentTeacher.Add(_context.Teachers.Where(t => t.TeacherId == teacherId).FirstOrDefault());
//                if (_context.Students.Where(s => s.StudentNumber == student.StudentNumber).Any() ||
//                    _context.Students.Where(s => s.StudentIdCard == student.StudentIdCard).Any())
//                {
//                    return Content("<script>alert('学号或身份证号已存在，请检查输入是否有误！');location.href=self.location.href</script>", "text/html");
//                }
//                else if (ModelState.IsValid)
//                {
//                    //密码默认为身份证号去掉最后一位的后6位
//                    student.StudentPwd = student.StudentIdCard.Substring(student.StudentIdCard.Length - 6, 6);
//                    await _context.Students.AddAsync(student);
//                    await _context.SaveChangesAsync();
//                    return Content("<script>alert('学籍录入成功！');location.href=self.location.href</script>", "text/html");
//                }
//            }
//            var careTakerViewModel = new CareTakerViewModel()
//            {
//                Students = _context.Students.ToList()
//            };
//            return View(careTakerViewModel);
//        }
//        public async Task<IActionResult> TeacherRegistration(TTeacher teacher)
//        {
//            if (teacher.TeacherNumber != null)
//            {
//                if (_context.Teachers.Where(s => s.TeacherNumber == teacher.TeacherNumber).Any() ||
//                    _context.Teachers.Where(s => s.TeacherIdCard == teacher.TeacherIdCard).Any())
//                {
//                    return Content("<script>alert('教师编号或身份证号已存在，请检查输入是否有误！');location.href=self.location.href</script>", "text/html");
//                }
//                else if (ModelState.IsValid)
//                {
//                    //密码默认为身份证号去掉最后一位的后6位
//                    teacher.TeacherPwd = teacher.TeacherIdCard.Substring(teacher.TeacherIdCard.Length - 6, 6);
//                    await _context.Teachers.AddAsync(teacher);
//                    await _context.SaveChangesAsync();
//                    return Content("<script>alert('教师录入成功！');location.href=self.location.href</script>", "text/html");
//                }
//            }
//            var careTakerViewModel = new CareTakerViewModel()
//            {
//                Teachers = _context.Teachers.ToList()
//            };
//            return View(careTakerViewModel);
//        }
//        public async Task<IActionResult> CareTakerRegistration(TCareTaker careTaker)
//        {
//            if (careTaker.CareTakerNumber != null)
//            {
//                if (_context.CareTakers.Where(c => c.CareTakerNumber == careTaker.CareTakerNumber).Any())
//                {
//                    return Content("<script>alert('该管理员账号已存在，请检查输入是否有误！');location.href=self.location.href</script>", "text/html");
//                }
//                else if (ModelState.IsValid)
//                {
//                    await _context.AddAsync(careTaker);
//                    await _context.SaveChangesAsync();
//                    var script = string.Format("<script>alert('管理员添加成功！');location.href = '{0}'", Url.Action("CareTakerIist"));
//                    return Content(script, "text/html");
//                }
//            }
//            var careTakerViewModel = new CareTakerViewModel()
//            {
//                CareTakers = _context.CareTakers.ToList()
//            };
//            return View(careTakerViewModel);
//        }
//        public async Task<IActionResult> CreateClass(TClass aClass)
//        {
//            if (aClass.ClassName != null)
//            {
//                if (_context.Classes.Where(c => c.ClassName == aClass.ClassName).Any())
//                {
//                    return Content("<script>alert('该班级已存在，请勿重复添加！');location.href=self.location.href</script>", "text/html");
//                }
//                else if (ModelState.IsValid)
//                {
//                    await _context.Classes.AddAsync(aClass);
//                    await _context.SaveChangesAsync();
//                    return Content("<script>alert('添加成功！');location.href = self.location.href</script>", "text/html");
//                }
//            }
//            var careTakerViewModel = new CareTakerViewModel()
//            {
//                Classes = _context.Classes.ToList()
//            };
//            return View(careTakerViewModel);
//        }
//        public async Task<IActionResult> CreateCourse(TCourse course)
//        {
//            if (course.CourseName != null)
//            {
//                if (_context.Courses.Where(c => c.CourseName == course.CourseName).Any())
//                {
//                    return Content("<script>alert('该课程已存在，请勿重复添加！');location.href=self.location.href</script>", "text/html");
//                }
//                else if (ModelState.IsValid)
//                {
//                    await _context.Courses.AddAsync(course);
//                    await _context.SaveChangesAsync();
//                    return Content("<script>alert('添加成功！');location.href = self.location.href</script>", "text/html");
//                }
//            }
//            var careTakerViewModel = new CareTakerViewModel()
//            {
//                Courses = _context.Courses.ToList()
//            };
//            return View(careTakerViewModel);
//        }
//        public IActionResult TeacherIndex()
//        {
//            return View();
//        }
        
//        //添加Person父类，先不删除
//        //public async Task<IActionResult> CreatePerson(TPerson person)
//        //{
//        //    if (person.PersonName != null || person.PersonNumber != null)
//        //    {
//        //        if (_context.Persons.Where(g => g.PersonIdCard == person.PersonIdCard).Any() ||
//        //            _context.Persons.Where(g => g.PersonNumber == person.PersonNumber).Any())
//        //        {
//        //            return Content("<script>alert('人员编号或身份证号已存在，请检查输入是否有误！');" +
//        //                "location.href=self.location.href</script>", "text/html");
//        //        }
//        //        else if (ModelState.IsValid)
//        //        {
//        //            person.PersonPwd = person.PersonIdCard.Substring(person.PersonIdCard.Length - 4, 6);
//        //            await _context.AddAsync(person);
//        //            await _context.SaveChangesAsync();
//        //        }
//        //    }
//        //    return View();
//        //}      
//        public IActionResult CareTakerList()
//        {
//            return View(_context.CareTakers.ToList());
//        }
//        public async Task<IActionResult> BrowseAndChooseCourse(TCourse course)
//        {
//            return View();
//        }


//    }
//}
