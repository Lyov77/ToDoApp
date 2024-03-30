using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Entities
{
    public abstract class EntityBase<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<int> 
    {
    }
}
