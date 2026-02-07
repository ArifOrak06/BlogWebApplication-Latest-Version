using BlogWebApplication.Core.Helpers;
using BlogWebApplication.Core.Models.ImgModels;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace BlogWebApplication.Service.Helpers
{

    public class ImgHelper : IImgHelper
    {

        private readonly IFileProvider _fileProvider;

        public ImgHelper(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<CustomResponseModel<ImgUploadViewModel>> UploadOneImageAsync(IFormFile photo)
        {
            var wwRootFolder = _fileProvider.GetDirectoryContents("wwwroot");

            var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            if (photo != null && photo.Length > 0)
            {
               
                var newPicturePath = Path.Combine(wwRootFolder.First(x => x.Name == "pictures").PhysicalPath!, randomFileName);

                // Resim Kaydetme

                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await photo.CopyToAsync(stream);
                return CustomResponseModel<ImgUploadViewModel>.Success(ResponseType.Success, new ImgUploadViewModel() { FullName = randomFileName }, "Resim Upload işlemi başarılı olarak gerçekleştirilmiştir.");
            }
            return CustomResponseModel<ImgUploadViewModel>.Fail(ResponseType.Error, "Resim upload edilemedi.");
        }
    }
}
