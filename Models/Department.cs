﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charity_management_system.Models
{
   public class Department
    {
        public String name { get; set; }
        public Department()
        {
            this.name = "";
        }
        public Department(String name)
        {
            this.name = name;
        }
    }
}
