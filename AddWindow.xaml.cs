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
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-DG8OM09\SQLEXPRESS;Initial Catalog=medicine_base;Integrated Security=True");

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
