using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aiubSynapse
{
    public partial class updateProfile : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;

        public updateProfile(int user)
        {
            InitializeComponent();
            this.user = user;
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update users set userName=userName, role=@role,position=@position,department=@department, interest=@interest, pass=@pass, about=@about, picture=@pic where userId=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@User", user);
            cmd.Parameters.AddWithValue("@UserName", textBox1.Text);
            cmd.Parameters.AddWithValue("@role", comboBox1.Text);
            cmd.Parameters.AddWithValue("@position", comboBox2.Text);
            cmd.Parameters.AddWithValue("@department", comboBox3.Text);
            cmd.Parameters.AddWithValue("@interest", textBox4.Text);
            cmd.Parameters.AddWithValue("@about", textBox2.Text);
            cmd.Parameters.AddWithValue("@pass", textBox3.Text);
            cmd.Parameters.AddWithValue("@pic", SavePhoto());
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                profile prof = new profile(user);
                prof.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unsuccessful");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.TabStop = false;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            //ofd.Filter = "PNG FILE (*.PNG) | *.PNG";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void updateProfile_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 150;
            textBox3.MaxLength = 10;
            textBox4.MaxLength = 100;
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
                            pictureBox1.Image = GetPhoto(pictureData);
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
                            textBox1.Text = userName;
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
                            comboBox1.Text = role;
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
                            comboBox2.Text = position;
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
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string department = reader.GetString(reader.GetOrdinal("department"));
                            comboBox3.Text = department;
                        }
                    }
                    con1.Close();
                }

            }
            // Retriving interest from the database
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT interest FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", user);
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
                                textBox4.Text = interest;
                            }
                            else
                            {
                                // Handle the case where the interest is NULL (optional)
                                textBox4.Text = "No Interest specified";
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
                                textBox2.Text = about;
                            }
                            else
                            {
                                // Handle the case where the about is NULL (optional)
                                textBox2.Text = "No about specified";
                            }
                        }
                    }
                    con1.Close();
                }
            }
            // Retriving the pass from the dataBase
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT pass FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int interestIndex = reader.GetOrdinal("pass");

                            // Check for DBNull before getting the string value
                            if (!reader.IsDBNull(interestIndex))
                            {
                                string pass = reader.GetString(interestIndex);
                                textBox3.Text = pass;
                            }
                        }
                    }
                    con1.Close();
                }
            }
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
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
