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
    public partial class changePass : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        private string email;
        public changePass(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update users set pass=@newPass where email=@email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@newPass", textBox2.Text);
            cmd.Parameters.AddWithValue("@email", email);
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
                MessageBox.Show("Unsuccessful Try");
            }
        }
        private int passVisibilityController = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (passVisibilityController == 0)
            {
                textBox2.UseSystemPasswordChar = false;
                passVisibilityController++;
                button1.BackgroundImage = Properties.Resources.show16px;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                passVisibilityController--;
                button1.BackgroundImage = Properties.Resources.hide16px;
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

        private void changePass_Load(object sender, EventArgs e)
        {
            textBox2.MaxLength = 10;
        }
    }
}
