using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aiubSynapse
{
    public partial class profileOthersView : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int viewUser, loggedUser;
        public profileOthersView(int viewUser, int loggedUser)
        {
            InitializeComponent();
            this.viewUser = viewUser;
            this.loggedUser = loggedUser;
            BindGridView();
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

        private void profileOthersView_Load(object sender, EventArgs e)
        {
            label6.MaximumSize = new Size(195, int.MaxValue);
            label4.MaximumSize = new Size(195, int.MaxValue);

            label4.BackColor = Color.FromArgb(40, 30, 0, 0);
            label6.BackColor = Color.FromArgb(40, 30, 0, 0);
            // Retriving the Profile picture from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT picture FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] pictureData = (byte[])reader["picture"];
                            roundPictureBox1.Image = GetPhoto(pictureData);
                        }
                    }
                    con1.Close();
                }

            }

            // Retriving the email from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT email FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userEmail = reader.GetString(reader.GetOrdinal("email"));
                            label11.Text = userEmail;
                        }
                    }
                    con1.Close();
                }

            }
            // Retriving the userName from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT userName FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userName = reader.GetString(reader.GetOrdinal("userName"));
                            label1.Text = userName;
                        }
                    }
                    con1.Close();
                }

            }

            // Retriving the role from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT role FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader.GetString(reader.GetOrdinal("role"));
                            label9.Text = role;
                        }
                    }
                    con1.Close();
                }

            }

            // Retriving the position from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT position FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string position = reader.GetString(reader.GetOrdinal("position"));
                            label8.Text = position;
                        }
                    }
                    con1.Close();
                }

            }

            // Retriving the department from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT department FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string department = reader.GetString(reader.GetOrdinal("department"));
                            label12.Text = department;
                        }
                    }
                    con1.Close();
                }

            }

            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT interest FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int interestIndex = reader.GetOrdinal("interest");

                            // Check for DBNull before getting the string value
                            if (!reader.IsDBNull(interestIndex))
                            {
                                string interest = reader.GetString(interestIndex);
                                label4.Text = interest;
                            }
                            else
                            {
                                // Handle the case where the interest is NULL (optional)
                                label4.Text = "No Interest specified";
                            }
                        }
                    }
                    con1.Close();
                }
            }


            // Retriving the about from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT about FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", viewUser);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int interestIndex = reader.GetOrdinal("about");

                            // Check for DBNull before getting the string value
                            if (!reader.IsDBNull(interestIndex))
                            {
                                string about = reader.GetString(interestIndex);
                                label6.Text = about;
                            }
                            else
                            {
                                // Handle the case where the about is NULL (optional)
                                label6.Text = "No about specified";
                            }
                        }
                    }
                    con1.Close();
                }
            }
        }
        void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT type, title, description, picture FROM contents WHERE userId = @user";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user", viewUser);

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

        void CustomizeDataGridViewAppearance()
        {
            // Hide column headers
            dataGridView1.ColumnHeadersVisible = true;

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

            // Set auto-size mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewColumn titleColumn = dataGridView1.Columns["title"];
            titleColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            DataGridViewColumn descriptionColumn = dataGridView1.Columns["description"];
            descriptionColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            // Set row height
            dataGridView1.RowTemplate.Height = 100;

            // Additional styling
            dataGridView1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridView1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            profile prof = new profile(loggedUser);
            prof.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            createPost crtPost = new createPost(loggedUser);
            crtPost.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            updatePost upPost = new updatePost(loggedUser);
            upPost.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            messagingInterface msg = new messagingInterface(loggedUser);
            msg.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            dashboard dash = new dashboard(loggedUser);
            dash.Show();
            this.Hide();
        }
    }
}
