using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using ToDoApp.Data;
using ToDoApp.Data.Context;
using ToDoApp.Data.Entities;
using ToDoApp.Models;
using ToDoApp.Models.Home;

namespace ToDoApp.Controllers
{
    [Authorize]
    public class HomeController : ToDoBaseController
    {
        private readonly ToDoAppContext _context;

        public HomeController(ToDoAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(new HomeViewModel
            {
                Tasks = await GetTasksCurrentUserAsync()
            });
        }

        public async Task<IActionResult> CreateAsync(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tasks = await GetTasksCurrentUserAsync();
                return View("Index", model);
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Name.ToLower() == model.TaskName.ToLower()
                && t.UserId == CurrentUserId
                && t.ExpiredDate == model.DateTime);

            if (task != null)
            {
                model.Tasks = await GetTasksCurrentUserAsync();
                ViewBag.Error = "The Task already exists!";

                return View("Index", model);
            }

            await _context.Tasks.AddAsync(new TaskApp
            {
                Name = model.TaskName,
                ExpiredDate = model.DateTime.Value,
                UserId = CurrentUserId
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCompletedAsync(int id, bool isCompleted)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
            if (task != null)
            {
                task.IsCompleted = isCompleted;
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateNameAsync(int id, string name)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
            if (task != null)
            {
                if (task.Name != name)
                {
                    task.Name = name;
                    _context.Tasks.Update(task);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<TaskApp>> GetTasksCurrentUserAsync()
        {
            return await _context.Tasks
                .Where(task => task.UserId == CurrentUserId)
                .ToListAsync();
        }
    }
}
