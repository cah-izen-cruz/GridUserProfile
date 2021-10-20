using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace GridUserProfile
{
    public partial class AddUser : Form
    {
        //SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
        //DataTable dt;

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader sqlRdr;
        string MachineName = Environment.MachineName.ToString();
        string DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        string StrQuery;
        string strLst; string strProjID; string strRole;
        string strDeptID; string strID; string strSupEmpNo;
        string Eshore;
        bool IsEmpNoExist;
        bool IsEmpEIDExist;

        public AddUser()
        {
            InitializeComponent();
            FillTower();
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
        void FillTower()
        {
            lstTowerProjID.Items.Clear();
            cmbETower.Items.Clear();

            try
            {
                StrQuery = "SELECT DISTINCT CLUSTER AS TOWER, ProjectID AS PROJID FROM [RPA_GRID].[dbo].[tblTeam] WHERE STATUS = '1'";
                using SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                using SqlCommand cmd = new SqlCommand(StrQuery, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();
                cmd.CommandTimeout = 1000;
                while (sqlRdr.Read())
                {
                    cmbETower.Items.Add(sqlRdr.GetString("TOWER"));
                    lstTowerProjID.Items.Add(sqlRdr.GetString("TOWER") + "*" + sqlRdr.GetValue("PROJID").ToString());
                }
                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            dtpIN.Text = "09:00 PM";
            dtpIN.CustomFormat = "hh:mm tt";

            clearObjects();

            btnSave.Enabled = true;
            cmbETower.Focus();

            txtEEmail.Text = "name.lastname@cardinalhealth.com";
            txtEEmail.GotFocus += new EventHandler(this.TextHitFocus);
            txtEEmail.LostFocus += new EventHandler(this.TextLostFocus);

        }

        void GetProjID()
        {
            strLst = ""; strProjID = "";
            for (int i = 0; i < lstTowerProjID.Items.Count; i++)
            {
                strLst = lstTowerProjID.Items[i].ToString();
                if (cmbETower.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
                {
                    int len = strLst.Substring(0, strLst.IndexOf('*')).Length + 1;
                    strProjID = strLst.Substring(len, 1).Trim();
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
            for (int i = 0; i < lstDeptID.Items.Count; i++)
            {
                strLst = lstSegment.Items[i].ToString();
                if (cmbESegment.Text.Trim() == strLst.Substring(0, strLst.IndexOf('*')))
                {
                    int len = strLst.Substring(0, strLst.IndexOf('*')).Length;
                    strID = strLst.Substring(len).Substring(1);
                    break;
                }
            }
        }
        void FillDepartment()
        {
            lstDeptID.Items.Clear();
            cmbEDept.Items.Clear();
            try
            {
                StrQuery = "SELECT DISTINCT DEPTID, SEGMENT AS DEPARTMENT FROM RPA_GRID.dbo.tblTeam WHERE STATUS = '1' ";

                if (cmbETower.Text == "")
                {
                    StrQuery += "";
                }
                else
                {
                    StrQuery += "AND PROJECTID = '" + strProjID + "' ";
                }

                if (cmbEDept.Text == "")
                {
                    StrQuery += "";
                }
                else
                {
                    if (cmbEDept.Text == "")
                    {
                        StrQuery += " ";
                    }
                    else
                    {
                        StrQuery += "AND DEPTID = '" + strDeptID + "' ";
                    }
                }
                StrQuery += "AND DEPTID IS NOT NULL ORDER BY DEPARTMENT";

                using (SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=RPA_GRID;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(StrQuery, conn))

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

                cmbEDept.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void FillSegment()
        {
            try
            {
                lstSegment.Items.Clear();
                cmbESegment.Items.Clear();

                StrQuery = "SELECT ID,WorkType FROM RPA_GRID.dbo.tblTeam WHERE STATUS = '1' ";

                if (cmbETower.Text == "")
                {
                    if (cmbEDept.Text == "")
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
                    if (cmbEDept.Text != "")
                    {
                        StrQuery += "AND PROJECTID = '" + strProjID + "' AND DEPTID = '" + strDeptID + "'";
                    }
                    else
                    {
                        StrQuery += "AND PROJECTID = '" + strProjID + "'";
                    }
                }

                using (SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=RPA_GRID;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(StrQuery, conn))

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

        private void cmbETower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbETower.Text != "")
            {
                GetProjID();
                lblProjID.Text = strProjID;
                FillDepartment();
                LoadSupervisors();
            }
        }

        private void cmbEDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDeptID();
            lblDeptID.Text = strDeptID;
            FillSegment();
        }

        private void cmbESegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTeamID();
            lblTeamID.Text = strID;
            GetTimeZone();
            txtEmpNo.Focus();
        }

        private void dtpIN_ValueChanged(object sender, EventArgs e)
        {
            dtpOUT.Value = dtpIN.Value.AddHours(9);
            dtpBreak.Value = dtpIN.Value.AddHours(4);
            dtpOUT.CustomFormat = "hh:mm tt";
            dtpBreak.CustomFormat = "hh:mm tt";
        }

        private void chklstPriv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selPrev = "";
            lblAccessCode.Text = "";
            for (int i = 0; i < chklstPriv.Items.Count; i++)
            {
                if (chklstPriv.GetItemCheckState(i) == CheckState.Checked)
                {
                    selPrev = "1";
                }
                else
                {
                    selPrev = "0";
                }

                lblAccessCode.Text = lblAccessCode.Text + selPrev;
            }
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
        void clearObjects()
        {

            this.cmbEDept.Text = "";
            this.cmbETower.Text = "";
            this.cmbESegment.Text = "";

            this.txtEmpNo.Text = "";
            this.txtEID.Text = "";
            this.txtName.Text = "";
            this.txtEEmail.Text = "";
            this.cmbEShore.Text = "";
            this.txtELoc.Text = "";
            this.txtCRM.Text = "";
            this.cmbERole.Text = "";
            this.cmbESupervisor.Text = "";
            this.txtSupEmpNo.Text = "";

            this.lstDeptID.Items.Clear();
            this.lstSegment.Items.Clear();
            this.lstSupervisors.Items.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearObjects();
            lockedObjs();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            for (int i = 0; i < chklstPriv.Items.Count; i++)
            {
                chklstPriv.SetItemChecked(i, false);
            }
            lblAccessCode.Text = "000000";

            dtpIN.Text = "09:00 PM";
            dtpIN.CustomFormat = "hh:mm tt";

       }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
                //MessageBox.Show("This field accepts only alphabetical characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmpNo.Text != "")
            {
                if (lblAccessCode.Text == "")
                {
                    MessageBox.Show("Please choose User Privileges", "User Privileges empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chklstPriv.Focus();
                    return;
                }
                else
                {
                    CheckEmpNoExist();
                    if (IsEmpNoExist == true)
                    {
                        MessageBox.Show("Employee No. (" + txtEmpNo.Text + ")" + " already exists.", "Saving Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else {

                        CheckEmpEIDExist();

                        if (IsEmpEIDExist == true)
                        {
                            MessageBox.Show("Employee Enterprise ID (" + txtEID.Text + ")" + " already exists.", "Saving Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else 
                        {

                            try
                            {
                                InsertUserInfo();
                                InsertUserLogs();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            finally {
                                MessageBox.Show("Record for " + txtName.Text + " has been stored successfully.", "Record Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearObjects();
                                lockedObjs();

                                btnCancel.Enabled = false;

                                for (int i = 0; i < chklstPriv.Items.Count; i++)
                                {
                                    chklstPriv.SetItemChecked(i, false);
                                }
                                lblAccessCode.Text = "000000";

                                dtpIN.Text = "09:00 PM";
                                dtpIN.CustomFormat = "hh:mm tt";
                                this.Close();
                            }
                          
                        }
                    }
                    

         
                }
            }
            else
            {
                MessageBox.Show("One or more fields in the request contains no data", "Saving Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        void CheckEmpNoExist() {
            IsEmpNoExist = false;
            try
            {
                StrQuery = "SELECT EMPNO FROM [RPA_GRID].[dbo].[tblUserInfo] WHERE EmpNo = '"+ txtEmpNo.Text +"'";
                using SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                using SqlCommand cmd = new SqlCommand(StrQuery, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlRdr = cmd.ExecuteReader();

                while (sqlRdr.Read())
                {
                    IsEmpNoExist = true;
                }
                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                { IsEmpEIDExist = true;  }
                             
                sqlRdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void InsertUserInfo()
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

            StrQuery = "INSERT INTO [RPA_GRID].[dbo].[tblUserInfo]";
            StrQuery += "(EmpNo,EID,EmpName,Email,TeamId,Role,AccessCode,SchedTimeIn,LunchBreak,SchedTimeOut,";
            StrQuery += " SupEmpNo,OnShore,[Location],CRMID,IsCurrentlyLogged,ShiftDate,GridVersion,GridUpdated,HostName,ActualTagging,DateCreated,CreatedBy,DateModified,ModifiedBy,Status)";
            StrQuery += "VALUES ('" + txtEmpNo.Text.Trim() + "','" + txtEID.Text.Trim() + "','" + txtName.Text + "','" + txtEEmail.Text + "','" + lblTeamID.Text + "','" + strRole + "','" + lblAccessCode.Text + "',";
            StrQuery += "'" + dtpIN.Text + "','" + dtpBreak.Text + "','" + dtpOUT.Text + "', '" + txtSupEmpNo.Text + "','" + Eshore + "','" + txtELoc.Text + "','" + txtCRM.Text + "','0',";
            StrQuery += "'" + DateAndTime.Now.ToString("MM/dd/yyyy") + "','Test GridVersion','0','" + MachineName + "','0','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "','"+ DomainName + "','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "','" + DomainName + "','1')";


            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=RPA_GRID;Integrated Security=True"))
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
                return;
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

            StrQuery = "INSERT INTO [RPA_GRID].[dbo].[tblUserInfoLogs]";
            StrQuery += "(EmpNo,EID,EmpName,Email,TeamId,Role,AccessCode,SchedTimeIn,LunchBreak,SchedTimeOut,SupEmpNo,OnShore,Location,CRMID,DateCreated,CreatedBy,DateModified,ModifiedBy,Status)";
            StrQuery += "VALUES ('" + txtEmpNo.Text + "','" + txtEID.Text + "','" + txtName.Text + "','" + txtEEmail.Text + "','" + lblTeamID.Text + "','" + strRole + "','" + lblAccessCode.Text + "',";
            StrQuery += "'" + dtpIN.Text + "','" + dtpBreak.Text + "','" + dtpOUT.Text + "', '" + txtSupEmpNo.Text + "','" + Eshore + "','" + txtELoc.Text + "','" + txtCRM.Text + "','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "','"+ DomainName +"','" + DateAndTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "','"+ DomainName +"','1')";

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=RPA_GRID;Integrated Security=True"))
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

        private void cmbESupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbESupervisor.Text != "")
            {
                strLst = ""; strSupEmpNo = "";
                for (int i = 0; i < lstSupervisors.Items.Count; i++)
                {
                    strLst = lstSupervisors.Items[i].ToString().Trim();
                    if (cmbESupervisor.Text.Trim() == strLst.Substring(0, strLst.IndexOf('@')))
                    {
                        int len = strLst.Substring(0, strLst.IndexOf('@')).Length;
                        strSupEmpNo = strLst.Substring(len).Substring(1);
                        break;
                    }
                }
                txtSupEmpNo.Text = strSupEmpNo;
                dtpIN.Focus();
            }
            else
            {
                txtSupEmpNo.Text = "";
            }
        }

        void lockedObjs()
        {
            this.txtEmpNo.Enabled = false;
            this.txtEID.Enabled = false;
            this.txtName.Enabled = false;
            this.cmbERole.Enabled = false;
            this.txtEEmail.Enabled = false;
            this.cmbEShore.Enabled = false;
            this.txtELoc.Enabled = false;
            this.txtCRM.Enabled = false;
            this.cmbESupervisor.Enabled = false;
            this.dtpIN.Enabled = false;
            this.dtpOUT.Enabled = false;
            this.dtpBreak.Enabled = false;
            this.cmbETower.Enabled = false;
            this.cmbEDept.Enabled = false;
            this.cmbESegment.Enabled = false;

            this.chklstPriv.Enabled = false;
            this.btnCheckAll.Enabled = false;
        }

        void unlockedObjs()
        {
            this.txtEmpNo.Enabled = true;
            this.txtEID.Enabled = true;
            this.txtName.Enabled = true;
            this.cmbERole.Enabled = true;
            this.txtEEmail.Enabled = true;
            this.cmbEShore.Enabled = true;
            this.txtELoc.Enabled = true;
            this.txtCRM.Enabled = true;
            this.cmbESupervisor.Enabled = true;
            this.dtpIN.Enabled = true;
            this.dtpOUT.Enabled = true;
            this.dtpBreak.Enabled = true;
            this.chklstPriv.Enabled = true;
            this.btnCheckAll.Enabled = true;

            this.cmbETower.Enabled = true;
            this.cmbEDept.Enabled = true;
            this.cmbESegment.Enabled = true;
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void cmbERole_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbERole.Text) {
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
;        }
    }
}





