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
    public partial class UserOrder : Form
    {
        public UserOrder()
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
            string query = "select * from ItemTbl where ItemCat='"+CatCb.SelectedItem.ToString()+"'";
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

        private void button4_Click(object sender, EventArgs e)
        {

        this.Hide();
       ItemsForm Item = new ItemsForm();
        Item.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersFrom ufrom = new UsersFrom();
            ufrom.Show();
        }
        int num = 0;
        int total, price;
        string item,cat;

        private void AddToCartBtn_Click(object sender, EventArgs e)
        {
            if(QtyTb.Text=="Quantity")
            {
                MessageBox.Show("What is the Quantity");
            }
            else if(flag==0)
            {
                MessageBox.Show("Select the product to be ordered");
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

        private void ItemGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            item= ItemGV.SelectedRows[0].Cells[1].Value.ToString();
            cat= ItemGV.SelectedRows[0].Cells[2].Value.ToString();
            price=Convert.ToInt32(ItemGV.SelectedRows[0].Cells[3].Value.ToString());
            flag = 1;
        }

         DataTable tabel= new DataTable();
        int flag = 0;

        private void CatCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filterbycategory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void PlaceOrderBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure OrderNum is a valid integer
                if (!int.TryParse(OrderNum.Text, out int orderNum))
                {
                    MessageBox.Show("Please enter a valid Order Number.");
                    return; // Exit if the OrderNum is not a valid integer
                }

                // Ensure OrderAmtLbl.Text is a valid integer
                if (!int.TryParse(OrderAmtLbl.Text, out int orderAmt))
                {
                    MessageBox.Show("Please enter a valid Order Amount.");
                    return; // Exit if the OrderAmtLbl is not a valid integer
                }

                // Open the connection
                con.Open();

                // Define the SQL query
                string query = "INSERT INTO OrdersTbl (OrderNum, OrderDate, [User], OrderAmt) VALUES (@OrderNum, @OrderDate, @User, @OrderAmt)";

                SqlCommand cmd = new SqlCommand(query, con);

                // Adding parameters to the query
                cmd.Parameters.AddWithValue("@OrderNum", orderNum);  // Use the parsed integer for OrderNum
                cmd.Parameters.AddWithValue("@OrderDate", DateLbl.Text); // Use DateLbl.Text as the OrderDate
                cmd.Parameters.AddWithValue("@User", Form1.User); // Use the user from Form1
                cmd.Parameters.AddWithValue("@OrderAmt", orderAmt);  // Use the parsed integer for OrderAmt

                // Execute the query
                cmd.ExecuteNonQuery();

                // Display a success message
                MessageBox.Show("Order Successfully Created");

                // Close the connection
                con.Close();
            }
            catch (Exception ex)
            {
                // Display error message if any exception occurs
                MessageBox.Show("Error: " + ex.Message);
            }
        }






        private void button3_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OrderGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        int sum = 0;
        private void UserOrder_Load(object sender, EventArgs e)
        {
            populate();
            tabel.Columns.Add("Num", typeof(int));
            tabel.Columns.Add("Item", typeof(String));
            tabel.Columns.Add("Category", typeof(String));
            tabel.Columns.Add("UnitPrice", typeof(int));
            tabel.Columns.Add("Total", typeof(int));
            OrderGV.DataSource = tabel;
            DateLbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            SellerName.Text = Form1.User;
        }
    }
}
