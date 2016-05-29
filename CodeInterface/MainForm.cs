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
using CodeCore.Codes;
using CodeCore.Info;
using MetroFramework.Forms;

namespace CodeInterface
{
    public partial class MainForm : MetroForm
    {
        private MetroFramework.Controls.MetroLabel nameLabel;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage HuffmanPage;
        private MetroFramework.Controls.MetroTabPage ShennonPage;
        private MetroFramework.Controls.MetroTabPage GMPage;
        private MetroFramework.Controls.MetroTabPage ACPage;
        private MetroFramework.Controls.MetroButton build_button;
        private PictureBox pictureBox1;
        private MetroFramework.Controls.MetroGrid symprobtable;
        private DataGridViewTextBoxColumn Sym;
        private DataGridViewTextBoxColumn Prob;
        private MetroFramework.Controls.MetroButton theory_button;
        private MetroFramework.Controls.MetroButton copy_button;
        private RichTextBox resbox;
        private MetroFramework.Controls.MetroButton clear_button;
        public MetroFramework.Controls.MetroGrid symprobtable2;
        private RichTextBox resbox2;
        private RichTextBox resbox3;
        private MetroFramework.Controls.MetroGrid symprobtable3;
        private DataGridViewTextBoxColumn Sym3;
        private DataGridViewTextBoxColumn p_i;
        private DataGridViewTextBoxColumn qum;
        private DataGridViewTextBoxColumn sigma;
        private DataGridViewTextBoxColumn length3;
        private DataGridViewTextBoxColumn Length;
        private DataGridViewTextBoxColumn Qum_prob;
        private DataGridViewTextBoxColumn Prob2;
        private DataGridViewTextBoxColumn Sym2;
        private MetroFramework.Controls.MetroGrid symprobtable4;
        private DataGridViewTextBoxColumn sym4;
        private DataGridViewTextBoxColumn pi;
        private DataGridViewTextBoxColumn qi;
        private DataGridViewTextBoxColumn P;
        private DataGridViewTextBoxColumn Q;
        private DataGridViewTextBoxColumn li;
        private DataGridViewTextBoxColumn Code;
        public TextBox inputstr;
        private MetroFramework.Controls.MetroLabel aclabel;
        private TextBox codeBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        List<Tuple<char, double>> symprob = new List<Tuple<char, double>>();
        

        public MainForm()
        {
            InitializeComponent();     
        }

        private void buildHuffman(Code cod, string res)
        {
            if (symprobtable.RowCount < 2)
            {
                MessageBox.Show("Таблица с данными пуста, сначала введите данные!");
                return;
            }
            for (int i = 0; i < symprobtable.RowCount - 1; i++)
                symprob.Add(new Tuple<char, double>(symprobtable.Rows[i].Cells[0].Value.ToString()[0], Convert.ToDouble(symprobtable.Rows[i].Cells[1].Value.ToString())));
            cod = new Huffman(symprob);

            cod.buildCode();
            var list = cod.getAllSym();
            for (int i = 0; i < cod.getAllSym().Count; i++)
                res += list[i].ToString() + " : " + cod.getRes(list[i]) + '\n';
            resbox.Enabled = true;
            resbox.Text = res;
            symprob.Clear();
        }

        private void buildShennon(Code cod, string res)
        {
            if (symprobtable2.RowCount < 2)
            {
                MessageBox.Show("Таблица с данными пуста, сначала введите данные!");
                return;
            }
            for (int i = 0; i < symprobtable2.RowCount - 1; i++)
            {
                double trynum;
                bool check = Double.TryParse(symprobtable2.Rows[i].Cells[1].Value.ToString(), out trynum);
                if (check)
                    symprob.Add(new Tuple<char, double>(symprobtable2.Rows[i].Cells[0].Value.ToString()[0], trynum));
                else
                {
                    MessageBox.Show("Uncorrect data in table!");
                    cod.clearData();
                    return;
                }
            }
            cod = new Shannon(symprob);
            cod.buildCode();
            var list = cod.getAllSym();
            for (int i = 0; i < cod.getAllSym().Count; i++)
                res += list[i].ToString() + " : " + cod.getRes(list[i]) + '\n';

            var qp = (cod as Shannon).getQumProb();
            var len = (cod as Shannon).setLength();
            for (int i = 0; i < symprobtable2.RowCount - 1; i++)
            {
                symprobtable2[3, i].Value = Math.Abs(len[i].Value).ToString();
                symprobtable2[2, i].Value = qp[i].Value.ToString();
            }

            resbox2.Enabled = true;
            resbox2.Text = res;
            symprob.Clear();
        }

