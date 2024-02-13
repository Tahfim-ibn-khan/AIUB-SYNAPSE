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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace aiubSynapse
{
    public partial class adminProfile : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;
        public adminProfile(int user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            adminFeedback adFeedback = new adminFeedback(user);
            adFeedback.Show();
            this.Hide();
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
        private void button3_Click(object sender, EventArgs e)
        {
            adminDashboard adminD = new adminDashboard(user);
            adminD.Show();
            this.Hide();
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
        private void adminProfile_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            button4.Visible = false;
            // Retriving the Profile picture from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT picture FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] pictureData = (byte[])reader["picture"];
                            roundPictureBox2.Image = GetPhoto(pictureData);
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
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userName = reader.GetString(reader.GetOrdinal("userName"));
                            label16.Text = userName;
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
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader.GetString(reader.GetOrdinal("role"));
                            label15.Text = role;
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
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string position = reader.GetString(reader.GetOrdinal("position"));
                            label17.Text = position;
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
                    cmd1.Parameters.AddWithValue("@userId", user);
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
                                label10.Text = about;
                            }
                            else
                            {
                                // Handle the case where the about is NULL (optional)
                                label10.Text = "No about specified";
                            }
                        }
                    }
                    con1.Close();
                }
            }
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            roundPictureBox2.Image.Save(ms, roundPictureBox2.Image.RawFormat);
            return ms.GetBuffer();
        }
        int update = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (update == 0)
            {
                textBox1.Visible = true;
                button4.Visible = true;
                button2.BackColor = Color.Beige;
                button2.Text = "Confirm";
                update++;
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "update users set about=@about, picture=@pic where userId=@user";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@User", user);
                cmd.Parameters.AddWithValue("@about", textBox1.Text);
                cmd.Parameters.AddWithValue("@pic", SavePhoto());
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    adminProfile prof = new adminProfile(user);
                    prof.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Unsuccessful");
                    adminProfile prof = new adminProfile(user);
                    prof.Show();
                    this.Hide();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            //ofd.Filter = "PNG FILE (*.PNG) | *.PNG";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                roundPictureBox2.Image = new Bitmap(ofd.FileName);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void button12_Click(object sender, EventArgs e)
        {
            adminProfile adminProf = new adminProfile(user);
            adminProf.Show();
            this.Hide();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            adminUserManagement adminMng = new adminUserManagement(user);
            adminMng.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            adminFeedback adminFdb = new adminFeedback(user);
            adminFdb.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
            this.Hide();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
