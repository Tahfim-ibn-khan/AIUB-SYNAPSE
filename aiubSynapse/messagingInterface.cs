using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aiubSynapse
{
    public partial class messagingInterface : Form
    {
        private int user;
        public messagingInterface(int user)
        {
            InitializeComponent();
            BindGridView();
            this.user = user;
        }
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "select userName, email, picture from users where userId != @user and role != 'Admin'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    DataTable data = new DataTable();
                    sda.Fill(data);
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
            dataGridView1.ColumnHeadersVisible = false;
            //Customizing the email collumn and email cell according to the content
            DataGridViewColumn emailColumn = dataGridView1.Columns["email"];
            emailColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            emailColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //Customizing the name collumn
            DataGridViewColumn nameColumn = dataGridView1.Columns["userName"];
            nameColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);            
            dataGridView1.AutoResizeColumns();            
            
            // Set auto-size mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set row height
            dataGridView1.RowTemplate.Height = 50;
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

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
            this.Hide();
        }
        public string getUserEmail()
        {
            // Retriving the userName from the database
            string userEmail = null; // Initialize with a default value

            using (SqlConnection con1 = new SqlConnection(cs))
            {
                string query1 = "SELECT email FROM users WHERE userId = @userId";
                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {
                    cmd1.Parameters.AddWithValue("@userId", user);
                    con1.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userEmail = reader.GetString(reader.GetOrdinal("email"));
                        }
                    }
                    con1.Close();
                }
            }

            return userEmail;
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = getUserEmail();
            textBox4.Text= dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(textBox1.Text);
                mail.To.Add(textBox4.Text);
                mail.Subject = textBox3.Text;
                mail.Body = textBox5.Text;

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(label8.Text);
                mail.Attachments.Add(attachment);

                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(textBox1.Text, textBox2.Text);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("Email sent Successfully!", "Email sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.ShowDialog();
            label8.Text = OpenFileDialog1.FileName;
        }

        private void messagingInterface_Load(object sender, EventArgs e)
        {
        }
        private int passVisibilityController = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (passVisibilityController == 0)
            {
                textBox2.UseSystemPasswordChar = false;
                passVisibilityController++;
                button4.BackgroundImage = Properties.Resources.show16px;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                passVisibilityController--;
                button4.BackgroundImage = Properties.Resources.hide16px;
            }
        }
        private int x = 0;
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if(x==0)
            {
                MessageBox.Show("Use the actual password of your email. SYNAPSE account password won't work here.", "Provide password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                x+=5;
            }
            x--;
        }
    }
}
