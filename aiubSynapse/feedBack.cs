using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace aiubSynapse
{
    public partial class feedBack : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;

        public feedBack(int user)
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
                        }
                    }
                    con1.Close();
                }
            }

            return userName; // Return the retrieved userName or null if not found
        }

        private void feedBack_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            dashboard facD = new dashboard(user);
            facD.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            profile prof = new profile(user);
            prof.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            createPost crPost = new createPost(user);
            crPost.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            updatePost upost = new updatePost(user);
            upost.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            messagingInterface msg = new messagingInterface(user);
            msg.Show();
            this.Hide();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            dashboard facD = new dashboard(user);
            facD.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
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
            if (textBox1.Text!="" && textBox2.Text!="")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into feedback(userId, userName, subject, description) values(@user,@userName,@subject,@description)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@userName", getUserName());
                cmd.Parameters.AddWithValue("@subject", textBox1.Text);
                cmd.Parameters.AddWithValue("@description", textBox2.Text);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    textBox1.Text= "Subject of your issue";
                    textBox2.Text= "Description of your issue......";
                    textBox2.Text= "Description of your issue......";
                }
                else
                {
                    MessageBox.Show("Unsuccessful");
                }
            }
            else
            {
                MessageBox.Show("You must add subject and description", "Try again", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                errorProvider2.Icon = Properties.Resources.error16px2;
                errorProvider2.SetError(this.textBox2, "Enter position");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider2.Icon = Icon.FromHandle(transparentImage.GetHicon());

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.Icon = Properties.Resources.error16px2;
                errorProvider1.SetError(this.textBox1, "Enter position");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider1.Icon = Icon.FromHandle(transparentImage.GetHicon());

            }
        }
    }
}
