using System.ComponentModel.DataAnnotations;
using ToDoApp.Data.Entities;

namespace ToDoApp.Models.Home
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public string TaskName {  get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime? DateTime { get; set; }

        public IEnumerable<TaskApp>? Tasks { get; set; }

    }
}
