using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VideoRental_jm_
{
    public partial class Form2 : Form
    {
        string videono;
        int rentcount;
        MySqlConnection cn = new MySqlConnection("server=localhost;database=videos;userid=root;password=;charset=utf8;");
        public Form2()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            cn.Open();
            MySqlDataAdapter adp = new
            MySqlDataAdapter("SELECT video_no, title, category, cast, director, cost, status FROM stock JOIN video_copy ON stock.catalog_no=video_copy.catalog_no WHERE video_copy.status = 'Available'", cn);
            DataTable dt = new
            DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            adp.Dispose();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT customer_no from customer");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                cbcustomerno.Items.Clear();
                cbcustomerno.ResetText();
                while (dreader.Read())
                {
                    cbcustomerno.Items.Add(dreader.GetString("customer_no"));
                }
            }
            cn.Close();
        }
        private void frmvideorental_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Do you want to go back", "Go back to MENU", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 menu = new Form1();
                menu.Show();
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cn.Open();
            MySqlDataAdapter adp = new
            MySqlDataAdapter("SELECT video_no, title, category, cast, director, cost, status FROM stock JOIN video_copy ON stock.catalog_no=video_copy.catalog_no WHERE video_copy.status = 'Available'", cn);
            DataTable dt = new
            DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            adp.Dispose();
            cn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT title, category FROM stock");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                cbsearchby.Items.Clear();
                cbsearchby.ResetText();
                while (dreader.Read())
                {
                    if (cbsearch.Text == "Title" == true)
                    {
                        cbsearchby.Items.Add(dreader.GetString("title"));
                    }
                    else if (cbsearch.Text == "Category" == true)
                    {
                        cbsearchby.Items.Add(dreader.GetString("category"));
                    }
                    else
                    {
                        cbsearchby.Items.Clear();
                    }
                }
            }
            cn.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("insert into customer (lname, fname, address, registration_date) values (@lname, @fname, @address, @registration_date)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@lname", txtcustomerlname.Text);
            cmd.Parameters.AddWithValue("@fname", txtcustomerfname.Text);
            cmd.Parameters.AddWithValue("@address", txtcustomeraddress.Text);
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            cmd.Parameters.AddWithValue("@registration_date", date);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Registration Successful!", "Success");
            txtcustomerlname.Clear();
            txtcustomerfname.Clear();
            txtcustomeraddress.Clear();
            GetData();

        }
        
   
        private void button3_Click(object sender, EventArgs e)
        {
         cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT COUNT(*) AS myCount FROM video_rental WHERE customer_no <=10");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    rentcount = dreader.GetInt32("myCount");
                }
            }
            cn.Close();
            if (rentcount < 10)
            {
                cmd.CommandText = ("INSERT INTO video_rental (customer_no, video_no, daily_rent, rent_date, return_date) VALUES (@customer_no, @video_no, @daily_rent, @rent_date, @return_date)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@customer_no", cbcustomerno.Text);
                cmd.Parameters.AddWithValue("@video_no", videono);
                cmd.Parameters.AddWithValue("@daily_rent", txtdailyrent.Text);
                string rentdate = DateTime.Now.ToString("yyyy-MM-dd");
                cmd.Parameters.AddWithValue("@rent_date", rentdate.ToString());
                double rentdays = Convert.ToDouble(txtrentdays.Text);
                string returndate = DateTime.Now.AddDays(rentdays).ToString("yyyy-MM-dd");
                cmd.Parameters.AddWithValue("@return_date", returndate.ToString());
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                cn.Open();
                cmd.CommandText = ("UPDATE video_copy SET status = 'Rented' WHERE video_no = '" + videono + "'");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Rent Successful!", "Success");
            }
            else
            {
                MessageBox.Show("The user has already rented the maximum amount of videos.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cn.Open();
            MySqlDataAdapter adp = new
            MySqlDataAdapter("SELECT video_no, title, category, cast, director, cost, status FROM stock JOIN video_copy ON stock.catalog_no=video_copy.catalog_no WHERE video_copy.status = 'Rented'", cn);
            DataTable dt = new
            DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            adp.Dispose();
            cn.Close();
        }

        private void cbcustomerno_SelectedIndexChanged(object sender, EventArgs e)
        {
        cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM customer WHERE customer_no = '" + cbcustomerno.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                cbsearchby.Items.Clear();
                cbsearchby.ResetText();
                while (dreader.Read())
                {
                    txtcustomerlname.Text = dreader.GetString("lname");
                    txtcustomerfname.Text = dreader.GetString("fname");
                    txtcustomeraddress.Text = dreader.GetString("address");
                }
            }
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        cn.Open();
            MySqlDataAdapter adp = new
            MySqlDataAdapter("SELECT video_no, title, status, customer_no FROM stock JOIN video_copy ON stock.catalog_no=video_copy.catalog_no JOIN video_rental ON video_copy.video_number=video_rental.video_number WHERE video_copy.status = 'Rented' AND video_rental.customer_no = '" + cbcustomerno.Text + "'", cn);
            DataTable dt = new
            DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            adp.Dispose();
            cn.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GetData();
        }
        }
        }

     