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

namespace aiubSynapse
{
    public partial class adminDashboard : Form
    {
        public string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;
        public adminDashboard(int user)
        {
            InitializeComponent();
            this.user = user;
        }
        public string getUserName()
        {
            // Retriving the userName from the database
            string userName = null; // Initialize with a default value

            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT userName FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userName = reader.GetString(reader.GetOrdinal("userName"));
                            label1.Text = userName;
                        }
                    }
                    con1.Close();
                }
            }
            return userName;
        }
            private void button1_Click(object sender, EventArgs e)
        {
            adminProfile adprof = new adminProfile(user);
            adprof.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminUserManagement adUserManagement = new adminUserManagement(user);
            adUserManagement.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            adminFeedback adFeedback = new adminFeedback(user);
            adFeedback.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
            this.Hide();    
           
        }
        int dashboardControl = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dashboardControl == 0)
            {
                panel2.Width = 155;

                button1.TabStop = false;
                button1.FlatStyle = FlatStyle.Popup;
                button1.FlatAppearance.BorderSize = 1;

                button3.TabStop = false;
                button3.FlatStyle = FlatStyle.Popup;
                button3.FlatAppearance.BorderSize = 1;

                button4.TabStop = false;
                button4.FlatStyle = FlatStyle.Popup;
                button4.FlatAppearance.BorderSize = 1;

                button5.TabStop = false;
                button5.FlatStyle = FlatStyle.Popup;
                button5.FlatAppearance.BorderSize = 1;

                dashboardControl++;
            }
            else
            {
                panel2.Width = 46;
                button1.TabStop = false;
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderSize = 0;
                button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button3.TabStop = false;
                button3.FlatStyle = FlatStyle.Flat;
                button3.FlatAppearance.BorderSize = 0;
                button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button4.TabStop = false;
                button4.FlatStyle = FlatStyle.Flat;
                button4.FlatAppearance.BorderSize = 0;
                button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button5.TabStop = false;
                button5.FlatStyle = FlatStyle.Flat;
                button5.FlatAppearance.BorderSize = 0;
                button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);
                dashboardControl--;
            }
        }

        private void adminDashboard_Load(object sender, EventArgs e)
        {
            label1.Text = getUserName();
            label5.Text = Convert.ToString(getTotalUsers());
            label6.Text = Convert.ToString(getTotalUploads());
            label7.Text = Convert.ToString(getTotalFeedbacks());
        }
        public int getTotalUsers()
        {
            int totalUser=-1;
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT COUNT(*) FROM users";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalUser = reader.GetInt32(0)-1;
                        }
                    }
                }
            }

            return totalUser;
        }

        public int getTotalUploads()
        {
            int totalContents = -1;
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT COUNT(*) FROM contents";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalContents = reader.GetInt32(0);
                        }
                    }
                }
            }

            return totalContents;
        }
        public int getTotalFeedbacks()
        {
            int totalFeedbacks = -1;
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT COUNT(*) FROM feedback";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalFeedbacks = reader.GetInt32(0);
                        }
                    }
                }
            }

            return totalFeedbacks;
        }

        private void button8_Click(object sender, EventArgs e)
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
