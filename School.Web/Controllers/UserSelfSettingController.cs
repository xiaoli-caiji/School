using Microsoft.AspNetCore.Mvc;
using School.Data;
using SchoolCore.Service;
using SchoolCore.UserIndex.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserSelfSettingController : Controller
    {
        private readonly ISchoolContracts _schoolContracts;
        public UserSelfSettingController(ISchoolContracts schoolContracts)
        {
            _schoolContracts = schoolContracts;
        }

        /// <summary>
        /// 用户修改信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> Settings(UserSelfSettingDto dto)
        {
            var result = await _schoolContracts.Settings(dto);
            return result;
        }
    }
}
