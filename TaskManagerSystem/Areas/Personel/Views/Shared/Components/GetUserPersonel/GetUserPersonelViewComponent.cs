using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.Areas.Personel.Views.Shared.Components.GetUserPersonel
{
    [ViewComponent(Name ="GetUserPersonel")]
    public class GetUserPersonelViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;

        public GetUserPersonelViewComponent(UserManager<IdentityUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IdentityUser user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
            User userinfo = _userRepository.GetDefault(a => a.Id == user.Id);
            if (userinfo!=null)
            {
                return View(userinfo);
            }
            return View();
        }
    }
}
