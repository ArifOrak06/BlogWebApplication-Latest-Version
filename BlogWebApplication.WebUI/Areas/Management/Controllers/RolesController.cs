using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppRoleModels;
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
        private readonly IMapper _mapper;
        public RolesController(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
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

    }
}
