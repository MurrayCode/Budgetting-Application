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

namespace BudgetApp
{
    public partial class Form1 : Form
    {
        string currentBA;
        string currentDA;
        double DA;
        double BA;
        double x;
        double y;
        double z;
        double rentAmount = 475.0;
        double payAmount = 1570.0;
        double reset = 0;
        public Form1()
        {
            InitializeComponent();
        }
        public double Balance(double balance)
        {
            balance += balance;
            return balance;
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddForm addForum = new AddForm();
            addForum.Show();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Murray\Documents\budgetData.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(source);
            con.Open();

            String sqlSelectQuery = "SELECT * FROM budget where id = 0";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                BalanceText.Text = (dr["Balance"].ToString());
                DepositedText.Text = (dr["Deposited"].ToString());
                SpentText.Text = (dr["Spent"].ToString());
            }
            con.Close();
        }

        private void SpentText_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void WithdrawButton_Click(object sender, EventArgs e)
        {
            WithdrawForm wf = new WithdrawForm();
            wf.Show();
        }

        private void GoalButton_Click(object sender, EventArgs e)
        {

        }

        private void RentButton_Click(object sender, EventArgs e)
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Murray\Documents\budgetData.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            minusRentBalance();
            SqlCommand cmd = new SqlCommand("UPDATE budget SET Balance = @balance, Spent = @spent where Id = @id", con);
            cmd.Parameters.AddWithValue("@balance", x);
            cmd.Parameters.AddWithValue("@spent", y);
            cmd.Parameters.AddWithValue("@id", 0);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Murray\Documents\budgetData.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            plusPayBalance();
            SqlCommand cmd = new SqlCommand("UPDATE budget SET Balance = @balance, Deposited = @deposited where Id = @id", con);
            cmd.Parameters.AddWithValue("@balance", x);
            cmd.Parameters.AddWithValue("@deposited", z);
            cmd.Parameters.AddWithValue("@id", 0);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void plusPayBalance()
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Murray\Documents\budgetData.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(source);
            con.Open();

            String sqlSelectQuery = "SELECT * FROM budget where id = 0";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                currentBA = (dr["Balance"].ToString());
                currentDA = (dr["Deposited"].ToString());
                x = Convert.ToDouble(currentBA);
                z = Convert.ToDouble(currentDA);
                x += payAmount;
                z += payAmount;
            }
            con.Close();
        }
        private void minusRentBalance()
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Murray\Documents\budgetData.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(source);
            con.Open();

            String sqlSelectQuery = "SELECT * FROM budget where id = 0";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                currentBA = (dr["Balance"].ToString());
                currentDA = (dr["Spent"].ToString());
                x = Convert.ToDouble(currentBA);
                y = Convert.ToDouble(currentDA);
                x -= rentAmount;
                y -= rentAmount;
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Murray\Documents\budgetData.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE budget SET Balance = @balance, Deposited = @deposited, Spent = @spent where Id = @id", con);
            cmd.Parameters.AddWithValue("@balance", reset);
            cmd.Parameters.AddWithValue("@deposited", reset);
            cmd.Parameters.AddWithValue("@spent", reset);
            cmd.Parameters.AddWithValue("@id", 0);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