        private void buildGM(Code cod, string res)
        {
            if (symprobtable3.RowCount < 2)
            {
                MessageBox.Show("Таблица с данными пуста, сначала введите данные!");
                return;
            }
            for (int i = 0; i < symprobtable3.RowCount - 1; i++)
            {
                double trynum;
                bool check = Double.TryParse(symprobtable3.Rows[i].Cells[1].Value.ToString(), out trynum);
                if (check)
                    symprob.Add(new Tuple<char, double>(symprobtable3.Rows[i].Cells[0].Value.ToString()[0], trynum));
                else
                {
                    MessageBox.Show("Uncorrect data in table!");
                    cod.clearData();
                    return;
                }
            }
            cod = new GuilbertMoore(symprob);
            cod.buildCode();
            var list = cod.getAllSym();
            for (int i = 0; i < cod.getAllSym().Count; i++)
                res += list[i].ToString() + " : " + cod.getRes(list[i]) + '\n';
            var qp = (cod as GuilbertMoore).getQumProb();
            var len = (cod as GuilbertMoore).setLength();
            var sig = (cod as GuilbertMoore).getSigmaProb();
            for (int i = 0; i < symprobtable3.RowCount - 1; i++)
            {
                symprobtable3[4, i].Value = Math.Abs(len[i].Value).ToString();
                symprobtable3[2, i].Value = qp[i].Value.ToString();
                symprobtable3[3, i].Value = sig[i].Value.ToString();
            }
            resbox3.Enabled = true;
            resbox3.Text = res;
            symprob.Clear();
        }

        public void buildAM(Code cod, string res)
        {
            if (symprobtable4.RowCount < 2)
            {
                MessageBox.Show("Таблица с данными пуста, сначала введите данные!");
                return;
            }
            for (int i = 0; i < symprobtable4.RowCount - 1; i++)
            {
                double trynum;
                bool check = Double.TryParse(symprobtable4.Rows[i].Cells[1].Value.ToString(), out trynum);
                if (check)
                    symprob.Add(new Tuple<char, double>(symprobtable4.Rows[i].Cells[0].Value.ToString()[0], trynum));
                else
                {
                    MessageBox.Show("Uncorrect data in table!");
                    cod.clearData();
                    return;
                }
            }

            cod = new ArithmetCoding(symprob, this.inputstr.Text);
            cod.buildCode();
            codeBox.Text = cod.getRes((cod as ArithmetCoding).getInput()[0]);
        }

        //событие по нажатию кнопки "построить"
        private void build_button_Click(object sender, EventArgs e)
        {
            Code cod = new Code();
            string res = "";
            if (this.metroTabControl1.SelectedTab.Text == "Код Хаффмана")
                buildHuffman(cod, res);
            else if (this.metroTabControl1.SelectedTab.Text == "Код Шеннона")
                buildShennon(cod, res);
            else if (this.metroTabControl1.SelectedTab.Text == "Код Гилберта-Мура")
                buildGM(cod, res);
           
           else if (this.metroTabControl1.SelectedTab.Text == "Арифметическое кодирование")
               buildAM(cod, res);
            cod.clearData();
        }

        //копирование результата в буфер обмена
        private void copy_button_Click(object sender, EventArgs e)
        {
            RichTextBox currResBox = new RichTextBox();
            Control currRes = metroTabControl1.SelectedTab;
            foreach (Control c in currRes.Controls)
                if (c.Name.StartsWith("resbox"))
                {
                    currResBox = c as RichTextBox;
                    break;
                }
            if (currResBox.Text != "")
            {
                Clipboard.Clear();
                Clipboard.SetText(currResBox.Text);
            }
            else
                MessageBox.Show("Результат ещё не построен!");
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            var selectedpage = this.metroTabControl1.SelectedTab;
            foreach (Control c in selectedpage.Controls)
                if (c.Name.StartsWith("symprobtable"))
                {
                    (c as MetroFramework.Controls.MetroGrid).DataSource = null;
                    (c as MetroFramework.Controls.MetroGrid).Rows.Clear();
                }
            resbox.Clear();   
        }


