using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace GridUserProfile
{
    public partial class MainSCR : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader sqlRdr;

        UserProfile F1 = new UserProfile();
        AccessMaintenance AM = new AccessMaintenance();

        string StrQuery;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
      

        private static extern IntPtr CreateRoundRectRgn
        (
        int NLeftRect,
        int NTopRect,
        int RightRect,
        int NButtomRect,
        int NWidthEllipse,
        int NHeightEllipse);

        public MainSCR()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        private void MainSCR_Load(object sender, EventArgs e)
        {

            //if(IsLogged == false)
            //{
            //    try
            //    {
            //        string StrQuery;

            //        StrQuery = "Select AccessCode from [RPA_GRID].[dbo].[tblUserInfo] where EmpNo = '" + Authentication.XUserId + "'";

            //        conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
            //        cmd = new SqlCommand(StrQuery, conn);

            //        {
            //            if (conn.State == ConnectionState.Closed)
            //                conn.Open();
            //            sqlRdr = cmd.ExecuteReader();

            //            while (sqlRdr.Read())
            //            {
            //                AccessCode = sqlRdr.GetString("AccessCode").ToString();
            //            }
            //            sqlRdr.Close();
            //            conn.Close();
            //        }

            //        if (AccessCode.Substring(0, 1) == "1")
            //        {
            //            btnActivity.Enabled = true;
            //        }
            //        else
            //        {
            //            btnActivity.Enabled = false;
            //        }

            //        if (AccessCode.Substring(1, 1) == "1")
            //        {
            //            btnUser.Enabled = true;
            //        }
            //        else
            //        {
            //            btnUser.Enabled = false;
            //        }


            //        if (AccessCode.Substring(2, 1) == "1")
            //        {
            //            btnTimeCard.Enabled = true;
            //        }
            //        else
            //        {
            //            btnTimeCard.Enabled = false;
            //        }

            //        if (AccessCode.Substring(3, 1) == "1")
            //        {
            //            btnData.Enabled = true;
            //        }
            //        else
            //        {
            //            btnData.Enabled = false;
            //        }

            //        if (AccessCode.Substring(4, 1) == "1")
            //        {
            //            btnRealTime.Enabled = true;
            //        }
            //        else
            //        {
            //            btnRealTime.Enabled = false;
            //        }

            //        if (AccessCode.Substring(5, 1) == "1")
            //        {
            //            btnReport.Enabled = true;
            //        }
            //        else
            //        {
            //            btnReport.Enabled = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
           
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {

            try
            {
                F1.cmbTower.Items.Clear();
                F1.lstTowerProjID.Items.Clear();

                StrQuery = "Select Distinct ProjectId,Cluster As Tower FROM RPA_GRID.dbo.tblTeam ";
                StrQuery += "Where ProjectId Is not null And Status=1  Order by Cluster";
                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();
                    cmd.CommandTimeout = 1000;
                    while (sqlRdr.Read())
                    {
                        F1.cmbTower.Items.Add(sqlRdr.GetString("Tower").ToString());
                        F1.lstTowerProjID.Items.Add(sqlRdr.GetString("Tower").ToString() + "*" + sqlRdr.GetValue("ProjectId").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();
                    F1.cmbTower.Items.Add("ALL");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                F1.Show();
                this.Hide();
            }
         
        }


        private void btnAccess_Click(object sender, EventArgs e)
        {
            try
            {
                AM.cmbTower.Items.Clear();
                AM.cmbATower.Items.Clear();
                AM.lstTowerProjID.Items.Clear();

                StrQuery = "Select Distinct ProjectId,Cluster As Tower From RPA_GRID.dbo.tblTeam Where Status <> 0 Order by Cluster"; 

                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        AM.cmbTower.Items.Add(sqlRdr.GetString("Tower").ToString());
                        AM.lstTowerProjID.Items.Add(sqlRdr.GetString("Tower").ToString() + "*" + sqlRdr.GetValue("ProjectId").ToString());
                        AM.cmbATower.Items.Add(sqlRdr.GetString("Tower").ToString());
                    }
                    sqlRdr.Close();
                    conn.Close();
                    AM.cmbTower.Items.Add("ALL");

                    this.Hide();
                    AM.Show();
                   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }
    }
}
