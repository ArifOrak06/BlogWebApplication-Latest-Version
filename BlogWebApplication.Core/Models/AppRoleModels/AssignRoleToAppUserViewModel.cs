namespace BlogWebApplication.Core.Models.AppRoleModels
{
    public class AssignRoleToAppUserViewModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = null!; // Role Adı 
        public bool Exist { get; set; } // Kullanıcının herhangi bir rolü var mı yok mu ? 
    }
}
