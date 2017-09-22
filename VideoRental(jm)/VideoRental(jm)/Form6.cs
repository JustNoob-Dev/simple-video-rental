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
    public partial class Form6 : Form
    {
        MySqlConnection cn = new MySqlConnection("server=localhost;database=videos;userid=root;password=;charset=utf8;");
        public Form6()
        {
            InitializeComponent();
        }
         public void GetData()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM stock JOIN branch ON stock.branch_no=branch.branch_no");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    comboBox1.Items.Add(dreader.GetString("catalog_no"));
                    comboBox2.Items.Add(dreader.GetString("branch_no"));
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
            cmd.CommandText = ("insert into stock (branch_no, title, category, cost, cast, director) values (@branch_no, @title, @category, @cost, @cast, @director)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@branch_no", comboBox2.Text);
            cmd.Parameters.AddWithValue("@title", txtstocktitle.Text);
            cmd.Parameters.AddWithValue("@category", txtstockcategory.Text);
            cmd.Parameters.AddWithValue("@cost", txtstockcost.Text);
            cmd.Parameters.AddWithValue("@cast", txtstockcast.Text);
            cmd.Parameters.AddWithValue("@director", txtstockdirector.Text);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Add Successful!", "Success");
            txtstockcast.Clear();
            txtstockcategory.Clear();
            txtstockcost.Clear();
            txtstockdirector.Clear();
            txtstocktitle.Clear();
            comboBox2.ResetText();
            comboBox1.ResetText();
            GetData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("update stock set title = '" + txtstocktitle.Text + "', category = '" + txtstockcategory.Text + "', cost = '" + txtstockcost.Text + "', cast = '" + txtstockcast.Text + "', director = '" + txtstockdirector.Text + "' where catalog_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Update Success!", "Success");
            txtstockcast.Clear();
            txtstockcategory.Clear();
            txtstockcost.Clear();
            txtstockdirector.Clear();
            txtstocktitle.Clear();
            comboBox2.ResetText();
            comboBox1.ResetText();
            GetData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ("SELECT * FROM stock where catalog_no = '" + comboBox1.Text + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            MySqlDataReader dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                {
                    txtstocktitle.Text = dreader.GetString("title");
                    txtstockcategory.Text = dreader.GetString("category");
                    txtstockcast.Text = dreader.GetString("cast");
                    txtstockcost.Text = dreader.GetString("cost");
                    txtstockdirector.Text = dreader.GetString("director");
                    comboBox2.Text = dreader.GetString("branch_no");
                }
            }
            cn.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            GetData();
        }
        }
        }

