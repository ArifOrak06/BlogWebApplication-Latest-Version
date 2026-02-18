using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppRoleModels;

namespace BlogWebApplication.Service.Utilities.AutoMapper
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<AppRole, AppRoleViewModel>().ReverseMap();
            CreateMap<AppRole, AppRoleUpdateViewModel>().ReverseMap();
            CreateMap<AppRole, AppRoleCreateViewModel>().ReverseMap();
            CreateMap<AppRoleViewModel, AppRoleCreateViewModel>().ReverseMap();
            CreateMap<AppRoleViewModel, AppRoleUpdateViewModel>().ReverseMap();
            CreateMap<AppRoleCreateViewModel, AppRoleUpdateViewModel>().ReverseMap();
        }
    }
}
