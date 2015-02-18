using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ToDoApp_AngJS_WebAPI.Model
{
    public class ToDoTaskContext : DbContext
    {
        public virtual DbSet<ToDoTask> ToDoTasks { get; set; }
       
    }
  
    
}