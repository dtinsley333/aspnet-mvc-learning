using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoolandiaZooPasses
{
    public interface IZooPass
    {
        Guid CustomerId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string PassType { get; set; }
        bool IsPassActive { get; set; }
    }
}
