using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop_App
{

    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            LoadData();
            GetDropdownValues();
            GetDropdownRefundValues();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-DG8OM09\SQLEXPRESS;Initial Catalog=medicine_base;Integrated Security=True");


        private void LoadData()
        {

            SqlCommand cmd = new SqlCommand("SELECT med_id, med_name, med_quantity, Refundations.refund, Companies.name FROM((Meds_Table INNER JOIN Refundations ON Meds_Table.refundation = Refundations.id) JOIN Companies ON Meds_Table.company = Companies.id)", sqlCon);
            DataTable dt = new DataTable();

            sqlCon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }

        public void ClearData()
        {
            Name.Clear();
            Quantity.Clear();
            MedId.Clear();
            Company.SelectedIndex = -1;
            Refund.SelectedIndex = -1;

        }

        private void Clear_Fields(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        public bool IsValid()
        {
            if (Name.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Quantity.Text == string.Empty)
            {
                MessageBox.Show("Quantity is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }





        private void Insert_Med(object sender, RoutedEventArgs e)
        {

            int DropdownOption = Company.SelectedIndex + 1;
            int RefundOption = Refund.SelectedIndex + 1;

            try
            {
                if (IsValid())
                {
                    SqlCommand query = new SqlCommand("INSERT INTO Meds_Table VALUES (@Name, @Quantity, @Price, @Company)", sqlCon);
                    query.CommandType = CommandType.Text;
                    query.Parameters.AddWithValue("@Name", Name.Text);
                    query.Parameters.AddWithValue("@Quantity", Quantity.Text);
                    query.Parameters.AddWithValue("@Price", RefundOption);
                    query.Parameters.AddWithValue("@Company", DropdownOption);
                    sqlCon.Open();
                    query.ExecuteNonQuery();
                    sqlCon.Close();
                    LoadData();
                    MessageBox.Show("You have added new medicine to database", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Update_Med(object sender, RoutedEventArgs e)
        {
            int DropdownOption = Company.SelectedIndex + 1;
            int RefundOption = Refund.SelectedIndex + 1;

            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("Update Meds_Table set med_name = '" + Name.Text + "',med_quantity = '" + Quantity.Text + "',refundation = '" + RefundOption + "',company = '" + DropdownOption + "' WHERE med_id = '" + MedId.Text + "'", sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rekord was updated", "Update operation", MessageBoxButton.OK, MessageBoxImage.Information);
                sqlCon.Close();
                ClearData();
                LoadData();
                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Rekord was not updated" + ex.Message);
            }
            finally
            {
                sqlCon.Close();
                ClearData();
                LoadData();
            }
        }


        private void Delete_Med(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("DELETE from Meds_Table where med_id = " + MedId.Text + "", sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Med was succesfully deleted", "Med delete operation", MessageBoxButton.OK, MessageBoxImage.Information);
                sqlCon.Close();
                ClearData();
                LoadData();
                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not succesfull" + " " + ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }



        public void GetDropdownValues()
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("Select * From Companies", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(1);
                    Company.Items.Add(name);
                }
                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void GetDropdownRefundValues()
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("Select * From Refundations", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(1);
                    Refund.Items.Add(name);
                }
                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    

    }
}
