using BlogWebApplication.Core.Helpers;
using BlogWebApplication.Core.Models.ImgModels;
using BlogWebApplication.SharedLibrary.RRP;
using Microsoft.AspNetCore.Http;

namespace BlogWebApplication.Service.Helpers
{

    public class ImgHelper : IImgHelper
    {
        public Task<CustomResponseModel<ImgUploadViewModel>> UploadOneImageAsync(IFormFile photo)
        {
            throw new NotImplementedException();
        }
    }
}
