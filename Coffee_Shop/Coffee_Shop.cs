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

namespace Coffee_Shop
{
    public partial class Coffee_Shop : Form
    {

        public Coffee_Shop()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {



            string connectionString = @"server=DESKTOP-V33KTP1;Database=CoffeeShopDb;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
              connection.Open();
              string commandString1 = "select name = '" + nameTextBox.Text + "',contact='" + contactTextBox.Text + "' from Item";
                                        
            SqlCommand sqlcommand = new SqlCommand(commandString1, connection);
              var reader = sqlcommand.ExecuteReader();
              if (reader.HasRows)
              {
           
                  MessageBox.Show("Duplicet");
                  
                  return;

              }
              else
              {
                  try
                  {

                    string commandString = @"insert into Item (name,contact,address,order1,quantity,price) values ('" + nameTextBox.Text + "'," + contactTextBox.Text + ",'" + addressTextBox.Text + "','" + orderTextBox.Text + "'," + quantityTextBox.Text + "," + priceTextBox.Text + ")";


                    int isExecute = ExecuteNonquery(commandString);
                    if (isExecute > 0)
                    {
                        MessageBox.Show("add done");


                    }
                    else
                    {
                        MessageBox.Show("Add not complete");
                    }

                    dataView();

                  }
                  catch (Exception exception)
                  {
                    MessageBox.Show(exception.Message);


                  }

              }
            

           
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            try
            { 
            
                dataView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);


            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string commandString = @"delete from Item Where id='" + searchTextBox.Text + "'";
                int isExecute = ExecuteNonquery(commandString); 
                if(isExecute>0)
                {
                    MessageBox.Show("Delete complete");
                }
                else
                {
                    MessageBox.Show("Delete not complete");
                }
                dataView();


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);


            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string commandString = @"update Item set name='" + nameTextBox.Text + "',contact=" + contactTextBox.Text +
               ",address='" + addressTextBox.Text + "',order1='" + orderTextBox.Text +
               "',quantity=" + quantityTextBox.Text + ",price=" + priceTextBox.Text + " where id=" + searchTextBox.Text + ";";
                int isExecute = ExecuteNonquery(commandString);
                if (isExecute > 0)
                {
                    MessageBox.Show("Update Success");
                }
                else
                {
                    MessageBox.Show("Update incomplete ");
                }
                dataView();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"server=DESKTOP-V33KTP1;Database=CoffeeShopDb;Integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                string commandString = @"select * from Item where name like '%" + searchTextBox.Text + "%'";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    showDataGridView.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Searching incomplete");
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
           

        }

        public int ExecuteNonquery(string commandString)
        {
            string connectionstring = @"server=DESKTOP-V33KTP1;Database=CoffeeShopDb;Integrated Security=True";
            SqlConnection sqlconnection = new SqlConnection(connectionstring);
            sqlconnection.Open();

            SqlCommand sqlcommand = new SqlCommand(commandString, sqlconnection);
            int success = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            return success;
        }
        public void dataView()
        {
            string connectionString = @"server=DESKTOP-V33KTP1;Database=CoffeeShopDb;Integrated Security=True";
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            string commandStrig = @"SELECT * FROM Item";
            SqlCommand sqlCommand = new SqlCommand(commandStrig, sqlconnection);
            sqlconnection.Open();
            SqlDataAdapter sqldataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqldataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                showDataGridView.DataSource = dataTable;
            }
            else
            {
                showDataGridView = null;
                MessageBox.Show("No data");
            }
        }

    }
}

