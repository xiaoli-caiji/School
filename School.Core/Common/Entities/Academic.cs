using EntityConfigurationBase;
using System;
using System.Collections.Generic;

namespace SchoolCore.Entities
{
    public class Academic : EntityBase<int>
    {
        /// <summary>
        /// 学院上一级是学校
        /// </summary>       
        public string AcademicName { get; set; }
        public int AcademicParentId { get; set; } 
        public virtual ICollection<User> AcademicUsers { get; set; }
        public virtual ICollection<AcademicCourse> AcademicCourses { get; set; }
        public virtual ICollection<AClass> AcademicClass { get; set; }

      
    }
}
