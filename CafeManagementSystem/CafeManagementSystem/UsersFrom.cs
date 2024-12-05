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

namespace CafeManagementSystem
{
    public partial class UsersFrom : Form
    {
        public UsersFrom()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MicroSoft\Downloads\CafeManagementSystem\CafeManagementSystem\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder uorder = new UserOrder();
            uorder.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder uorder = new UserOrder();
            uorder.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm item = new ItemsForm();
            item.Show();
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

        private void AddBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "insert into UserTbl values('"+unameTb.Text+"','"+uphoneTb.Text+"','"+upassTb.Text+"')";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User Successfully Created");
            con.Close();
            populate();
            reset();

        }

        private void UsersFrom_Load(object sender, EventArgs e)
        {
            populate();
        }
        //int key = 0;
        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            unameTb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
           uphoneTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            upassTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
        }
       
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
                try
                {
                    con.Open();
                    string query = "delete from UserTbl where Uphone='"+uphoneTb.Text+"';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfuly");
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
            if(unameTb.Text == "" || uphoneTb.Text =="" || upassTb.Text=="")
            {
                MessageBox.Show("Fill All the Field");
            }
            else
            {
                con.Open();
                string query = "update UserTbl set Uname= '"+unameTb.Text+"',Upassword='"+upassTb.Text+"' where Uphone='"+uphoneTb.Text+"'";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Users Successfully Updated");
                con.Close();
                populate();
                reset();
            }
        }
        public void reset()
        {
            unameTb.Text = "";
            upassTb.Text = "";
            uphoneTb.Text = "";

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
