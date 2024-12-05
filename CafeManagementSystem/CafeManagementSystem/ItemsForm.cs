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
    public partial class ItemsForm : Form
    {
        public ItemsForm()
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
        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form1 Login = new Form1();
            Login.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder uorder = new UserOrder();
            uorder.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersFrom user = new UsersFrom();
            user.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ItemNameTb.Text == "" || ItemNumTb.Text == "" || ItemPrice.Text == "")
            {
                MessageBox.Show("Fill All the Filleds");
            }
            else
            {
                con.Open();
                string query = "insert into ItemTbl values(" + ItemNumTb.Text + ",'" + ItemNameTb.Text + "','" + CatCb.SelectedItem.ToString() + "',"+ItemPrice.Text+")";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Entered");
                con.Close();
                populate();
                reset();
            }
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void ItemGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemNumTb.Text = ItemGV.SelectedRows[0].Cells[0].Value.ToString();
            ItemNameTb.Text = ItemGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.SelectedItem = ItemGV.SelectedRows[0].Cells[2].Value.ToString();
            ItemPrice.Text = ItemGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "delete from ItemTbl where ItemNum='" + ItemNumTb.Text + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Deleted Successfuly");
                con.Close();
                populate();
                reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "" || ItemNameTb.Text == "" || ItemPrice.Text == "" || CatCb.SelectedItem == null)
            {
                MessageBox.Show("Fill All the Data");
            }
            else
            {
                con.Open();

                // Ensure the category is not an empty string
                string category = CatCb.SelectedItem.ToString();

                // Construct the SQL query
                string query = "UPDATE ItemTbl SET ItemName = @ItemName, ItemCat = @ItemCat, ItemPrice = @ItemPrice WHERE ItemNum = @ItemNum";

                // Use parameters to prevent SQL injection and ensure correct data types
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ItemName", ItemNameTb.Text);
                cmd.Parameters.AddWithValue("@ItemCat", category);
                cmd.Parameters.AddWithValue("@ItemPrice", Convert.ToDecimal(ItemPrice.Text));
                cmd.Parameters.AddWithValue("@ItemNum", Convert.ToInt32(ItemNumTb.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Item Successfully Updated");

                con.Close();

                populate();
                reset();
            }
        }

        public void reset()
        {
            ItemNumTb.Text = "";
            ItemNameTb.Text = "";
            CatCb.SelectedItem = "";
            ItemPrice.Text = "";
        }

        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
