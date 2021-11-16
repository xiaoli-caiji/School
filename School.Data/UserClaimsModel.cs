
using System.Collections;
using System.Collections.Generic;

namespace School.Data
{
    public class UserClaimsModel:IEnumerable<string>
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string HeadImg { get; set; }
        public string Roles { get; set; }
        public string Departments { get; set; }
        public List<string> Claims { get; set; }
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            Claims = new() { 
                Name,Gender,BirthDate,PhoneNumber,HeadImg,Roles
            };          
            return ((IEnumerable<string>)Claims).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Claims;
        }

        //Name = 1,
        //Gender = 2,
        //BirthDate = 3,
        //PhoneNumber = 4,
        //HeadImg = 5
    }
}
