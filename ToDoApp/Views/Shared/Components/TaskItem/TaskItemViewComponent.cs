using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data.Entities;

namespace ToDoApp.Views.Shared.Components.TaskItem
{
    public class TaskItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TaskApp task)
        {
            return View(task);
        }
    }
}
