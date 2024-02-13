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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace aiubSynapse
{
    public partial class createPost : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;
        public createPost(int user)
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

        private void resetAll()
        {
            pictureBox1.Image = Properties.Resources.Profile;
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.ResetText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = getUserName();
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into contents (userId, userName, title, description, type, picture) values (@userId, @userName, @title, @description, @type ,@pic)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", user);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@title", textBox2.Text);
            cmd.Parameters.AddWithValue("@description", textBox1.Text);
            cmd.Parameters.AddWithValue("@type", comboBox1.Text);
            cmd.Parameters.AddWithValue("@pic", SavePhoto());
            con.Open();
            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a > 0)
            {
                resetAll();
            }
            else
            {
                MessageBox.Show("Content upload Failed");
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            dashboard dash = new dashboard(user);
            dash.Show();
            this.Hide();   
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

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox2.Focus();
                errorProvider2.Icon = Properties.Resources.error16px2;
                errorProvider2.SetError(this.textBox2, "Enter title");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider2.Icon = Icon.FromHandle(transparentImage.GetHicon());
             
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                comboBox1.Focus();
                errorProvider3.Icon = Properties.Resources.error16px2;
                errorProvider3.SetError(this.comboBox1, "You must select");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider3.Icon = Icon.FromHandle(transparentImage.GetHicon());

            }
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.SelectedIndex = -1;
        }

        private void createPost_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 100;
            textBox2.MaxLength = 30;
        }
    }
}
