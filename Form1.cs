using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter adapter1, adapter2;
        DataSet ds;
        SqlCommand cmd,cmd1,cmd2;
        SqlDataReader dr,dr1;
        int indexRow;
        int rowIndex;
       

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True;Pooling=False");


            con.Open();


            cmd = new SqlCommand("select roll from student", con);
            dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            con.Close();

            con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True;Pooling=False");
            con.Open();
            cmd1 = new SqlCommand("Select Max(roll)+1 from student", con);
            dr1 = cmd1.ExecuteReader();


            while (dr1.Read())
            {
                textBox1.Text = dr1[0].ToString();

            }

            con.Close();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from student where roll = '" + comboBox1.Text + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr["roll"].ToString();
                textBox2.Text = dr["name"].ToString();
                textBox3.Text = dr["marks"].ToString();
                textBox4.Text = dr["grade"].ToString();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string queryString = "Select * from student";
            SqlDataAdapter adapter1 = new SqlDataAdapter(queryString, con);
            DataSet ds = new DataSet();
            adapter1.Fill(ds, "stud");

            DataRow dr1 = ds.Tables[0].NewRow();
            dr1[0] = textBox1.Text;
            dr1[1] = textBox2.Text;
            dr1[2] = textBox3.Text;
            dr1[3] = textBox4.Text;
            ds.Tables[0].Rows.Add(dr1);
            dataGridView1.DataSource = ds.Tables[0];
            Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string queryString = "Select * from student";
            SqlDataAdapter adapter1 = new SqlDataAdapter(queryString, con);
            DataSet ds = new DataSet();
            adapter1.Fill(ds, "stud");
            DataGridViewRow newDataRow = dataGridView1.Rows[indexRow];
            newDataRow.Cells[0].Value = textBox1.Text;
            newDataRow.Cells[1].Value = textBox2.Text;
            newDataRow.Cells[2].Value = textBox3.Text;
            newDataRow.Cells[3].Value = textBox4.Text;
            dataGridView1.DataSource = ds.Tables[0];
           
           
            ds.Tables[0].Rows.Add(newDataRow);
            dataGridView1.DataSource = ds.Tables[0];
            Clear();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];

            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectedrow = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows.RemoveAt(selectedrow);
            MessageBox.Show("deleted");



        }



        private void button1_Click(object sender, EventArgs e)
        {
            string queryString = "SELECT * FROM student";
            SqlDataAdapter adapter1 = new SqlDataAdapter(queryString, con);
            DataSet ds = new DataSet();
            adapter1.Fill(ds, "stud");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string queryString = "SELECT * FROM student";
            SqlDataAdapter adapter1 = new SqlDataAdapter(queryString, con);
            DataSet ds = new DataSet();
            adapter1.Fill(ds, "stud");
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter1);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                
                adapter1.Update(ds.Tables[0]);
            }


           

          



        }

        public void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
      





    }
}


