using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace GridUserProfile
{
    
    public partial class SearchManual : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
    
        string StrQuery;

        public SearchManual()
        {
            InitializeComponent();
        }
 
             
        private void SearchManual_Load(object sender, EventArgs e)
        {
            DGSearchRes.EnableHeadersVisualStyles = false;
            DGSearchRes.ColumnHeadersDefaultCellStyle.BackColor = Color.GreenYellow;
            DGSearchRes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            try
            {
                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                QueryState();

                da = new SqlDataAdapter(StrQuery, conn);

                DataTable dt = new DataTable();
                dt.Clear();
                DGSearchRes.DataSource = dt;

                dt = new DataTable("SEARCHGRID");
                da.Fill(dt);
                DGSearchRes.DataSource = dt;
                conn.Close();
                da.Dispose();


                if (dt.Rows.Count >= 0)
                {
                    DGFormatting();
                }

                StrQuery = "";
                toolStripStatusLabel1.Text = "Fetched Rows : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void QueryState()
        {
            StrQuery = "SELECT A.EmpNo, A.EmpName, A.EID, A.Email, C.EmpName, ";
            StrQuery += "A.Location, B.Cluster, B.Segment, B.WorkType, CASE A.ROLE WHEN 0 THEN 'User' WHEN '1' THEN 'Supervisor' WHEN 2 THEN 'Manager' WHEN 3 ";
            StrQuery += "THEN 'Director' WHEN 4 THEN 'VP' END AS ROLE, A.AccessCode, ";
            StrQuery += "LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeIn, 100), 7)), LTRIM(RIGHT(CONVERT(VARCHAR(20), A.SchedTimeOut, 100), 7)), A.CRMID, ";
            StrQuery += "B.ProjectId,B.DeptId,B.ID, A.OnShore, B.TimeZone, A.DateModified, A.ModifiedBy, A.DateCreated, A.CreatedBy, C.EmpNo, LTRIM(RIGHT(CONVERT(VARCHAR(20), A.LunchBreak, 100), 7)), A.STATUS ";
            StrQuery += "FROM RPA_GRID.dbo.tblUserInfoLogs AS A INNER JOIN RPA_GRID.dbo.tblTeam AS B ";
            StrQuery += "ON A.TeamId = B.ID LEFT OUTER JOIN ";
            StrQuery += "RPA_GRID.dbo.tblUserInfo AS C ON A.SupEmpNo = C.EmpNo ";
            StrQuery += "WHERE A.EmpNo = '" + Maintenance.StrEmpNo + "' ORDER BY ID ASC";          
        }

        void DGFormatting()
        {
            DGSearchRes.Columns[0].HeaderText = "EMPLOYEE NO.";
            DGSearchRes.Columns[1].HeaderText = "EMPLOYEE NAME";
            DGSearchRes.Columns[2].HeaderText = "ENTERPRISE ID";
            DGSearchRes.Columns[3].HeaderText = "EMAIL";
            DGSearchRes.Columns[4].HeaderText = "SUPERVISOR";
            DGSearchRes.Columns[5].HeaderText = "LOCATION";
            DGSearchRes.Columns[6].HeaderText = "TOWER";
            DGSearchRes.Columns[7].HeaderText = "DEPARTMENT";
            DGSearchRes.Columns[8].HeaderText = "SEGMENT";
            DGSearchRes.Columns[9].HeaderText = "ROLE";
            DGSearchRes.Columns[10].HeaderText = "ACCESS-CODE";
            DGSearchRes.Columns[11].HeaderText = "TIME-IN";
            DGSearchRes.Columns[12].HeaderText = "TIME-OUT";
            DGSearchRes.Columns[13].HeaderText = "CRM ID";
            DGSearchRes.Columns[14].HeaderText = "PROJID";
            DGSearchRes.Columns[15].HeaderText = "DEPTID";
            DGSearchRes.Columns[16].HeaderText = "TEAMID";
            DGSearchRes.Columns[17].HeaderText = "ONSHORE";
            DGSearchRes.Columns[19].HeaderText = "LASTDATE MODIFIED";
            DGSearchRes.Columns[19].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
            DGSearchRes.Columns[20].HeaderText = "MODIFIED BY";
            DGSearchRes.Columns[21].HeaderText = "DATECREATED";
            DGSearchRes.Columns[22].HeaderText = "CREATEDBY";
            DGSearchRes.Columns[23].HeaderText = "SUPPEMPNO";
            DGSearchRes.Columns[24].HeaderText = "LUNCHBR";

            DGSearchRes.Columns[14].Visible = false;
            DGSearchRes.Columns[15].Visible = false;
            DGSearchRes.Columns[16].Visible = false;
            DGSearchRes.Columns[17].Visible = false;
            DGSearchRes.Columns[18].Visible = false;
            DGSearchRes.Columns[21].Visible = false;
            DGSearchRes.Columns[22].Visible = false;
            DGSearchRes.Columns[23].Visible = false;
            DGSearchRes.Columns[24].Visible = false;
        }

        private void DGSearchRes_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            (myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.FilterString;
        }

        private void DGSearchRes_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            var myGrid = sender as Zuby.ADGV.AdvancedDataGridView;
            (myGrid.DataSource as DataTable).DefaultView.RowFilter = myGrid.SortString;
        }
    }

     
    
}
