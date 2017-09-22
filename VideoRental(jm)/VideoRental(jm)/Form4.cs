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
    public partial class Form4 : Form
    {
        MySqlConnection cn = new MySqlConnection("server=localhost;database=videos;userid=root;password=;charset=utf8;");
        public Form4()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            comboBox1.Items.Clear();
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM staff");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    comboBox1.Items.Add(dreader.GetString("staff_no"));
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

        private void Form4_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM staff where staff_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    txtstaffname.Text = dreader.GetString("name");
                    txtstaffposition.Text = dreader.GetString("position");
                    txtstaffsalary.Text = dreader.GetString("salary");
                }
            }
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("insert into staff (name, position, salary) values(@name, @position, @salary)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@name", txtstaffname.Text);
            cmd.Parameters.AddWithValue("@position", txtstaffposition.Text);
            cmd.Parameters.AddWithValue("@salary", txtstaffsalary.Text);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Add Success!", "Success");
            txtstaffname.Clear();
            txtstaffposition.Clear();
            txtstaffsalary.Clear();
            GetData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("update staff set name = '" + txtstaffname.Text + "', position = '" + txtstaffposition.Text + "', salary = '" + txtstaffsalary.Text + "' where staff_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Update Success!", "Success");
            txtstaffname.Clear();
            txtstaffposition.Clear();
            txtstaffsalary.Clear();
            GetData();
        }
        }
        }
    


