using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GridUserProfile
{
    public partial class AccessMaintenance : Form
    {
        //public virtual void Sort(System.Windows.Forms.DataGridViewColumn dataGridViewColumn, System.ComponentModel.ListSortDirection direction);

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader sqlRdr;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        string StrQuery;
        string AccessCode;

        string TeamId;
        string EmployeeNo;
        string EmployeeName;

        string dbCon = @"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True";
        //string Tower = DGTeamAccess.CurrentRow.Cells[6].Value.ToString();
        //string Department = DGTeamAccess.CurrentRow.Cells[7].Value.ToString();
        //string Segment = DGTeamAccess.CurrentRow.Cells[8].Value.ToString();
        string Modified;
        string DateModif;

        string AM;
        string UM;
        string TM;
        string DM;
        string RD;
        string RW;

        bool IsExistUserSeg;

        static bool IsMax = false, IsFull = false;
        static Point OldLoc, DefaultLoc;
        static Size OldSize, DefaultSze;

        string StrLst;
        string StrEmpList; string StrEmpNo;
        public static string strProjID; public static string strDeptID; public static string strID;
        private object m_dataGridBindingList;

        public AccessMaintenance()
        {
            InitializeComponent();
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
        static void FullSCR(AccessMaintenance Form)
        {
            if (Form.WindowState == FormWindowState.Maximized)
                Form.WindowState = FormWindowState.Normal;
            else if (Form.WindowState == FormWindowState.Normal)
                Form.WindowState = FormWindowState.Maximized;
        }

        static void Maximize(AccessMaintenance Form)
        {
            int x = SystemInformation.WorkingArea.Width;
            int y = SystemInformation.WorkingArea.Height;
            Form.WindowState = FormWindowState.Normal;
            Form.Location = new Point(0, 0);
            Form.Size = new Size(x, y);
        }

        static void Minimize(AccessMaintenance Form)
        {
            if (Form.WindowState == FormWindowState.Minimized)
                Form.WindowState = FormWindowState.Normal;
            else if (Form.WindowState == FormWindowState.Normal)
                Form.WindowState = FormWindowState.Minimized;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            AccessMaintenance.Minimize(this);
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            AccessMaintenance.DoMaximize(this, btn);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MainSCR MS = new MainSCR();
            MS.Show();
            this.Close();
        }

        void GetProjID()
        {
            StrLst = ""; strProjID = "";
            for (int i = 0; i < lstTowerProjID.Items.Count; i++)
            {
                StrLst = lstTowerProjID.Items[i].ToString();
                if (cmbTower.Text.Trim() == StrLst.Substring(0, StrLst.IndexOf('*')))
                {
                    int len = StrLst.Substring(0, StrLst.IndexOf('*')).Length;
                    strProjID = StrLst[len..][1..];
                    break;
                }
            }
        }
        void GetDeptID()
        {
            StrLst = ""; strDeptID = "";
            for (int i = 0; i < lstDeptID.Items.Count; i++)
            {
                StrLst = lstDeptID.Items[i].ToString().Trim();
                if (cmbDept.Text.Trim() == StrLst.Substring(0, StrLst.IndexOf('*')))
                {
                    int len = StrLst.Substring(0, StrLst.IndexOf('*')).Length;
                    strDeptID = StrLst[len..][1..];
                    break;
                }
            }
        }
        void GetTeamID()
        {
            StrLst = ""; strID = "";
            for (int i = 0; i < lstSegment.Items.Count; i++)
            {
                StrLst = lstSegment.Items[i].ToString();
                if (cmbSegment.Text.Trim() == StrLst.Substring(0, StrLst.IndexOf('*')))
                {
                    int len = StrLst.Substring(0, StrLst.IndexOf('*')).Length;
                    strID = StrLst[len..][1..];
                    break;
                }
            }
        }
        void FillSegment()
        {
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

                while (sqlRdr.Read())
                {
                    cmbSegment.Items.Add(sqlRdr.GetString("WorkType"));
                    lstSegment.Items.Add(sqlRdr.GetString("WorkType") + "*" + sqlRdr.GetValue("ID").ToString());
                }
                cmbSegment.Items.Add("ALL");
                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbTower_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProjID();
            lblProjID.Text = strProjID;
            GetSegmentID();
            cmbDept.Focus();
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDeptID();
            lblDeptID.Text = strDeptID;
            FillSegment();
            cmbSegment.Focus();
        }

        private void cmbSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTeamID();
            lblTeamID.Text = strID;
            btnShow.Focus();
        }

        void GetData()
        {
            //StrQuery = "select TEAMID,TOWER,DEPARTMENT,SEGMENT,ACCESSCODE from [RPA_GRID].[dbo].[AccessMaintenance] ";
            //StrQuery += "Where UserID = '" + Authentication.XUserId + "' ORDER BY SEGMENT";
            if (cmbTower.Text == "" || cmbDept.Text == "" || cmbSegment.Text == "" || cmbTower.Text == "<Select Here>" || cmbDept.Text == "<Select Here>" || cmbSegment.Text == "<Select Here>")
            {
                MessageBox.Show("Some fields are empty.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            else
            {
                StrQuery = "Select A.TEAMID AS Id, A.UserId AS EmpNo, B.EmpName AS EmpName, A.Tower,A.Department,A.Segment, ";
                StrQuery += "SUBSTRING(A.AccessCode,1,1) As Activity,SUBSTRING(A.AccessCode,2,1) As [User],SUBSTRING(A.AccessCode,3,1) As TimeCard, ";
                StrQuery += "SUBSTRING(A.AccessCode,4,1) As [Data], SUBSTRING(A.AccessCode,5,1) As RealTime, SUBSTRING(A.AccessCode,6,1) As ReportView,A.ModifiedBy,A.DateModified ";
                StrQuery += "From [RPA_GRID].[dbo].[AccessMaintenance] A INNER JOIN[RPA_GRID].[dbo].[tblUserInfo] B ON A.UserId = B.EmpNo ";
                StrQuery += "Where A.ProjectId Is not null ";


                if (cmbTower.Text == "ALL")
                {
                    StrQuery += "";
                }
                else
                {
                    StrQuery += "And A.ProjectId = '" + lblProjID.Text + "' ";
                }

                if (cmbDept.Text == "ALL")
                {
                    StrQuery += "";
                }
                else
                {
                    StrQuery += "And A.DeptId = '" + lblDeptID.Text + "'";
                }

                if (cmbSegment.Text == "ALL")
                {
                    StrQuery += "";
                }
                else
                {
                    StrQuery += "And A.TeamID = '" + lblTeamID.Text + "'";
                }

                conn = new SqlConnection(dbCon);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter(StrQuery, conn);
                da.Fill(dt);
                conn.Close();
                da.Dispose();


                DGTeamAccess.DataSource = dt;
                DGTeamAccess.Refresh();

                DGTeamAccess.Columns[9].ReadOnly = true;
                DGTeamAccess.Columns[10].ReadOnly = true;
                DGTeamAccess.Columns[11].ReadOnly = true;
                DGTeamAccess.Columns[12].ReadOnly = true;
                DGTeamAccess.Columns[13].ReadOnly = true;
                DGTeamAccess.Columns[14].ReadOnly = true;

                //toolStripStatusLabel1.Text = "Modify TEAM ACCESS Available in : (" + dt.Rows.Count.ToString() + ")" + " SEGMENTS.";
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            GetData();
        }

        void AllowAccess()
        {
            StrQuery = "Select A.TEAMID AS Id, A.UserId AS EmpNo, B.EmpName AS EmpName, A.Tower,A.Department,A.Segment, ";
            StrQuery += "SUBSTRING(A.AccessCode,1,1) As Activity,SUBSTRING(A.AccessCode,2,1) As [User],SUBSTRING(A.AccessCode,3,1) As TimeCard, ";
            StrQuery += "SUBSTRING(A.AccessCode,4,1) As [Data], SUBSTRING(A.AccessCode,5,1) As RealTime, SUBSTRING(A.AccessCode,6,1) As ReportView,A.ModifiedBy,A.DateModified ";
            StrQuery += "From [RPA_GRID].[dbo].[AccessMaintenance] A INNER JOIN [RPA_GRID].[dbo].[tblUserInfo] B ON A.UserId = B.EmpNo ";
            StrQuery += "Where A.UserId = '" + Authentication.XUserId + "'";

            conn = new SqlConnection(dbCon);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(StrQuery, conn);
            da.Fill(dt);
            conn.Close();
            da.Dispose();

            DGTeamAccess.DataSource = dt;
            DGTeamAccess.Columns[9].ReadOnly = true;
            DGTeamAccess.Columns[10].ReadOnly = true;
            DGTeamAccess.Columns[11].ReadOnly = true;
            DGTeamAccess.Columns[12].ReadOnly = true;
            DGTeamAccess.Columns[13].ReadOnly = true;
            DGTeamAccess.Columns[14].ReadOnly = true;
            DGTeamAccess.Refresh();

            toolStripStatusLabel1.Text = "Modify TEAM ACCESS Available in : (" + dt.Rows.Count.ToString() + ") SEGMENTS";
        }

        private void DGTeamAccess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex.ToString());

            TeamId = DGTeamAccess.CurrentRow.Cells[3].Value.ToString();
            EmployeeNo = DGTeamAccess.CurrentRow.Cells[4].Value.ToString();
            EmployeeName = DGTeamAccess.CurrentRow.Cells[5].Value.ToString();
            //string Tower = DGTeamAccess.CurrentRow.Cells[6].Value.ToString();
            //string Department = DGTeamAccess.CurrentRow.Cells[7].Value.ToString();
            //string Segment = DGTeamAccess.CurrentRow.Cells[8].Value.ToStrings();
            Modified = DGTeamAccess.CurrentRow.Cells[15].Value.ToString();
            DateModif = DGTeamAccess.CurrentRow.Cells[16].Value.ToString();

            AM = DGTeamAccess.CurrentRow.Cells[9].Value.ToString();
            UM = DGTeamAccess.CurrentRow.Cells[10].Value.ToString();
            TM = DGTeamAccess.CurrentRow.Cells[11].Value.ToString();
            DM = DGTeamAccess.CurrentRow.Cells[12].Value.ToString();
            RD = DGTeamAccess.CurrentRow.Cells[13].Value.ToString();
            RW = DGTeamAccess.CurrentRow.Cells[14].Value.ToString();

            if (e.ColumnIndex == 0)
            {

                DataGridViewButtonCell vCell = (DataGridViewButtonCell)DGTeamAccess.CurrentCell;
                if (vCell.Tag == null) { vCell.Value = "Disable"; vCell.Tag = true; }

                if (this.DGTeamAccess.CurrentCell.Value.ToString() == "Edit")
                {

                    int rw = e.RowIndex;
                    int col = e.ColumnIndex;

                    DGTeamAccess.Rows[rw].Cells[9].ReadOnly = false;
                    DGTeamAccess.Rows[rw].Cells[10].ReadOnly = false;
                    DGTeamAccess.Rows[rw].Cells[11].ReadOnly = false;
                    DGTeamAccess.Rows[rw].Cells[12].ReadOnly = false;
                    DGTeamAccess.Rows[rw].Cells[13].ReadOnly = false;
                    DGTeamAccess.Rows[rw].Cells[14].ReadOnly = false;

                    vCell.UseColumnTextForButtonValue = false;
                    this.DGTeamAccess.CurrentCell.Value = "Update";
                }
                else if (this.DGTeamAccess.CurrentCell.Value.ToString() == "Update")
                {
                    if (AM == "1")
                    {
                        AccessCode = "1";
                    }
                    else
                    {
                        AccessCode = "0";
                    }

                    if (UM == "1")
                    {
                        AccessCode += "1";
                    }
                    else
                    {
                        AccessCode += "0";
                    }

                    if (TM == "1")
                    {
                        AccessCode += "1";
                    }
                    else
                    {
                        AccessCode += "0";
                    }


                    if (DM == "1")
                    {
                        AccessCode += "1";
                    }
                    else
                    {
                        AccessCode += "0";
                    }

                    if (RD == "1")
                    {
                        AccessCode += "1";
                    }
                    else
                    {
                        AccessCode += "0";
                    }

                    if (RW == "1")
                    {
                        AccessCode += "1";
                    }
                    else
                    {
                        AccessCode += "0";
                    }

                    DialogResult dialogResult = MessageBox.Show("New Team Access Code for " + EmployeeName + " is " + AccessCode + System.Environment.NewLine + System.Environment.NewLine + "Confirm changes?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                        int len = DomainName.Substring(0, DomainName.IndexOf('\\')).Length;
                        DomainName = DomainName[len..][1..];

                        vCell.UseColumnTextForButtonValue = false;
                        this.DGTeamAccess.CurrentCell.Value = "Edit";

                        InsertAccessLogs(e);

                        try
                        {
                            StrQuery = "UPDATE [RPA_GRID].[dbo].[tblTeamAccess] SET AccessCode = '" + AccessCode + "',";
                            StrQuery += "ModifiedBy = '" + DomainName + "', DateModified  = '" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "' ";
                            StrQuery += "where UserId = '" + EmployeeNo + "' And TeamId = '" + TeamId + "'";

                            conn = new SqlConnection(dbCon);
                            {
                                if (conn.State == ConnectionState.Closed)
                                    conn.Open();
                                cmd = new SqlCommand(StrQuery, conn);
                                cmd.ExecuteNonQuery();
                                conn.Close();

                                MessageBox.Show("Team Access Priviledges for (" + EmployeeName + ") has been successfully set.", "Team Access", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                int rw = e.RowIndex;
                                int col = e.ColumnIndex;

                                DGTeamAccess.Rows[rw].Cells[9].ReadOnly = true;
                                DGTeamAccess.Rows[rw].Cells[10].ReadOnly = true;
                                DGTeamAccess.Rows[rw].Cells[11].ReadOnly = true;
                                DGTeamAccess.Rows[rw].Cells[12].ReadOnly = true;
                                DGTeamAccess.Rows[rw].Cells[13].ReadOnly = true;
                                DGTeamAccess.Rows[rw].Cells[14].ReadOnly = true;

                                //int i = e.RowIndex;
                                //DGTeamAccess.Rows.RemoveAt(i);
                                GetData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }



                //foreach (DataGridViewRow row in this.DGGrant.Rows)
                //{
                //    if (row.Cells[0].Value.Equals(TeamID))
                //    {
                //        found = true;
                //        break;
                //    }
                //}

                //if (found)
                //{
                //    MessageBox.Show("Opps! This Segment " + "('" + Segment + "')" + System.Environment.NewLine + "from Team Member (" + EmpName + ") has been already listed below for access granting.","Already selected", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                //    return;
                //}
                //else {

                //    DGGrant.Rows.Add(TeamID, UserId, Eid, EmpName, Supervisor, Tower, Department, Segment, ModBy, DateMod, Access, false, false, false, false, false, false);
                //    DGTeamAccess.Rows.RemoveAt(this.DGTeamAccess.SelectedRows[0].Index);

                //    toolStripStatusLabel1.Text = "Modify TEAM ACCESS Available in : (" + DGTeamAccess.Rows.Count.ToString() + " ) SEGMENTS";
                //}

            }
            else if (e.ColumnIndex == 1)
            {

                DialogResult dialogResult = MessageBox.Show("Are you sure deleting Team Access for " + EmployeeName + "?" + System.Environment.NewLine, "Confirm User Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    InsertAccessLogs(e);

                    StrQuery = "Delete from [RPA_GRID].[dbo].[tblTeamAccess] Where UserID = '" + EmployeeNo + "' And TeamId = '" + TeamId + "'";
                    conn = new SqlConnection(dbCon);
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd = new SqlCommand(StrQuery, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Team Access Priviledges for (" + EmployeeName + ") has been successfully deleted.", "Team Access Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int i = e.RowIndex;
                    DGTeamAccess.Rows.RemoveAt(i);


                }
            }
            else if (e.ColumnIndex == 2)
            {

                StrQuery = " Select A.EmpNo,A.EmpName As EmpName,B.Cluster As Tower,B.Segment As Department,B.WorkType As Segment,";
                StrQuery += "C.AccessCode As Access,C.ModifiedBy As ModBy,C.DateModified As DateMod ";
                StrQuery += "From [RPA_GRID].[dbo].[tblTeamAccessLogs] C Inner Join [RPA_GRID].[dbo].[tblTeam] B ";
                StrQuery += "ON C.TeamId = B.Id Left Join [RPA_GRID].[dbo].[tblUserInfo] A ON A.EmpNo = C.UserId ";
                StrQuery += "Where A.EmpNo = '" + EmployeeNo + "' Order By C.Id Desc";

                conn = new SqlConnection(dbCon);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                dt2.Clear();
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter(StrQuery, conn);
                da.Fill(dt2);
                conn.Close();
                da.Dispose();

                DGAMLogs.DataSource = dt2;

                DGAMLogs.Refresh();


                panelLogs.Visible = true;
            }
        }

        void InsertAccessLogs(DataGridViewCellEventArgs Del)
        {
            string DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            int len = DomainName.Substring(0, DomainName.IndexOf('\\')).Length;
            DomainName = DomainName[len..][1..];

            StrQuery = "Insert into [RPA_GRID].[dbo].[tblTeamAccessLogs](UserId,TeamId,AccessCode,ModifiedBy,DateModified) ";
            StrQuery += "Values ('" + EmployeeNo + "','" + TeamId + "','" + AccessCode + "','" + DomainName + "','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "')";
            conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd = new SqlCommand(StrQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            StrQuery = "Select A.TEAMID AS Id, A.UserId AS EmpNo, B.EmpName AS EmpName, A.Tower,A.Department,A.Segment, ";
            StrQuery += "SUBSTRING(A.AccessCode,1,1) As Activity,SUBSTRING(A.AccessCode,2,1) As [User],SUBSTRING(A.AccessCode,3,1) As TimeCard, ";
            StrQuery += "SUBSTRING(A.AccessCode,4,1) As [Data], SUBSTRING(A.AccessCode,5,1) As RealTime, SUBSTRING(A.AccessCode,6,1) As ReportView,A.ModifiedBy,A.DateModified ";
            StrQuery += "From [RPA_GRID].[dbo].[AccessMaintenance] A INNER JOIN[RPA_GRID].[dbo].[tblUserInfo] B ON A.UserId = B.EmpNo ";
            StrQuery += "Where A.ProjectId Is not null ";

            conn = new SqlConnection(dbCon);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(StrQuery, conn);
            da.Fill(dt);
            conn.Close();
            da.Dispose();

            //DGTeamAccess.DataSource = dt;
            //DGTeamAccess.Refresh();

            DataRow[] filteredRows = dt.Select("EmpName LIKE '%" + txtSearch.Text + "%'");
            //DataRow[] foundRows = dt.Select("EmpName Like '%" + txtSearch.Text + "%'");
            //DataRow[] filteredRows = tb.Select("FIRSTNAME LIKE '%" + searchstring + "%' OR LASTNAME LIKE '%" + searchstring + "%' OR NAME LIKE '%" + searchstring + "%' OR COMPANY LIKE '%" + searchstring + "%' OR CREATOR LIKE '%" + searchstring + "%'");


            if (filteredRows.Length == 0)
            {
                MessageBox.Show("Employee Name ('" + txtSearch.Text + "') not found.", "Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                toolStripStatusLabel1.Text = "Modify TEAM ACCESS Available in : (" + dt.Rows.Count.ToString() + ")" + " SEGMENTS.";

            }
            else
            {
                dt = filteredRows.CopyToDataTable();
                this.DGTeamAccess.DataSource = dt;
                toolStripStatusLabel1.Text = "Modify TEAM ACCESS Available in : (" + dt.Rows.Count.ToString() + ")" + " SEGMENTS.";
            }

            panelSearch.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chklstPriv.Items.Count; i++)
            {
                chklstPriv.SetItemChecked(i, false);
            }

            try
            {
                cmbAName.Items.Clear();
                lstEmpName.Items.Clear();

                StrQuery = "SELECT Distinct EmpName,EmpNo from [RPA_GRID].[dbo].[tblUserInfo] Order by EmpName";

                using SqlConnection conn = new SqlConnection(dbCon);
                using SqlCommand cmd = new SqlCommand(StrQuery, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();

                while (sqlRdr.Read())
                {
                    cmbAName.Items.Add(sqlRdr.GetString("EmpName").ToString());
                    lstEmpName.Items.Add(sqlRdr.GetString("EmpName") + "*" + sqlRdr.GetString("EmpNo"));
                }
                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.lblAccessCode.Text = "000000";

            this.cmbTower.Enabled = false;
            this.cmbDept.Enabled = false;
            this.cmbSegment.Enabled = false;

            this.btnShow.Enabled = false;
            this.btnFind.Enabled = false;
            this.btnAdd.Enabled = false;
            this.DGTeamAccess.Enabled = false;

            if (cmbTower.Text == "ALL")
            {
                cmbATower.Text = "";
            }
            else {
                cmbATower.Text = cmbTower.Text;
            }

            if (cmbDept.Text == "ALL")
            {
                cmbADept.Text = "";
            }
            else
            {
                cmbADept.Text = cmbDept.Text;
            }


            if (cmbSegment.Text == "ALL")
            {
                cmbASegment.Text = "";
            }
            else
            {
                cmbASegment.Text = cmbSegment.Text;
            }

            this.panelAdd.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            cmbTower.Enabled = true;
            cmbDept.Enabled = true;
            cmbSegment.Enabled = true;

            this.btnShow.Enabled = true;
            this.btnFind.Enabled = true;
            this.btnAdd.Enabled = true;
            this.DGTeamAccess.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CheckExistUserSegment();
            if (IsExistUserSeg == false)
            {
                if (txtEmpNo.Text != "")
                {
                    string DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    int len = DomainName.Substring(0, DomainName.IndexOf('\\')).Length;
                    DomainName = DomainName[len..][1..];

                    try
                    {
                        StrQuery = "Insert into [RPA_GRID].[dbo].[tblTeamAccess] (UserId,TeamId,AccessCode,ModifiedBy,DateModified)";
                        StrQuery += "Values ('" + txtEmpNo.Text + "','" + lblASegId.Text + "','" + lblAccessCode.Text + "','" + DomainName + "','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "')";

                        conn = new SqlConnection(dbCon);
                        {
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();
                            cmd = new SqlCommand(StrQuery, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        StrQuery = "Insert into [RPA_GRID].[dbo].[tblTeamAccessLogs] (UserId,TeamId,AccessCode,ModifiedBy,DateModified)";
                        StrQuery += "Values ('" + txtEmpNo.Text + "','" + lblASegId.Text + "','" + lblAccessCode.Text + "','" + DomainName + "','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "')";

                        conn = new SqlConnection(dbCon);
                        {
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();
                            cmd = new SqlCommand(StrQuery, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show("New Privilege has been added to " + cmbAName.Text, "Team Access", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            cmbASegment.Text = "";
                            lblASegId.Text = "";
                            cmbAName.Text = "";


                            ReAddTower();

                            cmbASegment.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    this.cmbTower.Enabled = true;
                    this.cmbDept.Enabled = true;
                    this.cmbSegment.Enabled = true;

                    this.btnShow.Enabled = true;
                    this.btnFind.Enabled = true;
                    this.btnAdd.Enabled = true;
                    this.DGTeamAccess.Enabled = true;
                    GetData();
                    panelAdd.Visible = false;
                }
                else
                {
                    MessageBox.Show("One or more field required is missing.", "Saving failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbAName.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Team Access for " + cmbAName.Text + " with Segment : " + cmbASegment.Text + " already Exists.", "Adding failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


        }

        void ReAddTower()
        {
            cmbATower.Items.Clear();
            StrQuery = "Select Distinct ProjectId,Cluster As Tower From RPA_GRID.dbo.tblTeam Where Status <> 0 Order by Cluster";

            conn = new SqlConnection(dbCon);
            cmd = new SqlCommand(StrQuery, conn);

            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();

                while (sqlRdr.Read())
                {
                    cmbATower.Items.Add(sqlRdr.GetString("Tower").ToString());
                    //lstTowerProjID.Items.Add(sqlRdr.GetString("Tower").ToString() + "*" + sqlRdr.GetValue("ProjectId").ToString());
                }
                sqlRdr.Close();
                conn.Close();
            }
        }

        void CheckExistUserSegment()
        {
            IsExistUserSeg = false;

            StrQuery = "Select UserId, TeamId From RPA_GRID.dbo.tblTeamAccess Where UserId = '" + txtEmpNo.Text + "' And TeamId = '" + lblASegId.Text + "'";

            conn = new SqlConnection(dbCon);
            cmd = new SqlCommand(StrQuery, conn);

            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();

                while (sqlRdr.Read())
                {
                    IsExistUserSeg = true;
                }
                sqlRdr.Close();
                conn.Close();
            }
        }

        private void cmbATower_SelectedIndexChanged(object sender, EventArgs e)
        {
            StrLst = ""; strProjID = "";
            for (int i = 0; i < lstTowerProjID.Items.Count; i++)
            {
                StrLst = lstTowerProjID.Items[i].ToString();
                if (cmbATower.Text.Trim() == StrLst.Substring(0, StrLst.IndexOf('*')))
                {
                    int len = StrLst.Substring(0, StrLst.IndexOf('*')).Length;
                    strProjID = StrLst[len..][1..];
                    break;
                }
            }
            lblAProjId.Text = strProjID;


            try
            {
                cmbADept.Items.Clear();
                lstDeptID.Items.Clear();

                StrQuery = "select Distinct Segment as Department,DeptId ";
                StrQuery += "From RPA_GRID.dbo.tblTeam Where ProjectId = '" + lblAProjId.Text + "' And DeptId is not Null";

                //StrQuery = "Select Distinct Segment as Department,DeptId,Status ";
                //StrQuery += "From RPA_GRID.dbo.tblTeam Where ProjectId = '" + XProjId + "' And DeptId Is Not Null And Status = '1' Order By Segment";

                conn = new SqlConnection(dbCon);
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        cmbADept.Items.Add(sqlRdr.GetString("Department").ToString());
                        lstDeptID.Items.Add(sqlRdr.GetString("Department").ToString() + "*" + sqlRdr.GetInt32("DeptId").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbADept.Focus();
            cmbADept.Text = "";
            cmbASegment.Text = "";

        }

        private void cmbADept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbATower.Text != "")
            {
                StrLst = ""; strDeptID = "";
                for (int i = 0; i < lstDeptID.Items.Count; i++)
                {
                    StrLst = lstDeptID.Items[i].ToString().Trim();
                    if (cmbADept.Text.Trim() == StrLst.Substring(0, StrLst.IndexOf('*')))
                    {
                        int len = StrLst.Substring(0, StrLst.IndexOf('*')).Length;
                        strDeptID = StrLst[len..][1..];
                        break;
                    }
                }

                lblADeptId.Text = strDeptID;

                try
                {
                    cmbASegment.Items.Clear();
                    lstSegment.Items.Clear();


                    StrQuery = "SELECT ID,WorkType FROM RPA_GRID.dbo.tblTeam WHERE STATUS = '1' And ProjectId = '" + lblAProjId.Text + "' And DEPTID = '" + lblADeptId.Text + "'";

                    using SqlConnection conn = new SqlConnection(dbCon);
                    using SqlCommand cmd = new SqlCommand(StrQuery, conn);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        cmbASegment.Items.Add(sqlRdr.GetString("WorkType"));
                        lstSegment.Items.Add(sqlRdr.GetString("WorkType") + "*" + sqlRdr.GetValue("ID").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                cmbSegment.Focus();
            }
            else
            {
                MessageBox.Show("Tower name cannot be empty.", "Select Tower", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbATower.Focus();
                return;
            }
        }

        private void cmbASegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbADept.Text != "")
            {

                StrLst = ""; strID = "";
                for (int i = 0; i < lstSegment.Items.Count; i++)
                {
                    StrLst = lstSegment.Items[i].ToString().Trim();
                    if (cmbASegment.Text.Trim() == StrLst.Substring(0, StrLst.IndexOf('*')))
                    {
                        int len = StrLst.Substring(0, StrLst.IndexOf('*')).Length;
                        strID = StrLst[len..][1..];
                        break;
                    }
                }

                lblASegId.Text = strID;

                cmbAName.Focus();

            }
            else
            {
                MessageBox.Show("Department name cannot be empty.", "Select Department", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbADept.Focus();
                return;
            }


        }

        private void cmbAName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbASegment.Text != "")
            {
                StrEmpList = ""; StrEmpNo = ""; txtEmpNo.Text = "";
                for (int i = 0; i < lstEmpName.Items.Count; i++)
                {
                    StrEmpList = lstEmpName.Items[i].ToString();
                    if (cmbAName.Text.Trim() == StrEmpList.Substring(0, StrEmpList.IndexOf('*')))
                    {
                        int len = StrEmpList.Substring(0, StrEmpList.IndexOf('*')).Length;
                        StrEmpNo = StrEmpList[len..][1..];
                        break;
                    }
                }
                txtEmpNo.Text = StrEmpNo.Trim();
                }
            else
            {
                MessageBox.Show("Employee designation cannot be identified without a Segment", "Segment is empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbASegment.Focus();
                return;
            }

        }

        public static void DoMaximize(AccessMaintenance Form, Button btn)
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

        private void DGTeamAccess_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            (myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.FilterString;
        }

        private void DGTeamAccess_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            //var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            //(myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.SortString;
            dt.DefaultView.Sort = DGTeamAccess.SortString;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            switch (btnCheckAll.Text)
            {
                case "Check All":
                    for (int i = 0; i < chklstPriv.Items.Count; i++)
                    {
                        chklstPriv.SetItemChecked(i, true);
                    }

                    lblAccessCode.Text = "111111";

                    //foreach (string s in chklstPriv.CheckedItems)
                    //{
                    //    lstChecked.Items.Add(s + "*1");
                    //}

                    btnCheckAll.Text = "Uncheck All";
                    break;
                case "Uncheck All":
                    for (int i = 0; i < chklstPriv.Items.Count; i++)
                    {
                        chklstPriv.SetItemChecked(i, false);
                    }

                    lblAccessCode.Text = "000000";

                    btnCheckAll.Text = "Check All";
                    break;

            }
        }

        private void chklstPriv_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAccessCode.Text = "";
            for (int i = 0; i < chklstPriv.Items.Count; i++)
            {
                string selPrev;
                if (chklstPriv.GetItemCheckState(i) == CheckState.Checked)
                {
                    selPrev = "1";
                }
                else
                {
                    selPrev = "0";
                }

                lblAccessCode.Text += selPrev;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelLogs.Visible = false;
        }

        private void DGAMLogs_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            (myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.FilterString;
        }

        private void DGAMLogs_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            dt2.DefaultView.Sort = DGAMLogs.SortString;
            //var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            //(myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.SortString;

        }

        private void AccessMaintenance_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtSearch;
            cmbTower.GotFocus += new EventHandler(this.TextHitFocus);
            cmbTower.LostFocus += new EventHandler(this.TextLostFocus);
            cmbDept.GotFocus += new EventHandler(this.TextHitFocus);
            cmbDept.LostFocus += new EventHandler(this.TextLostFocus);
            cmbSegment.GotFocus += new EventHandler(this.TextHitFocus);
            cmbSegment.LostFocus += new EventHandler(this.TextLostFocus);
            panelSearch.Visible = false;
            panelAdd.Visible = false;
            panelLogs.Visible = false;

            AllowAccess();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = true;
            txtSearch.Focus();
        }

        void GetSegmentID()
        {

            try
            {
                cmbDept.Items.Clear();
                lstDeptID.Items.Clear();

                StrQuery = "select Distinct Segment as Department,DeptId ";
                StrQuery += "From RPA_GRID.dbo.tblTeam Where ProjectId = '" + lblProjID.Text + "' And DeptId is not null";

                conn = new SqlConnection(dbCon);
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        cmbDept.Items.Add(sqlRdr.GetString("Department").ToString());
                        lstDeptID.Items.Add(sqlRdr.GetString("Department").ToString() + "*" + sqlRdr.GetInt32("DeptId").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();

                    cmbDept.Items.Add("ALL");
                    cmbSegment.Items.Add("ALL");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
