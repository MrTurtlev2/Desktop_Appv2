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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop_App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void NavigateToSearching(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new();
            searchWindow.Show();
            Close();
        }

        public void NavigateToAdding(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new();
            addWindow.Show();
            Close();
        }
    }
}
