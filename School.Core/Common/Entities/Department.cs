using EntityConfigurationBase;
using System;
using System.Collections.Generic;


namespace SchoolCore.Entities
{
    public class Department : EntityBase<int>
    {
        /// <summary>
        /// 学院上一级是学校
        /// </summary>       
        public string DepartmentName { get; set; }
        public int DepartmentParentId { get; set; } 
        public virtual ICollection<User> DepartmentUsers { get; set; }
        public string DepartmentFunctions { get; set; }
      
    }
}
