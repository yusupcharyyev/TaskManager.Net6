using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Areas.Manager.Models.MailSend;
using TaskManagerSystem.Areas.Manager.Models.VMs;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.Enums;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TaskManagerSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Route("Manager/[controller]/[action]")]
    public class ManagerController : Controller
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

        public ManagerController(UserManager<IdentityUser> userManager, IUserRepository userRepository, ITasksRepository tasksRepository, ITaskDescriptionRespository taskDescriptionRespository, IUserTaskRepository userTaskRepository, IManagerUserRepository managerUserRepository, IMapper mapper, IHostingEnvironment hostingEnvironment, SignInManager<IdentityUser> signInManager)
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
            List<Tasks> getAllTask = _tasksRepository.GetDefaults(a => a.Statu != Statu.Passive && a.UserID.ToString() == identityUser.Id);
            return View(getAllTask);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            IEnumerable<ManagerUser> managerUsers = _managerUserRepository.GetDefaults(a => a.ManagerId.ToString() == identityUser.Id);
            CreateTaskVM createTaskVM = new CreateTaskVM()
            {
                Users = managerUsers.Select(a => a.Users).ToList()
            };
            return View(createTaskVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskVM taskVM)
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            User user = _userRepository.GetDefault(a => a.Id == identityUser.Id);
            Guid guidID = Guid.NewGuid();
            var taskMap = _mapper.Map<Tasks>(taskVM);
            taskMap.ID = guidID;

            if (taskVM.File != null)
            {
                //var fileName = Path.GetFileName(taskVM.File.FileName);
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
            taskMap.UserID = Guid.Parse(user.Id);
            _tasksRepository.Create(taskMap);

            UserTask userTask = new UserTask()
            {
                TaskID = taskMap.ID,
                Taskss = taskMap,
                UserId = taskVM.SelectPersonel,
                Users = _userRepository.GetDefault(a => a.Id == taskVM.SelectPersonel.ToString())
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

            string email = _userRepository.GetDefault(a => a.Id == taskVM.SelectPersonel.ToString()).Email;
            EmailService emailService = new EmailService();
            emailService.Send(email, taskMap.Title, "Task'nızı Oluşturuldu");
         
            return RedirectToAction("Giris", "Manager");
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
