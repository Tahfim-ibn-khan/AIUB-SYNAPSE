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
    public partial class dashboard : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private int user;
        public dashboard(int user)
        {
            InitializeComponent();
            this.user = user;
            BindGridView();
        }        
        int dashboardControl = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dashboardControl == 0)
            {
                panel2.Width = 46;

                button1.TabStop = false;
                button1.FlatStyle = FlatStyle.Popup;
                button1.FlatAppearance.BorderSize = 1;
               
                button2.TabStop = false;
                button2.FlatStyle = FlatStyle.Popup;
                button2.FlatAppearance.BorderSize = 1;

                button3.TabStop = false;
                button3.FlatStyle = FlatStyle.Popup;
                button3.FlatAppearance.BorderSize = 1;

                button4.TabStop = false;
                button4.FlatStyle = FlatStyle.Popup;
                button4.FlatAppearance.BorderSize = 1;

                button5.TabStop = false;
                button5.FlatStyle = FlatStyle.Popup;
                button5.FlatAppearance.BorderSize = 1;

                button6.TabStop = false;
                button6.FlatStyle = FlatStyle.Popup;
                button6.FlatAppearance.BorderSize = 1;
                dashboardControl++;
            }
            else
            {
                panel2.Width = 155;
                button1.TabStop = false;
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderSize = 0;
                button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button2.TabStop = false;
                button2.FlatStyle = FlatStyle.Flat;
                button2.FlatAppearance.BorderSize = 0;
                button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button3.TabStop = false;
                button3.FlatStyle = FlatStyle.Flat;
                button3.FlatAppearance.BorderSize = 0;
                button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button4.TabStop = false;
                button4.FlatStyle = FlatStyle.Flat;
                button4.FlatAppearance.BorderSize = 0;
                button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button5.TabStop = false;
                button5.FlatStyle = FlatStyle.Flat;
                button5.FlatAppearance.BorderSize = 0;
                button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

                button6.TabStop = false;
                button6.FlatStyle = FlatStyle.Flat;
                button6.FlatAppearance.BorderSize = 0;
                button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);
                dashboardControl--;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            profile prof = new profile(user);
            prof.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            landingPage lPage= new landingPage();
            lPage.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            createPost crtpost = new createPost(user);
            crtpost.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            updatePost updtpost = new updatePost(user);
            updtpost.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            messagingInterface msg = new messagingInterface(user);
            msg.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            feedBack fback = new feedBack(user);
            fback.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            profile prof = new profile(user);
            prof.Show();
            this.Hide();
        }
        
        private void dashboard_Load(object sender, EventArgs e)
        {
            panel2.Width = 155;
            button1.TabStop = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

            button2.TabStop = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

            button4.TabStop = false;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

            button5.TabStop = false;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);

            button6.TabStop = false;
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 30, 0, 0);
        }
        void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT userName, userId, type, title, description, picture FROM contents WHERE userId != @user";

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

                    // Customize DataGridView appearance
                    CustomizeDataGridViewAppearance();

                    DataGridViewColumn titleColumn = dataGridView1.Columns["title"];
                    titleColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    //titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                    DataGridViewColumn descriptionColumn = dataGridView1.Columns["description"];
                    descriptionColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    descriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                    DataGridViewColumn idColumn = dataGridView1.Columns["userId"];
                    idColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    //idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                    DataGridViewColumn userNameColumn = dataGridView1.Columns["userName"];
                    userNameColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    userNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewColumn typeColumn = dataGridView1.Columns["type"];
                    typeColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    typeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


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

            // Set auto-size mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set row height
            dataGridView1.RowTemplate.Height = 100;
            // Additional styling
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Filtering the Gridview according to type
        private void button10_Click(object sender, EventArgs e)
        {
            // Displaying all the profies of aiubSynapse
           if(comboBox1.Text== "Users Profile")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select userName, userId, role, picture from users where userId!=@user and role!='Admin'";
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
            else
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "SELECT userName, userId, type, title, description, picture FROM contents WHERE userId != @user and type=@type";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@type", comboBox1.Text);


                        SqlDataAdapter sda = new SqlDataAdapter(cmd);

                        DataTable data = new DataTable();
                        sda.Fill(data);

                        // Assuming that dataGridView1 is your DataGridView
                        dataGridView1.DataSource = data;

                        // Set up image column
                        DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["picture"];
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                        // Modifying the columns
                        DataGridViewColumn titleColumn = dataGridView1.Columns["title"];
                        titleColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                        titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                        DataGridViewColumn descriptionColumn = dataGridView1.Columns["description"];
                        descriptionColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                        descriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                        DataGridViewColumn idColumn = dataGridView1.Columns["userId"];
                        idColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                        //idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                        DataGridViewColumn userNameColumn = dataGridView1.Columns["userName"];
                        userNameColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                        userNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                        DataGridViewColumn typeColumn = dataGridView1.Columns["type"];
                        typeColumn.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                        typeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                        // Customize DataGridView appearance
                        CustomizeDataGridViewAppearance();

                        // Enable visual styles
                        dataGridView1.EnableHeadersVisualStyles = false;
                        dataGridView1.AllowUserToAddRows = false;
                    }
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                int viewUser = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["userId"].Value);
                profileOthersView profOthers = new profileOthersView(viewUser,user);
                profOthers.Show();
                this.Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            landingPage lPage = new landingPage();
            lPage.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
    
}
