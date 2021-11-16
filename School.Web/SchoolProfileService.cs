using IdentityServer4.Models;
using IdentityServer4.Services;
using School.Core.Repository;
using SchoolCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IdentityModel;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    public class SchoolProfileService : IProfileService
    {
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<User> _userRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="logger">The logger.</param>
        public SchoolProfileService(IRepository<UserRole> userRoleRepository, IRepository<User> userRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                List<Claim> claims = new List<Claim>();
                var code = context.Subject.Claims.Where(s => s.Type == "sub").Select(s => s.Value).FirstOrDefault();
                var userRole = _userRoleRepository.GetEntities<UserRole>(u => u.User.UserCode == code);
                //var user = userRole.Select(ur => ur.User).Include(u => u.UserDepartment).FirstOrDefault();
                var user = _userRepository.GetEntities<User>(u => u.UserCode == code).Include(u => u.UserDepartment).FirstOrDefault();
                // user.UserRoles = (ICollection<UserRole>)userRole;
                var rolesList = userRole.Select(ur => ur.Role.RoleName).ToList();
                var roles = JsonConvert.SerializeObject(rolesList);
                if (user != null)
                {
                    UserClaimsModel userClaimsType = new()
                    {
                        Name = "name",
                        Gender = "gender",
                        BirthDate = "birthdate",
                        PhoneNumber = "phone_number",
                        HeadImg = "picture",
                        Roles = "role",
                        Departments = "department"
                    };
                    claims = BuildClaim(context.RequestedClaimTypes, user, roles);
                    //claims.Add(new Claim("name", user.Name));
                    //claims.Add(new Claim("roles", roles));
                }
                context.AddRequestedClaims(claims);
                context.IssuedClaims = claims.ToList();
            }
            return Task.CompletedTask;
        }


        ///<summary>
        ///创建claim用户详细信息
        ///</summary>

        private List<Claim> BuildClaim(IEnumerable<string> requestedClaimTypes, User user,string role)
        {
            List<Claim> claims = new List<Claim>();

            // List<Dictionary<string, string>> dics = null;
            foreach (var claim in requestedClaimTypes)
            {
                switch (claim)
                {
                    case JwtClaimTypes.Name:
                        if (!string.IsNullOrEmpty(user.Name))
                            claims.Add(new Claim(JwtClaimTypes.Name, user.Name));
                        else
                            claims.Add(new Claim(JwtClaimTypes.Name, ""));
                        break;
                    case JwtClaimTypes.PhoneNumber:
                        if (!string.IsNullOrEmpty(user.PhoneNumber))
                            claims.Add(new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber));
                        else
                            claims.Add(new Claim(JwtClaimTypes.PhoneNumber, ""));
                        break;
                    case JwtClaimTypes.Picture:
                        if (user.HeadPictureAddress != null)
                        {
                            claims.Add(new Claim(JwtClaimTypes.Picture, user.HeadPictureAddress));
                        }
                           
                        else
                            claims.Add(new Claim(JwtClaimTypes.Picture, ""));
                        break;
                    case JwtClaimTypes.Gender:
                        if (!string.IsNullOrEmpty(user.Sex))
                            claims.Add(new Claim(JwtClaimTypes.Gender, user.Sex));
                        else
                            claims.Add(new Claim(JwtClaimTypes.Gender, ""));
                        break;
                    case JwtClaimTypes.BirthDate:
                        if (user.BirthDate != null)
                            claims.Add(new Claim(JwtClaimTypes.BirthDate, user.BirthDate.ToString()));
                        else
                            claims.Add(new Claim(JwtClaimTypes.BirthDate, ""));
                        break;
                    case JwtClaimTypes.Role:
                        if (role!= null)
                            claims.Add(new Claim(JwtClaimTypes.Role, role));
                        else
                            claims.Add(new Claim(JwtClaimTypes.Role, ""));
                        break;
                    case "department":
                        if (user.UserDepartment != null)
                            claims.Add(new Claim("department", user.UserDepartment.DepartmentName));
                        else
                            claims.Add(new Claim("department", ""));
                        break;
                }
            }
            return claims;
        }

        /// <summary>
        /// 验证用户是否有效 例如：token创建或者验证
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}