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
             
            CreateMap<Entities.sys_user, Sys_UserMapping>();
            CreateMap<Sys_UserMapping, Entities.sys_user>();

            CreateMap<Entities.sys_user_login, Sys_UserLoginMapping>();
            CreateMap<Sys_UserLoginMapping, Entities.sys_user_login>();

            CreateMap<Entities.sys_user_role, Sys_UserRoleMapping>();
            CreateMap<Sys_UserRoleMapping, Entities.sys_user_role>();

            CreateMap<Entities.sys_nlog, Sys_NLogMapping>();
            CreateMap<Sys_NLogMapping, Entities.sys_nlog>();

            CreateMap<Entities.sys_permission, Sys_PermissionMapping>();
            CreateMap<Sys_PermissionMapping, Entities.sys_permission>();

            CreateMap<Entities.sys_role, Sys_RoleMapping>();
            CreateMap<Sys_RoleMapping, Entities.sys_role>();

            CreateMap<Entities.sys_setting, Sys_SettingMapping>();
            CreateMap<Sys_SettingMapping, Entities.sys_setting>();

            CreateMap<Entities.quarzt_schedule, QuarztScheduleMapping>();
            CreateMap<QuarztScheduleMapping, Entities.quarzt_schedule>();

            CreateMap<Entities.sys_activitylog_comment, Sys_ActivityLogCommentMapping>();
            CreateMap<Sys_ActivityLogCommentMapping, Entities.sys_activitylog_comment>();

        }
    }
}
