using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace GridUserProfile
{

    
    public partial class UserProfile : Form
    {
        public UserProfile()
        {
            InitializeComponent();
        }

        private bool IsDataLoaded = false;
        public static string XEmpno;
        public static string XName;
        public static string XEID;
        public static string XEmail;
        public static string XTeamID;
        public static string XAccessCode;
        public static string XTimeIn;
        public static string XTimeOut;
        public static string XSuppEmpNo;
        public static string XLocation;
        public static string XCRMID;
        public static string XDateCreated;
        public static string XCreatedBy;
        public static string XLunchBr;
        public static string XOnShore;
        public static string XTower;
        public static string XDept;
        public static string XSegment;
        public static string XProjID;
        public static string XDeptID;
        public static string XRole;
        public static string XSupervisor;
        public static string XTimeZone;
        public static string XSearch;
        public static int indexrow;
        public static bool IsManualSearch = false;
        public static string STower;
        public static string SDept;
        public static string SSegment;

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader sqlRdr;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable Clrdt = new DataTable();

        string StrQuery;
        string strLst;
        public static string strProjID; public static string strDeptID; public static string strID;

        string dbCon = @"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True";


        Maintenance f2 = new Maintenance();
        SearchManual f3 = new SearchManual();
        AddUser f4 = new AddUser();
  

        static bool IsMax = false, IsFull = false;
        static Point OldLoc, DefaultLoc;
        static Size OldSize, DefaultSze;


        public static void SetInitial(UserProfile Form)
        {
            OldLoc = Form.Location;
            OldSize = Form.Size;
            DefaultLoc = Form.Location;
            DefaultSze = Form.Size;
        }

        static void FullSCR(UserProfile Form)
        {
            if (Form.WindowState == FormWindowState.Maximized)
                Form.WindowState = FormWindowState.Normal;
            else if (Form.WindowState == FormWindowState.Normal)
                Form.WindowState = FormWindowState.Maximized;
        }

        static void Maximize(UserProfile Form)
        {
            int x = SystemInformation.WorkingArea.Width;
            int y = SystemInformation.WorkingArea.Height;
            Form.WindowState = FormWindowState.Normal;
            Form.Location = new Point(0, 0);
            Form.Size = new Size(x, y);
        }

        static void Minimize(UserProfile Form)
        {
            if (Form.WindowState == FormWindowState.Minimized)
                Form.WindowState = FormWindowState.Normal;
            else if (Form.WindowState == FormWindowState.Normal)
                Form.WindowState = FormWindowState.Minimized;
        }

        public static void DoMaximize(UserProfile Form, Button btn)
        {
            if (IsMax == false) //form not maximized then maximized it
            {
                OldLoc = new Point(Form.Location.X, Form.Location.Y);
                OldSize = new Size(Form.Size.Width, Form.Size.Height);
                Maximize(Form);
                IsMax = true;
                IsFull = false;

            }
            else
            { // form is currently fullscr to normal;

                if (OldSize.Width >= SystemInformation.WorkingArea.Width || OldSize.Height >= SystemInformation.WorkingArea.Height)
                {
                    Form.Location = default;
                    Form.Size = DefaultSze;
                }
                else
                {
                    Form.Location = OldLoc;
                    Form.Size = OldSize;
                }

                IsMax = false;
                IsFull = false;
            }

        }

        public static void DoFullSCR(UserProfile Form)
        {
            if (IsMax == false) //form not maximized then maximized it
            {
                OldLoc = new Point(Form.Location.X, Form.Location.Y);
                OldSize = new Size(Form.Size.Width, Form.Size.Height);
                FullSCR(Form);
                IsMax = false;
                IsFull = true;

            }
            else
            { // form is currently fullscr to normal;

                if (OldSize.Width >= SystemInformation.WorkingArea.Width || OldSize.Height >= SystemInformation.WorkingArea.Height)
                {
                    Form.Location = default;
                    Form.Size = DefaultSze;
                }
                else
                {
                    Form.Location = OldLoc;
                    Form.Size = OldSize;
                }

                IsMax = false;
                IsFull = false;
            }

        }
    
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cmbTower.Text == "<Select Here>" || cmbDept.Text == "<Select Here>" || cmbSegment.Text == "<Select Here>")
            {
                MessageBox.Show("Please check your selections from the dropdown list.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                loader.Visible = true;
                loader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public void TextHitFocus(object sender, EventArgs e)
        {
            ComboBox tb = (ComboBox)sender;
            if (tb.Text == "<Select Here>")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        public void TextLostFocus(object sender, EventArgs e)
        {
            ComboBox tb = (ComboBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "<Select Here>";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void ToolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Ready.";
            panelSearch.Visible = false;

            this.ActiveControl = txtSearch;
            cmbTower.GotFocus += new EventHandler(this.TextHitFocus);
            cmbTower.LostFocus += new EventHandler(this.TextLostFocus);
            cmbDept.GotFocus += new EventHandler(this.TextHitFocus);
            cmbDept.LostFocus += new EventHandler(this.TextLostFocus);
            cmbSegment.GotFocus += new EventHandler(this.TextHitFocus);
            cmbSegment.LostFocus += new EventHandler(this.TextLostFocus);

            strProjID = lblProjID.Text;
            strDeptID = lblDeptID.Text;
            strID = lblTeamID.Text;

        }

        void DGFormatting()
        {
            DGUserInfo.Columns[0].HeaderText = "EMP NO.";
            DGUserInfo.Columns[1].HeaderText = "NAME";
            DGUserInfo.Columns[2].HeaderText = "EID";
            DGUserInfo.Columns[3].HeaderText = "EMAIL";
            DGUserInfo.Columns[4].HeaderText = "SUPERVISOR";
            DGUserInfo.Columns[5].HeaderText = "LOCATION";
            DGUserInfo.Columns[6].HeaderText = "TOWER";
            DGUserInfo.Columns[7].HeaderText = "DEPARTMENT";
            DGUserInfo.Columns[8].HeaderText = "SEGMENT";
            DGUserInfo.Columns[9].HeaderText = "ROLE";
            DGUserInfo.Columns[10].HeaderText = "ACCESS";
            DGUserInfo.Columns[11].HeaderText = "TIME-IN";
            DGUserInfo.Columns[12].HeaderText = "TIME-OUT";
            DGUserInfo.Columns[13].HeaderText = "CRM ID";
            DGUserInfo.Columns[14].HeaderText = "PROJID";
            DGUserInfo.Columns[15].HeaderText = "DEPTID";
            DGUserInfo.Columns[16].HeaderText = "TEAMID";
            DGUserInfo.Columns[17].HeaderText = "ONSHORE";
            DGUserInfo.Columns[19].HeaderText = "LASTDATE MODIFIED";
            DGUserInfo.Columns[19].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
            DGUserInfo.Columns[20].HeaderText = "MODIFIED BY";
            DGUserInfo.Columns[21].HeaderText = "DATECREATED";
            DGUserInfo.Columns[22].HeaderText = "CREATEDBY";
            DGUserInfo.Columns[23].HeaderText = "SUPPEMPNO";
            DGUserInfo.Columns[24].HeaderText = "LUNCHBR";

            DGUserInfo.Columns[14].Visible = false;
            DGUserInfo.Columns[15].Visible = false;
            DGUserInfo.Columns[16].Visible = false;
            DGUserInfo.Columns[17].Visible = false;
            DGUserInfo.Columns[18].Visible = false;
            DGUserInfo.Columns[21].Visible = false;
            DGUserInfo.Columns[22].Visible = false;
            DGUserInfo.Columns[23].Visible = false;
            DGUserInfo.Columns[24].Visible = false;
        }

        void FetchData()
        {

            DGUserInfo.EnableHeadersVisualStyles = false;
            //DGUserInfo.ColumnHeadersDefaultCellStyle.BackColor = Color.GreenYellow;
            //DGUserInfo.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            try
            {

                conn = new SqlConnection(dbCon);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                QueryState();

                da = new SqlDataAdapter(StrQuery, conn);

                dt = new DataTable("SEARCHGRID");
                da.Fill(dt);
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }
 

        void FillSegment()
        {
            int ctr = 0;
            try
            {
                lstSegment.Items.Clear();
                cmbSegment.Items.Clear();

                StrQuery = "SELECT ID,WorkType FROM RPA_GRID.dbo.tblTeam WHERE STATUS = '1' ";

                if (cmbTower.Text == "ALL")
                {
                    if (cmbDept.Text == "ALL")
                    {
                        StrQuery += "";
                    }
                    else
                    {
                        StrQuery += "AND DEPTID = '" + strDeptID + "'";
                    }
                }
                else
                {
                    if (cmbDept.Text != "ALL")
                    {
                        StrQuery += "AND PROJECTID = '" + strProjID + "' AND DEPTID = '" + strDeptID + "'";
                    }
                    else
                    {
                        StrQuery += "AND PROJECTID = '" + strProjID + "'";
                    }

                }

                using SqlConnection conn = new SqlConnection(dbCon);
                using SqlCommand cmd = new SqlCommand(StrQuery, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();

                cmd.CommandTimeout = 300;
                while (sqlRdr.Read())
                {
                    ctr +=1;
                    cmbSegment.Items.Add(sqlRdr.GetString("WorkType"));
                    lstSegment.Items.Add(sqlRdr.GetString("WorkType") + "*" + sqlRdr.GetValue("ID").ToString());
                }

                if (ctr > 1) {
                    cmbSegment.Items.Add("ALL");
                }
               
                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbTower_MouseHover(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Show("Select a Tower", cmbTower);
            toolTip1.ForeColor = Color.Red;
            toolTip1.BackColor = Color.Yellow;
        }
        private void cmbDept_MouseHover(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Show("Select Department", cmbDept);
            toolTip1.ForeColor = Color.Red;
            toolTip1.BackColor = Color.Yellow;
        }


        private void cmbDept_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (cmbDept.Text != "")
                {
                    cmbSegment.Focus();
                }
                else
                {
                    MessageBox.Show("Please select a Department", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbDept.Focus();
                }
            }
        }
        private void cmbSegment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (cmbSegment.Text != "")
                {
                    btnShow.Focus();
                }
                else
                {
                    MessageBox.Show("Please select a Segment", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSegment.Focus();
                }
            }
        }
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDeptID();
            lblDeptID.Text = strDeptID;
            FillSegment();
            cmbSegment.Focus();

            if (cmbDept.Text == "ALL"){ SDept = "ALL";}
        }

        private void cmbSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTeamID();
            lblTeamID.Text = strID;
            btnShow.Focus();

            if (cmbSegment.Text == "ALL") { SSegment = "ALL"; }
        }

        void QueryState()
        {
           
            
            StrQuery = "SELECT A.EmpNo, A.EmpName, A.EID, A.Email, C.EmpName, ";
            StrQuery += "A.Location, B.Cluster, B.Segment, B.WorkType, CASE A.ROLE WHEN 0 THEN 'User' WHEN '1' THEN 'Supervisor' WHEN 2 THEN 'Manager' WHEN 3 ";
            StrQuery += "THEN 'Director' WHEN 4 THEN 'VP' END AS ROLE, A.AccessCode, ";
            StrQuery += "LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeIn, 100), 7)), LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeOut, 100), 7)), A.CRMID, ";
            StrQuery += "B.ProjectId,B.DeptId,B.ID, A.OnShore, B.TimeZone, A.DateModified, A.ModifiedBy, A.DateCreated, A.CreatedBy, C.EmpNo, LTRIM(RIGHT(CONVERT(VARCHAR(20), A.LunchBreak, 100), 7)), ";
            StrQuery += "CASE A.Status WHEN '0' THEN 'Not Active' WHEN '1' THEN 'Active' END AS STATUS ";
            StrQuery += "FROM RPA_GRID.dbo.tblUserInfo AS A INNER JOIN RPA_GRID.dbo.tblTeam AS B ";
            StrQuery += "ON A.TeamId = B.ID LEFT OUTER JOIN ";
            StrQuery += "RPA_GRID.dbo.tblUserInfo AS C ON A.SupEmpNo = C.EmpNo ";
            StrQuery += "WHERE (A.Status in ('0','1'))"; 

            if (STower == "ALL")
            {
                StrQuery += "";
            }
            else
            {
                StrQuery += " AND (B.ProjectId ='" + strProjID + "')";
            }

            if (SDept == "ALL")
            {
                StrQuery += "";
            }
            else
            {
                StrQuery += " AND (B.DEPTID ='" + strDeptID + "')";
            }

            if (SSegment == "ALL")
            {
                StrQuery += "";
            }
            else
            {
                StrQuery += " AND A.TeamID ='" + strID + "'";
            }
            StrQuery += " ORDER BY A.EMPNAME ASC";
        }


        void QuerySearch()
        {
            StrQuery = "SELECT A.EmpNo, A.EmpName, A.EID, A.Email, C.EmpName, ";
            StrQuery += "A.Location, B.Cluster, B.Segment, B.WorkType, CASE A.ROLE WHEN 0 THEN 'User' WHEN '1' THEN 'Supervisor' WHEN 2 THEN 'Manager' WHEN 3 ";
            StrQuery += "THEN 'Director' WHEN 4 THEN 'VP' END AS ROLE, A.AccessCode, ";
            StrQuery += "LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeIn, 100), 7)), LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeOut, 100), 7)), A.CRMID, ";
            StrQuery += "B.ProjectId,B.DeptId,B.ID, A.OnShore, B.TimeZone,A.DateModified, A.ModifiedBy, A.DateCreated, A.CreatedBy, C.EmpNo, ";
            StrQuery += "LTRIM(RIGHT(CONVERT(VARCHAR(20), A.LunchBreak, 100), 7)),CASE A.Status WHEN '0' THEN 'Not Active' WHEN '1' THEN 'Active' END AS STATUS ";
            StrQuery += "FROM RPA_GRID.dbo.tblUserInfo AS A INNER JOIN RPA_GRID.dbo.tblTeam AS B ";
            StrQuery += "ON A.TeamId = B.ID LEFT OUTER JOIN ";
            StrQuery += "RPA_GRID.dbo.tblUserInfo AS C ON A.SupEmpNo = C.EmpNo ";
            StrQuery += "WHERE A.EMPNO LIKE '%" + txtSearch.Text + "%' OR A.EMPNAME LIKE '%" + txtSearch.Text + "%'";
            IsManualSearch = true;
            XSearch = txtSearch.Text;
        }

        //List<string> list = new List<string>();
        //for (int i = 0; i< 1000; i++)
        //    list.Add(i.ToString());
        //lblStatus.Text = "Keeping App Responsive...";

        //var progress = new Progress<ProgressReport>();
        //progress.ProgressChanged += (o, report) =>
        //{
        //    lblStatus.Text = string.Format("Processing...{0}%", report.PercentComplete);
        //progressBar1.Value = report.PercentComplete;
        //    progressBar1.Update();
        //};

        //await ProcessData(list, progress); //para mareduce cpu usage and magregister yung packet ping ng db server

        //grpMenu1.Enabled = true;

        //private Task ProcessData(List<string> list, IProgress<ProgressReport> progress)
        //{
        //    int index = 1;
        //    int totalProcess = list.Count;
        //    var progressReport = new ProgressReport();
        //    grpMenu1.Enabled = false;
        //    return Task.Run(() => //reduce ng cpu memory usage and para ma-keep responsive si app.
        //    {
        //        for (int i = 0; i < totalProcess; i++)
        //        {
        //            progressReport.PercentComplete = index++ * 100 / totalProcess;
        //            progress.Report(progressReport);
        //            Thread.Sleep(1); //para ma-simulate yung tagal ng operation
        //    }

        //    });
        //}

        private void cmbTower_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (cmbTower.Text != "")
                {
                    cmbDept.Focus();
                }
                else
                {
                    MessageBox.Show("Please select a Tower", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTower.Focus();
                }
            }

        }

        private void cmbTower_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProjID();
            lblProjID.Text = strProjID;
            GetSegmentID();
            cmbDept.Focus();

            if (cmbTower.Text == "ALL")
            {
                STower = "ALL";
            }
        }

        void GetSegmentID()
        {
            int ctr = 0;
            try
            {
                cmbDept.Items.Clear();
                lstDeptID.Items.Clear();

                StrQuery = "Select Distinct DeptId,Segment as Department ";
                StrQuery += "From RPA_GRID.dbo.tblTeam Where ProjectId = '" + strProjID  + "' And DeptId Is Not Null Order by Segment";  //Where Id = '" + Authentication.XTeamId + "'"; -- Commented October 19,2021

                conn = new SqlConnection(dbCon);
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();
                    cmd.CommandTimeout = 1000;

                    while (sqlRdr.Read())
                    {
                        ctr += 1;
                        cmbDept.Items.Add(sqlRdr.GetString("Department").ToString());
                        lstDeptID.Items.Add(sqlRdr.GetString("Department").ToString() + "*" + sqlRdr.GetInt32("DeptId").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();

                    if (ctr > 1)
                    {
                        cmbDept.Items.Add("ALL");
                    }
                 
               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = true;
            txtSearch.Focus();
        }

        void GetProjID()
        {
            strLst = ""; strProjID = "";
            for (int i = 0; i < lstTowerProjID.Items.Count; i++)
            {
                strLst = lstTowerProjID.Items[i].ToString();
                if (cmbTower.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
                {
                    int len = strLst.Substring(0, strLst.IndexOf('*')).Length;
                    strProjID = strLst[len..][1..];
                    break;
                }
            }
        }
        void GetDeptID()
        {
            strLst = ""; strDeptID = "";
            for (int i = 0; i < lstDeptID.Items.Count; i++)
            {
                strLst = lstDeptID.Items[i].ToString().Trim();
                if (cmbDept.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
                {
                    int len = strLst.Substring(0, strLst.IndexOf('*')).Length;
                    strDeptID = strLst[len..][1..];
                    break;
                }
            }
        }
        void GetTeamID()
        {
            strLst = ""; strID = "";
            for (int i = 0; i < lstSegment.Items.Count; i++)
            {
                strLst = lstSegment.Items[i].ToString();
                if (cmbSegment.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
                {
                    int len = strLst.Substring(0, strLst.IndexOf('*')).Length;
                    strID = strLst[len..][1..];
                    break;
                }
            }
        }

        private void UserProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult dr = MessageBox.Show("Are you sure you want to close this Application?", "Confirm GRID Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dr == DialogResult.No)
            //{
            //    e.Cancel = true;

            //}
            this.Hide();
            MainSCR MS = new MainSCR();
            MS.Show();

            //Application.Exit();

        }
        private void btnGO_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                try
                {
                    conn = new SqlConnection(dbCon);
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        DataTable dtSearch = new DataTable("SEARCHMANUAL");
                        {
                            QuerySearch();

                            cmd = new SqlCommand(StrQuery, conn);
                            {
                                //cmd.Parameters.AddWithValue("empno", txtSearch.Text);
                                //cmd.Parameters.AddWithValue("employeename", string.Format("%{0}%", txtSearch.Text));

                                DataTable dt = new DataTable();
                                dt.Clear();
                                DGUserInfo.DataSource = dt;

                                da = new SqlDataAdapter(cmd);
                                da.Fill(dtSearch);

                                DGUserInfo.DataSource = dtSearch;
                                conn.Close();
                                da.Dispose();

                                if (dtSearch.Rows.Count > 0)
                                {
                                    IsManualSearch = true;
                                    DGFormatting();
                                    panelSearch.Visible = false;
                                }

                                StrQuery = "";
                                toolStripStatusLabel1.Text = "Fetched Rows : " + dtSearch.Rows.Count.ToString();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please indicate search value corresponds to your query.", "Invalid Search input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Focus();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            f4.ShowDialog();
        }
        private void DGUserInfo_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            //var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            //(myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.SortString;
            dt.DefaultView.Sort = DGUserInfo.SortString;
        }
        private void DGUserInfo_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            (myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.FilterString;
        }
        private void DGUserInfo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            XEmpno = this.DGUserInfo.CurrentRow.Cells[0].Value.ToString();
            XName = this.DGUserInfo.CurrentRow.Cells[1].Value.ToString();
            XEID = this.DGUserInfo.CurrentRow.Cells[2].Value.ToString();
            XEmail = this.DGUserInfo.CurrentRow.Cells[3].Value.ToString();
            XSupervisor = this.DGUserInfo.CurrentRow.Cells[4].Value.ToString();
            XLocation = this.DGUserInfo.CurrentRow.Cells[5].Value.ToString();
            XTower = this.DGUserInfo.CurrentRow.Cells[6].Value.ToString();
            XDept = this.DGUserInfo.CurrentRow.Cells[7].Value.ToString();
            XSegment = this.DGUserInfo.CurrentRow.Cells[8].Value.ToString();
            XRole = this.DGUserInfo.CurrentRow.Cells[9].Value.ToString();
            XAccessCode = this.DGUserInfo.CurrentRow.Cells[10].Value.ToString();
            XTimeIn = this.DGUserInfo.CurrentRow.Cells[11].Value.ToString();
            XTimeOut = this.DGUserInfo.CurrentRow.Cells[12].Value.ToString();
            XCRMID = this.DGUserInfo.CurrentRow.Cells[13].Value.ToString();
            XProjID = this.DGUserInfo.CurrentRow.Cells[14].Value.ToString();
            XDeptID = this.DGUserInfo.CurrentRow.Cells[15].Value.ToString();
            XTeamID = this.DGUserInfo.CurrentRow.Cells[16].Value.ToString();
            XOnShore = this.DGUserInfo.CurrentRow.Cells[17].Value.ToString();
            XTimeZone = this.DGUserInfo.CurrentRow.Cells[18].Value.ToString();
            XDateCreated = this.DGUserInfo.CurrentRow.Cells[21].Value.ToString();
            XCreatedBy = this.DGUserInfo.CurrentRow.Cells[22].Value.ToString();
            XSuppEmpNo = this.DGUserInfo.CurrentRow.Cells[23].Value.ToString();
            XLunchBr = this.DGUserInfo.CurrentRow.Cells[24].Value.ToString();

            //indexrow = e.RowIndex;
            //DataGridViewRow rw = DGUserInfo.Rows[indexrow];
            f2.cmbETower.Items.Clear();
            f2.lstTowerProjID.Items.Clear();
            f2.cmbEDept.Items.Clear();
            f2.lstDeptID.Items.Clear();

            strLst = "";
            for (int i = 0; i < lstTowerProjID.Items.Count; i++)
            {
                strLst = lstTowerProjID.Items[i].ToString();
                f2.cmbETower.Items.Add(strLst.Substring(0, strLst.IndexOf('*')).Trim());
                f2.lstTowerProjID.Items.Add(strLst);
            }

            strLst = "";
            for (int i = 0; i < lstDeptID.Items.Count; i++)
            {
                strLst = lstDeptID.Items[i].ToString().Trim();
                f2.cmbEDept.Items.Add(strLst.Substring(0, strLst.IndexOf('*')).Trim());
                f2.lstDeptID.Items.Add(strLst);
            }

            this.Hide();
            f2.btnEdit.Focus();
            f2.Show();

            ////Activity Maintenance
            //if (f2.lblAccessCode.Text.Substring(0, 1) == "0")
            //{
            //    f2.chklstPriv.SetItemChecked(0, false);
            //}
            //else
            //{
            //    f2.chklstPriv.SetItemChecked(0, true);
            //}
            ////User Maintenance
            //if (f2.lblAccessCode.Text.Substring(1, 1) == "0")
            //{
            //    f2.chklstPriv.SetItemChecked(1, false);
            //}
            //else
            //{
            //    f2.chklstPriv.SetItemChecked(1, true);
            //}
            ////TimeCard 
            //if (f2.lblAccessCode.Text.Substring(2, 1) == "0")
            //{
            //    f2.chklstPriv.SetItemChecked(2, false);
            //}
            //else
            //{
            //    f2.chklstPriv.SetItemChecked(2, true);
            //}
            ////Data
            //if (f2.lblAccessCode.Text.Substring(3, 1) == "0")
            //{
            //    f2.chklstPriv.SetItemChecked(3, false);
            //}
            //else
            //{
            //    f2.chklstPriv.SetItemChecked(3, true);
            //}
            ////RealTime
            //if (f2.lblAccessCode.Text.Substring(4, 1) == "0")
            //{
            //    f2.chklstPriv.SetItemChecked(4, false);
            //}
            //else
            //{
            //    f2.chklstPriv.SetItemChecked(4, true);
            //}
            ////Report
            //if (f2.lblAccessCode.Text.Substring(5, 1) == "0")
            //{
            //    f2.chklstPriv.SetItemChecked(5, false);
            //}
            //else
            //{
            //    f2.chklstPriv.SetItemChecked(5, true);
            //}
            //f2.dtpIN.CustomFormat = "hh:mm tt";
            //f2.dtpIN.Text = this.DGUserInfo.CurrentRow.Cells[10].Value.ToString();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            UserProfile.Minimize(this);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IsManualSearch = false;
            FetchData();
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Clrdt.Clear();
            DGUserInfo.DataSource = Clrdt;
           

            DGUserInfo.DataSource = dt;
            da.Dispose();
          
            if (dt.Rows.Count >= 0)
            {
                DGFormatting();
            }
         
            btnShow.Enabled = true;
            StrQuery = "";
            toolStripStatusLabel1.Text = "Total Rows : " + dt.Rows.Count.ToString();
            Thread.Sleep(1000);
            loader.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            UserProfile.DoMaximize(this, btn);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainSCR MS = new MainSCR();
            MS.Show();
            
        }

   
    }

}
