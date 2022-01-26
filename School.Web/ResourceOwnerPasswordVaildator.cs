// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityServer4.Models;
using IdentityServer4.Validation;
using SchoolCore.Dtos;
using SchoolCore.Service;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ResourceOwnerPasswordVaildator : IResourceOwnerPasswordValidator
    {
        private readonly ISchoolContracts _schoolContracts;
        public ResourceOwnerPasswordVaildator(ISchoolContracts schoolContracts)
        {
            _schoolContracts = schoolContracts;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            UserInputDto dto = new();
            dto.UserCode = context.UserName;
            dto.Password = context.Password;
            var loginReult = await _schoolContracts.UserLogin(dto);

            //List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim("name","xx"));

            if (loginReult.Type == School.Data.AjaxResultType.Success)
            {
                context.Result = new GrantValidationResult(dto.UserCode, "password"/*, null, "local", (Dictionary<string, object>)loginReult.Data*/);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, loginReult.Content);
            }
        }
    }

}