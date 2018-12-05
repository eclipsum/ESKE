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

namespace ESKEQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Focus();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            if (Run.IsChecked.Value || Export.IsChecked.Value)
                Decrypt.IsEnabled = true;
            else
                Decrypt.IsEnabled = false;
        }
    }
}
