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

namespace CollectMoney
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadgrid();
            Total();
        }
        int x100, x50, x20, x10, xtotal = 0;
        int t100, t50, t20, t10;
        string id ;
        DateTime date =DateTime.Now;
        DateTime time = DateTime.Now;
        
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Collect;Integrated Security=True");

        private void btninsert_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand insert = new SqlCommand("insert into collect (x100,x50,x20,x10,xtotal,cdate,ctime) values ('" + x100 + "','" + x50 + "','" + x20 + "','" + x10 + "','" + xtotal + "','" + date.ToString("dd/MM/yyyy") + "','" + time.ToString("hh:mm:ss") + "')", con);
            insert.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record has been successfully Inserted !!");
            loadgrid();
        }
        void loadgrid()
        {
            con.Open();
            SqlCommand select = new SqlCommand("select * from collect ", con);
            SqlDataAdapter dataadpter = new SqlDataAdapter(select);
            DataTable dt = new DataTable();
            dataadpter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["id"].Visible = false;
            con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("hi");
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt100.Text = row.Cells["x100"].Value.ToString();
                txt50.Text = row.Cells["x50"].Value.ToString();
                txt20.Text = row.Cells["x20"].Value.ToString();
                txt10.Text = row.Cells["x10"].Value.ToString();
                id = row.Cells["id"].Value.ToString();

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand select = new SqlCommand("select * from collect where cdate = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' ", con);
            SqlDataAdapter dataadpter = new SqlDataAdapter(select);
            DataTable dt = new DataTable();
            dataadpter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["id"].Visible = false;
            con.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            con.Open();
            
            SqlCommand update = new SqlCommand("update collect set x100='" + x100 + "',x50='" + x50 + "',x20='" + x20 + "',x10='" + x10 + "',xtotal='" + xtotal + "',cdate='" + date.ToString("dd/MM/yyyy") + "',ctime='" + time.ToString("hh:mm:ss") + "' where id='"+id+"'", con);
            update.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record has been successfully updated !!");
            loadgrid();

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txt100.Text = ""; txt50.Text = ""; txt20.Text = ""; txt10.Text = "";
            txttotal.Text = ""; lbl100.Text = ""; lbl50.Text = ""; lbl20.Text = "";
            lbl10.Text = ""; 

        }

        void Total()
        {
            xtotal = t100 + t50 + t20 + t10;
            txttotal.Text = xtotal.ToString();
        }
        private void txt10_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            x10 = Convert.ToInt32(txt10.Text);
            t10 = 10 * x10;
            lbl10.Text = (t10).ToString();
            Total();
            }
            catch
            {

            }
        }

        private void txt20_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x20 = Convert.ToInt32(txt20.Text);
                t20 = 20 * x20;
                lbl20.Text = (t20).ToString();
                Total();
            
            }
            catch
            {

            }
        }

        private void txt50_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x50 = Convert.ToInt32(txt50.Text);
                t50 = 50 * x50;
                lbl50.Text = (t50).ToString();
                Total();
            
            }
            catch
            {

            }
        }

        private void txt100_TextChanged(object sender, EventArgs e)
        {
            try {
                x100 = Convert.ToInt32(txt100.Text);
                t100 = 100 * x100;
                lbl100.Text = (t100).ToString();
                Total();
            }
            catch
            {

            }
        }
    }
}
