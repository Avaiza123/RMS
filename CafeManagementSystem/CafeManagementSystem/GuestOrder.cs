using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class ItemForm : Form
    {
        public ItemForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MicroSoft\Downloads\CafeManagementSystem\CafeManagementSystem\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemGV.DataSource = ds.Tables[0];
            con.Close();
        }
        void Filterbycategory()
        {
            con.Open();
            string query = "select * from ItemTbl where ItemCat='" + CatCb.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Login = new Form1();
            Login.Show();
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            populate();
            tabel.Columns.Add("Num", typeof(int));
            tabel.Columns.Add("Item", typeof(String));
            tabel.Columns.Add("Category", typeof(String));
            tabel.Columns.Add("UnitPrice", typeof(int));
            tabel.Columns.Add("Total", typeof(int));
            OrderGV.DataSource = tabel;
            DateLbl.Text = DateTime.Today.Day.ToString()+"/"+ DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString() ;
        }
        int num = 0;
        int total, price;
        string item, cat;

        DataTable tabel = new DataTable();
        int flag = 0,sum=0;

        private void ItemGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            item = ItemGV.SelectedRows[0].Cells[1].Value.ToString();
            cat = ItemGV.SelectedRows[0].Cells[2].Value.ToString();
            price = Convert.ToInt32(ItemGV.SelectedRows[0].Cells[3].Value.ToString());
            flag = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "insert into OrdersTbl values(" + OrderNumTb.Text + ",'" + DateLbl.Text + "','" + SellernameTb.Text + "',"+OrderAmtLbl.Text+")";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order Successfully Created");
            con.Close();
            //populate();
           // reset();

        }

        private void QtyTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void DateLbl_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "Quantity")
            {
                MessageBox.Show("What is the Quantity");
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the product to b ordered");
            }
            else
            {
                num = num + 1;
                total = price * Convert.ToInt32(QtyTb.Text);
                tabel.Rows.Add(num, item, cat, price, total);
                OrderGV.DataSource = tabel;
                flag = 0;
            }
            sum = sum + total;
            OrderAmtLbl.Text = "" + sum;
        }

        private void CatCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filterbycategory();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
