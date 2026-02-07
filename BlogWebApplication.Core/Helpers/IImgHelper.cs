using BlogWebApplication.Core.Models.ImgModels;
using BlogWebApplication.SharedLibrary.RRP;
using Microsoft.AspNetCore.Http;

namespace BlogWebApplication.Core.Helpers
{
    public interface IImgHelper
    {
        Task<CustomResponseModel<ImgUploadViewModel>> UploadOneImageAsync(IFormFile photo);
    }
}
