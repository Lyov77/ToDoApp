using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ToDoApp.Controllers
{
    public abstract class ToDoBaseController : Controller
    {
       protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
