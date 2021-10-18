using AutoMapper;
using EntityConfigurationBase;
using School.Core.Common.Entities;
using School.Core.UserIndex.Dtos;
using SchoolCore.Dtos;
using SchoolCore.Entities;
using SchoolCore.UserIndex.Dtos;
using System;

namespace School.Web.MappingMapper
{
    public class MapperProfile : Profile, IEntity<int>,IDto
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseOutputDto>();
            CreateMap<UserSelfSettingDto, User>();
            CreateMap<StudentRegistrationDto, User>();
            CreateMap<User, StudentRegistrationDto>();
        }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
