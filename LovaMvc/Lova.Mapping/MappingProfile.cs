using AutoMapper;
using System;

namespace Lova.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.sys_category, Sys_CategoryMapping>();
            CreateMap<Sys_CategoryMapping, Entities.sys_category>();

            CreateMap<Entities.sys_user_jwt, Sys_UserJwtMapping>();
            CreateMap<Sys_UserJwtMapping, Entities.sys_user_jwt>();

        }
    }
}
