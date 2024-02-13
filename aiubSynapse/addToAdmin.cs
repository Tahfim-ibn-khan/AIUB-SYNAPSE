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
    public partial class addToAdmin : Form
    {
        private int user;
        public addToAdmin(int user)
        {
            InitializeComponent();
            this.user = user;
        }
        private bool checkDoubleEmail()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT * FROM users WHERE email = @email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@email", textBox2.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            bool x = dr.HasRows;
            con.Close();
            return x;

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
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into users (userName,email,role,department,position,pass,picture) values(@userName,@email,@role,@department,@position,@pass,@pic)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userName", textBox1.Text);
            cmd.Parameters.AddWithValue("@email", textBox2.Text);
            cmd.Parameters.AddWithValue("@role", comboBox4.Text);
            cmd.Parameters.AddWithValue("@department", textBox5.Text);
            cmd.Parameters.AddWithValue("@position", textBox4.Text);
            cmd.Parameters.AddWithValue("@pass", textBox3.Text);
            cmd.Parameters.AddWithValue("@pic", SavePhoto());
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                adminDashboard adminD = new adminDashboard(user);
                adminD.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unsuccessful Registration","Registration");
            }
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            adminFeedback adminFdb = new adminFeedback(user);
            adminFdb.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            adminProfile adminProf = new adminProfile(user);
            adminProf.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            adminUserManagement adminMng = new adminUserManagement(user);
            adminMng.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            adminDashboard adminD = new adminDashboard(user);
            adminD.Show();
            this.Hide();
        }
        private int topdashBoard = 0;
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

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.Icon = Properties.Resources.error16px2;
                errorProvider1.SetError(this.textBox1, "Enter an username");
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
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                errorProvider2.Icon = Properties.Resources.error16px2;
                errorProvider2.SetError(this.textBox2, "Enter an valid email");
            }
            else
            {
                bool x = checkDoubleEmail();
                if (x == true)
                {
                    textBox2.Focus();
                    MessageBox.Show("This email already existsl", "Try Another", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Bitmap transparentImage = new Bitmap(1, 1);
                    transparentImage.MakeTransparent();
                    errorProvider2.Icon = Icon.FromHandle(transparentImage.GetHicon());
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox4.Text))
            {
                comboBox4.Focus();
                errorProvider3.Icon = Properties.Resources.error16px2;
                errorProvider3.SetError(this.comboBox4, "A role must be selected");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider3.Icon = Icon.FromHandle(transparentImage.GetHicon());

            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox4.Focus();
                errorProvider4.Icon = Properties.Resources.error16px2;
                errorProvider4.SetError(this.textBox4, "Enter a proper position");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider4.Icon = Icon.FromHandle(transparentImage.GetHicon());

            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                errorProvider5.Icon = Properties.Resources.error16px2;
                errorProvider5.SetError(this.textBox3, "Enter a proper password");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider5.Icon = Icon.FromHandle(transparentImage.GetHicon());

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private int passVisibilityController = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (passVisibilityController == 0)
            {
                textBox3.UseSystemPasswordChar = false;
                passVisibilityController++;
                button3.BackgroundImage = Properties.Resources.show16px;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
                passVisibilityController--;
                button3.BackgroundImage = Properties.Resources.hide16px;
            }
        }
    }
}
