using AutoMapper;
using EntityConfigurationBase;
using IdentityServer4.Test;
using School.Core.UserIndex.Dtos.OfficeTeacherDtos;
using School.Core.UserIndex.Entities;
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
            //CreateMap<object, object>();
            CreateMap<NewsSaveDto, News>();
            CreateMap<News, NewsSaveDto>();
            CreateMap<Course, CourseOutputDto>();
            CreateMap<UserSelfSettingDto, User>();
            CreateMap<StudentRegistrationDto, User>();
            CreateMap<User, StudentRegistrationDto>();
            CreateMap<TeachingTeacherRegistrationDto, User>();
            CreateMap<User, TeachingTeacherRegistrationDto>();
            CreateMap<OtherStuffRegistrationDto, User>();
            CreateMap<User, OtherStuffRegistrationDto>();
            CreateMap<OfficeTeacherRegistrationDto, User>();
            CreateMap<User, OfficeTeacherRegistrationDto>();
            CreateMap<User, TestUser>();

        }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
