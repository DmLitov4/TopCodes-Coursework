namespace CodeCore.Info
{
    partial class HuffmanInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fontBar = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox_info = new System.Windows.Forms.GroupBox();
            this.panel_info = new System.Windows.Forms.Panel();
            this.rb = new System.Windows.Forms.RichTextBox();
            this.groupBox_info.SuspendLayout();
            this.panel_info.SuspendLayout();
            this.SuspendLayout();
            // 
            // fontBar
            // 
            this.fontBar.BackColor = System.Drawing.Color.Transparent;
            this.fontBar.Location = new System.Drawing.Point(371, 28);
            this.fontBar.Name = "fontBar";
            this.fontBar.Size = new System.Drawing.Size(75, 23);
            this.fontBar.TabIndex = 0;
            this.fontBar.Text = "metroTrackBar1";
            this.fontBar.Value = 24;
            this.fontBar.ValueChanged += new System.EventHandler(this.metroTrackBar1_ValueChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(256, 28);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(109, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Размер шрифта:";
            // 
            // groupBox_info
            // 
            this.groupBox_info.Controls.Add(this.panel_info);
            this.groupBox_info.Location = new System.Drawing.Point(20, 70);
            this.groupBox_info.Name = "groupBox_info";
            this.groupBox_info.Size = new System.Drawing.Size(522, 372);
            this.groupBox_info.TabIndex = 2;
            this.groupBox_info.TabStop = false;
            this.groupBox_info.Text = "Код Хаффмена";
            // 
            // panel_info
            // 
            this.panel_info.AutoScroll = true;
            this.panel_info.Controls.Add(this.rb);
            this.panel_info.Location = new System.Drawing.Point(17, 19);
            this.panel_info.Name = "panel_info";
            this.panel_info.Size = new System.Drawing.Size(499, 346);
            this.panel_info.TabIndex = 0;
            this.panel_info.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_info_Paint);
            // 
            // rb
            // 
            this.rb.Location = new System.Drawing.Point(0, 0);
            this.rb.Name = "rb";
            this.rb.Size = new System.Drawing.Size(489, 343);
            this.rb.TabIndex = 0;
            this.rb.Text = "";
            // 
            // HuffmanInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 455);
            this.Controls.Add(this.groupBox_info);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.fontBar);
            this.MaximizeBox = false;
            this.Name = "HuffmanInfo";
            this.ShowIcon = false;
            this.Text = "Справка";
            this.Load += new System.EventHandler(this.HuffmanInfo_Load);
            this.groupBox_info.ResumeLayout(false);
            this.panel_info.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTrackBar fontBar;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.GroupBox groupBox_info;
        private System.Windows.Forms.Panel panel_info;
        private System.Windows.Forms.RichTextBox rb;
    }
}