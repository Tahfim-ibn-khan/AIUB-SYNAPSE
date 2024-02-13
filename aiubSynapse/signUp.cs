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
    public partial class signUp : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public signUp()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            landingPage lpage = new landingPage();
            lpage.Show();
            this.Hide();
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

        protected int lengthCheck(int maxLength, int length)
        {
            if (length>maxLength)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        private void signUp_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 50;
            textBox3.MaxLength = 10;
            // Background design for brows button
            button1.TabStop = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);

            // Background design for signUp button
            button2.TabStop = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);

            // Background design for show hide button
            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 255, 255, 255);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.Icon = Properties.Resources.error16px2;
                errorProvider1.SetError(this.textBox1, "Enter Username");
            }
            else
            {
               int length=textBox1.Text.Length;
                int x = lengthCheck(15, length);
                if(x==1)
                {
                    textBox1.Focus();
                    MessageBox.Show("You reached the max Limit of maximum charecters", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Bitmap transparentImage = new Bitmap(1, 1);
                    transparentImage.MakeTransparent();
                    errorProvider1.Icon = Icon.FromHandle(transparentImage.GetHicon());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text!="" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into users (userName,email,role,position,department,pass,picture) values (@userName,@email,@role,@position,@department,@pass,@pic)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", textBox1.Text);
                cmd.Parameters.AddWithValue("@email", textBox2.Text);
                cmd.Parameters.AddWithValue("@role", comboBox1.Text);
                cmd.Parameters.AddWithValue("@position", comboBox2.Text);
                cmd.Parameters.AddWithValue("@department", comboBox3.Text);
                cmd.Parameters.AddWithValue("@pass", textBox3.Text);
                cmd.Parameters.AddWithValue("@pic", SavePhoto());
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    signIn sgnIn = new signIn();
                    sgnIn.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Unsuccessful Registration");
                }
            }
            else
            {
                MessageBox.Show("Must provide all information","Information",MessageBoxButtons.OK);
            }
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                comboBox2.Focus();
                errorProvider4.Icon = Properties.Resources.error16px2;
                errorProvider4.SetError(this.comboBox2, "You must select");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider4.Icon = Icon.FromHandle(transparentImage.GetHicon());
            }
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                comboBox3.Focus();
                errorProvider5.Icon = Properties.Resources.error16px2;
                errorProvider5.SetError(this.comboBox3, "You must select");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider5.Icon = Icon.FromHandle(transparentImage.GetHicon());
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                errorProvider6.Icon = Properties.Resources.error16px2;
                errorProvider6.SetError(this.textBox3, "Enter a valid password");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider6.Icon = Icon.FromHandle(transparentImage.GetHicon());
            }
        }

        private int passVisibilityController = 0;
        private void button3_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
