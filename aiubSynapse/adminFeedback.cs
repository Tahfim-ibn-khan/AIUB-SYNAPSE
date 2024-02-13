using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace aiubSynapse
{
    public partial class adminFeedback : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;
        public adminFeedback(int user)
        {
            InitializeComponent();
            this.user = user;
            BindGridView();
        }
        void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT userId, userName, subject, description FROM feedback";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    DataTable data = new DataTable();
                    sda.Fill(data);

                    // Assuming that dataGridView1 is your DataGridView
                    dataGridView1.DataSource = data;

                    // Customize DataGridView appearance
                    CustomizeDataGridViewAppearance();

                    // Enable visual styles
                    dataGridView1.EnableHeadersVisualStyles = true;
                    dataGridView1.AllowUserToAddRows = false;
                }
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
            userNameColumn.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            userNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn userIdColumn = dataGridView1.Columns["userId"];
            userIdColumn.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            userIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn subjectColumn = dataGridView1.Columns["subject"];
            subjectColumn.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            //subjectColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn descriptiontColumn = dataGridView1.Columns["description"];
            descriptiontColumn.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
           // descriptiontColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Set auto-size mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set row height
            dataGridView1.RowTemplate.Height = 100;

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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
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
    }
}
