using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoolandiaZooPasses
{
    public class FamilyPassHolder : IZooPass
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PassType { get; set; }
        public int FamilySize { get; set; }
        public bool IsPassActive { get; set; }
    }
}
