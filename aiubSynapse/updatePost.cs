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
    public partial class updatePost : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;
        public updatePost(int user)
        {
            InitializeComponent();
            this.user = user;
            BindGridView();
        }
        private int content;
        void BindGridView()
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT userId, type, title, description, picture, contentId FROM contents WHERE userId = @user";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@user", user);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    DataTable data = new DataTable();
                    sda.Fill(data);

                    // Assuming that dataGridView1 is your DataGridView
                    dataGridView1.DataSource = data;

                    // Set up image column
                    DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["picture"];
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    imageColumn.Width = 180;
                    // Customize DataGridView appearance
                    CustomizeDataGridViewAppearance();

                    // Enable visual styles
                    dataGridView1.EnableHeadersVisualStyles = true;
                    dataGridView1.AllowUserToAddRows = false;
                }
            }
        }
        void CustomizeDataGridViewAppearance()
        {
            // Hide column headers
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.RowHeadersVisible = false;
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

            DataGridViewColumn titleColumn = dataGridView1.Columns["title"];
            titleColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn descriptionColumn = dataGridView1.Columns["description"];
            descriptionColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            descriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn userIdColumn = dataGridView1.Columns["userId"];
            userIdColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            userIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            DataGridViewColumn typeColumn = dataGridView1.Columns["type"];
            typeColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            typeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewColumn contentIdColumn = dataGridView1.Columns["contentId"];
            contentIdColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            contentIdColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Set auto-size mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set row height
            dataGridView1.RowTemplate.Height = 100;

            // Additional styling;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            dashboard facD = new dashboard(user);
            facD.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            messagingInterface msg = new messagingInterface(user);
            msg.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            profile prf = new profile(user);
            prf.Show();
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

        private void updatePost_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 100;
            textBox2.MaxLength = 30;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                int userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["userId"].Value);
                int contentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["contentId"].Value);
                this.content = contentId;
                this.user = userId;
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["type"].Value.ToString();
                pictureBox1.Image = GetPhoto((byte[])dataGridView1.Rows[e.RowIndex].Cells["picture"].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["description"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["title"].Value.ToString();
            }
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
        //Updating post
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update contents set title=@title, description=@description, type=@type, picture=@pic where contentId=@content";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@title", textBox2.Text);
            cmd.Parameters.AddWithValue("@description", textBox1.Text);
            cmd.Parameters.AddWithValue("@type", comboBox1.Text);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@pic", SavePhoto());
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                updatePost upPost = new updatePost(user);
                upPost.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unsuccessful","Update Post");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from contents where contentId=@content";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@content", content);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                updatePost upPost = new updatePost(user);
                upPost.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unsuccessful");
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
    }
}
