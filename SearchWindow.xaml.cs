using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        /*public SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-DG8OM09\SQLEXPRESS;Initial Catalog=medicine_base;Integrated Security=True");*/
        public SearchWindow()
        {
            InitializeComponent();
            List<String> ListOfMeds = new List<string>()
            {
                "Paracetamol", "Ibuprom", "Aspiryna"
            };
            this.MedsBoxList.ItemsSource = ListOfMeds;
        }

        public void handleSearch(object sender, RoutedEventArgs e) {
        }

        public void backToMenu(object sender, RoutedEventArgs e) {

            MainWindow dashboard = new MainWindow();
            dashboard.Show();
            this.Close();
        }
    }
}
