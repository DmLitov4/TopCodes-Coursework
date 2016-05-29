using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace CodeCore.Info
{
    public partial class ShannonInfo : MetroForm
    {
       
            public ShannonInfo()
            {
                InitializeComponent();
            }

            private void ShannonInfo_Load_1(object sender, EventArgs e)
            {
                rb2.LoadFile("shannon.rtf");

                panel_info2.Controls.Add(rb2);


                using (var img = Image.FromFile("screen2.png"))
                {
                    Clipboard.SetImage(img);
                    rb2.Paste();
                }
                using (var img = Image.FromFile("screen3.png"))
                {
                    Clipboard.SetImage(img);
                    rb2.Paste();
                }
        }

         
            private void panel_info_Paint(object sender, PaintEventArgs e)
            {

            }

        private void fontBar2_ValueChanged(object sender, EventArgs e)
        {
            if (fontBar2.Value > 1)
            {
                rb2.SelectAll();
                rb2.SelectionFont = new Font("Tahoma", fontBar2.Value / 2, FontStyle.Regular);
            }
        }

        private void rb2_TextChanged(object sender, EventArgs e)
        {

        }
    }
 
}
