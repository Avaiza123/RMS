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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MicroSoft\Downloads\CafeManagementSystem\CafeManagementSystem\CafeDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemForm Item = new ItemForm();
            Item.Show();
            
            
        }
        public static string User;
        private void button1_Click(object sender, EventArgs e)
        {
            User = unameTb.Text;
           
            if(unameTb.Text=="" || PasswordTb.Text=="")

            {
                MessageBox.Show("Please Enter Username OR Password");
            }
            else
            {
                con.Open();
                SqlDataAdapter sad = new SqlDataAdapter("select count(*) from UserTbl where Uname='"+unameTb.Text+"' and UPassword='"+PasswordTb.Text+"'",con  );
                DataTable dt = new DataTable();
                sad.Fill(dt);
                if(dt.Rows[0][0].ToString()=="1")
                {
                    UserOrder Uorder = new UserOrder();
                    Uorder.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username OR Password");
                }
                con.Close();
            }
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
