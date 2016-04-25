using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicEntityFrameworkDataAccess.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SupervisorTitle { get; set; }

    }
}