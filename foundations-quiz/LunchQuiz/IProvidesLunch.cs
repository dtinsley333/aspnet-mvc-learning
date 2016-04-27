using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    interface IProvidesLunch
    {
        bool openForLunch { get; set; }
        bool hasExpressLunchMenu { get; set; }
    }
}
