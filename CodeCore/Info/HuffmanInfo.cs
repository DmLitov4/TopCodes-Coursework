using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeCore;
using MetroFramework.Forms;
using System.IO;

namespace CodeCore.Info
{
    public partial class HuffmanInfo : MetroForm
    {
        public HuffmanInfo()
        {
            InitializeComponent();
        }

        private void HuffmanInfo_Load(object sender, EventArgs e)
        {
            rb.LoadFile("huffman.rtf");
            
            panel_info.Controls.Add(rb);


            using (var img = Image.FromFile("screen.png"))
            {
                Clipboard.SetImage(img);
                rb.Paste();
            }
        }

        private void metroTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (fontBar.Value > 1)
            {
                rb.SelectAll();
                rb.SelectionFont = new Font("Tahoma", fontBar.Value / 2, FontStyle.Regular);
            }
        }

        private void panel_info_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
