using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BudgetApp
{
    public partial class AddForm : Form
    {
        string currentBA;
        string currentDA;
        double DA;
        double BA;
        double x;
        double y;
       

        public AddForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Murray\Documents\budgetData.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            BA = Convert.ToDouble(DepositAmount.Text);
            DA = Convert.ToDouble(DepositAmount.Text);
            getBalance();
            SqlCommand cmd = new SqlCommand("UPDATE budget SET Balance = @balance, Deposited = @deposited where Id = @id", con);
            cmd.Parameters.AddWithValue("@balance", x);
            cmd.Parameters.AddWithValue("@deposited", y);
            cmd.Parameters.AddWithValue("@id", 0);
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();
        }
        private void getBalance()
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
                y = Convert.ToDouble(currentDA);
                x += BA;
                y += DA;
            }
            con.Close();
        }
    }
}
