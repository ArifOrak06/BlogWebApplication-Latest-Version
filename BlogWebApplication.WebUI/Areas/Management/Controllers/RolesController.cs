using AutoMapper;
using Azure.Core;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppRoleModels;
using BlogWebApplication.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApplication.WebUI.Areas.Management.Controllers
{
    [Authorize]
    [Area("Management")]
    public class RolesController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<AppRole> roleManager, IMapper mapper, IAppUserService appUserService, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _appUserService = appUserService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roleViewModels = _mapper.Map<List<AppRoleViewModel>>(await _roleManager.Roles.ToListAsync());
            return View(roleViewModels);
        }

        public IActionResult AddRole()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(AppRoleCreateViewModel request)
        {
            var roleResult = await _roleManager.CreateAsync(_mapper.Map<AppRole>(request));
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }
            TempData["StatusMessage"] = "Role Ekleme İşlemi başarıyla gerçekleştirilmiştir.";
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> AssignRoleToAppUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            ViewBag.UserId = userId;
            // tüm roller
            List<AppRole>? allRoles = await _roleManager.Roles.ToListAsync();

            // kullanıcıda yer alan rolleri ve olmayan rollerin durumunu işaretleyerek listeleyeceğimiz obje list.
            List<AssignRoleToAppUserViewModel> assignRoleList = new();

            // Kullanıcının Rollerini seçelim.
            var userRoles = await _userManager.GetRolesAsync(user!);
            
            foreach(AppRole role in allRoles)
            {
                AssignRoleToAppUserViewModel assignRoleToAppUserModel = new()
                {
                    Id = role.Id,
                    Name=role.Name!
                    
                };
                // tüm roller üzerinde kurulan döngü sayesinde kullanıcının sahip olduğu rollerin Exist propertysini true'ya set edeceğiz, yani role sahiplik belli olacak.
                if (userRoles.Contains(role.Name!)) 
                    assignRoleToAppUserModel.Exist = true;
                assignRoleList.Add(assignRoleToAppUserModel);

            }

            return View(assignRoleList);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToAppUser(Guid userId, List<AssignRoleToAppUserViewModel> matchedRoleList)
        {
            // Buradaki kritik nokta [HttpGet] AssignRoleToAppUser() action methodu ile  View sayfasına ViewBag ile ilgili kullanıcının Id bilgisini taşıdık
            // akabinde form içerisinde route üzerinden [HttpPost] AssignRoleToAppUser methoduna gönderdik.  Burada da yakaladık. 

            AppUser user = (await _userManager.FindByIdAsync(userId.ToString()))!;
            foreach(AssignRoleToAppUserViewModel role in matchedRoleList)
            {
                if (role.Exist)
                    await _userManager.AddToRoleAsync(user,role.Name);
                else
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
            }
            await _userManager.UpdateSecurityStampAsync(user);
            TempData["StatusMessage"] = $"{user.UserName} isimli kullanıcıya role atama işlemi başarıyla gerçekleştirilmiştir.";
            return RedirectToAction("Index", "Users", new {Area="Management"});
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(Guid appRoleId)
        {
            AppRoleUpdateViewModel roleUpdateModel = _mapper.Map<AppRoleUpdateViewModel>(await _roleManager.Roles.Where(x => x.Id.Equals(appRoleId)).SingleOrDefaultAsync());
            if (roleUpdateModel == null)
                return NotFound();
            return View(roleUpdateModel);

        }
        [HttpPost]
        public async Task<IActionResult> EditRole(AppRoleUpdateViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);
            AppRole role = (await _roleManager.FindByIdAsync(request.Id.ToString()))!;
            role.Name = request.Name;
            var roleResult = await _roleManager.UpdateAsync(role);
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, roleResult.Errors.First().Description);
                return View();
            }
            TempData["StatusMessage"] = $" Role Id : {request.Id} olan role başarılı bir şekilde güncellenmiştir.";
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> DeleteRole(Guid appRoleId)
        {
            AppRole role = (await _roleManager.FindByIdAsync(appRoleId.ToString()))!;
            if (role == null) return NotFound();
            await _roleManager.DeleteAsync(role);
            TempData["StatusMessage"] = $"Role adı  : {role.Name} olan role silme işlemi başarıyla gerçekleştirilmiştir.";
            return RedirectToAction(nameof(Index));
        }

    }
}
