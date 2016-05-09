using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TravelCardPrint
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1(string filename)
        {
            InitializeComponent();
            this.axAcroPDF1.LoadFile(filename);
        }

        private void axAcroPDF1_OnError(object sender, EventArgs e)
        {

        }
    }
}
