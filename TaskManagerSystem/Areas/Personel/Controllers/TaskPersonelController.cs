using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.Enums;
using Action = TaskManagerSystem.Models.Enums.Action;

namespace TaskManagerSystem.Areas.Personel.Controllers
{
    [Area("Personel")]
    [Route("Personel/[controller]/[action]")]
    public class TaskPersonelController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITasksRepository _tasksRepository;
        private readonly ITaskDescriptionRespository _taskDescriptionRespository;
        private readonly IUserRepository _userRepository;

        public TaskPersonelController(UserManager<IdentityUser> userManager, ITasksRepository tasksRepository, ITaskDescriptionRespository taskDescriptionRespository, IUserRepository userRepository)
        {
            _userManager = userManager;
            _tasksRepository = tasksRepository;
            _taskDescriptionRespository = taskDescriptionRespository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            Tasks userTasks = _tasksRepository.GetDefault(a => a.ID == id);
            return View(userTasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddDescription(IFormCollection keys)
        {
            IdentityUser user = await _userManager.GetUserAsync(User);
            User userO = _userRepository.GetDefault(a => a.Id == user.Id);
            var Description = keys["description"];
            var taskId = keys["ID"];
            Tasks tasks = _tasksRepository.GetDefault(a => a.ID == Guid.Parse(taskId));
            TaskDescription taskDescription = new TaskDescription()
            {
                ID = Guid.NewGuid(),
                Description = Description,
                TaskId = Guid.Parse(taskId),
                Taskss = tasks,
                UserId = Guid.Parse(userO.Id),
                Users = userO
            };
            _taskDescriptionRespository.Create(taskDescription);

            return RedirectToAction("Update", "TaskPersonel", new { id = taskId.ToString() });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatu(IFormCollection keyValues)
        {
            IdentityUser user = await _userManager.GetUserAsync(User);
            User userO = _userRepository.GetDefault(a => a.Id == user.Id);
            var action = keyValues["action"];
            var priority = keyValues["priority"];
            var taskId = keyValues["ID"];
            Tasks tasks = _tasksRepository.GetDefault(a => a.ID == Guid.Parse(taskId));
            tasks.Action = Enum.Parse<Action>(action);
            tasks.priorityStatu = Enum.Parse<PriorityStatu>(priority);
            _tasksRepository.Update(tasks);

            return RedirectToAction("Update", "TaskPersonel", new { id = taskId.ToString() });
        }
    }
}
