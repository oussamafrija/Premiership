using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace my_test
{
    public partial class Form1 : Form
    {
        public string S;
        SqlDataReader dr;
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-CP58I16\SQLEXPRESS;Initial Catalog=db;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn.ConnectionString = "Data Source=DESKTOP-CP58I16\\SQLEXPRESS;Initial Catalog=Sport;Integrated Security=True";
            
            cn.Open();
            S = "select * from ranking order by Points desc";
            SqlCommand cmd = new SqlCommand(S, cn);
            dr = cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(dr);
            dataGridView1.DataSource = t;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
                row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView1.Refresh();
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cb_ht.SelectedIndex=0;
            cb_at.SelectedIndex=0;
            nud_ht.Value = 0;
            nud_at.Value = 0;            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("update ranking set Points =0, For_=0, Against=0, Difference=0", cn);
            cmd.ExecuteNonQuery();
           
            S = "select * from ranking order by Points desc";
            SqlCommand cmd1 = new SqlCommand(S, cn);
            dr = cmd1.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(dr);
            dataGridView1.DataSource = t;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
                row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView1.Refresh();
            cn.Close();
            MessageBox.Show("Start");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (cb_at.Text != cb_ht.Text )
            {
                cn.Open();
                if (nud_at.Value > nud_ht.Value)
                {
                SqlCommand cmd = new SqlCommand("update ranking set Points += @po where Teams=@tea ", cn);
                cmd.Parameters.AddWithValue("@po", 3);
                cmd.Parameters.AddWithValue("@tea", cb_at.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show(cb_at.Text + " Lost \n" + cb_ht.Text + " Won " );
                }
                else if(nud_at.Value < nud_ht.Value)
                {
                SqlCommand cmd = new SqlCommand("update ranking set Points += @po where Teams=@tea ", cn);
                cmd.Parameters.AddWithValue("@po", 3);
                cmd.Parameters.AddWithValue("@tea", cb_ht.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show(cb_ht.Text + " Won \n" + cb_at.Text + " Lost ");
                }
                else if (nud_at.Value == nud_ht.Value)
                {
                SqlCommand cmd = new SqlCommand("update ranking set Points += @po where Teams=@tea ", cn);
                cmd.Parameters.AddWithValue("@po", 1);
                cmd.Parameters.AddWithValue("@tea", cb_at.Text);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("update ranking set Points += @po where Teams=@tea ", cn);
                cmd2.Parameters.AddWithValue("@po", 1);
                cmd2.Parameters.AddWithValue("@tea", cb_ht.Text);
                cmd2.ExecuteNonQuery();
                MessageBox.Show(cb_ht.Text + " And " + cb_at.Text + "Draw");
                }

                SqlCommand cmd4 = new SqlCommand("update ranking set Against += @po1 where Teams=@tea1 ", cn);
                cmd4.Parameters.AddWithValue("@po1", nud_at.Value);
                cmd4.Parameters.AddWithValue("@tea1", cb_at.Text);
                cmd4.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("update ranking set For_ += @po where Teams=@tea ", cn);
                cmd3.Parameters.AddWithValue("@po", nud_ht.Value);
                cmd3.Parameters.AddWithValue("@tea", cb_ht.Text);
                cmd3.ExecuteNonQuery();
                
                SqlCommand cmd5 = new SqlCommand("update ranking set Difference += @dif where Teams=@tea ", cn);
                cmd5.Parameters.AddWithValue("@dif", nud_ht.Value-nud_at.Value );
                cmd5.Parameters.AddWithValue("@tea", cb_ht.Text);
                cmd5.ExecuteNonQuery();

                SqlCommand cmd6 = new SqlCommand("update ranking set Difference += @dif where Teams=@tea1 ", cn);
                cmd6.Parameters.AddWithValue("@dif", nud_at.Value - nud_ht.Value);
                cmd6.Parameters.AddWithValue("@tea1", cb_at.Text);
                cmd6.ExecuteNonQuery();

                S = "select * from ranking order by Points desc";
                 SqlCommand cmd1 = new SqlCommand(S, cn);
                  dr = cmd1.ExecuteReader();
                   DataTable t = new DataTable();
                   t.Load(dr);
                    dataGridView1.DataSource = t;
                   foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                row.HeaderCell.Value = (row.Index + 1).ToString();
                row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                     }
            dataGridView1.Refresh();
            cn.Close();
            }
            else
            {
                MessageBox.Show("That's impossible");
            }
        }
    }
}
