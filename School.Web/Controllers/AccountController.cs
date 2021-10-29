using Microsoft.AspNetCore.Mvc;
using SchoolCore.Dtos;
using SchoolCore.Service;
using System.Web;
using System.Threading.Tasks;
using School.Data;
using Microsoft.AspNetCore.Authorization;

namespace School.Web.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ISchoolContracts _schoolContracts;
        
        public AccountController(ISchoolContracts schoolContracts)
        {
            _schoolContracts = schoolContracts;
        }
        [HttpPost]
        public async Task<AjaxResult> Login([FromBody] UserInputDto user)
        {
            AjaxResult userRoles = new();
            if (user.UserCode != null && user.Password != null)
            {
                userRoles = await _schoolContracts.UserLogin(user);          
            }

            return userRoles;
        }

        [HttpPost]
        public async Task<AjaxResult> Logout()
        {
            var result = await _schoolContracts.UserLogout();
            return result;
        }

    }
}
