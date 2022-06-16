using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace Desktop_App
{
    public partial class LoginWindow : Window
    {

        public SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-DG8OM09\SQLEXPRESS;Initial Catalog=medicine_base;Integrated Security=True");
        public LoginWindow()
        {
            InitializeComponent();
        }
        public void handleLoginUser(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                String query = "SELECT COUNT(1) FROM User_Table WHERE user_name = @UserName AND user_password=@UserPassword";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", UserLogin.Text);
                sqlCmd.Parameters.AddWithValue("@UserPassword", UserPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                } else
                {
                    MessageBox.Show("User is not valid!");
                }

            } catch(Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            } finally
            {
                sqlCon.Close();
            }
        }


        public bool isDataOK()
        {
            if (UserLogin.Text == string.Empty)
            {
                MessageBox.Show("Enter Login!", "Login not found", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (UserPassword.Password == string.Empty)
            {
                MessageBox.Show("Enter Password!", "Password not found", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }



        public void handleRegisterUser(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isDataOK())
                {
                    bool exists = false;
                    SqlCommand cmd = new SqlCommand("Select count(*) from User_Table where user_name=@Login", sqlCon);
                    sqlCon.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Login", UserLogin.Text);
                    exists = (int)cmd.ExecuteScalar() > 0;

                    if (exists)
                    {
                        MessageBox.Show(string.Format("User Exists {0} already in database", UserLogin.Text), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        SqlCommand comand = new SqlCommand("INSERT INTO User_Table VALUES (@Login, @Password)", sqlCon);
                        comand.CommandType = CommandType.Text;
                        comand.Parameters.AddWithValue("@Login", UserLogin.Text);
                        comand.Parameters.AddWithValue("@Password", UserPassword.Password);
                        comand.ExecuteNonQuery();
                        sqlCon.Close();
                        MessageBox.Show("Now You can login into application", "Registration was succesfull", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
