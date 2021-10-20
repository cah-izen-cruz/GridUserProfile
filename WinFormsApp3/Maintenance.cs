using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GridUserProfile
{
    public partial class Maintenance : Form
    {
        //SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
        //SqlCommand cmd = new SqlCommand();
        //DataTable dt;

        SearchManual f3 = new SearchManual();


        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader sqlRdr;

        public static string StrEmpNo;
        string DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        string StrQuery;
        string strLst; string strProjID; string strRole;
        string strDeptID; string strID;
        string Eshore;
        string LastModDate;
        string CreatedBy;
        bool IsDeleteUser;
        bool IsEmpEIDExist;

        private DGMembers Data;

        public DGMembers GetData()
        {
            return this.Data;
        }

        public void SetData(DGMembers _Data)
        {
            this.Data = _Data;
        }

        public Maintenance()
        {
            InitializeComponent();
            Data = new DGMembers();

        }

        void GetTimeZone()
        {
            try
            {
                lblTzone.Text = "";
                StrQuery = "SELECT DISTINCT TimeZone FROM RPA_GRID.dbo.tblTeam where id ='" + lblTeamID.Text + "'";

                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                cmd = new SqlCommand(StrQuery, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();

                while (sqlRdr.Read())
                {
                    lblTzone.Text = sqlRdr.GetValue("TimeZone").ToString();
                }
                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Maintenance_Load(object sender, EventArgs e)
        {


            Bitmap b2 = new Bitmap(@"C:\Users\izen.cruz\source\PNGs\EDIT.png");
            btnEdit.Image = b2;
            btnEdit.Text = "E";

            txtEEmail.GotFocus += new EventHandler(this.TextHitFocus);
            txtEEmail.LostFocus += new EventHandler(this.TextLostFocus);

            cmbETower.Text = UserProfile.XTower;
            cmbEDept.Text = UserProfile.XDept;
            cmbESegment.Text = UserProfile.XSegment;
            lblProjID.Text = UserProfile.XProjID;
            lblDeptID.Text = UserProfile.XDeptID;
            lblTeamID.Text = UserProfile.XTeamID;
            txtEmpNo.Text = UserProfile.XEmpno;
            txtEID.Text = UserProfile.XEID.Trim();
            txtName.Text = UserProfile.XName;
            cmbERole.Text = UserProfile.XRole.Trim();
            txtEEmail.Text = UserProfile.XEmail.Trim();
            cmbEShore.Text = UserProfile.XOnShore.ToString();
            txtELoc.Text = UserProfile.XLocation;
            txtCRM.Text = UserProfile.XCRMID;
            cmbESupervisor.Text = UserProfile.XSupervisor;
            dtpIN.Text = UserProfile.XTimeIn;
            dtpOUT.Text = UserProfile.XTimeOut;
            dtpBreak.Text = UserProfile.XLunchBr;
            txtSupEmpNo.Text = UserProfile.XSuppEmpNo;
            lblTzone.Text = UserProfile.XTimeZone;
            LastModDate = DateTime.Parse(UserProfile.XDateCreated).ToString();
            CreatedBy = UserProfile.XCreatedBy;

            switch (cmbERole.Text)
            {
                case "User":
                    for (int i = 0; i < chklstPriv.Items.Count; i++)
                    {
                        chklstPriv.SetItemChecked(i, false);
                    }
                    lblAccessCode.Text = "000000";
                    break;
                default:
                    for (int i = 0; i < chklstPriv.Items.Count; i++)
                    {
                        chklstPriv.SetItemChecked(i, true);
                    }
                    lblAccessCode.Text = "111111";
                    break;
            }

            switch (UserProfile.XOnShore.ToString())
            {
                case "True":
                    cmbEShore.Text = "YES";
                    break;
                case "False":
                    cmbEShore.Text = "NO";
                    break;
            }

            dtpIN.CustomFormat = "hh:mm tt";
            dtpOUT.CustomFormat = "hh:mm tt";
            dtpBreak.CustomFormat = "hh:mm tt";

            LockedObjs();
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            IsDeleteUser = false;

           
        }
        void GetProjID()
        {
            strLst = ""; strProjID = "";
            for (int i = 0; i < lstTowerProjID.Items.Count; i++)
            {
                strLst = lstTowerProjID.Items[i].ToString();
                if (cmbETower.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
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
                if (cmbEDept.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
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
                if (cmbESegment.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
                {
                    int len = strLst.Substring(0, strLst.IndexOf('*')).Length;
                    strID = strLst[len..][1..];
                    break;
                }
            }
        }
        void FillSegment()
        {
            try
            {
                StrQuery = "";
                lstSegment.Items.Clear();
                cmbESegment.Items.Clear();

                StrQuery += "SELECT ID,WorkType FROM RPA_GRID.dbo.tblTeam WHERE STATUS = '1' ";

                if (cmbETower.Text == "ALL")
                {
                    if (cmbEDept.Text == "ALL")
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
                    if (cmbEDept.Text != "ALL")
                    {
                        StrQuery += "AND PROJECTID = '" + strProjID + "' AND DEPTID = '" + strDeptID + "'";
                    }
                    else
                    {
                        StrQuery += "AND PROJECTID = '" + strProjID + "'";
                    }

                }

                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        cmbESegment.Items.Add(sqlRdr.GetString("WorkType"));
                        lstSegment.Items.Add(sqlRdr.GetString("WorkType") + "*" + sqlRdr.GetValue("ID").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void LoadSupervisors()
        {
            try
            {
                cmbESupervisor.Items.Clear();
                lstSupervisors.Items.Clear();
                StrQuery = "SELECT DISTINCT B.EmpName AS SUPERVISORS,B.EmpNo AS SUPPEMPNO FROM [RPA_GRID].[dbo].[tblTeam] A INNER JOIN [RPA_GRID].[dbo].[tblUserInfo] B ON B.TEAMID = A.ID  ";
                StrQuery += "WHERE B.ROLE <> 0 AND A.ProjectId = '" + lblProjID.Text + "' ORDER BY B.EmpName ASC";

                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        lstSupervisors.Items.Add(sqlRdr.GetString("SUPERVISORS").ToString() + "@" + sqlRdr.GetValue("SUPPEMPNO").ToString());
                        cmbESupervisor.Items.Add(sqlRdr.GetString("SUPERVISORS").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LockedObjs()
        {
            cmbEDept.Enabled = false;
            cmbETower.Enabled = false;
            cmbESegment.Enabled = false;

            txtEmpNo.Enabled = false;
            txtEID.Enabled = false;
            txtName.Enabled = false;
            txtEEmail.Enabled = false;
            cmbEShore.Enabled = false;
            txtELoc.Enabled = false;
            txtCRM.Enabled = false;
            cmbERole.Enabled = false;
            cmbESupervisor.Enabled = false;
            dtpIN.Enabled = false;
            dtpOUT.Enabled = false;
            dtpBreak.Enabled = false;
            chklstPriv.Enabled = false;
            btnCheckAll.Enabled = false;
        }
        void FillDepartment()
        {
            StrQuery = "";
            lstDeptID.Items.Clear();
            cmbEDept.Items.Clear();
            try
            {
                StrQuery += "SELECT DISTINCT DEPTID, SEGMENT AS DEPARTMENT FROM RPA_GRID.dbo.tblTeam WHERE STATUS = '1' AND PROJECTID = '" + strProjID + "'";
                StrQuery += "AND DEPTID IS NOT NULL ORDER BY DEPARTMENT";

                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        cmbEDept.Items.Add(sqlRdr.GetString("DEPARTMENT").Trim());
                        lstDeptID.Items.Add(sqlRdr.GetString("DEPARTMENT").Trim() + "*" + sqlRdr.GetValue("DEPTID").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ClearObjects()
        {
            cmbEDept.Text = "";
            cmbETower.Text = "";
            cmbESegment.Text = "";
            cmbEShore.Text = "";

            txtEmpNo.Text = "";
            txtEID.Text = "";
            txtName.Text = "";
            txtEEmail.Text = "";
            cmbEShore.Text = "";
            txtELoc.Text = "";
            txtCRM.Text = "";
            cmbERole.Text = "";
            cmbESupervisor.Text = "";
            dtpBreak.Text = "";
            txtSupEmpNo.Text = "";
            lstDeptID.Items.Clear();
            lstSegment.Items.Clear();
            lstSupervisors.Items.Clear();

        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }
        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            //toolTip1.OwnerDraw = true;
            //toolTip1.Show("Add New User", btnAdd);
            //toolTip1.ForeColor = Color.Red;
            //toolTip1.BackColor = Color.Yellow;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            switch (btnEdit.Text)
            {
                case "E":
                    btnDelete.Visible = false;
                    cmbETower.Enabled = true;
                    cmbEDept.Enabled = true;
                    cmbESegment.Enabled = true;
                    txtEID.Enabled = true;
                    txtName.Enabled = true;
                    txtEEmail.Enabled = true;
                    cmbEShore.Enabled = true;
                    txtELoc.Enabled = true;
                    txtCRM.Enabled = true;
                    cmbERole.Enabled = true;
                    cmbESupervisor.Enabled = true;
                    dtpIN.Enabled = true;
                    dtpOUT.Enabled = true;
                    dtpBreak.Enabled = true;
                    chklstPriv.Enabled = true;
                    btnCheckAll.Enabled = true;
                    btnDelete.Enabled = true;
                    txtCRM.Text = "N/A";

                    LoadSupervisors();
                    cmbETower.Focus();

                    btnEdit.Text = "S";
                    Bitmap b = new Bitmap(@"C:\Users\izen.cruz\source\PNGs\SAVE.png");
                    btnEdit.Image = b;
                    break;
                case "S":
                    SaveChanges();
                    btnDelete.Visible = true;
                    break;
            }

        }

        void CheckEmpEIDExist()
        {
            IsEmpEIDExist = false;
            try
            {
                StrQuery = "SELECT EID FROM [RPA_GRID].[dbo].[tblUserInfo] WHERE EID = '" + txtEID.Text.Trim() + "'";
                using SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                using SqlCommand cmd = new SqlCommand(StrQuery, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();

                while (sqlRdr.Read())
                { IsEmpEIDExist = true; }

                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void InsertUserLogs()
        {
            switch (cmbERole.Text)
            {
                case "User":
                    strRole = "0";
                    break;
                case "Supervisor":
                    strRole = "1";
                    break;
                case "Manager":
                    strRole = "2";
                    break;
                case "Director":
                    strRole = "3";
                    break;
                case "VP":
                    strRole = "4";
                    break;
            }

            switch (cmbEShore.Text)
            {
                case "YES":
                    Eshore = "1";
                    break;
                case "NO":
                    Eshore = "0";
                    break;
            }

            string DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            int len = DomainName.Substring(0, DomainName.IndexOf('\\')).Length;
            DomainName = DomainName[len..][1..];

            if (IsDeleteUser == true)
            {
                StrQuery = "INSERT INTO [RPA_GRID].[dbo].[tblUserInfoLogs]";
                StrQuery += "(EmpNo,EID,EmpName,Email,TeamId,Role,AccessCode,SchedTimeIn,LunchBreak,SchedTimeOut,SupEmpNo,OnShore,Location,CRMID,DateCreated,CreatedBy,DateModified,ModifiedBy,Status)";
                StrQuery += "VALUES ('" + UserProfile.XEmpno + "','" + txtEID.Text + "','" + txtName .Text+ "','" + UserProfile.XEmail + "','" + UserProfile.XTeamID + "','" + strRole + "','" + lblAccessCode.Text + "',";
                StrQuery += "'" + UserProfile.XTimeIn + "','" + UserProfile.XLunchBr + "','" + UserProfile.XTimeOut + "', '" + UserProfile.XSuppEmpNo + "','" + Eshore + "','" + UserProfile.XLocation + "','" + UserProfile.XCRMID + "','" + UserProfile.XDateCreated + "','" + UserProfile.XCreatedBy + "','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "','" + DomainName + "','0')";
            }
            else
            {
                StrQuery = "INSERT INTO [RPA_GRID].[dbo].[tblUserInfoLogs]";
                StrQuery += "(EmpNo,EID,EmpName,Email,TeamId,Role,AccessCode,SchedTimeIn,LunchBreak,SchedTimeOut,SupEmpNo,OnShore,Location,CRMID,DateCreated,CreatedBy,DateModified,ModifiedBy,Status)";
                StrQuery += "VALUES ('" + UserProfile.XEmpno + "','" + txtEID.Text + "','" + txtName.Text + "','" + UserProfile.XEmail + "','" + UserProfile.XTeamID + "','" + strRole + "','" + lblAccessCode.Text + "',";
                StrQuery += "'" + UserProfile.XTimeIn + "','" + UserProfile.XLunchBr + "','" + UserProfile.XTimeOut + "', '" + UserProfile.XSuppEmpNo + "','" + Eshore + "','" + UserProfile.XLocation + "','" + UserProfile.XCRMID + "','" + UserProfile.XDateCreated + "','" + UserProfile.XCreatedBy + "','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "','" + DomainName + "','1')";

            }

            try
            {
                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (SqlCommand cmd = new SqlCommand(StrQuery, conn))
                        cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void UpdateUserLogs()
        {
            string DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            int len = DomainName.Substring(0, DomainName.IndexOf('\\')).Length;
            DomainName = DomainName[len..][1..];

            try
            {
                StrQuery = "UPDATE [RPA_GRID].[dbo].[tblUserInfo] SET Status = '1', EmpName = '" + txtName.Text + "', EID='" + txtEID.Text + "', Role = '" + strRole + "', AccessCode = '" + lblAccessCode.Text + "', ";
                StrQuery += " Email = '" + txtEEmail.Text + "', TeamID = '" + lblTeamID.Text + "', SchedTimeIN = '" + dtpIN.Text + "', LunchBreak = '" + dtpBreak.Text + "', SchedTimeOut = '" + dtpOUT.Text + "',";
                StrQuery += " SupEmpNo = '" + txtSupEmpNo.Text + "', OnShore = '" + Eshore + "', Location = '" + txtELoc.Text + "', CRMID = '" + txtCRM.Text + "', DateModified = '" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "', ModifiedBy = '" + DomainName + "' WHERE EmpNo = '" + txtEmpNo.Text + "'";
                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    cmd = new SqlCommand(StrQuery, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ReQueryUserProfile()
        {

            UserProfile f1 = new UserProfile();
            f1.lblProjID.Text = UserProfile.strProjID;
            f1.lblDeptID.Text = UserProfile.strDeptID;
            f1.lblTeamID.Text = UserProfile.strID;

            f1.cmbTower.Items.Clear();
            f1.lstTowerProjID.Items.Clear();
            f1.cmbDept.Items.Clear();
            f1.lstDeptID.Items.Clear();

            strLst = "";
            for (int i = 0; i < lstTowerProjID.Items.Count; i++)
            {
                strLst = lstTowerProjID.Items[i].ToString();
                f1.cmbTower.Items.Add(strLst.Substring(0, strLst.IndexOf('*')).Trim());
                f1.lstTowerProjID.Items.Add(strLst);
            }

            strLst = "";
            for (int i = 0; i < lstDeptID.Items.Count; i++)
            {
                strLst = lstDeptID.Items[i].ToString().Trim();
                f1.cmbDept.Items.Add(strLst.Substring(0, strLst.IndexOf('*')).Trim());
                f1.lstDeptID.Items.Add(strLst);
            }

            f1.cmbTower.Items.Add("ALL");
            f1.cmbDept.Items.Add("ALL");
            f1.cmbSegment.Items.Add("ALL");

            DataTable dt = new DataTable();
            dt.Clear();
            f1.DGUserInfo.DataSource = dt;

            if (UserProfile.IsManualSearch == true)
            {
                StrQuery = "SELECT A.EmpNo, A.EmpName, A.EID, A.Email, C.EmpName, ";
                StrQuery += "A.Location, B.Cluster, B.Segment, B.WorkType, CASE A.ROLE WHEN 0 THEN 'User' WHEN '1' THEN 'Supervisor' WHEN 2 THEN 'Manager' WHEN 3 ";
                StrQuery += "THEN 'Director' WHEN 4 THEN 'VP' END AS ROLE, A.AccessCode, ";
                StrQuery += "LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeIn, 100), 7)), LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeOut, 100), 7)), A.CRMID, ";
                StrQuery += "B.ProjectId,B.DeptId,B.ID, A.OnShore, B.TimeZone, A.DateModified, A.ModifiedBy, A.DateCreated, A.CreatedBy, C.EmpNo, LTRIM(RIGHT(CONVERT(VARCHAR(20), A.LunchBreak, 100), 7)) ";
                StrQuery += "FROM RPA_GRID.dbo.tblUserInfo AS A INNER JOIN RPA_GRID.dbo.tblTeam AS B ";
                StrQuery += "ON A.TeamId = B.ID LEFT OUTER JOIN ";
                StrQuery += "RPA_GRID.dbo.tblUserInfo AS C ON A.SupEmpNo = C.EmpNo ";
                StrQuery += "WHERE A.EMPNO LIKE '%" + UserProfile.XSearch + "%' OR A.EMPNAME LIKE '%" + UserProfile.XSearch + "%'  ORDER BY A.EmpName ASC";
            }
            else
            {
                StrQuery = "SELECT A.EmpNo, A.EmpName, A.EID, A.Email, C.EmpName, ";
                StrQuery += "A.Location, B.Cluster, B.Segment, B.WorkType, CASE A.ROLE WHEN 0 THEN 'User' WHEN '1' THEN 'Supervisor' WHEN 2 THEN 'Manager' WHEN 3 ";
                StrQuery += "THEN 'Director' WHEN 4 THEN 'VP' END AS ROLE, A.AccessCode, ";
                StrQuery += "LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeIn, 100), 7)), LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeOut, 100), 7)), A.CRMID, ";
                StrQuery += "B.ProjectId,B.DeptId,B.ID, A.OnShore, B.TimeZone, A.DateModified, A.ModifiedBy, A.DateCreated, A.CreatedBy, C.EmpNo, LTRIM(RIGHT(CONVERT(VARCHAR(20), A.LunchBreak, 100), 7)) ";
                StrQuery += "FROM RPA_GRID.dbo.tblUserInfo AS A INNER JOIN RPA_GRID.dbo.tblTeam AS B ";
                StrQuery += "ON A.TeamId = B.ID LEFT OUTER JOIN ";
                StrQuery += "RPA_GRID.dbo.tblUserInfo AS C ON A.SupEmpNo = C.EmpNo ";
                StrQuery += "WHERE A.STATUS = '1'  AND B.DEPTID ='" + UserProfile.XDeptID + "' AND A.TeamID ='" + UserProfile.XTeamID + "' AND  B.ProjectId ='" + UserProfile.XProjID + "'";

            }

            conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(StrQuery, conn);
            dt = new DataTable("REQUERY");
            da.Fill(dt);
            f1.DGUserInfo.DataSource = dt;
            f1.DGUserInfo.Refresh();
            conn.Close();
            da.Dispose();

            f1.cmbTower.ForeColor = Color.Black;
            f1.cmbDept.ForeColor = Color.Black;
            f1.cmbSegment.ForeColor = Color.Black;

            f1.DGUserInfo.Columns[0].HeaderText = "EMPLOYEE NO.";
            f1.DGUserInfo.Columns[1].HeaderText = "EMPLOYEE NAME";
            f1.DGUserInfo.Columns[2].HeaderText = "ENTERPRISE ID";
            f1.DGUserInfo.Columns[3].HeaderText = "EMAIL";
            f1.DGUserInfo.Columns[4].HeaderText = "SUPERVISOR";
            f1.DGUserInfo.Columns[5].HeaderText = "LOCATION";
            f1.DGUserInfo.Columns[6].HeaderText = "TOWER";
            f1.DGUserInfo.Columns[7].HeaderText = "DEPARTMENT";
            f1.DGUserInfo.Columns[8].HeaderText = "SEGMENT";
            f1.DGUserInfo.Columns[9].HeaderText = "ROLE";
            f1.DGUserInfo.Columns[10].HeaderText = "ACCESS-CODE";
            f1.DGUserInfo.Columns[11].HeaderText = "TIME-IN";
            f1.DGUserInfo.Columns[12].HeaderText = "TIME-OUT";
            f1.DGUserInfo.Columns[13].HeaderText = "CRM ID";
            f1.DGUserInfo.Columns[14].HeaderText = "PROJID";
            f1.DGUserInfo.Columns[15].HeaderText = "DEPTID";
            f1.DGUserInfo.Columns[16].HeaderText = "TEAMID";
            f1.DGUserInfo.Columns[17].HeaderText = "ONSHORE";
            f1.DGUserInfo.Columns[19].HeaderText = "LASTDATE MODIFIED";
            f1.DGUserInfo.Columns[19].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
            f1.DGUserInfo.Columns[20].HeaderText = "MODIFIED BY";
            f1.DGUserInfo.Columns[21].HeaderText = "DATECREATED";
            f1.DGUserInfo.Columns[22].HeaderText = "CREATEDBY";
            f1.DGUserInfo.Columns[23].HeaderText = "SUPPEMPNO";
            f1.DGUserInfo.Columns[24].HeaderText = "LUNCHBR";

            f1.DGUserInfo.Columns[14].Visible = false;
            f1.DGUserInfo.Columns[15].Visible = false;
            f1.DGUserInfo.Columns[16].Visible = false;
            f1.DGUserInfo.Columns[17].Visible = false;
            f1.DGUserInfo.Columns[18].Visible = false;
            f1.DGUserInfo.Columns[21].Visible = false;
            f1.DGUserInfo.Columns[22].Visible = false;
            f1.DGUserInfo.Columns[23].Visible = false;
            f1.DGUserInfo.Columns[24].Visible = false;

            f1.Refresh();
            f1.Show();
        }

        void SaveChanges()
        {
            if (MessageBox.Show("Are all modifications final?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Please review your entries before updating.", "Review Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //CheckEmpEIDExist();
                //if (IsEmpEIDExist == true)
                //{
                //    MessageBox.Show("Employee Enterprise ID (" + txtEID.Text + ")" + " already exists.", "Saving Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //else
                //{

                    try
                    {
                        if (cmbETower.Text != "" || cmbEDept.Text != "" || cmbESegment.Text != "")
                        {
                            InsertUserLogs();
                            UpdateUserLogs();

                            Data.SetName(txtName.Text);

                        }
                        else
                        {
                            MessageBox.Show("One or more fields cannot be empty.", "Missing entries", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        ReQueryUserProfile();

                        Bitmap b2 = new Bitmap(@"C:\Users\izen.cruz\source\PNGs\EDIT.png");
                        btnEdit.Image = b2;
                        btnEdit.Text = "E";

                        MessageBox.Show("Record for " + txtName.Text + " has been updated successfully.", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //ClearObjects();
                        //LockedObjs();

                        this.Close();
                    }
                }
            //}
        }

        private void btnCheck_Click(object sender, EventArgs e)
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

        private void chklstPriv_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void dtOUT_MouseDown(object sender, MouseEventArgs e)
        {
            dtpOUT.CustomFormat = "hh:mm tt";
        }

        private void dtIN_ValueChanged(object sender, EventArgs e)
        {
            dtpOUT.Value = dtpIN.Value.AddHours(9);
            dtpBreak.Value = dtpIN.Value.AddHours(4);
            dtpOUT.CustomFormat = "hh:mm tt";
            dtpBreak.CustomFormat = "hh:mm tt";
        }

        private void cmbETower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbETower.Text != "")
            {
                GetProjID();
                lblProjID.Text = strProjID;
                //FillDepartment();
                LoadSupervisors();
                cmbEDept.Text = "";
                cmbESegment.Text = "";
                cmbEDept.Focus();

            }
        }

        private void cmbEDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbESegment.Text = "ALL";
            GetDeptID();
            lblDeptID.Text = strDeptID;
            FillSegment();
            cmbESegment.Text = "";
            cmbESegment.Focus();
        }

        private void cmbESegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbESegment.Text != "")
            {
                GetTeamID();
                lblTeamID.Text = strID;
                GetTimeZone();
                txtEID.Focus();
            }
            else
            {
                MessageBox.Show("Please select a Segment", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtEmpNo.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure deleting this User?", "Confirm User Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    IsDeleteUser = true;
                    try
                    {
                        StrQuery = "UPDATE [RPA_GRID].[dbo].[tblUserInfo] SET STATUS = '0' WHERE EMPNO = '" + txtEmpNo.Text + "' ";
                        conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                        {
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();
                            cmd = new SqlCommand(StrQuery, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally {
                        //INSERT LOGS
                        InsertUserLogs();
                  
                        MessageBox.Show("Record for " + txtName.Text + " has been deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearObjects();
                        LockedObjs();
                    }

                    
                    

      
                }
                else if (dialogResult == DialogResult.No)
                {
                    IsDeleteUser = false;
                    btnDelete.Focus();
                }
            }
        }

        private void cmbESupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbESupervisor.Text != "")
            {
                strLst = ""; txtSupEmpNo.Text = "";
                for (int i = 0; i < lstSupervisors.Items.Count; i++)
                {
                    strLst = lstSupervisors.Items[i].ToString().Trim();
                    if (cmbESupervisor.Text.Trim() == strLst.Substring(0, strLst.IndexOf('@')))
                    {
                        int len = strLst.Substring(0, strLst.IndexOf('@')).Length;
                        txtSupEmpNo.Text = strLst[len..][1..];
                        break;
                    }
                }
                dtpIN.Focus();
            }
            else
            {
                MessageBox.Show("Please select Supervisor from the dropdown list.", "Select Supervisor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbESupervisor.Focus();
                return;
            }
        }

        private void cmbERole_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbERole.Text)
            {
                case "User":
                    for (int i = 0; i < chklstPriv.Items.Count; i++)
                    {
                        chklstPriv.SetItemChecked(i, false);
                    }

                    lblAccessCode.Text = "000000";
                    break;
                default:
                    for (int i = 0; i < chklstPriv.Items.Count; i++)
                    {
                        chklstPriv.SetItemChecked(i, true);
                    }

                    lblAccessCode.Text = "111111";
                    break;
            }
        }

        private void txtEEmail_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;
            if (txtEEmail.Text.Trim() != string.Empty)
            {
                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(txtEEmail.Text.Trim()))
                {
                    MessageBox.Show("E-mail address format is not correct.", "Incorrect Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEEmail.Focus();
                }
            }
        }

        public void TextHitFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "name.lastname@cardinalhealth.com")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        public void TextLostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "name.lastname@cardinalhealth.com";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            StrEmpNo = txtEmpNo.Text;
            f3.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ReQueryUserProfile();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
