using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoApp.Models
{
    public class todo
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool check { get; set; }
    }
}