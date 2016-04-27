using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    public class FineDining : Restaurant, IProvidesDinner
    {
        public bool openForDinner { get; set; }
        public bool hasEarlyBirdSpecial { get; set; }
        public int NumberofSeatsAtBar { get; set; } // only fine dining has a bar

        public override int capacity() // this method overrides Restaurant.capacity()
        {
            return this.NumberofSeatsAtBar + this.Numberof2PersonTables * 2 + this.Numberof4PersonTables * 4;
        }

        // constructor
        public FineDining()
        {
            this.openForDinner = true;
            this.hasEarlyBirdSpecial = true;
        }
    }
}
