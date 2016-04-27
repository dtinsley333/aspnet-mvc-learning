using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    interface IProvidesDinner
    {
        bool openForDinner { get; set; }
        bool hasEarlyBirdSpecial { get; set; }
    }
}
