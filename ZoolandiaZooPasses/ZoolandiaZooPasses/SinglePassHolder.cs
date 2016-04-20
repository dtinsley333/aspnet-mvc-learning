using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoolandiaZooPasses
{
    public class SinglePassHolder : IZooPass
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PassType { get; set; }
        public bool IsPassActive { get; set; }
    }
}
