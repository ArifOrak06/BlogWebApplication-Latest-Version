using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppUserModels;

namespace BlogWebApplication.Service.Utilities.AutoMapper
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser,AppUserViewModel>().ReverseMap();
            CreateMap<AppUser,AppUserSignUpViewModel>().ReverseMap();
            CreateMap<AppUser, AppUserEditViewModel>().ReverseMap();
            
            CreateMap<AppUserViewModel, AppUserSignUpViewModel>().ReverseMap();
            CreateMap<AppUserViewModel, AppUserEditViewModel>().ReverseMap();
            CreateMap<AppUserSignUpViewModel, AppUserEditViewModel>().ReverseMap();
        }
    }
}
