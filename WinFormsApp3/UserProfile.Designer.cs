
using System.Windows.Forms;

namespace GridUserProfile
{
    partial class UserProfile
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserProfile));
            this.lblTeamID = new System.Windows.Forms.Label();
            this.lblDeptID = new System.Windows.Forms.Label();
            this.lblProjID = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.cmbTower = new System.Windows.Forms.ComboBox();
            this.cmbSegment = new System.Windows.Forms.ComboBox();
            this.cmbDept = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGO = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbl = new System.Windows.Forms.Label();
            this.DGUserInfo = new Zuby.ADGV.AdvancedDataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.lstDeptID = new System.Windows.Forms.ListBox();
            this.lstSegment = new System.Windows.Forms.ListBox();
            this.lstTowerProjID = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.loader = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGUserInfo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loader)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTeamID
            // 
            this.lblTeamID.AutoSize = true;
            this.lblTeamID.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTeamID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTeamID.Location = new System.Drawing.Point(691, 23);
            this.lblTeamID.Name = "lblTeamID";
            this.lblTeamID.Size = new System.Drawing.Size(61, 21);
            this.lblTeamID.TabIndex = 33;
            this.lblTeamID.Text = "TeamID";
            this.lblTeamID.Visible = false;
            // 
            // lblDeptID
            // 
            this.lblDeptID.AutoSize = true;
            this.lblDeptID.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDeptID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblDeptID.Location = new System.Drawing.Point(425, 23);
            this.lblDeptID.Name = "lblDeptID";
            this.lblDeptID.Size = new System.Drawing.Size(58, 21);
            this.lblDeptID.TabIndex = 32;
            this.lblDeptID.Text = "DeptID";
            this.lblDeptID.Visible = false;
            // 
            // lblProjID
            // 
            this.lblProjID.AutoSize = true;
            this.lblProjID.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblProjID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblProjID.Location = new System.Drawing.Point(189, 23);
            this.lblProjID.Name = "lblProjID";
            this.lblProjID.Size = new System.Drawing.Size(53, 21);
            this.lblProjID.TabIndex = 31;
            this.lblProjID.Text = "ProjID";
            this.lblProjID.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Image = global::GridUserProfile.Properties.Resources.user_male_add;
            this.btnAdd.Location = new System.Drawing.Point(1004, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(44, 44);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.White;
            this.btnFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFind.Image = global::GridUserProfile.Properties.Resources.Search_icon;
            this.btnFind.Location = new System.Drawing.Point(955, 46);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(44, 44);
            this.btnFind.TabIndex = 22;
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.LightCyan;
            this.label4.Location = new System.Drawing.Point(605, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 21);
            this.label4.TabIndex = 20;
            this.label4.Text = "SEGMENT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.LightCyan;
            this.label3.Location = new System.Drawing.Point(313, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 21);
            this.label3.TabIndex = 19;
            this.label3.Text = "DEPARTMENT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.LightCyan;
            this.label2.Location = new System.Drawing.Point(121, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 21);
            this.label2.TabIndex = 18;
            this.label2.Text = "TOWER";
            // 
            // btnShow
            // 
            this.btnShow.BackColor = System.Drawing.Color.White;
            this.btnShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow.ForeColor = System.Drawing.Color.Black;
            this.btnShow.Image = global::GridUserProfile.Properties.Resources.down_icon;
            this.btnShow.Location = new System.Drawing.Point(906, 46);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(44, 44);
            this.btnShow.TabIndex = 7;
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // cmbTower
            // 
            this.cmbTower.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTower.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTower.BackColor = System.Drawing.Color.White;
            this.cmbTower.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTower.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTower.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cmbTower.ForeColor = System.Drawing.Color.LightGray;
            this.cmbTower.FormattingEnabled = true;
            this.cmbTower.Location = new System.Drawing.Point(118, 50);
            this.cmbTower.Name = "cmbTower";
            this.cmbTower.Size = new System.Drawing.Size(189, 36);
            this.cmbTower.TabIndex = 0;
            this.cmbTower.Text = "<Select Here>";
            this.cmbTower.SelectedIndexChanged += new System.EventHandler(this.cmbTower_SelectedIndexChanged);
            this.cmbTower.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTower_KeyPress);
            this.cmbTower.MouseHover += new System.EventHandler(this.cmbTower_MouseHover);
            // 
            // cmbSegment
            // 
            this.cmbSegment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSegment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSegment.BackColor = System.Drawing.Color.White;
            this.cmbSegment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSegment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbSegment.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cmbSegment.ForeColor = System.Drawing.Color.LightGray;
            this.cmbSegment.FormattingEnabled = true;
            this.cmbSegment.Location = new System.Drawing.Point(605, 50);
            this.cmbSegment.Name = "cmbSegment";
            this.cmbSegment.Size = new System.Drawing.Size(295, 36);
            this.cmbSegment.TabIndex = 2;
            this.cmbSegment.Text = "<Select Here>";
            this.cmbSegment.SelectedIndexChanged += new System.EventHandler(this.cmbSegment_SelectedIndexChanged);
            this.cmbSegment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSegment_KeyPress);
            // 
            // cmbDept
            // 
            this.cmbDept.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDept.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDept.BackColor = System.Drawing.Color.White;
            this.cmbDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbDept.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cmbDept.ForeColor = System.Drawing.Color.LightGray;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.Location = new System.Drawing.Point(313, 50);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(286, 36);
            this.cmbDept.TabIndex = 1;
            this.cmbDept.Text = "<Select Here>";
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            this.cmbDept.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.cmbDept.MouseHover += new System.EventHandler(this.cmbDept_MouseHover);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1059, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 15);
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.GreenYellow;
            this.panelSearch.Controls.Add(this.button1);
            this.panelSearch.Controls.Add(this.label6);
            this.panelSearch.Controls.Add(this.label5);
            this.panelSearch.Controls.Add(this.btnGO);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.pictureBox2);
            this.panelSearch.Location = new System.Drawing.Point(1, 152);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(538, 116);
            this.panelSearch.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Image = global::GridUserProfile.Properties.Resources.delete_icon;
            this.button1.Location = new System.Drawing.Point(485, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 39);
            this.button1.TabIndex = 23;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(116, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(228, 21);
            this.label6.TabIndex = 19;
            this.label6.Text = "(Employee ID, Employee Name)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(116, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 28);
            this.label5.TabIndex = 18;
            this.label5.Text = "SEARCH AREA";
            // 
            // btnGO
            // 
            this.btnGO.BackColor = System.Drawing.Color.White;
            this.btnGO.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGO.ForeColor = System.Drawing.Color.Black;
            this.btnGO.Location = new System.Drawing.Point(379, 42);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(74, 39);
            this.btnGO.TabIndex = 2;
            this.btnGO.Text = "Search";
            this.btnGO.UseVisualStyleBackColor = false;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSearch.Location = new System.Drawing.Point(119, 42);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(254, 39);
            this.txtSearch.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GridUserProfile.Properties.Resources.Search_Male_User_icon;
            this.pictureBox2.Location = new System.Drawing.Point(9, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(98, 93);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl.Location = new System.Drawing.Point(-1, 26);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(110, 21);
            this.lbl.TabIndex = 142;
            this.lbl.Text = "<Select Here>";
            this.lbl.Visible = false;
            // 
            // DGUserInfo
            // 
            this.DGUserInfo.AllowUserToAddRows = false;
            this.DGUserInfo.AllowUserToDeleteRows = false;
            this.DGUserInfo.AllowUserToResizeRows = false;
            this.DGUserInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGUserInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGUserInfo.BackgroundColor = System.Drawing.Color.White;
            this.DGUserInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.DGUserInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DGUserInfo.ColumnHeadersHeight = 45;
            this.DGUserInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGUserInfo.DefaultCellStyle = dataGridViewCellStyle6;
            this.DGUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGUserInfo.EnableHeadersVisualStyles = false;
            this.DGUserInfo.FilterAndSortEnabled = true;
            this.DGUserInfo.GridColor = System.Drawing.Color.MidnightBlue;
            this.DGUserInfo.Location = new System.Drawing.Point(0, 105);
            this.DGUserInfo.Name = "DGUserInfo";
            this.DGUserInfo.ReadOnly = true;
            this.DGUserInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DGUserInfo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGUserInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DGUserInfo.RowHeadersVisible = false;
            this.DGUserInfo.RowHeadersWidth = 10;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.MidnightBlue;
            this.DGUserInfo.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.DGUserInfo.RowTemplate.Height = 33;
            this.DGUserInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGUserInfo.Size = new System.Drawing.Size(1059, 515);
            this.DGUserInfo.TabIndex = 30;
            this.DGUserInfo.SortStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.SortEventArgs>(this.DGUserInfo_SortStringChanged);
            this.DGUserInfo.FilterStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.FilterEventArgs>(this.DGUserInfo_FilterStringChanged);
            this.DGUserInfo.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGUserInfo_CellContentDoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.ToolTip1_Draw);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblTeamID);
            this.panel1.Controls.Add(this.cmbSegment);
            this.panel1.Controls.Add(this.lblDeptID);
            this.panel1.Controls.Add(this.cmbTower);
            this.panel1.Controls.Add(this.cmbDept);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.lblProjID);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnShow);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1059, 105);
            this.panel1.TabIndex = 139;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Location = new System.Drawing.Point(758, 22);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(42, 22);
            this.lblDateTime.TabIndex = 140;
            this.lblDateTime.Text = "----";
            this.lblDateTime.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GridUserProfile.Properties.Resources.config_users;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(91, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 139;
            this.pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(99, 136);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(42, 44);
            this.btnExit.TabIndex = 146;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackColor = System.Drawing.Color.White;
            this.btnMax.Image = global::GridUserProfile.Properties.Resources.Maximize_Window_icon;
            this.btnMax.Location = new System.Drawing.Point(52, 136);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(42, 44);
            this.btnMax.TabIndex = 144;
            this.btnMax.UseVisualStyleBackColor = false;
            this.btnMax.Visible = false;
            this.btnMax.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.White;
            this.btnMin.Image = global::GridUserProfile.Properties.Resources.Minimize_Window_icon;
            this.btnMin.Location = new System.Drawing.Point(4, 136);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(42, 44);
            this.btnMin.TabIndex = 143;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Visible = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // lstDeptID
            // 
            this.lstDeptID.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstDeptID.FormattingEnabled = true;
            this.lstDeptID.ItemHeight = 19;
            this.lstDeptID.Location = new System.Drawing.Point(374, 358);
            this.lstDeptID.Name = "lstDeptID";
            this.lstDeptID.Size = new System.Drawing.Size(207, 156);
            this.lstDeptID.TabIndex = 147;
            this.lstDeptID.Visible = false;
            // 
            // lstSegment
            // 
            this.lstSegment.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstSegment.FormattingEnabled = true;
            this.lstSegment.ItemHeight = 19;
            this.lstSegment.Location = new System.Drawing.Point(587, 358);
            this.lstSegment.Name = "lstSegment";
            this.lstSegment.Size = new System.Drawing.Size(210, 156);
            this.lstSegment.TabIndex = 147;
            this.lstSegment.Visible = false;
            // 
            // lstTowerProjID
            // 
            this.lstTowerProjID.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstTowerProjID.FormattingEnabled = true;
            this.lstTowerProjID.ItemHeight = 19;
            this.lstTowerProjID.Location = new System.Drawing.Point(161, 358);
            this.lstTowerProjID.Name = "lstTowerProjID";
            this.lstTowerProjID.Size = new System.Drawing.Size(207, 156);
            this.lstTowerProjID.TabIndex = 147;
            this.lstTowerProjID.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // loader
            // 
            this.loader.BackColor = System.Drawing.Color.White;
            this.loader.Image = global::GridUserProfile.Properties.Resources.ajax_loader;
            this.loader.Location = new System.Drawing.Point(313, 342);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(217, 75);
            this.loader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loader.TabIndex = 148;
            this.loader.TabStop = false;
            this.loader.Visible = false;
            // 
            // UserProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1059, 642);
            this.Controls.Add(this.loader);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.DGUserInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lstTowerProjID);
            this.Controls.Add(this.lstSegment);
            this.Controls.Add(this.lstDeptID);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnMax);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserProfile";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Profile - Maintenance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserProfile_FormClosing);
            this.Load += new System.EventHandler(this.UserProfile_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGUserInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public StatusStrip statusStrip1;
        public ComboBox cmbSegment;
        public ComboBox cmbDept;
        public ToolStripStatusLabel toolStripStatusLabel1;
        public ToolTip toolTip1;
        public ComboBox cmbTower;
        public Button btnShow;
        public Label label3;
        public Label label2;
        public Label label4;
        public Button btnFind;
        public Panel panelSearch;
        public PictureBox pictureBox2;
        public TextBox txtSearch;
        public Button btnGO;
        public Label label5;
        public Label label6;
        public Button button1;
        public Button btnAdd;
        public Zuby.ADGV.AdvancedDataGridView DGUserInfo;
        public Label lblTeamID;
        public Label lblDeptID;
        public Label lblProjID;
        public Panel panel1;
        public PictureBox pictureBox1;
        public Label lblDateTime;
        public Label lbl;
        private Button btnMax;
        private Button btnMin;
        private Button btnExit;
        public ListBox lstDeptID;
        public ListBox lstSegment;
        public ListBox lstTowerProjID;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private PictureBox loader;
    }
}