        private void theory_button_Click(object sender, EventArgs e)
        {
            if (this.metroTabControl1.SelectedTab.Text == "Код Хаффмана")
            {
                HuffmanInfo HuffInfo = new HuffmanInfo();
                HuffInfo.Show();
            }
            else if (this.metroTabControl1.SelectedTab.Text == "Код Шеннона")
            {
                ShannonInfo ShanInfo = new ShannonInfo();
                ShanInfo.Show();
            }
        }


        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.nameLabel = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.HuffmanPage = new MetroFramework.Controls.MetroTabPage();
            this.resbox = new System.Windows.Forms.RichTextBox();
            this.symprobtable = new MetroFramework.Controls.MetroGrid();
            this.Sym = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShennonPage = new MetroFramework.Controls.MetroTabPage();
            this.resbox2 = new System.Windows.Forms.RichTextBox();
            this.symprobtable2 = new MetroFramework.Controls.MetroGrid();
            this.Sym2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prob2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qum_prob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GMPage = new MetroFramework.Controls.MetroTabPage();
            this.symprobtable3 = new MetroFramework.Controls.MetroGrid();
            this.Sym3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_i = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resbox3 = new System.Windows.Forms.RichTextBox();
            this.ACPage = new MetroFramework.Controls.MetroTabPage();
            this.symprobtable4 = new MetroFramework.Controls.MetroGrid();
            this.sym4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Q = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.li = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inputstr = new System.Windows.Forms.TextBox();
            this.aclabel = new MetroFramework.Controls.MetroLabel();
            this.clear_button = new MetroFramework.Controls.MetroButton();
            this.copy_button = new MetroFramework.Controls.MetroButton();
            this.theory_button = new MetroFramework.Controls.MetroButton();
            this.build_button = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.metroTabControl1.SuspendLayout();
            this.HuffmanPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable)).BeginInit();
            this.ShennonPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable2)).BeginInit();
            this.GMPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable3)).BeginInit();
            this.ACPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.nameLabel.Location = new System.Drawing.Point(180, 28);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(375, 132);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Добро пожаловать в программу, позволяющую\r\nстроить популярные коды, представленны" +
    "е в курсе\r\n\"Теория информации и криптографии\" (В.С. Пилиди).\r\nНаучный руководите" +
    "ль - Абрамян М.Э.";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nameLabel.Click += new System.EventHandler(this.metroLabel1_Click);
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.HuffmanPage);
            this.metroTabControl1.Controls.Add(this.ShennonPage);
            this.metroTabControl1.Controls.Add(this.GMPage);
            this.metroTabControl1.Controls.Add(this.ACPage);
            this.metroTabControl1.Location = new System.Drawing.Point(23, 153);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 3;
            this.metroTabControl1.Size = new System.Drawing.Size(628, 248);
            this.metroTabControl1.TabIndex = 5;
            this.metroTabControl1.UseSelectable = true;
            this.metroTabControl1.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            // 
            // HuffmanPage
            // 
            this.HuffmanPage.Controls.Add(this.resbox);
            this.HuffmanPage.Controls.Add(this.symprobtable);
            this.HuffmanPage.HorizontalScrollbarBarColor = true;
            this.HuffmanPage.HorizontalScrollbarHighlightOnWheel = false;
            this.HuffmanPage.HorizontalScrollbarSize = 10;
            this.HuffmanPage.Location = new System.Drawing.Point(4, 35);
            this.HuffmanPage.Name = "HuffmanPage";
            this.HuffmanPage.Size = new System.Drawing.Size(620, 209);
            this.HuffmanPage.TabIndex = 0;
            this.HuffmanPage.Text = "Код Хаффмана";
            this.HuffmanPage.VerticalScrollbarBarColor = true;
            this.HuffmanPage.VerticalScrollbarHighlightOnWheel = false;
            this.HuffmanPage.VerticalScrollbarSize = 10;
            // 
            // resbox
            // 
            this.resbox.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resbox.Location = new System.Drawing.Point(435, 3);
            this.resbox.Name = "resbox";
            this.resbox.ReadOnly = true;
            this.resbox.Size = new System.Drawing.Size(182, 194);
            this.resbox.TabIndex = 6;
            this.resbox.Text = "";
            // 
            // symprobtable
            // 
            this.symprobtable.AllowUserToResizeRows = false;
            this.symprobtable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.symprobtable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.symprobtable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.symprobtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symprobtable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sym,
            this.Prob});
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.symprobtable.DefaultCellStyle = dataGridViewCellStyle32;
            this.symprobtable.EnableHeadersVisualStyles = false;
            this.symprobtable.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.symprobtable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable.Location = new System.Drawing.Point(3, 0);
            this.symprobtable.Name = "symprobtable";
            this.symprobtable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable.RowHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.symprobtable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.symprobtable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.symprobtable.Size = new System.Drawing.Size(258, 197);
            this.symprobtable.TabIndex = 3;
            this.symprobtable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.metroGrid1_CellContentClick);
            // 
            // Sym
            // 
            this.Sym.HeaderText = "Sym";
            this.Sym.Name = "Sym";
            // 
            // Prob
            // 
            this.Prob.HeaderText = "p_i";
            this.Prob.Name = "Prob";
            // 
            // ShennonPage
            // 
            this.ShennonPage.Controls.Add(this.resbox2);
            this.ShennonPage.Controls.Add(this.symprobtable2);
            this.ShennonPage.HorizontalScrollbarBarColor = true;
            this.ShennonPage.HorizontalScrollbarHighlightOnWheel = false;
            this.ShennonPage.HorizontalScrollbarSize = 10;
            this.ShennonPage.Location = new System.Drawing.Point(4, 35);
            this.ShennonPage.Name = "ShennonPage";
            this.ShennonPage.Size = new System.Drawing.Size(620, 209);
            this.ShennonPage.TabIndex = 1;
            this.ShennonPage.Text = "Код Шеннона";
            this.ShennonPage.VerticalScrollbarBarColor = true;
            this.ShennonPage.VerticalScrollbarHighlightOnWheel = false;
            this.ShennonPage.VerticalScrollbarSize = 10;
            // 
            // resbox2
            // 
            this.resbox2.Enabled = false;
            this.resbox2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resbox2.Location = new System.Drawing.Point(434, 3);
            this.resbox2.Name = "resbox2";
            this.resbox2.ReadOnly = true;
            this.resbox2.Size = new System.Drawing.Size(183, 197);
            this.resbox2.TabIndex = 3;
            this.resbox2.Text = "";
            // 
            // symprobtable2
            // 
            this.symprobtable2.AllowUserToResizeRows = false;
            this.symprobtable2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.symprobtable2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.symprobtable2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.symprobtable2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symprobtable2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sym2,
            this.Prob2,
            this.Qum_prob,
            this.Length});
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.symprobtable2.DefaultCellStyle = dataGridViewCellStyle35;
            this.symprobtable2.EnableHeadersVisualStyles = false;
            this.symprobtable2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.symprobtable2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable2.Location = new System.Drawing.Point(0, 0);
            this.symprobtable2.Name = "symprobtable2";
            this.symprobtable2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable2.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
            this.symprobtable2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.symprobtable2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.symprobtable2.Size = new System.Drawing.Size(428, 197);
            this.symprobtable2.TabIndex = 2;
            this.symprobtable2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.symprobtable2_CellContentClick);
            // 
            // Sym2
            // 
            this.Sym2.HeaderText = "Sym";
            this.Sym2.Name = "Sym2";
            // 
            // Prob2
            // 
            this.Prob2.HeaderText = "p_i";
            this.Prob2.Name = "Prob2";
            // 
            // Qum_prob
            // 
            this.Qum_prob.HeaderText = "q_i";
            this.Qum_prob.Name = "Qum_prob";
            this.Qum_prob.ReadOnly = true;
            // 
            // Length
            // 
            this.Length.HeaderText = "l_i";
            this.Length.Name = "Length";
            this.Length.ReadOnly = true;
            // 
            // GMPage
            // 
            this.GMPage.Controls.Add(this.symprobtable3);
            this.GMPage.Controls.Add(this.resbox3);
            this.GMPage.HorizontalScrollbarBarColor = true;
            this.GMPage.HorizontalScrollbarHighlightOnWheel = false;
            this.GMPage.HorizontalScrollbarSize = 10;
            this.GMPage.Location = new System.Drawing.Point(4, 38);
            this.GMPage.Name = "GMPage";
            this.GMPage.Size = new System.Drawing.Size(620, 206);
            this.GMPage.TabIndex = 2;
            this.GMPage.Text = "Код Гилберта-Мура";
            this.GMPage.VerticalScrollbarBarColor = true;
            this.GMPage.VerticalScrollbarHighlightOnWheel = false;
            this.GMPage.VerticalScrollbarSize = 10;
            // 
            // symprobtable3
            // 
            this.symprobtable3.AllowUserToResizeRows = false;
            this.symprobtable3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.symprobtable3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.symprobtable3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.symprobtable3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symprobtable3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sym3,
            this.p_i,
            this.qum,
            this.sigma,
            this.length3});
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.symprobtable3.DefaultCellStyle = dataGridViewCellStyle26;
            this.symprobtable3.EnableHeadersVisualStyles = false;
            this.symprobtable3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.symprobtable3.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable3.Location = new System.Drawing.Point(3, 3);
            this.symprobtable3.Name = "symprobtable3";
            this.symprobtable3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable3.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.symprobtable3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.symprobtable3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.symprobtable3.Size = new System.Drawing.Size(427, 200);
            this.symprobtable3.TabIndex = 3;
            // 
            // Sym3
            // 
            this.Sym3.HeaderText = "Sym";
            this.Sym3.Name = "Sym3";
            // 
            // p_i
            // 
            this.p_i.HeaderText = "p_i";
            this.p_i.Name = "p_i";
            // 
            // qum
            // 
            this.qum.HeaderText = "q_i";
            this.qum.Name = "qum";
            // 
            // sigma
            // 
            this.sigma.HeaderText = "sigma_i";
            this.sigma.Name = "sigma";
            // 
            // length3
            // 
            this.length3.HeaderText = "l_i";
            this.length3.Name = "length3";
            // 
            // resbox3
            // 
            this.resbox3.BackColor = System.Drawing.SystemColors.Control;
            this.resbox3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resbox3.Location = new System.Drawing.Point(436, 4);
            this.resbox3.Name = "resbox3";
            this.resbox3.Size = new System.Drawing.Size(181, 199);
            this.resbox3.TabIndex = 2;
            this.resbox3.Text = "";
            // 
            // ACPage
            // 
            this.ACPage.Controls.Add(this.codeBox);
            this.ACPage.Controls.Add(this.metroLabel1);
            this.ACPage.Controls.Add(this.symprobtable4);
            this.ACPage.Controls.Add(this.inputstr);
            this.ACPage.Controls.Add(this.aclabel);
            this.ACPage.HorizontalScrollbarBarColor = true;
            this.ACPage.HorizontalScrollbarHighlightOnWheel = false;
            this.ACPage.HorizontalScrollbarSize = 10;
            this.ACPage.Location = new System.Drawing.Point(4, 38);
            this.ACPage.Name = "ACPage";
            this.ACPage.Size = new System.Drawing.Size(620, 206);
            this.ACPage.TabIndex = 3;
            this.ACPage.Text = "Арифметическое кодирование";
            this.ACPage.VerticalScrollbarBarColor = true;
            this.ACPage.VerticalScrollbarHighlightOnWheel = false;
            this.ACPage.VerticalScrollbarSize = 10;
            // 
            // symprobtable4
            // 
            this.symprobtable4.AllowUserToResizeRows = false;
            this.symprobtable4.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.symprobtable4.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.symprobtable4.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.symprobtable4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symprobtable4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sym4,
            this.pi,
            this.qi,
            this.P,
            this.Q,
            this.li,
            this.Code});
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.symprobtable4.DefaultCellStyle = dataGridViewCellStyle29;
            this.symprobtable4.EnableHeadersVisualStyles = false;
            this.symprobtable4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.symprobtable4.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.symprobtable4.Location = new System.Drawing.Point(12, 32);
            this.symprobtable4.Name = "symprobtable4";
            this.symprobtable4.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.symprobtable4.RowHeadersDefaultCellStyle = dataGridViewCellStyle30;
            this.symprobtable4.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.symprobtable4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.symprobtable4.Size = new System.Drawing.Size(612, 171);
            this.symprobtable4.TabIndex = 4;
            // 
            // sym4
            // 
            this.sym4.HeaderText = "sym";
            this.sym4.Name = "sym4";
            // 
            // pi
            // 
            this.pi.HeaderText = "p_i";
            this.pi.Name = "pi";
            // 
            // qi
            // 
            this.qi.HeaderText = "q_i";
            this.qi.Name = "qi";
            // 
            // P
            // 
            this.P.HeaderText = "P";
            this.P.Name = "P";
            // 
            // Q
            // 
            this.Q.HeaderText = "Q";
            this.Q.Name = "Q";
            // 
            // li
            // 
            this.li.HeaderText = "l_i";
            this.li.Name = "li";
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            // 
            // inputstr
            // 
            this.inputstr.Location = new System.Drawing.Point(195, 9);
            this.inputstr.Name = "inputstr";
            this.inputstr.Size = new System.Drawing.Size(139, 20);
            this.inputstr.TabIndex = 3;
            // 
            // aclabel
            // 
            this.aclabel.AutoSize = true;
            this.aclabel.Location = new System.Drawing.Point(12, 9);
            this.aclabel.Name = "aclabel";
            this.aclabel.Size = new System.Drawing.Size(186, 19);
            this.aclabel.TabIndex = 2;
            this.aclabel.Text = "Пожалуйста, введите строку: ";
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(538, 407);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(130, 40);
            this.clear_button.TabIndex = 7;
            this.clear_button.Text = "Очистить всё";
            this.clear_button.UseSelectable = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // copy_button
            // 
            this.copy_button.Location = new System.Drawing.Point(363, 407);
            this.copy_button.Name = "copy_button";
            this.copy_button.Size = new System.Drawing.Size(142, 40);
            this.copy_button.TabIndex = 5;
            this.copy_button.Text = "Копировать код";
            this.copy_button.UseSelectable = true;
            this.copy_button.Click += new System.EventHandler(this.copy_button_Click);
            // 
            // theory_button
            // 
            this.theory_button.Location = new System.Drawing.Point(189, 407);
            this.theory_button.Name = "theory_button";
            this.theory_button.Size = new System.Drawing.Size(142, 40);
            this.theory_button.TabIndex = 4;
            this.theory_button.Text = "Посмотреть теорию\r\nпо коду";
            this.theory_button.UseSelectable = true;
            this.theory_button.Click += new System.EventHandler(this.theory_button_Click);
            // 
            // build_button
            // 
            this.build_button.Location = new System.Drawing.Point(27, 407);
            this.build_button.Name = "build_button";
            this.build_button.Size = new System.Drawing.Size(122, 40);
            this.build_button.TabIndex = 2;
            this.build_button.Text = "Построить код";
            this.build_button.UseSelectable = true;
            this.build_button.Click += new System.EventHandler(this.build_button_Click);
            this.build_button.MouseEnter += new System.EventHandler(this.build_button_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(122, 97);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(349, 9);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(114, 19);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "Полученный код:";
            // 
            // codeBox
            // 
            this.codeBox.Location = new System.Drawing.Point(469, 9);
            this.codeBox.Name = "codeBox";
            this.codeBox.ReadOnly = true;
            this.codeBox.Size = new System.Drawing.Size(148, 20);
            this.codeBox.TabIndex = 7;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(685, 470);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.build_button);
            this.Controls.Add(this.copy_button);
            this.Controls.Add(this.theory_button);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "TopCodes";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.metroTabControl1.ResumeLayout(false);
            this.HuffmanPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable)).EndInit();
            this.ShennonPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable2)).EndInit();
            this.GMPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable3)).EndInit();
            this.ACPage.ResumeLayout(false);
            this.ACPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symprobtable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private void symprobtable2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void build_button_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


}
