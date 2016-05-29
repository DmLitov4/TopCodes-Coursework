namespace CodeCore.Info
{
    partial class ShannonInfo
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
            this.groupBox_info2 = new System.Windows.Forms.GroupBox();
            this.panel_info2 = new MetroFramework.Controls.MetroPanel();
            this.rb2 = new System.Windows.Forms.RichTextBox();
            this.fontBar2 = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox_info2.SuspendLayout();
            this.panel_info2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_info2
            // 
            this.groupBox_info2.Controls.Add(this.panel_info2);
            this.groupBox_info2.Location = new System.Drawing.Point(26, 58);
            this.groupBox_info2.Name = "groupBox_info2";
            this.groupBox_info2.Size = new System.Drawing.Size(662, 372);
            this.groupBox_info2.TabIndex = 0;
            this.groupBox_info2.TabStop = false;
            this.groupBox_info2.Text = "Код Шеннона";
            // 
            // panel_info2
            // 
            this.panel_info2.Controls.Add(this.rb2);
            this.panel_info2.HorizontalScrollbarBarColor = true;
            this.panel_info2.HorizontalScrollbarHighlightOnWheel = false;
            this.panel_info2.HorizontalScrollbarSize = 10;
            this.panel_info2.Location = new System.Drawing.Point(16, 20);
            this.panel_info2.Name = "panel_info2";
            this.panel_info2.Size = new System.Drawing.Size(640, 340);
            this.panel_info2.TabIndex = 0;
            this.panel_info2.VerticalScrollbarBarColor = true;
            this.panel_info2.VerticalScrollbarHighlightOnWheel = false;
            this.panel_info2.VerticalScrollbarSize = 10;
            // 
            // rb2
            // 
            this.rb2.Location = new System.Drawing.Point(4, 4);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(624, 342);
            this.rb2.TabIndex = 2;
            this.rb2.Text = "";
            this.rb2.TextChanged += new System.EventHandler(this.rb2_TextChanged);
            // 
            // fontBar2
            // 
            this.fontBar2.BackColor = System.Drawing.Color.Transparent;
            this.fontBar2.Location = new System.Drawing.Point(378, 29);
            this.fontBar2.Name = "fontBar2";
            this.fontBar2.Size = new System.Drawing.Size(75, 23);
            this.fontBar2.TabIndex = 1;
            this.fontBar2.Text = "metroTrackBar1";
            this.fontBar2.Value = 24;
            this.fontBar2.ValueChanged += new System.EventHandler(this.fontBar2_ValueChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(263, 29);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(109, 38);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Размер шрифта:\r\n";
            // 
            // ShannonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 441);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.fontBar2);
            this.Controls.Add(this.groupBox_info2);
            this.Name = "ShannonInfo";
            this.Text = "Справка";
            this.Load += new System.EventHandler(this.ShannonInfo_Load_1);
            this.groupBox_info2.ResumeLayout(false);
            this.panel_info2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel panel_info2;
        private System.Windows.Forms.RichTextBox rb2;
        private System.Windows.Forms.GroupBox groupBox_info2;
        private MetroFramework.Controls.MetroTrackBar fontBar2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}