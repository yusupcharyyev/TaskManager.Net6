using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models;
using TaskManagerSystem.Models.DTOs;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.Enums;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TaskManagerSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserRepository userRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await _userManager.FindByNameAsync(userDTO.UserName);

                if (identityUser!=null)
                {
                    await _signInManager.SignOutAsync();
                    SignInResult signInResult = await _signInManager.PasswordSignInAsync(identityUser, userDTO.Password, true, true);
                    User user = _userRepository.GetDefault(a => a.Id == identityUser.Id);
                    if (signInResult.Succeeded && user.Statu!=Statu.Passive)
                    {
                        string role = (await _userManager.GetRolesAsync(identityUser)).FirstOrDefault();
                        if (role == "Yonetici")
                        {
                            return RedirectToAction("Giris", "Manager", new { area = "Manager" });
                        }
                        if (role == "Admin")
                        {
                            return RedirectToAction("Giris", "Admin", new { area=role });
                        }
                        if (role == "Personel")
                        {
                            return RedirectToAction("Giris", "Personel", new { area = role });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Girilen bilgiler hatalı");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Girilen bilgiler hatalı");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Girilen bilgiler hatalı");
            }
            return View(userDTO);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}