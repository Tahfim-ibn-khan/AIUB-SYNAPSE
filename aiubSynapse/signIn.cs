using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace aiubSynapse
{
    public partial class signIn : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public signIn()
        {
            InitializeComponent();
        }
        // THIS IS THE SIGNIN BUTTON BACKEND OPERATIONS, here it took credentials and checked into the database 
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "")
            {
                //Checking the credantials and matching it with the data base 
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from users where username=@userName and email = @email and pass = @pass and role = @role";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userName", textBox1.Text);
                cmd.Parameters.AddWithValue("@email", textBox2.Text);
                cmd.Parameters.AddWithValue("@pass", textBox3.Text);
                cmd.Parameters.AddWithValue("@role", comboBox1.Text);


                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    string email = textBox2.Text;
                    int user=loggedAcc(email);                    
                    if(comboBox1.Text=="Admin")
                    {
                        adminDashboard admin = new adminDashboard(user);
                        admin.Show();
                        this.Hide();
                    }
                    else
                    {
                        dashboard Dashboard = new dashboard(user);
                        Dashboard.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Login Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Provide us the proper informtions to sign in", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //This method is for saving the loggedAccount info
        private int loggedAcc(string email)
        {
            int userId = -1;
            // Reading the value of userId
            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT userId FROM users WHERE email = @Email";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@Email", email);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);

                        }
                    }
                }
            }
            return userId;
        }
        //To go to forget password form
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            landingPage lpage = new landingPage();
            lpage.Show();
            this.Hide();
        }

        //Functionality for must fill, you can't skip
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
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider1.Icon=Icon.FromHandle(transparentImage.GetHicon());

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                errorProvider2.Icon = Properties.Resources.error16px2;
                errorProvider2.SetError(this.textBox2, "Enter email");
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
                errorProvider3.SetError(this.comboBox1, "Please select");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider3.Icon = Icon.FromHandle(transparentImage.GetHicon());
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                errorProvider4.Icon = Properties.Resources.error16px2;
                errorProvider4.SetError(this.textBox3, "Enter Password");
            }
            else
            {
                Bitmap transparentImage = new Bitmap(1, 1);
                transparentImage.MakeTransparent();
                errorProvider4.Icon = Icon.FromHandle(transparentImage.GetHicon());
            }
        }
        // This is for the funtionality of password show and hide
        int passVisibilityController=0;
        private void button1_Click(object sender, EventArgs e)
        {
            if(passVisibilityController==0)
            {
                textBox3.UseSystemPasswordChar = false;
                passVisibilityController++;
                button1.BackgroundImage = Properties.Resources.show16px;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
                passVisibilityController--;
                button1.BackgroundImage = Properties.Resources.hide16px;
            }
        }

        private void signIn_Load(object sender, EventArgs e)
        {
            // This codes are added to change the backgound style of the buttons
            button2.TabStop = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forgetPass forget = new forgetPass();
            forget.Show();
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
    }
}
