using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoApp.Models;
using System.Data.Entity;


namespace ToDoApp.DAL
{
    public class context : DbContext
    {
        public DbSet<todo> Todos { get; set; }
    }
}