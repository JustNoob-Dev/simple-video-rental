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
    public partial class Form5 : Form
    {
        MySqlConnection cn = new MySqlConnection("server=localhost;database=videos;userid=root;password=;charset=utf8;");
        public Form5()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            comboBox1.Items.Clear();
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM customer");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                   comboBox1.Items.Add(dreader.GetString("customer_no"));
                }
            }
            cn.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("update customer set lname = '" + txtcustomerlname.Text + "', fname = '" + txtcustomerfname.Text + "', address = '" + txtcustomeraddress.Text + "' where customer_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Update Success!", "Success");
            txtcustomerlname.Clear();
            txtcustomerfname.Clear();
            txtcustomeraddress.Clear();
            GetData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM customer where customer_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    txtcustomerlname.Text = dreader.GetString("lname");
                    txtcustomerfname.Text = dreader.GetString("fname");
                    txtcustomeraddress.Text = dreader.GetString("address");
                }
            }
            cn.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
