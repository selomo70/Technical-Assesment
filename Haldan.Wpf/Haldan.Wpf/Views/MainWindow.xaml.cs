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

namespace Haldan.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        private void Close_Application(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Application can not be closed while performing a test", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Cancel = true;
                }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new Uri("/Views/LoginWindow.xaml", UriKind.Relative));
        }
    }
}
