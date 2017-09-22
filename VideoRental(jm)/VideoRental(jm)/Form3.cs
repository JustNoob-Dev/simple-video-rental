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
    public partial class Form3 : Form
    {
        MySqlConnection cn = new MySqlConnection("server=localhost;database=videos;userid=root;password=;charset=utf8;");
        public Form3()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            comboBox1.Items.Clear();
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM branch");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    comboBox1.Items.Add(dreader.GetString("branch_no"));
                }
            }
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
            cmd.CommandText = ("insert into branch (street, city, state, zip_code, tel_no) values (@street, @city, @state, @zip_code, @tel_no)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@street", txtbranchstreet.Text);
            cmd.Parameters.AddWithValue("@city", txtbranchcity.Text);
            cmd.Parameters.AddWithValue("@state", txtbranchstate.Text);
            cmd.Parameters.AddWithValue("@zip_code", txtbranchzip.Text);
            cmd.Parameters.AddWithValue("@tel_no", txtbranchtelno.Text);
            cn.Close();
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Add Successful!", "Success");
            txtbranchcity.Clear();
            txtbranchstate.Clear();
            txtbranchstreet.Clear();
            txtbranchtelno.Clear();
            txtbranchzip.Clear();
            GetData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("update branch set street = '" + txtbranchstreet.Text + "', city = '" + txtbranchcity.Text + "', state = '" + txtbranchstate.Text + "', zip_code = '" + txtbranchzip.Text + "', tel_no = '" + txtbranchtelno.Text + "' where branch_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Update Success!", "Success");
            txtbranchcity.Clear();
            txtbranchstate.Clear();
            txtbranchstreet.Clear();
            txtbranchtelno.Clear();
            txtbranchzip.Clear();
            GetData();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Close();
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM branch where branch_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    txtbranchstreet.Text = dreader.GetString("street");
                    txtbranchcity.Text = dreader.GetString("city");
                    txtbranchstate.Text = dreader.GetString("state");
                    txtbranchzip.Text = dreader.GetString("zip_code");
                    txtbranchtelno.Text = dreader.GetString("tel_no");
                }
            }
            cn.Close();
        }

        }
    }

