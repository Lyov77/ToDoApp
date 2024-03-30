using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data.Context
{
    public class ToDoAppContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<TaskApp> Tasks { get; set; }

        public ToDoAppContext(DbContextOptions options) : base(options)
        {
        }
    }
}
