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
        private string ReplaceInvalidChars(string fileName)
        {
            return fileName.Replace("İ", "I")
                 .Replace("ı", "i")
                 .Replace("Ğ", "G")
                 .Replace("ğ", "g")
                 .Replace("Ü", "U")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "S")
                 .Replace("Ö", "O")
                 .Replace("ö", "o")
                 .Replace("Ç", "C")
                 .Replace("ç", "c")
                 .Replace("é", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace("*", "")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("#", "")
                 .Replace("$", "")
                 .Replace("½", "")
                 .Replace("{", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("}", "")
                 .Replace(@"\", "")
                 .Replace("|", "")
                 .Replace("~", "")
                 .Replace("¨", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace("`", "")
                 .Replace(".", "")
                 .Replace(":", "")
                 .Replace(" ", "");
        }

        public async Task<CustomResponseModel<ImgUploadViewModel>> UploadOneImageAsync(IFormFile photo,string fileName= null)
        {
            var wwRootFolder = _fileProvider.GetDirectoryContents("wwwroot");

            var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            if (photo != null && photo.Length > 0)
            {
               
                var newPicturePath = Path.Combine(wwRootFolder.First(x => x.Name == "pictures").PhysicalPath!, randomFileName);

                // Resim Kaydetme

                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await photo.CopyToAsync(stream);
                await stream.FlushAsync();
                return CustomResponseModel<ImgUploadViewModel>.Success(ResponseType.Success, new ImgUploadViewModel() { FullName = randomFileName,FileType = Path.GetExtension(photo.FileName).ToString() }, "Resim Upload işlemi başarılı olarak gerçekleştirilmiştir.");
            }
            return CustomResponseModel<ImgUploadViewModel>.Fail(ResponseType.Error, "Resim upload edilemedi.");
        }

        public async Task DeleteAsync(string imageName)
        {
            var fileToDelete = Path.Combine($"{_fileProvider.GetDirectoryContents("wwwroot")}/pictures/{imageName}");
            if(File.Exists(fileToDelete))
                File.Delete(fileToDelete);
            await Task.CompletedTask;
        }
    }
}
