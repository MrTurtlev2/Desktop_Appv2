using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-DG8OM09\SQLEXPRESS;Initial Catalog=medicine_base;Integrated Security=True");

        private void LoadData()
        {
            SqlCommand cmd = new SqlCommand("select * from Meds_Table", sqlCon);
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
            try
            {
                if (IsValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Meds_Table VALUES (@Name, @Quantity)", sqlCon);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", Name.Text);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity.Text);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
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


    }
}
