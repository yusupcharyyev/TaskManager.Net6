using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Areas.Manager.Models.MailSend;
using TaskManagerSystem.Areas.Personel.Models.VMs;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.Enums;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TaskManagerSystem.Areas.Personel.Controllers
{
    [Area("Personel")]
    [Route("Personel/[controller]/[action]")]
    public class PersonelController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly ITaskDescriptionRespository _taskDescriptionRespository;
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IManagerUserRepository _managerUserRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PersonelController(UserManager<IdentityUser> userManager, IUserRepository userRepository, ITasksRepository tasksRepository, ITaskDescriptionRespository taskDescriptionRespository, IUserTaskRepository userTaskRepository, IManagerUserRepository managerUserRepository, IMapper mapper, IHostingEnvironment hostingEnvironment, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _tasksRepository = tasksRepository;
            _taskDescriptionRespository = taskDescriptionRespository;
            _userTaskRepository = userTaskRepository;
            _managerUserRepository = managerUserRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Giris()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            List<UserTask> userTasks = _userTaskRepository.GetDefaults(a => a.UserId.ToString() == identityUser.Id);

            return View(userTasks);
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskPersonelVM taskVM)
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            User user = _userRepository.GetDefault(a => a.Id == identityUser.Id);
            Guid guidID = Guid.NewGuid();
            var taskMap = _mapper.Map<Tasks>(taskVM);
            taskMap.ID = guidID;

            if (taskVM.File != null)
            {
                string ext = Path.GetExtension(taskVM.File.FileName);
                if (ext.ToLower() != ".pdf")
                {
                    return View(taskVM);
                }
                else
                {
                    var filePath = Path.Combine("wwwroot\\TaskDoc", guidID.ToString() + ".pdf");
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await taskVM.File.CopyToAsync(fileSteam);
                    }
                    taskMap.FilePath = guidID + ".pdf";
                }
            }
            taskMap.UserID = _managerUserRepository.GetDefault(a=>a.UserId.ToString()==user.Id).ManagerId;
            _tasksRepository.Create(taskMap);

            UserTask userTask = new UserTask()
            {
                TaskID = taskMap.ID,
                Taskss = taskMap,
                UserId = Guid.Parse(user.Id),
                Users = user
            };
            _userTaskRepository.Create(userTask);

            if (taskVM.Description != null)
            {
                TaskDescription taskDescription = new TaskDescription()
                {
                    Description = taskVM.Description,
                    UserId = Guid.Parse(user.Id),
                    Users = user,
                    TaskId = taskMap.ID,
                    Taskss = taskMap
                };
                _taskDescriptionRespository.Create(taskDescription);
            }

            //string email = _managerUserRepository.GetDefault(a => a.UserId.ToString() == user.Id).Manager.Email;
            //EmailService emailService = new EmailService();
            //emailService.Send(email, taskMap.Title, "Task'nızı Oluşturuldu");

            return RedirectToAction("Giris", "Personel");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public FileResult DownloadFile(string fileName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "TaskDoc/") + fileName;

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", fileName);
        }

    }
}
