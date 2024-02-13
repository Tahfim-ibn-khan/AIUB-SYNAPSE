using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace aiubSynapse
{
    public partial class adminUserManagement : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;
        private int rmvUser;
        public adminUserManagement(int user)
        {
            InitializeComponent();
            this.user = user;
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select userName, userId, email, department, role, picture from users where userId!=@user and position!='CEO'";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@user", user);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                // Set up image column
                DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["picture"];
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                // Customize DataGridView appearance
                CustomizeDataGridViewAppearance();

                // Enable visual styles
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.AllowUserToAddRows = false;
            }
        }

        void CustomizeDataGridViewAppearance()
        {
            // Hide column headers
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.RowHeadersVisible = false;
            //making the header names capitalized, center aligned
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray; // Set the desired header background color
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Set the desired header text color
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold); // Set the desired font

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                // Capitalize header text
                column.HeaderText = column.HeaderText.ToUpper();

                // Center-align header text
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Modifying the columns
            DataGridViewColumn userNameColumn = dataGridView1.Columns["userName"];
            userNameColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            userNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn userIdColumn = dataGridView1.Columns["userId"];
            userIdColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            userIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn emailColumn = dataGridView1.Columns["email"];
            emailColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            emailColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn departmentColumn = dataGridView1.Columns["department"];
            departmentColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            departmentColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewColumn roleColumn = dataGridView1.Columns["role"];
            roleColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            roleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Set auto-size mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set row height
            dataGridView1.RowTemplate.Height = 50;

            // Additional styling
            dataGridView1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridView1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            adminProfile adprof = new adminProfile(user);
            adprof.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            adminUserManagement adUserManagement = new adminUserManagement(user);
            adUserManagement.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            adminFeedback adFeedback = new adminFeedback(user);
            adFeedback.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            adminDashboard lPage = new adminDashboard(user);
            lPage.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            adminDashboard adDashboard = new adminDashboard(user);
            adDashboard.Show();
            this.Hide();
        }
        int topdashBoard = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            if (topdashBoard == 0)
            {
                panel2.Width = 43;
                topdashBoard++;
            }
            else
            {
                panel2.Width = 369;
                topdashBoard--;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addToAdmin newAdmin=new addToAdmin(user);
            newAdmin.Show();
            this.Hide();
        }

        private void adminUserManagement_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                rmvUser = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["userId"].Value);
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["role"].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["department"].Value.ToString();
                pictureBox1.Image = GetPhoto((byte[])dataGridView1.Rows[e.RowIndex].Cells["picture"].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["userName"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["userId"].Value.ToString();
            }
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            String StringUser=textBox2.Text;
            int user = Convert.ToInt32(StringUser);
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from users where userId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", rmvUser);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox5.Clear();
                comboBox1.Text = "";
                comboBox3.Text = "";
                pictureBox1.Image = Properties.Resources.Profile;
                BindGridView();
            }
            else
            {
                MessageBox.Show("Unsuccessful");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text == "All")
            {
                BindGridView();
            }
            else
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "select userName, userId, email, department, role, picture from users where userId!=@user and role=@role";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@role", comboBox4.Text);


                        SqlDataAdapter sda = new SqlDataAdapter(cmd);

                        DataTable data = new DataTable();
                        sda.Fill(data);

                        // Assuming that dataGridView1 is your DataGridView
                        dataGridView1.DataSource = data;

                        // Set up image column
                        DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["picture"];
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                        // Customize DataGridView appearance
                        CustomizeDataGridViewAppearance();

                        // Enable visual styles
                        dataGridView1.EnableHeadersVisualStyles = false;
                        dataGridView1.AllowUserToAddRows = false;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addToAdmin addAdmin = new addToAdmin(user);
            addAdmin.Show();
            this.Hide();
        }
    }
}
