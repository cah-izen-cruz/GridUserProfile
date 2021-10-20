using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace GridUserProfile
    {

   
    public partial class Authentication : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader sqlRdr;

        string StrQuery;
        string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public static string XUserId;
        public static string XTeamId;
        public static string XProjId;
        public static string XDeptId;
        public static string XTeamAccess;
        public static string XEID;
        bool IsEIDExists;

        MainSCR MS = new MainSCR();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        
        private static extern IntPtr CreateRoundRectRgn
        (
        int NLeftRect,
        int NTopRect,
        int RightRect,
        int NButtomRect,
        int NWidthEllipse,
        int NHeightEllipse);


        public Authentication()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        [DllImport("shell32.dll", EntryPoint = "#261",
                  CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void GetUserTilePath(
         string username,
         UInt32 whatever, // 0x80000000
         StringBuilder picpath, int maxLength);

        public static string GetUserTilePath(string username)
        {   // username: use null for current user
            var sb = new StringBuilder(1000);
            GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
            return sb.ToString();
        }

        public static Image GetUserTile(string username)
        {
            return Image.FromFile(GetUserTilePath(username));
            
        }
        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            pictureBox2.Image = Properties.Resources.UserOrange2;
            panel1.BackColor = Color.FromArgb(78, 184, 206);
            txtUsername.ForeColor = Color.FromArgb(78, 184, 206);

            pictureBox3.Image = Properties.Resources.key_icon;
            panel2.BackColor = Color.WhiteSmoke;
            txtPassword.ForeColor = Color.WhiteSmoke;
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtPassword.PasswordChar = '*';

            pictureBox2.Image = Properties.Resources.User_blue_icon;
            panel1.BackColor = Color.WhiteSmoke;
            txtUsername.ForeColor = Color.WhiteSmoke;

            pictureBox3.Image = Properties.Resources.KeyOrange;
            panel2.BackColor = Color.FromArgb(78, 184, 206);
            txtPassword.ForeColor = Color.FromArgb(78, 184, 206);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            GetConn_Tower_Id();
            XEID = txtUsername.Text;
        }

        private void Authentication_Load(object sender, EventArgs e)
        {
            int len = UserName.Substring(0, UserName.IndexOf('\\')).Length;
            txtUsername.Text = UserName[len..][1..];

            //panelCircle.Visible = false;
            //btnLogin.Text = "Log as: " + UserName;

            pictureBox2.Image = GetUserTile(UserName);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //SS.Left -= 10;
            //if (SS.Left <= 755) {
            //    timer2.Stop();                
            //}            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //SS.Left += 10;
            //if (SS.Left >= 1000)
            //{                
            //timer1.Stop();
            //this.TopMost = false;
            //SS.TopMost = true;
            //timer2.Start();
            //} 

            //CirProg.Value += 1;
            //CirProg.Text = CirProg.Value.ToString() + "%";

            //if (CirProg.Value == 100)
            //{
            //    timer1.Stop();
            //    timer1.Enabled = false;

            //    try
            //    {

            //        MS.Show();
            //        this.Hide();
            //    }

            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}
        }

        void GetConn_Tower_Id() {

            try
            {
                IsEIDExists = false;
                //StrQuery = "select EmpNo,TeamId,ProjectId from RPA_GRID.dbo.tblUserInfo Where EID = '" + txtUsername.Text + "'";
                StrQuery = "Select A.EmpNo,A.TeamId,A.AccessCode,B.ProjectId,B.DeptId from RPA_GRID.dbo.tblUserInfo A ";
                StrQuery += "INNER JOIN RPA_GRID.dbo.tblTeam B ON A.TeamId = B.Id Where A.EID = '" + txtUsername.Text + "' And B.DeptId Is not null";
                conn = new SqlConnection(@"Data Source=WPPHL039SQL01;Initial Catalog=;Integrated Security=True");
                cmd = new SqlCommand(StrQuery, conn);

                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlRdr = cmd.ExecuteReader();

                    while (sqlRdr.Read())
                    {
                        IsEIDExists = true;
                        XUserId = sqlRdr.GetString("EmpNo").ToString();
                        XTeamId = sqlRdr.GetInt32("TeamId").ToString();
                        XProjId = sqlRdr.GetValue("ProjectId").ToString();
                        XDeptId = sqlRdr.GetValue("DeptId").ToString();
                        XTeamAccess = sqlRdr.GetValue("AccessCode").ToString();
                    }
                    sqlRdr.Close();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                if (IsEIDExists == true)
                {
                    timer1.Start();
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;
                    //CirProg.Value = 0;
                    //panelCircle.Visible = true;

                    MS.Show();
                    this.Hide();

                }
                else {
                    MessageBox.Show("Enterprise ID ('" + txtUsername.Text + "') not found!", "Enterprise Id Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Enabled = true;
                    txtPassword.Enabled = true;
                    txtUsername.Focus();


                }

            }

        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            //CirProg.Value += 1;
            //CirProg.Text = CirProg.Value.ToString() + "%";

            //if (CirProg.Value == 100)
            //{
            //timer3.Stop();
            //timer3.Enabled = false;

            //    try
            //    {

            MS.Show();
            this.Hide();
            //    }

            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            //btnLogin.Text = "Log as: CARDINALHEALTH\\" + txtUsername.Text;
            btnLogin.Text = "LOG IN";
        }
    }
}
