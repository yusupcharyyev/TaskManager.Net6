using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Areas.Admin.Models.DTOs;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.Enums;

namespace TaskManagerSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IManagerUserRepository _managerUserRepository;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(UserManager<IdentityUser> userManager, IMapper mapper, ICompanyRepository companyRepository, IUserRepository userRepository,IManagerUserRepository managerUserRepository, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _managerUserRepository = managerUserRepository;
            _signInManager = signInManager;
        }
        public IActionResult Giris()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateYonetici()
        {
            CreateManagerDTO dTO = new CreateManagerDTO()
            {
                Companies = _companyRepository.GetDefaults(a => a.Statu != Statu.Passive)
            };
            return View(dTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateYonetici(CreateManagerDTO dto)
        {
            //if (ModelState.IsValid)
            //{
            Company company = _companyRepository.GetDefault(a => a.ID == dto.SelectCompany);

            string newID = Guid.NewGuid().ToString();
            User user = new User { Email = dto.Email, UserName = dto.UserName, Id = newID, FirstName = dto.FirstName, LastName = dto.LastName, CompanyID = dto.SelectCompany, Companys = company };

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Yonetici");

                return RedirectToAction("CreateYonetici", "Admin");
            }
            //}

            return View(dto);
        }

        [HttpGet]
        public IActionResult CreateCompany()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCompany(CreateCompanyDTO companyDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Company>(companyDTO);
                _companyRepository.Create(map);
                ModelState.AddModelError("Success", "Şirket Başarıyla Eklendi");
            }
            return View(companyDTO);
        }

        [HttpGet]
        public IActionResult CreatePersonel()
        {

            CreatePersonelDTO dTO = new CreatePersonelDTO()
            {
                Yoneticiler = (List<IdentityUser>)_userManager.GetUsersInRoleAsync("Yonetici").Result
            };
            return View(dTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonel(CreatePersonelDTO personelDTO)
        {
            //if (ModelState.IsValid)
            //{
                User yonetici = _userRepository.GetDefault(a => a.Id == personelDTO.YoneticiID.ToString());
                Company company = _companyRepository.GetDefault(a => a.ID == yonetici.CompanyID);

                string newID = Guid.NewGuid().ToString();
                User user = new User { Email = personelDTO.Email, UserName = personelDTO.UserName, Id = newID, FirstName = personelDTO.FirstName, LastName = personelDTO.LastName, CompanyID = yonetici.CompanyID, Companys = company };

                IdentityResult result = await _userManager.CreateAsync(user, personelDTO.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Personel");
                    ManagerUser managerUser = new ManagerUser()
                    {
                        Manager = yonetici,
                        ManagerId = personelDTO.YoneticiID,
                        Users = user,
                        UserId = Guid.Parse(newID)
                    };
                    _managerUserRepository.Create(managerUser);
                    return RedirectToAction("CreateYonetici", "Admin");
                }
            //}
            return View(personelDTO);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
